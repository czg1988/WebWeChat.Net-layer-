using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.common;
using WebWeChat.Net.common;

namespace WebWeChat.Net.Admin.backstage.page.kfzx
{
    public partial class csd_list : SysPage
    {
        public string uin = "";
        public string level = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            uin = Session["uin"].ToString();
            level = Session["level"].ToString();
            if (!IsPostBack)
            {
                sqd_manage_load(csd_lists);//-->绑定客服
            }
        }

        private void sqd_manage_load(Repeater rp)
        {
           
            string condition = SysUtils.getCondition("uin",uin,level);
            string sql = "select Uin,NickName,Signature,HeadImg from CSD where 1=1 "+ condition + " order by OnLine desc";
            ConnectionDb PowerDb = new ConnectionDb();
            DataTable dt = PowerDb.SelectDT(sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("friends", typeof(int));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    dt.Rows[k]["friends"] = getfriendCount(dt.Rows[k]["Uin"].ToString());
                }
            }
            rp.DataSource = dt;
            rp.DataBind();
            rp.Dispose();
        }

        private int getfriendCount(string uin)
        {
            string sql = "select * from Customer where CsUin = '" + uin + "'";
            ConnectionDb PowerDb = new ConnectionDb();
            DataTable dt = PowerDb.SelectDT(sql, null);
            return dt.Rows.Count;
        }
    }
}