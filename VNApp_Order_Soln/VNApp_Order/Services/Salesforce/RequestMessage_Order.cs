using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VNApp_Order.Controllers.Orders;
using VNApp_Order.Controllers.Members;
using VNApp_Order.Controllers.Products;

namespace VNApp_Order.Services.Salesforce
{
    public class RequestMessage_Order
    {
        public List<APIType_Product> lstTempProducts { get; set; }
        public APIType_Member lead { get; set; }

        public RequestMessage_Order()
        {
            lstTempProducts = new List<APIType_Product>();
            lead = new APIType_Member();
        }
    }
}