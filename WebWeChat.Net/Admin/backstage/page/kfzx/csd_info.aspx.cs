using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.common;

namespace WebWeChat.Net.Admin.backstage.page.kfzx
{
    public partial class csd_info : SysPage
    {
        private ConnectionDb db = new ConnectionDb();
        public string uin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            uin = Request.QueryString["uin"];
            csd_infoLoad();

        }

        private void csd_infoLoad()
        {
            string sql = "select * from csd where uin = '" + uin + "'";
            DataTable dt = db.SelectDT(sql, null);
            if (null!=dt&&dt.Rows.Count>0) {
                //
                this.nick_Name.Text = xxHTML(dt.Rows[0]["nickname"].ToString().Trim());
                this.login_Time.Text = dt.Rows[0]["logintime"].ToString().Trim();
                this.wxImg.Src = dt.Rows[0]["WeiXinImgUrl"].ToString().Trim();

                //
                this.user_uin.Value = uin;
                this.nickname.Value = xxHTML(dt.Rows[0]["nickname"].ToString().Trim());
                this.area .Value = dt.Rows[0]["province"].ToString().Trim()+"-" + dt.Rows[0]["city"].ToString().Trim();
                this.signature.Value = dt.Rows[0]["signature"].ToString().Trim();
                this.loginname.Value = dt.Rows[0]["loginname"].ToString().Trim();
                this.realname.Value = dt.Rows[0]["realname"].ToString().Trim();
                this.certnumber.Value = dt.Rows[0]["certnumber"].ToString().Trim();
                this.tel.Value = dt.Rows[0]["mobile"].ToString().Trim();
                this.email.Value = dt.Rows[0]["email"].ToString().Trim();
            }           
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