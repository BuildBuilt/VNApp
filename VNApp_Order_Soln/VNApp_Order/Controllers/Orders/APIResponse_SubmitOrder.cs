using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers.Orders
{
    public class APIResponse_SubmitOrder : APIResponse
    {
        public string OrderId { get; set; }
        public string Status { get; set; }

        public APIResponse_SubmitOrder()
        {
            OrderId = "";
            Status = "";
        }
    }
}