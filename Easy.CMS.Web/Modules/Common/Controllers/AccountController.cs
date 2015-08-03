using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            // _userService.Login(userName, password);
            FormsAuthentication.SetAuthCookie(userName, false);
            if (ReturnUrl.IsNullOrEmpty())
            {
                return RedirectToAction("Index", "Layout", new { module = "common" });
            }
            return Redirect(ReturnUrl);
        }
    }
}
