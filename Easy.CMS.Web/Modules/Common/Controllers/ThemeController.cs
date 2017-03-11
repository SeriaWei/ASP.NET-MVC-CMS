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

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class ThemeController : BasicController<ThemeEntity, String, IThemeService>
    {
        private const string ThemePath = "~/Themes";
        public ThemeController(IThemeService service)
            : base(service)
        {
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
            var package = new ThemePackageInstaller().Pack(id) as ThemePackage;
            return File(JsonConvert.SerializeObject(package).ToByte(), "Application/zip", package.Theme.Title + ".theme");
        }
        [HttpPost]
        public JsonResult UploadTheme()
        {
            var result = new AjaxResult(AjaxStatus.Normal, "主题安装成功，正在刷新...");
            if (Request.Files.Count > 0)
            {
                try
                {
                    StreamReader reader = new StreamReader(Request.Files[0].InputStream);
                    var theme = JsonConvert.DeserializeObject<ThemePackage>(reader.ReadToEnd());
                    new ThemePackageInstaller().Install(theme);
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
    }
}
