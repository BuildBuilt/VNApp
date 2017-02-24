using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.IO;

using VNApp_Order.Controllers.Members;
using VNApp_Order.Services;
using VNApp_Order.Services.Salesforce;
using VNApp_Order.Controllers.Orders;

namespace VNApp_Order.Controllers.Orders
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        public OrderController()
        {

        }

        //POST api/order/Submit
        [Route("Submit")]
        public HttpResponseMessage submitQuote(APIType_Order order)
        {
            try
            {
                // Business logics.
                #region "Configurations"
                // Get configuration settings.
                string consumerKey = WebConfigurationManager.AppSettings["SalesforceConsumerKey"];
                string consumerSecret = WebConfigurationManager.AppSettings["SalesforceConsumerSecret"];
                string userName = WebConfigurationManager.AppSettings["SalesforceUserName"];
                string password = WebConfigurationManager.AppSettings["SalesforcePassword"];
                string token = WebConfigurationManager.AppSettings["SalesforceToken"];
                string baselineURL = WebConfigurationManager.AppSettings["BaselineURL"];
                string authEndpoint = WebConfigurationManager.AppSettings["AuthEndpoint"];
                string customURL = WebConfigurationManager.AppSettings["CustomURL"];
                #endregion

                RESTAPIClient client = new RESTAPIClient(consumerKey, consumerSecret, userName, password, token, baselineURL, authEndpoint, customURL);

                APIResponse_Create apiResponse = new APIResponse_Create();

                order.OrderId = "ORD-00001";
                order.CustomerCodeExternal__c = "C1";
                order.FirstName = "Test 3";
                order.LastName = "Test 3";
                order.Email = "test@a.co.th";
                order.MobilePhone = "0909998888";
                order.LeadSource = "App";
                order.RecordTypeId = "012p00000004fJMAAY"; //VNRecordTypeId[Sandbox]

                
                List<APIType_Order> lstTempProduct = new List<APIType_Order>();

                foreach (string product in order.ProductCodes)
                {
                    APIType_Order newProduct = new APIType_Order();
                    newProduct.ProductCodeExternal__c = product;
                    lstTempProduct.Add(newProduct);
                }


                client.CreateOrder(order, lstTempProduct, apiResponse).Wait();



                // Populate response data.
                // this is where you create a response data type to capture data return to a client...
                APIResponse_SubmitOrder quoteResponse = new APIResponse_SubmitOrder();
                quoteResponse.OrderId = order.OrderId;


                // Assign return code/status.
                quoteResponse.Success = true;


                // Lastly, send out response message.
                var httpResponse = Request.CreateResponse<APIResponse_SubmitOrder>(HttpStatusCode.OK, quoteResponse);
                return httpResponse;

            }
            catch (Exception ex)
            {
                // First, you will always want to do internal error handling first. Note that,
                // we use utility metod, so that error handling is done at a singe location.
                AppUtils.AppUtility.CreateErrorLog("", "");

                #region "Error Response"

                // Now, create error response.
                // It does "not" make sense to put the following codes into utility method, because the codes are not
                // not muh here, and if we include "Requesrt.CreateResponse" into the utility method, then we don't
                // have access to the Request object outside controller.
                APIResponse errorResponse = new APIResponse();
                errorResponse.Success = false;

                errorResponse.ErrorCode = "";
                errorResponse.ErrorMessage = "";

                #if DEBUG
                // optionally you can return exception message only when in debug mode...
                errorResponse.ErrorMessage = ex.Message;
                #endif

                #endregion

                var httpResponse = Request.CreateResponse<APIResponse>(HttpStatusCode.InternalServerError, errorResponse);
                return httpResponse;
            }
        }
    }
}