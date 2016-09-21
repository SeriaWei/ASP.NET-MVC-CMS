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
using Easy.Web.Authorize;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Section.Controllers
{
    [PopUp, DefaultAuthorize]
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
                PartialView = "SectionTemplate.Default",
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
                    ServiceLocator.Current.GetInstance<IWidgetService>().InstallPackWidget(Request.Files[0].InputStream);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return Json(new AjaxResult { Status = AjaxStatus.Error, Message = "上传的模板不正确" });
                }
            }

            return Json(new AjaxResult { Status = AjaxStatus.Normal, Message = "上传成功" });
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
