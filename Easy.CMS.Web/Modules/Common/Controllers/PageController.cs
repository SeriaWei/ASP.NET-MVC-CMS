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
    public class PageController : BasicController<PageEntity, string, PageService>
    {
        public PageController()
            : base(new PageService())
        {

        }
        [Widget, OutputCache(CacheProfile = "Page")]
        public ActionResult PreView()
        {
            return View();
        }
        [AdminTheme, Authorize]
        public override ActionResult Index()
        {
            return base.Index();
        }

        public JsonResult GetPageTree()
        {
            var pages = Service.Get(new DataFilter().OrderBy("DisplayOrder", OrderType.Ascending));
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.ID, m => m.PageName, m => m.ParentId, "#");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        public override ActionResult Create()
        {
            return base.Create();
        }

        [AdminTheme, ViewDataLayouts, Authorize]
        public ActionResult Create(string ParentID)
        {
            var page = new PageEntity
            {
                ParentId = ParentID,
                DisplayOrder = Service.Get("ParentID", OperatorType.Equal, ParentID).Count() + 1,
                Url = "~/"
            };
            var parentPage = Service.Get(ParentID);
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
        [AdminTheme, ViewDataLayouts, HttpPost, Authorize]
        public override ActionResult Create(PageEntity entity)
        {
            base.Create(entity);
            return RedirectToAction("Design", new { ID = entity.ID });
        }
        [AdminTheme, ViewDataLayouts, Authorize]
        public override ActionResult Edit(string Id)
        {
            return base.Edit(Id);
        }
        [AdminTheme, ViewDataLayouts, Authorize]
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
        [EditWidget, Authorize]
        public ActionResult Design(string ID)
        {
            return View();
        }
        [Authorize]
        public ActionResult RedirectView(string Id)
        {
            return Redirect(Service.Get(Id).Url + "?ViewType=Review");
        }
        [PopUp, Authorize]
        public ActionResult Select()
        {
            return View();
        }
        [Authorize]
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
                    LayoutID = layout.ID,
                    Zones = zoneService.GetZonesByPageId(context.PageID),
                    Widgets = widgetService.GetAllByPageId(context.PageID),
                    LayoutHtml = layout.Html
                };
            return View(viewModel);
        }
        [HttpPost, Authorize]
        public JsonResult MovePage(string id, int position, int oldPosition)
        {
            Service.Move(id, position, oldPosition);
            return Json(true);
        }
    }
}
