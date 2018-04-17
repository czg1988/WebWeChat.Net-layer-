using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.common;

namespace WebWeChat.Net.Admin.backstage
{
    public partial class index : SysPage
    {
        public string menu_url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ("0".Equals(Session["level"].ToString().Trim()))
            {

                menu_url = "datas/menudatas_admin.json";
            }
            else if ("1".Equals(Session["level"].ToString().Trim()))
            {
                menu_url = "datas/menudatas_user.json";
            }
            else {

            }
        }
    }
}