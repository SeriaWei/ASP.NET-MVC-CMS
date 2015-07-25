using Easy.Constant;
using Easy.Data;
using Easy.Web.CMS;
using Easy.Web.CMS.Filter;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Widget;
using Easy.Web.Attribute;
using Easy.Web.CMS.Zone;
using Easy.Web.Controller;
using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Easy.Web.CMS.Layout;

namespace Easy.CMS.Common.Controllers
{
    public class PageController : BasicController<PageEntity, PageService>
    {
        public PageController() : base(new PageService())
        {
            
        }
        [Widget]
        [OutputCache(CacheProfile = "Page")]
        public ActionResult PreView()
        {
            return View();
        }
        [AdminTheme]
        public override ActionResult Index(ParamsContext context)
        {
            return base.Index(context);
        }

        public JsonResult GetPageTree(ParamsContext context)
        {
            var pages = Service.Get(new DataFilter().OrderBy("DisplayOrder", OrderType.Ascending));
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.ID, m => m.PageName, m => m.ParentId, "#");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
        [AdminTheme, ViewData_Layouts]
        public override ActionResult Create(ParamsContext context)
        {
            var page = new PageEntity
            {
                ParentId = context.ParentID,
                DisplayOrder = Service.Get("ParentID", OperatorType.Equal, context.ParentID).Count() + 1,
                Url = "~/"
            };
            var parentPage = Service.Get(context.ParentID);
            if (parentPage != null)
            {
                page.Url = parentPage.Url;
            }
            if (!page.Url.EndsWith("/"))
            {
                page.Url += "/";
            }
            return View(page);

        }
        [AdminTheme, ViewData_Layouts, HttpPost]
        public override ActionResult Create(PageEntity entity)
        {
            base.Create(entity);
            return RedirectToAction("Design", new { ID = entity.ID });
        }
        [AdminTheme, ViewData_Layouts]
        public override ActionResult Edit(ParamsContext context)
        {
            return base.Edit(context);
        }
        [AdminTheme, ViewData_Layouts]
        [HttpPost]
        public override ActionResult Edit(PageEntity entity)
        {
            if (entity.ActionType == ActionType.Design)
            {
                return RedirectToAction("Design", new { ID = entity.ID });
            }
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
                HttpResponse.RemoveOutputCacheItem(entity.Url.Replace("~", ""));
                if (entity.IsHomePage)
                {
                    HttpResponse.RemoveOutputCacheItem("/");
                }
            }
            return result;
        }
        [EditWidget]
        public ActionResult Design(string ID)
        {
            return View();
        }
        public ActionResult RedirectView(ParamsContext context)
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
            var zoneService = new ZoneService();
            var widgetService = new WidgetService();
            var page = Service.Get(context.PageID);
            var layoutService = new LayoutService();
            var layout = layoutService.Get(page.LayoutId);
            var viewModel = new ViewModels.LayoutZonesViewModel
                {
                    PageID = context.PageID,
                    Zones = zoneService.GetZonesByPageId(context.PageID),
                    Widgets = widgetService.GetAllByPageId(context.PageID),
                    LayoutHtml = layout.Html
                };
            return View(viewModel);
        }
        [HttpPost]
        public JsonResult MovePage(string id, int position, int oldPosition)
        {
            Service.Move(id, position, oldPosition);
            return Json(true);
        }
    }
}
