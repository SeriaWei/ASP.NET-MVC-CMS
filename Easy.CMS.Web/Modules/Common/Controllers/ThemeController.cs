/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Web;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Theme;
using Easy.Web.Controller;
using EasyZip;
using Newtonsoft.Json;
using Easy.Extend;
using Easy.Web.CMS.PackageManger;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class ThemeController : BasicController<ThemeEntity, String, IThemeService>
    {

        private readonly IPackageInstallerProvider _packageInstallerProvider;

        public ThemeController(IThemeService service, IPackageInstallerProvider packageInstallerProvider)
            : base(service)
        {
            _packageInstallerProvider = packageInstallerProvider;
        }

        public override ActionResult Index()
        {
            return View(Service.Get());
        }

        public ActionResult PreView(string id)
        {
            Service.SetPreview(id);
            return Redirect("~/");
        }

        public ActionResult CancelPreView()
        {
            Service.CancelPreview();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeTheme(string id)
        {
            Service.ChangeTheme(id);
            return Json(true);
        }


        public FileResult ThemePackage(string id)
        {
            var package = _packageInstallerProvider.CreateInstaller("ThemePackageInstaller").Pack(id) as ThemePackage;
            return File(package.ToFilePackage(), "Application/zip", package.Theme.Title + ".theme");
        }
        [HttpPost]
        public JsonResult UploadTheme()
        {
            var result = new AjaxResult(AjaxStatus.Normal, "主题安装成功，正在刷新...");
            if (Request.Files.Count > 0)
            {
                try
                {
                    ThemePackage package;
                    var installer = _packageInstallerProvider.CreateInstaller(Request.Files[0].InputStream, out package);
                    installer.Install(package);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    result.Message = "上传的主题不正确！" + ex.Message;
                    result.Status = AjaxStatus.Error;
                    return Json(result);
                }
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetCurrentTheme()
        {
            return Json(Url.Content(Service.GetCurrentTheme().Url));
        }
    }
}
