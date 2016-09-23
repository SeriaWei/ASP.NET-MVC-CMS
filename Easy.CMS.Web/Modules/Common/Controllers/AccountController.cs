/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using System.Web.Security;
using Easy.Extend;
using Easy.Modules.User.Service;

namespace Easy.CMS.Common.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string password, string ReturnUrl)
        {
            var user = _userService.Login(userName, password, Request.ServerVariables["REMOTE_ADDR"]);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                if (ReturnUrl.IsNullOrEmpty())
                {
                    return RedirectToAction("Index", "Layout", new { module = "common" });
                }
                return Redirect(ReturnUrl);
            }
            ViewBag.Errormessage = "登录失败，用户名密码不正确";
            return View();
        }

        public ActionResult Logout(string returnurl)
        {
            FormsAuthentication.SignOut();
            return Redirect(returnurl ?? "~/");
        }
    }
}
