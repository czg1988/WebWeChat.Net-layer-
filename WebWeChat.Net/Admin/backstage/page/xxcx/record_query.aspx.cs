using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.common;

namespace WebWeChat.Net.Admin.backstage.page.xxcx
{
    public partial class record_query : SysPage
    {
        public string id = "";
        public string customer_uin = "";
        ConnectionDb db = new ConnectionDb();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                string sql = "select nickname,uin from customer where id = " + int.Parse(id);
                DataTable dt = db.SelectDT(sql, null);
                this.nick_name.Text = dt.Rows[0][0].ToString().Trim();
                customer_uin = dt.Rows[0][1].ToString().Trim();
            }

            //this.key.Attributes.Add("onchange", GetPostBackEventReference(this.key));

            //if (!IsPostBack)
            //{
            //    frieng_load(friend_lists, customer_uin, "");//-->绑定客服
            //    dialogue_load(dialogue_lists, customer_uin, "");
            //}
        }


        //private void dialogue_load(Repeater rp, string customer_uin, string to)
        //{
        //    if (string.IsNullOrEmpty(to))
        //    {
        //        to = "";
        //        string t_sql = "select top 1 uin from CSD where Uin = (select CsUin from Customer where uin = '" + customer_uin + "') order by id asc";
        //        DataTable t_dt = db.SelectDT(t_sql, null);
        //        if (t_dt != null && t_dt.Rows.Count > 0)
        //        {
        //            to = t_dt.Rows[0][0].ToString();
        //        }
        //    }
        //    string sql = "select * FROM Dialogue where(FromUin = " + customer_uin + " and ToUin = " + to + ") or(ToUin = " + customer_uin + " and FromUin = " + to + ") order by SystemTime asc";
        //    DataTable dt = db.SelectDT(sql, null);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        dt.Columns.Add("tb", typeof(string));
        //        dt.Columns.Add("class", typeof(string));
        //        for (int k = 0; dt.Rows.Count > k; k++)
        //        {

        //            dt.Rows[k]["tb"] = "CSD";
        //            dt.Rows[k]["class"] = "class=\"out\"";
        //            if (dt.Rows[k]["fromuin"].ToString().Equals(customer_uin))
        //            {
        //                dt.Rows[k]["tb"] = "Customer";
        //                dt.Rows[k]["class"] = "class=\"in\"";
        //            }
        //        }
        //    }
        //    rp.DataSource = dt;
        //    rp.DataBind();
        //    rp.Dispose();

        //}

        //private void frieng_load(Repeater rp, string customer_uin, string nickName)
        //{
        //    string sql = "select * from CSD where Uin = (select CsUin from Customer where uin='" + customer_uin + "') and nickname like '%" + nickName + "%'  order by id asc";
        //    DataTable dt = db.SelectDT(sql, null);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        dt.Columns.Add("sig", typeof(string));
        //        for (int k = 0; dt.Rows.Count > k; k++)
        //        {
        //            string sig = xxHTML(dt.Rows[k]["signature"].ToString().Trim()).Replace('<', ' ');
        //            dt.Rows[k]["sig"] = sig;
        //        }
        //    }
        //    rp.DataSource = dt;
        //    rp.DataBind();
        //    rp.Dispose();
        //}

        //protected void keys_ServerChange(object sender, EventArgs e)
        //{
        //    string key = this.key.Value;
        //    frieng_load(friend_lists, customer_uin, key);//-->绑定客服
        //    dialogue_load(dialogue_lists, customer_uin, "");
        //}

        //public string xxHTML(string html)
        //{

        //    html = html.Replace("(<style)+[^<>]*>[^\0]*(</style>)+", "");
        //    html = html.Replace(@"\<img[^\>] \>", "");
        //    html = html.Replace(@"<p>", "");
        //    html = html.Replace(@"</p>", "");


        //    System.Text.RegularExpressions.Regex regex0 =
        //    new System.Text.RegularExpressions.Regex("(<style)+[^<>]*>[^\0]*(</style>)+", System.Text.RegularExpressions.RegexOptions.Multiline);
        //    System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S] </script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S] </iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S] </frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>] \>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //    html = regex1.Replace(html, ""); //过滤<script></script>标记  
        //    html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性   
        //    html = regex0.Replace(html, ""); //过滤href=javascript: (<A>) 属性   


        //    //html = regex10.Replace(html, "");  
        //    html = regex3.Replace(html, "");// _disibledevent="); //过滤其它控件的on...事件  
        //    html = regex4.Replace(html, ""); //过滤iframe  
        //    html = regex5.Replace(html, ""); //过滤frameset  
        //    html = regex6.Replace(html, ""); //过滤frameset  
        //    html = regex7.Replace(html, ""); //过滤frameset  
        //    html = regex8.Replace(html, ""); //过滤frameset  
        //    html = regex9.Replace(html, "");
        //    //html = html.Replace(" ", "");  
        //    html = html.Replace("</strong>", "");
        //    html = html.Replace("<strong>", "");
        //    html = html.Replace(" ", "");
        //    return html;
        //}

    }
}