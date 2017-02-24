using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers.Products
{
    public class APIType_Product
    {
        public string ProductCodeExternal__c { get; set; }

        public APIType_Product()
        {
            ProductCodeExternal__c = "";
        }
    }
}