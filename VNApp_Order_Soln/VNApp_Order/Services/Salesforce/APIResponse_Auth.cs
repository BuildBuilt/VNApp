using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Services.Salesforce
{
    public class APIResponse_Auth : APIResponse_Salesforce
    {
        public string access_token { get; set; }
        public string instance_url { get; set; }
        public string id { get; set; }
        public string issued_at { get; set; }
        public string signature { get; set; }

        public APIResponse_Auth()
        {
            access_token = "";
            instance_url = "";
            id = "";
            issued_at = "";
            signature = "";
        }
    }
}