using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.IO;

using VNApp_Order.Controllers.Apps;

namespace VNApp_Order.Controllers.Apps
{
    [RoutePrefix("api/SendToApp")]
    public class AppController : ApiController
    {
        public AppController()
        {

        }

        //POST api/SendToApp/OrderStage
        [Route("OrderStage")]
        public HttpResponseMessage DownloadJSONMessage(APIType_App Salesforce)
        {
            try
            {
                // Business logics.


                // Populate response data.
                // this is where you create a response data type to capture data return to a client...

                APIResponse_App appResponse = new APIResponse_App();
                appResponse.HTTPStatusCode = "200";

                // Assign return code/status.
                appResponse.success = true;


                // Lastly, send out response message.
                var httpResponse = Request.CreateResponse<APIResponse_App>(HttpStatusCode.OK, appResponse);
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