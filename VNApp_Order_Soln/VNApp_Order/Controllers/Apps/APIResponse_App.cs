using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers.Apps
{
    public class APIResponse_App : APIResponse_Authen
    {
        public string id { get; set; }
        public List<string> errors { get; set; }
        public bool success { get; set; }

        public APIResponse_App()
        {
            id = "";
            success = false;
        }
    }
}