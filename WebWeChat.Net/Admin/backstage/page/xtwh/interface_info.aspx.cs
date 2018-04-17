using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.common;

namespace WebWeChat.Net.Admin.backstage.page.xtwh
{
    public partial class interface_info : SysPage
    {
        private ConnectionDb db = new ConnectionDb();
        public string id = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                id = Request.QueryString["id"];
                interface_load(id);
            }
        }

        private void interface_load(string id)
        {
            string sql = "select * from interface where id = "+ int.Parse(id);
            DataTable dt = db.SelectDT(sql, null);
            if (null != dt && dt.Rows.Count > 0) {
                this.i_name.Value = dt.Rows[0]["interface_name"].ToString().Trim();
                this.i_url.Value = dt.Rows[0]["interface_url"].ToString().Trim();
            }
        }
    }
}