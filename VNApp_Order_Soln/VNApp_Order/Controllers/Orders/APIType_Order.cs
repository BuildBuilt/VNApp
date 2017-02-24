using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VNApp_Order.Controllers.Members;

namespace VNApp_Order.Controllers.Orders
{
    public class APIType_Order : APIType_Member
    {
        public string OrderId { get; set; }
        public List<string> ProductCodes { get; set; }
        public string ProductCodeExternal__c { get; set; }

        public APIType_Order()
        {
            //Order
            OrderId = "";
            ProductCodes = new List<string>();
            ProductCodeExternal__c = "";

            //Members
            FirstName = "";
            LastName = "";
            Email = "";
            MobilePhone = "";
            CustomerCodeExternal__c = "";
            LeadSource = "";
            RecordTypeId = "";
        }
    }
}