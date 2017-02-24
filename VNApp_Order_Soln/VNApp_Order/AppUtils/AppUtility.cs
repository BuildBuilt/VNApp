using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VNApp_Order.AppUtils
{
    public class AppUtility
    {
        public static void CreateErrorLog(string errorCode, string errorMsg)
        {
            // this is where you do log and notification etc...
            // by having a single method to do this, you can consistly control
            // how error handling is being done...
        }
    }
}