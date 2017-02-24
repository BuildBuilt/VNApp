using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Services.Salesforce
{
    public class APIResponse_Create : APIResponse_Salesforce
    {
        public string id { get; set; }
        public List<string> errors { get; set; }
        public bool success { get; set; }

        public APIResponse_Create()
        {
            id = "";
            errors = new List<string>();
            success = false;
        }
    }
}