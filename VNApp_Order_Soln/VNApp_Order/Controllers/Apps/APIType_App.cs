using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers.Apps
{
    public class APIType_App
    {
        public string CustomerCode { get; set; }
        public string OrderId { get; set; }
        public string OrderStage { get; set; }

        public APIType_App()
        {
            CustomerCode = "";
            OrderId = "";
            OrderStage = "";
        }
    }
}