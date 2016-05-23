using Employee.Bll;
using Employee.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Itcast.CMS.WebApp.Controllers
{
    public class AdminNewListController : BaseController //Controller
    {
        //
        // GET: /AdminNewList/
        #region 分页列表
             public ActionResult Index()
        {
            EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();   
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 5;
            int pageCount = NewInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List <EmployeeNewInfo>list= NewInfoService.GetPageList(pageIndex, pageSize);//获取分页的数据
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }

        #endregion
        #region 获取详细记录
             public ActionResult GetNewInfoModel()
             {
                 EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
                 int id = int.Parse(Request["id"]);
                 EmployeeNewInfo newInfo = NewInfoService.GetModel(id);//获取详细信息.
                return Json(newInfo, JsonRequestBehavior.AllowGet);
             }
        #endregion
        #region 删除信息
             public ActionResult DeleteNewInfo()
             {
                 EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
                 int id = int.Parse(Request["id"]);
                 if (NewInfoService.DeleteInfo(id))
                 {
                     return Content("ok");
                 }
                 else
                 {
                     return Content("no");
                 }
             }
        #endregion

        #region 展示添加信息的表单页面
             public ActionResult ShowAddInfo()
             {
                 //EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
                 return View();
             }
        #endregion

        #region 文件上传
             public ActionResult FileUpload()
             {
                 EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
                 HttpPostedFileBase postFile = Request.Files["fileUp"];//接收文件数据
                 if (postFile == null)
                 {
                     return Content("no:请选择上传文件");
                 }
                 else
                 {
                     string fileName = Path.GetFileName(postFile.FileName);//文件名称
                     string fileExt = Path.GetExtension(fileName);//文件的扩展名称.
                     if (fileExt == ".jpg")
                     {
                         string dir = "/ImagePath/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                         Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹.
                         string newfileName = Guid.NewGuid().ToString();//新的文件名称.
                         string fullDir = dir + newfileName + fileExt;//完整的路径.
                         postFile.SaveAs(Request.MapPath(fullDir));//保存文件.
                         return Content("ok:" + fullDir);

                     }
                     else
                     {
                         return Content("no:文件格式错误!!");
                     }

                 }
             }
        #endregion

        #region 添加信息
        [ValidateInput(false)]
             public ActionResult AddNewInfo(EmployeeNewInfo newInfo)
             {
                 EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
                 newInfo.SubDateTime = DateTime.Now;
                 if (NewInfoService.AddInfo(newInfo))
                 {
                     return Content("ok");
                 }
                 else
                 {
                     return Content("no");
                 }
             }
        #endregion
    }
}
