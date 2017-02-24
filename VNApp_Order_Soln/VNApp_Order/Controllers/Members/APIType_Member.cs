using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.Controllers.Members
{
    public class APIType_Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string CustomerCodeExternal__c { get; set; }
        public string LeadSource { get; set; }
        public string RecordTypeId { get; set; }

        public APIType_Member()
        {
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