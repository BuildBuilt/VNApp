using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public String ErrorCode { get; set; }
        public String ErrorMessage { get; set; }

        public APIResponse()
        {
            Success = false;
            ErrorCode = "";
            ErrorMessage = "";
        }
    }
}