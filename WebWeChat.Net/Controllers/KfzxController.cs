using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.common;

namespace WebWeChat.Net.Controllers
{
    public class KfzxController : Controller
    {
        public ConnectionDb db = new ConnectionDb();
        // GET: Kfzx
        public ActionResult Index()
        {
            return View();
        }
        // GET: Kfzx
        public JsonResult GetFriendList()
        {
            string uin = Request["uin"];
            string key = Request["key"];
            string sql = "select Uin,province,city,signature,NickName from [Customer] where csuin = '" + uin + "' and nickname like '%" + key + "%' order by uin";
            DataTable dt = db.SelectDT(sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("sig", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    string sig = xxHTML(dt.Rows[k]["signature"].ToString().Trim()).Replace('<', ' ');
                    dt.Rows[k]["sig"] = sig;
                }
            }
            var data = new { data = ConvertHelper.GetJSonArrayList(dt) };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDialogueList()
        {
            string uin = Request["uin"];
            string customer_uin = Request["customer_uin"];
            if (string.IsNullOrEmpty(customer_uin))
            {
                customer_uin = "";
                string t_sql = "select top 1 touin from Dialogue where fromuin = '" + uin + "'";
                DataTable t_dt = db.SelectDT(t_sql, null);
                if (t_dt != null && t_dt.Rows.Count > 0)
                {
                    customer_uin = t_dt.Rows[0][0].ToString();
                }
            }
            string sql = "select *,SysTime=CONVERT(varchar(20),SystemTime,120) FROM Dialogue where(FromUin = '" + uin + "' and ToUin = '" + customer_uin + "') or(ToUin = '" + uin + "' and FromUin = '" + customer_uin + "') order by SystemTime asc";
            DataTable dt = db.SelectDT(sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("tb", typeof(string));
                dt.Columns.Add("class", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {

                    dt.Rows[k]["tb"] = "CSD";
                    dt.Rows[k]["class"] = "class=\"out\"";
                    if (dt.Rows[k]["fromuin"].ToString().Equals(customer_uin))
                    {
                        dt.Rows[k]["tb"] = "Customer";
                        dt.Rows[k]["class"] = "class=\"in\"";
                    }
                }
            }
            var data = new { data = ConvertHelper.GetJSonArrayList(dt) };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        

        public string xxHTML(string html)
        {

            html = html.Replace("(<style)+[^<>]*>[^\0]*(</style>)+", "");
            html = html.Replace(@"\<img[^\>] \>", "");
            html = html.Replace(@"<p>", "");
            html = html.Replace(@"</p>", "");


            System.Text.RegularExpressions.Regex regex0 =
            new System.Text.RegularExpressions.Regex("(<style)+[^<>]*>[^\0]*(</style>)+", System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S] </script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S] </iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S] </frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>] \>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记  
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性   
            html = regex0.Replace(html, ""); //过滤href=javascript: (<A>) 属性   


            //html = regex10.Replace(html, "");  
            html = regex3.Replace(html, "");// _disibledevent="); //过滤其它控件的on...事件  
            html = regex4.Replace(html, ""); //过滤iframe  
            html = regex5.Replace(html, ""); //过滤frameset  
            html = regex6.Replace(html, ""); //过滤frameset  
            html = regex7.Replace(html, ""); //过滤frameset  
            html = regex8.Replace(html, ""); //过滤frameset  
            html = regex9.Replace(html, "");
            //html = html.Replace(" ", "");  
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace(" ", "");
            return html;
        }

    }
}