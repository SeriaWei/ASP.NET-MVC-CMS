using Easy.CMS.Filter;
using Easy.CMS.Page;
using Easy.CMS.Widget;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.CMS.Common.Controllers
{
    public class PageController : BasicController<PageEntity, string, PageService>
    {
        [Widget]
        public ActionResult PreView()
        {
            return View();
        }
        [AdminTheme]
        public override ActionResult Index(ParamsContext<string> context)
        {
            return base.Index(context);
        }

        public JsonResult GetPageTree(ParamsContext<string> context)
        {
            var pages = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.ID, m => m.PageName, m => m.ParentId, "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
        [AdminTheme]
        public override ActionResult Create(ParamsContext<string> context)
        {
            if (context == null || context.ParentID.IsNullOrEmpty())
            {
                return base.Create(context);
            }
            var parentPage = Service.Get(context.ParentID);
            if (parentPage != null)
            {
                var page = new PageEntity
                {
                    ParentId = parentPage.ID,
                    Url = parentPage.Url
                };
                if (!page.Url.EndsWith("/"))
                {
                    page.Url += "/";
                }
                return View(page);
            }
            return base.Create(context);
        }
        [AdminTheme]
        public override ActionResult Create(PageEntity entity)
        {
            return base.Create(entity);
        }
        [AdminTheme]
        public override ActionResult Edit(ParamsContext<string> context)
        {
            return base.Edit(context);
        }
        [AdminTheme]
        [HttpPost]
        public override ActionResult Edit(PageEntity entity, Constant.ActionType? actionType)
        {
            if (actionType.HasValue && actionType.Value == Constant.ActionType.Design)
            {
                return RedirectToAction("Design", new { ID = entity.ID });
            }
            var result = base.Edit(entity, actionType);
            if (actionType.HasValue && actionType.Value == Constant.ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
        [EditWidget]
        public ActionResult Design(string ID)
        {
            return View();
        }
        public ActionResult RedirectView(ParamsContext<string> context)
        {
            return Redirect(Service.Get(context.ID).Url + "?ViewType=Review");
        }
        [PopUp]
        public ActionResult Select()
        {
            return View();
        }

        public ActionResult PageZones(QueryContext context)
        {
            Zone.ZoneService zoneService = new Zone.ZoneService();
            WidgetService widgetService = new WidgetService();

            var viewModel = new ViewModels.LayoutZonesViewModel
                {
                    PageID = context.PageID,
                    Zones = zoneService.GetZonesByPageId(context.PageID),
                    Widgets = widgetService.GetAllByPageId(context.PageID)
                };
            return View(viewModel);
        }
    }
}
