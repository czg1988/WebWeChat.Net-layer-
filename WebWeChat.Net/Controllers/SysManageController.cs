﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.common;

namespace WebWeChat.Net.Controllers
{
    public class SysManageController : Controller
    {
        private ConnectionDb db = new ConnectionDb();
        // GET: SysManage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSysLog()
        {

            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int totalCount = 0;
            int PageCount = 0;
            string keyWord = Request["kw"];
            string sTime = Request["sTime"];
            string eTime = Request["eTime"];

            string conditions = " csd c,LoginLogs l where c.Uin = l.Uin  ";
            string filed = " l.id,c.uin,c.NickName,ltime=CONVERT(varchar(20),l.loginTime,120),l.loginIP  ";
            if (!string.IsNullOrEmpty(keyWord))
            {
                conditions += "  and c.NickName like '%" + keyWord + "%' ";
            }
            if (!string.IsNullOrEmpty(sTime))
            {
                conditions += " and dateDiff(day, l.loginTime, '" + sTime + "') <= 0";
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                conditions += " and dateDiff(day, l.loginTime, '" + eTime + "') >= 0";
            }
            string OrderFields = " order by l.loginTime desc,  c.NickName desc";

            //PageIndex 当前页码
            //PageSize  每页条数
            //Condition 表明+条件
            //RecordCount 总记录数
            //PageCount 总页数
            //AllFields查询字段
            //IndexField 排序字段
            SqlParameter[] param = null;
            DataTable dt = Tools.GetPageList((start / length + 1), length, filed, "l.id", conditions, OrderFields, out totalCount, out PageCount, param);

            var data = new { draw = draw, recordsTotal = totalCount, recordsFiltered = totalCount, data = ConvertHelper.GetJSonArrayList(dt) };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteLogById()
        {
            string id = Request["id"];
            string sql_dellog = "delete LoginLogs where id =" + int.Parse(id);
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
        public JsonResult GetAdminList()
        {

            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int totalCount = 0;
            int PageCount = 0;
            string keyWord = Request["kw"];
            string sTime = Request["sTime"];
            string eTime = Request["eTime"];

            string conditions = "fs_sys_User where 1=1  ";
            string filed = " *,lltime=CONVERT(varchar(20),lastLoginTime,120) ";
            if (!string.IsNullOrEmpty(keyWord))
            {
                conditions += "  and username like '%" + keyWord + "%' ";
            }
            if (!string.IsNullOrEmpty(sTime))
            {
                conditions += " and dateDiff(day, lastLoginTime, '" + sTime + "') <= 0";
            }
            if (!string.IsNullOrEmpty(eTime))
            {
                conditions += " and dateDiff(day, lastLoginTime, '" + eTime + "') >= 0";
            }
            string OrderFields = " order by lastLoginTime desc,  username desc";

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
        public JsonResult GetInterfaceList()
        {

            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int totalCount = 0;
            int PageCount = 0;
            string keyWord = Request["kw"];
            string sTime = Request["sTime"];
            string eTime = Request["eTime"];

            string conditions = "interface where 1=1  ";
            string filed = " *,stime=CONVERT(varchar(20),systime,120)";
            if (!string.IsNullOrEmpty(keyWord))
            {
                conditions += "  and (interface_name like '%" + keyWord + "%' or interface_url like '%" + keyWord + "%') ";
            }

            string OrderFields = " order by systime desc";

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
        public JsonResult SaveorUpdateIterInfo()
        {
            string id = Request["id"];
            string i_name = Request["i_name"];
            string i_url = Request["i_url"];
            bool flag = true;
            if (!string.IsNullOrEmpty(id) && !("0".Equals(id)))
            {
                string sql = "update interface set interface_name = '" + i_name + "' ,interface_url='" + i_url + "',systime='"+ DateTime.Now + "' where id = " + int.Parse(id);
                flag = db.Update(sql, null);
            }
            else
            {
                string sql = "insert interface (interface_name,interface_url,systime,isused) values('"+ i_name + "','" + i_url + "','" + DateTime.Now + "','1')";
                flag = db.Insert(sql, null);
            }
            if (flag)
            {
                return Json(Tools.GetResult(true, "保存成功", null));
            }
            else
            {
                return Json(Tools.GetResult(false, "保存失败", null));
            }
        }


        [HttpPost]
        public JsonResult ChangeIStatus()
        {
            string id = Request["id"];
            string sql_i = "update interface set isused=~isused where id =" + int.Parse(id);
            bool flag = db.Update(sql_i, null);
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
        public JsonResult DeleteInterfaceById()
        {
            string id = Request["id"];
            string sql_deli = "delete interface where id =" + int.Parse(id);
            bool flag = db.Delete(sql_deli, null);
            if (flag)
            {
                return Json(Tools.GetResult(true, "", null));
            }
            else
            {
                return Json(Tools.GetResult(false, "", null));
            }

        }


        public string TztjfxWxB()
        {
            JavaScriptSerializer jsS = new JavaScriptSerializer();
            List<object> list = new List<object>();


            string sql = "SELECT zclb,COUNT(*) num FROM wxjl w,sysbtz s where w.jszxbh =s.jszxbh and zclb in( select nr from  zdb_q where zdm = 'zclb' and xh!= 0 ) group by zclb order by num desc";
            DataTable dt = db.SelectDT(sql, null);
            if (null != dt && dt.Rows.Count > 0)
            {

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    Obj o = new Obj();
                    o.value = dt.Rows[a]["num"].ToString();
                    o.name = dt.Rows[a]["zclb"].ToString();
                    list.Add(o);
                }
            }
            return jsS.Serialize(list);
        }

        public JsonResult UpdatePwd()
        {
            string uin = Session["uin"].ToString().Trim();
            string oldPwd = Request["oldpwd"];
            string newpwd = Request["newpwd"];
            if (null != uin)
            {
                string oPwdMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldPwd, "MD5").ToLower();
                string nPwdMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(newpwd, "MD5").ToLower();

                string sql1 = "SELECT * FROM csd where uin='" + uin + "' and password = '" + oPwdMd5 + "'";
                DataTable dt = db.SelectDT(sql1, null);
                if (null != dt && dt.Rows.Count > 0)
                {
                    sql1 = "update csd set password ='" + nPwdMd5 + "' where uin='" + uin + "'";
                    bool flag = db.Update(sql1, null);
                    if (flag)
                    {
                        return Json(Tools.GetResult(true, "修改成功", null));
                    }
                    else
                    {
                        return Json(Tools.GetResult(false, "修改失败", null));
                    }
                }
                else
                {
                    return Json(Tools.GetResult(false, "原密码输入不正确", null));

                }
            }
            else
            {
                return Json(Tools.GetResult(false, "0", null));
            }
        }
        internal class Obj
        {
            private string _name;
            private string _value;

            public string value
            {
                get { return _value; }
                set { _value = value; }
            }
            public string name
            {
                get { return _name; }
                set { _name = value; }
            }
        }



    }
}