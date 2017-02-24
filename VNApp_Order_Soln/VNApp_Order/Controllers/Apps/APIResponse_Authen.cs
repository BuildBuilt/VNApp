using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers.Apps
{
    public class APIResponse_Authen
    {
        public string Content { get; set; }
        public bool IsAuthen { get; set; }
        public string HTTPStatusCode { get; set; }

        public APIResponse_Authen()
        {
            Content = "";
            IsAuthen = false;
            HTTPStatusCode = "";
        }
    }
}