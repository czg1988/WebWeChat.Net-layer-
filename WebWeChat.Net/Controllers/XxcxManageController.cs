using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.common;
using WebWeChat.Net.common;

namespace WebWeChat.Net.Controllers
{

    public class XxcxManageController : Controller
    {
        private ConnectionDb db = new ConnectionDb();
       
        // GET: XxcxManage
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetDialogueList()
        {
            string condition = SysUtils.getCondition1(Session["uin"].ToString(), Session["level"].ToString());

            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int totalCount = 0;
            int PageCount = 0;
            string keyWord = Request["kw"];
            string sTime = Request["sTime"];
            string eTime = Request["eTime"];

            string conditions = " Dialogue where 1=1  " + condition;
            string filed = "id, FromNickName,ToNickName,DContent,sysTime=CONVERT(varchar(20),SystemTime,120)";
            if (!string.IsNullOrEmpty(keyWord))
            {
                conditions += "  and (FromNickName like '%" + keyWord + "%' or ToNickName like '%" + keyWord + "%' or  DContent like '%" + keyWord+"%') ";
            }
            if (!string.IsNullOrEmpty(sTime))
            {
                conditions += " and dateDiff(day, SystemTime, '" + sTime + "') <= 0";
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                conditions += " and dateDiff(day, SystemTime, '" + eTime + "') >= 0";
            }
            string OrderFields = " order by SystemTime desc";

            //PageIndex 当前页码
            //PageSize  每页条数
            //Condition 表明+条件
            //RecordCount 总记录数
            //PageCount 总页数
            //AllFields查询字段
            //IndexField 排序字段
            SqlParameter[] param = null;
            DataTable dt = Tools.GetPageList((start / length + 1), length, filed, "id", conditions, OrderFields, out totalCount, out PageCount, param);

            var data = new { draw = draw, recordsTotal = totalCount, recordsFiltered = totalCount, data = ConvertHelper.GetJSonArrayList(dt) };
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult DeleteDialogueById()
        {
            string id = Request["id"];
            string sql_dellog = "delete dialogue where id =" + int.Parse(id);
            bool flag = db.Delete(sql_dellog, null);
            if (flag)
            {
                return Json(Tools.GetResult(true, "", null));
            }
            else
            {
                return Json(Tools.GetResult(false, "", null));
            }

        }

        [HttpPost]
        public JsonResult GetCustomerList()
        {
            string condition = SysUtils.getCondition("csuin", Session["uin"].ToString(), Session["level"].ToString());

            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int totalCount = 0;
            int PageCount = 0;
            string keyWord = Request["kw"];

            string conditions = " Customer where 1=1  " + condition ;
            string filed = "id, nickname,case when sex=1 then '男' when sex = 0 then '女' else '' end as gender,province+'-'+city as area,signature";
            if (!string.IsNullOrEmpty(keyWord))
            {
                conditions += "  and  nickname like '%" + keyWord + "%' ";
            }

            string OrderFields = " order by id desc";

            //PageIndex 当前页码
            //PageSize  每页条数
            //Condition 表明+条件
            //RecordCount 总记录数
            //PageCount 总页数
            //AllFields查询字段
            //IndexField 排序字段
            SqlParameter[] param = null;
            DataTable dt = Tools.GetPageList((start / length + 1), length, filed, "id", conditions, OrderFields, out totalCount, out PageCount, param);

            var data = new { draw = draw, recordsTotal = totalCount, recordsFiltered = totalCount, data = ConvertHelper.GetJSonArrayList(dt) };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        // GET: Kfzx
        public JsonResult GetFriendList()
        {
            string customer_uin = Request["customer_uin"];
            string key = Request["key"];
            string sql = "select Uin,province,city,signature,NickName from CSD where Uin = (select CsUin from Customer where uin='" + customer_uin + "') and nickname like '%" + key + "%'  order by id asc";
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

        public JsonResult GetCsdDialogueList()
        {
            string uin = Request["uin"];
            string customer_uin = Request["customer_uin"];
            if (string.IsNullOrEmpty(uin))
            {
                uin = "";
                string t_sql = "select top 1 uin from CSD where Uin = (select CsUin from Customer where uin = '" + customer_uin + "') order by id asc";
                DataTable t_dt = db.SelectDT(t_sql, null);
                if (t_dt != null && t_dt.Rows.Count > 0)
                {
                    uin = t_dt.Rows[0][0].ToString();
                }
            }
            string sql = "select *,sysTime=CONVERT(varchar(20),SystemTime,120) FROM Dialogue where (FromUin = '" + uin + "' and ToUin = '" + customer_uin + "') or (ToUin = '" + uin + "' and FromUin = '" + customer_uin + "') order by SystemTime asc";
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