using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VNApp_Order.Controllers.Orders;
using VNApp_Order.Controllers.Members;

namespace VNApp_Order.Services.Salesforce
{
    public class RequestMessage_Order
    {
        public List<APIType_Order> lstTempProducts { get; set; }
        public APIType_Member lead { get; set; }

        public RequestMessage_Order()
        {
            lstTempProducts = new List<APIType_Order>();
            lead = new APIType_Member();
        }
    }
}