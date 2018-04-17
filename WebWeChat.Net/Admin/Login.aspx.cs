using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChatManager
{
    public partial class Login : System.Web.UI.Page
    {
        ConnectionDb db = new ConnectionDb();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = this.username.Value.Trim();
                string password = this.password.Value.Trim();
                string sql = "select * from csd where loginname = '" + username + "' and password = '" + softMD5(password, 32) + "'";
                DataTable dt = db.SelectDT(sql, null);

                if (null != dt && dt.Rows.Count > 0)
                {
                    Session["uin"] = dt.Rows[0]["uin"].ToString().Trim();
                    Session["nickname"] = dt.Rows[0]["nickname"].ToString().Trim();
                    Session["user_id"] = dt.Rows[0]["id"].ToString().Trim();
                    Session["loginname"] = dt.Rows[0]["loginname"].ToString().Trim();
                    Session["level"] = dt.Rows[0]["level"].ToString().Trim();
                    Response.Redirect("/Admin/backstage/index.aspx");
                }
                else {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "MsgBox", "<script language=javascript>alert('用户名或密码错误！');</script>");
                }
            }
            catch (Exception ex)
            {
                string c_db = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DB"].ToString().Replace("Password=", "-");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "MsgBox", "<script language=javascript>alert('数据库连接错误。请检查配置文件，确保服务器运行，网络畅通。检查连接语句：" + c_db + "');</script>");//+ ex 
            }
        }


        string softMD5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }
    }
}