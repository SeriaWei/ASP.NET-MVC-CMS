using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Data;
using Easy.Web;
using Easy.Web.Attribute;
using Easy.Web.CMS.Theme;
using Easy.Web.Controller;
using EasyZip;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, Authorize]
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
            string path = Server.MapPath(ThemePath + "/" + id);
            if (Directory.Exists(path))
            {
                var theme = Service.Get(id);
                if (theme != null)
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(theme);
                    var writer = System.IO.File.CreateText(path + "/info.json");
                    writer.Write(json);
                    writer.Dispose();
                    ZipFile zipFile = new ZipFile();
                    zipFile.AddDirectory(new DirectoryInfo(path));
                    return File(zipFile.ToMemoryStream(), "application/theme", theme.Title + ".zip");
                }
            }
            return null;
        }
        [HttpPost]
        public JsonResult UploadTheme()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var file = Request.Files[0];
                    ZipFile zipFile = new ZipFile();
                    var files = zipFile.ToFileCollection(file.InputStream);
                    var themeInfo = files.FirstOrDefault(m => m.RelativePath.EndsWith("\\info.json"));
                    string json = Encoding.UTF8.GetString(themeInfo.FileBytes);
                    var newTheme = Newtonsoft.Json.JsonConvert.DeserializeObject<ThemeEntity>(json);
                    newTheme.IsActived = false;
                    if (Service.Count(m => m.ID == newTheme.ID) == 0)
                    {
                        var themePath = Server.MapPath(ThemePath);
                        foreach (ZipFileInfo item in files)
                        {
                            using (
                              var fs =
                                  System.IO.File.Create(themePath + item.RelativePath)
                              )
                            {
                                fs.Write(item.FileBytes, 0, item.FileBytes.Length);
                            }
                        }
                        Service.Add(newTheme);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return Json(new AjaxResult { Status = AjaxStatus.Error, Message = "上传的模板不正确" });
                }
            }

            return Json(new AjaxResult { Status = AjaxStatus.Normal, Message = "上传成功" });
        }
    }
}
