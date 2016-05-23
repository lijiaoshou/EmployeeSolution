using Employee.Bll;
using Employee.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogin()
        {
            string validateCode = Session["code_login"] == null ? string.Empty : Session["code_login"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!!");
            }
            Session["code_login"] = null;
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!!");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            UserInfoService UserInfoService = new UserInfoService();
            UserInfo userInfo = UserInfoService.GetUserInfo(userName, userPwd);
            if (userInfo != null)
            {
                //Nginx
                Session["userInfo"] = userInfo;
                return Content("ok:登录成功");
            }
            else
            {
                return Content("no:登录失败!!");
            }
        }

        /// <summary>
        /// 返回验证码图片
        /// </summary>
        /// <returns>验证码图片</returns>
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);//获取验证码.
            Session["code_login"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }

    }
}
