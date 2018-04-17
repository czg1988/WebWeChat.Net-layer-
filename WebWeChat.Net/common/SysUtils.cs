using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.SessionState;

namespace WebWeChat.Net.common
{
   
    public class SysUtils
    {
        internal static string getCondition(string field,string u,string l)
        {
            string condition = "";
            if (!"0".Equals(l))
            {
                condition = "  and "+ field + " = '" + u + "'";
            }
            return condition;
        }

        internal static string getCondition1(string u, string l)
        {
            string condition = "";
            if (!"0".Equals(l))
            {
                condition = "  and (FromUin = '" + u + "' or ToUin = '" + u + "')";
            }
            return condition;
            
        }
    }
}