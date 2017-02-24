using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using VNApp_Order.Controllers.Orders;
using VNApp_Order.Controllers.Members;
using VNApp_Order.Services.Salesforce;

namespace VNApp_Order.Services.Salesforce
{
    public class RESTAPIClient
    {
        private string _consumerKey;
        private string _consumerSecret;
        private string _userName;
        private string _password;
        private string _token;
        private string _baselineURL;
        private string _customURL;
        private string _authEndpoint;

        private APIResponse_Auth _authResponse;
        private bool _isAuthen;

        public RESTAPIClient(string consumerKey, string consumerSecret, string userName, string password, string token,
                     string baselineURL, string authEndpoint, string customURL)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _userName = userName;
            _password = password;
            _token = token;
            _baselineURL = baselineURL;
            _authEndpoint = authEndpoint;
            _customURL = customURL;

            _authResponse = new APIResponse_Auth();
            _isAuthen = false;
        }

        private async Task Authenticate()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
                    // TODO - Send HTTP requests
                    // set the base URI for HTTP requests...
                    client.BaseAddress = new Uri(_authEndpoint);
                    // set the Accept header to "application/json", which tells the server to send data in JSON format...
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP POST
                    #region "Bug Example"
                    //var authen = new Authen() { grant_type = "password", client_id = AppSettings.salesforce_consumerKey,
                    //    client_secret = AppSettings.salesforce_consumerSecret,
                    //    user_name = AppSettings.salesforce_userName, password = AppSettings.salesforce_password + AppSettings.salesforce_token };
                    //HttpResponseMessage response = await client.PostAsJsonAsync("", authen);
                    // NOTE: the call to HttpClient.PostAsJsonAsync gave this error:
                    // {"error":"unsupported_grant_type","error_description":"grant type not supported"}
                    // This API expects form-urlencoded, not JSON on the request body.

                    // This also donesn't work.
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                    #endregion

                    // build content with "application/x-www-form-urlencoded" content type...
                    Dictionary<string, string> dictForm = new Dictionary<string, string>();
                    dictForm.Add("grant_type", "password");
                    dictForm.Add("client_id", _consumerKey);
                    dictForm.Add("client_secret", _consumerSecret);
                    dictForm.Add("username", _userName);
                    dictForm.Add("password", _password + _token);
                    var content = new FormUrlEncodedContent(dictForm);
                    //var content = dictForm;

                    // send POST method...             
                    HttpResponseMessage response = await client.PostAsync(_authEndpoint, content).ConfigureAwait(false);
                    _authResponse.HTTPStatusCode = Convert.ToString(response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        _authResponse = await response.Content.ReadAsAsync<APIResponse_Auth>();

                        if (_authResponse == null)
                        {
                            //throw new Exception("_authenResult is null...");
                        }

                        _isAuthen = true;
                    }
                    else
                    {
                        // get response content, which contains potential error messages...
                        _isAuthen = false;
                        _authResponse.Content = await response.Content.ReadAsStringAsync();
                    }

                } // end using...

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateOrder(APIType_Order orderId, List<APIType_Order> lstRequestProduct, APIResponse_Create apiResponse)
        {
            try
            {
                // Authentication.
                Authenticate().Wait();

                if (!_isAuthen)
                {
                    apiResponse.IsAuthen = false;
                    apiResponse.Content = _authResponse.Content;
                    apiResponse.HTTPStatusCode = _authResponse.HTTPStatusCode;
                    return;
                }

                apiResponse.IsAuthen = true;


                // Build request message.

                #region "Build Request Message"

                //InterestedProducts

                RequestMessage_Order requestQuote = new RequestMessage_Order();
                requestQuote.lstTempProducts = lstRequestProduct;
                requestQuote.lead = orderId;

                RequestWrapper_Order wrapper = new RequestWrapper_Order();
                wrapper.item = requestQuote;


                #endregion


                // Now, create HttpClient and send the request.
                using (var client = new HttpClient())
                {
                    // Setup base address and heads (both request and respose headers)...
                    client.BaseAddress = new Uri(_customURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.access_token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    String urlPath = "";
                    if (_customURL.Substring(_customURL.Length - 1) == "/")
                        urlPath = _customURL + "createvnquote/";
                    else
                        urlPath = _customURL + "/createvnquote/";


                    // Send GET request.
                    HttpResponseMessage response = await client.PostAsJsonAsync(urlPath, wrapper).ConfigureAwait(false);
                    apiResponse.HTTPStatusCode = response.StatusCode.ToString();

                    // HttpResponseMessage is at HTTP level, we continue with API specific response below...
                    if (response.IsSuccessStatusCode)
                    {
                        // you "cannot" use "apiResponse" variable directly here, because it would override other
                        // settings priori to this line...
                        APIResponse_Create tempResponse = await response.Content.ReadAsAsync<APIResponse_Create>();

                        // assign to the "APIResponse" parameter...
                        apiResponse.id = tempResponse.id;
                        apiResponse.errors = tempResponse.errors;
                        apiResponse.success = tempResponse.success;
                    }
                    else
                    {
                        apiResponse.Content = await response.Content.ReadAsStringAsync();
                    }

                } // end using...

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}