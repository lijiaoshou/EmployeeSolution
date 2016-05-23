using Employee.Bll;
using Employee.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Itcast.CMS.WebApp.Controllers
{
    public class NewListController : Controller
    {
        //
        // GET: /NewList/
        public ActionResult Index()
        {
            EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 5;
            int pageCount = NewInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<EmployeeNewInfo> list = NewInfoService.GetPageList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;
            return View();
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowDetail()
        {
            EmployeeNewInfoService NewInfoService = new EmployeeNewInfoService();
            int id = int.Parse(Request["id"]);
            EmployeeNewInfo newInfo = NewInfoService.GetModel(id);
           ViewData.Model = newInfo;
           return View();
        }

    }
}
