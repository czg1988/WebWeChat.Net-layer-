using System;
using System.Web;
using System.Web.SessionState;

namespace Web.common
{
    public class SysPage : System.Web.UI.Page, IRequiresSessionState
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["user_id"] == null || Session["uin"] == null)
            {
                Response.Write("<script>alert('对不起，登陆超时！');window.location='/Admin/logout.aspx';</script>");
                Response.End();
            }
        }
    }
}
