using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebWeChat.Net.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult UploadWxImg()
        {
            string uin = Request["uin"];
            try
            {
                var file = Request.Files[0];
                if ((file.ContentLength / 1024 / 1024) > 2)
                {
                    return Json(new
                    {
                        code = 1,
                        msg = "上传的图片大小不能大于2MB"
                    });
                }
                //获取保存路径
                var filesUrl = Server.MapPath("~/Uploads/WeiXin/");
                if (Directory.Exists(filesUrl) == false)//路径不存在则创建
                    Directory.CreateDirectory(filesUrl);
                var fileName = Path.GetFileName(file.FileName);
                //文件后缀名
                var filePostfixName = fileName.Substring(fileName.LastIndexOf('.'));
                //新文件名
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + filePostfixName;
                var path = Path.Combine(filesUrl, newFileName);
                //保存文件
                file.SaveAs(path);

                var newPath = "/Uploads/WeiXin/" + newFileName;

                string sql = "update csd set WeiXinImgUrl = '" + newPath + "' where uin = '" + uin + "'";
                ConnectionDb db = new ConnectionDb();
                bool rs = db.Update(sql, null);
                if (rs)
                {
                    return Json(new
                    {
                        code = 0,
                        msg = "上传成功",
                        data = new
                        {
                            src = newPath
                        }
                    });
                }
                else {
                    return Json(new
                    {
                        code = 1,
                        msg = "上传成功,路径存储失败"
                    });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 1,
                    msg = ex.Message
                });
            }
        }
    }
}