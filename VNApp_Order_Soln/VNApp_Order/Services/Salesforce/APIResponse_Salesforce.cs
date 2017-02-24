using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Services.Salesforce
{
    public class APIResponse_Salesforce
    {
        public string Content { get; set; }
        public bool IsAuthen { get; set; }
        public string HTTPStatusCode { get; set; }

        public APIResponse_Salesforce()
        {
            Content = "";
            IsAuthen = false;
            HTTPStatusCode = "";
        }
    }
}