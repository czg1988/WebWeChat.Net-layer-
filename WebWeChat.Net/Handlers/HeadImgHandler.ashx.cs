using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebChatManager.Manager.sysHandler
{
    /// <summary>
    /// HeadImgHandler 的摘要说明
    /// </summary>
    public class HeadImgHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tb = context.Request.QueryString["tb"];
            string uin = context.Request.QueryString["uin"];
            string sql = "select HeadImg from "+ tb + " where Uin = '" + uin + "'";
            ConnectionDb PowerDb = new ConnectionDb();
            DataTable dt = PowerDb.SelectDT(sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.ContentType = "text/plain";
                if (!string.IsNullOrEmpty(dt.Rows[0]["HeadImg"].ToString())) { 
                context.Response.BinaryWrite((byte[])dt.Rows[0]["HeadImg"]);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}