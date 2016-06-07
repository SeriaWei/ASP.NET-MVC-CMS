using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Easy.CMS.Section.Models;
using Easy.CMS.Section.Service;
using Easy.Constant;
using Easy.Data;
using Easy.Extend;
using Easy.Web;
using Easy.Web.Attribute;
using EasyZip;
using Microsoft.Practices.ServiceLocation;


namespace Easy.CMS.Section.Controllers
{
    [PopUp, Authorize]
    public class SectionGroupController : Controller
    {
        private readonly ISectionGroupService _sectionGroupService;
        private readonly ISectionContentProviderService _sectionContentProviderService;
        public SectionGroupController(ISectionGroupService sectionGroupService, ISectionContentProviderService sectionContentProviderService)
        {
            _sectionGroupService = sectionGroupService;
            _sectionContentProviderService = sectionContentProviderService;
        }

        public ActionResult Create(string sectionWidgetId)
        {
            var order = _sectionGroupService.Get("SectionWidgetId", OperatorType.Equal, sectionWidgetId).Count() + 1;
            return View("Form", new SectionGroup
            {
                SectionWidgetId = sectionWidgetId,
                ActionType = ActionType.Create,
                Order = order,
                GroupName = "组 " + order
            });
        }
        public ActionResult Edit(int Id)
        {
            var group = _sectionGroupService.Get(Id);
            group.ActionType = ActionType.Update;
            return View("Form", group);
        }
        [HttpPost]
        public ActionResult Save(SectionGroup group)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", group);
            }
            if (group.ActionType == ActionType.Create)
            {
                _sectionGroupService.Add(group);
            }
            else
            {
                _sectionGroupService.Update(group);
            }
            ViewBag.Close = true;
            return View("Form", group);
        }

        public JsonResult Delete(int Id)
        {
            _sectionGroupService.Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Sort(List<SectionGroup> groups)
        {
            groups.Each(m =>
            {
                var g = _sectionGroupService.Get(m.ID);
                g.Order = m.Order;
                _sectionGroupService.Update(g);
            });
            return Json(true);
        }

        [HttpPost]
        public JsonResult SortContent(List<SectionContent> contents)
        {
            contents.Each(m =>
            {
                var g = _sectionContentProviderService.Get(m.ID);
                g.Order = m.Order;
                _sectionContentProviderService.Update(g, new DataFilter(new List<string> { "Order" }).Where("ID", OperatorType.Equal, m.ID));
            });
            return Json(true);
        }
        [HttpPost]
        public JsonResult UploadTemplate()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var file = Request.Files[0];
                    ZipFile zipFile = new ZipFile();
                    var files = zipFile.ToFileCollection(file.InputStream);
                    SectionTemplate template = null;
                    bool asTemplate = false;
                    foreach (ZipFileInfo item in files)
                    {
                        if (item.RelativePath.EndsWith(".cshtml"))
                        {
                            using (
                                var fs = System.IO.File.Create(Server.MapPath("~/Modules/Section/Views") + item.RelativePath)
                                )
                            {
                                fs.Write(item.FileBytes, 0, item.FileBytes.Length);
                            }
                        }
                        else
                        {
                            using (
                                var fs =
                                    System.IO.File.Create(Server.MapPath("~/Modules/Section/Views/Thumbnail") +
                                                          item.RelativePath))
                            {
                                fs.Write(item.FileBytes, 0, item.FileBytes.Length);
                            }
                            if (item.RelativePath.EndsWith(".json"))
                            {
                                var info = System.IO.File.ReadAllText(Server.MapPath("~/Modules/Section/Views/Thumbnail") +
                                                          item.RelativePath);
                                template = Newtonsoft.Json.JsonConvert.DeserializeObject<SectionTemplate>(info);
                                var sectionTemplateService =
                                    ServiceLocator.Current.GetInstance<ISectionTemplateService>();
                                if (sectionTemplateService.Count(m => m.TemplateName == template.TemplateName) > 0)
                                {
                                    sectionTemplateService.Update(template);
                                }
                                else
                                {
                                    sectionTemplateService.Add(template);
                                }
                            }
                            else if (item.RelativePath.EndsWith(".xml"))
                            {
                                asTemplate = true;
                            }
                        }
                    }
                    if (asTemplate && template != null)
                    {
                        SectionWidget widget = new SectionWidget
                        {
                            IsTemplate = true,
                            WidgetName = template.Title,
                            PartialView = "Widget.Section",
                            AssemblyName = "Easy.CMS.Section",
                            ServiceTypeName = "Easy.CMS.Section.Service.SectionWidgetService",
                            ViewModelTypeName = "Easy.CMS.Section.Models.SectionWidget",
                            FormView = "SectionWidgetForm",
                            IsSystem = false,
                            Thumbnail = "~/Modules/Section/Views/" + template.Thumbnail
                        };
                        widget.Thumbnail = widget.Thumbnail.Replace("\\", "/");
                        SectionGroup group = new SectionGroup();
                        group.PartialView = template.TemplateName;
                        _sectionGroupService.GenerateContentFromConfig(group);
                        widget.Groups = new List<SectionGroup> { group };
                        ServiceLocator.Current.GetInstance<ISectionWidgetService>().Add(widget);
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

        public FileResult TemplatePackage(string name)
        {
            var template = ServiceLocator.Current.GetInstance<ISectionTemplateService>().Get(name);

            string infoFile = Server.MapPath("~/Modules/Section/Views/Thumbnail/{0}.json".FormatWith(name));
            using (var writer = System.IO.File.CreateText(infoFile))
            {
                writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(template));
            }

            ZipFile zipFile = new ZipFile();
            zipFile.AddFile(new System.IO.FileInfo(infoFile));
            var files = new[]
            {
                "~/Modules/Section/Views/{0}.cshtml",
                "~/Modules/Section/Views/Thumbnail/{0}.png",
                "~/Modules/Section/Views/Thumbnail/{0}.xml",
                "~/Modules/Section/Views/Thumbnail/{0}.json",
            };
            files.Each(f =>
            {
                string file = Server.MapPath(f.FormatWith(name));
                if (System.IO.File.Exists(file))
                {
                    zipFile.AddFile(new System.IO.FileInfo(file));
                }
            });

            return File(zipFile.ToMemoryStream(), "application/zip", template.Title + ".zip");
        }

        [HttpPost]
        public JsonResult SplitColumn(List<SectionGroup> groups)
        {
            if (groups != null)
            {
                groups.Each(g =>
                {
                    if (g.ID > 0)
                    {
                        var group = _sectionGroupService.Get(g.ID);
                        if (group != null)
                        {
                            group.PercentWidth = g.PercentWidth;
                        }
                        _sectionGroupService.Update(group);
                    }
                    else
                    {
                        _sectionGroupService.Add(g);
                    }
                });
            }
            return Json(new { Groups = groups.Count });
        }
    }
}
