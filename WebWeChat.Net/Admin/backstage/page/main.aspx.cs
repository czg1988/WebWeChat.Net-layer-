using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.common;

namespace WebWeChat.Net.Admin.backstage.page
{
    public partial class main : SysPage
    {
        public string lg_class = "layui-col-lg3";
        private ConnectionDb db = new ConnectionDb();
        protected void Page_Load(object sender, EventArgs e)
        {
            string level = Session["level"].ToString().Trim();
            string sql = "select (select COUNT(*) from csd ) as n1,(select COUNT(*) from Customer) as n2,(select COUNT(*) from Dialogue) as n3";
            this.tongji_a.Visible = true;
            if (!"0".Equals(level)) {
                this.tongji_a.Visible = false;
                lg_class = "layui-col-lg4";
                sql = "select 0 as n1, (select COUNT(*) from Customer where CsUin = '" + Session["uin"].ToString().Trim() + "') as n2,(select COUNT(*) from Dialogue where (FromUin = '" + Session["uin"].ToString().Trim() + "' or ToUin = " + Session["uin"].ToString().Trim() + ")) as n3";
            }
            DataTable dt = db.SelectDT(sql, null);
            if (null!=dt&& dt.Rows.Count>0) {
                this.labTotalNumber.Text = dt.Rows[0]["n1"].ToString().Trim();
                this.labZxNumber.Text = dt.Rows[0]["n2"].ToString().Trim();
                this.labSjNumber.Text = dt.Rows[0]["n3"].ToString().Trim();
                this.labBfNumber.Text = dt.Rows[0]["n2"].ToString().Trim();
            }

        }
    }
}