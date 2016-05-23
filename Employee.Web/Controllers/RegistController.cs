using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee.Entity;
using Employee.Bll;

namespace Employee.Web.Controllers
{
    public class RegistController : Controller
    {
        //
        // GET: /Regist/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserRegist()
        {
            //调用业务逻辑层进行注册
            string validateCode = Session["code_regist"] == null ? string.Empty : Session["code_regist"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!!");
            }
            Session["code_regist"] = null;
            string txtCode = Request["vCode_regist"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!!");
            }
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = Request["RegistCode"];
            userinfo.UserPwd = Request["RegistPwd"];
            userinfo.UserMail=Request["RegistMail"];
            userinfo.RegTime = System.DateTime.Now;
            UserInfoService UserInfoService = new UserInfoService();
            bool registSucceed = UserInfoService.AddUserInfo(userinfo);
            //注册成功
            if(registSucceed)
            {
                return Content("ok:"+userinfo.UserName+":"+userinfo.UserPwd);
            }
            //注册失败
            else
            {
                return Content("no:注册失败：原因");
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
            Session["code_regist"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }

        public ActionResult RegistSuuceed()
        {
            return View();
        }

    }
}
