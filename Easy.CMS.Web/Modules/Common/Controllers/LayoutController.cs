using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Data;
using Easy.Web.Controller;
using Easy.Web.CMS.Layout;
using Easy.Web.Attribute;
using Easy.Extend;
using Easy.Constant;
using Easy.Web.CMS.Zone;
using Easy.CMS.Common.ViewModels;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Common.Controllers
{
    [Authorize]
    public class LayoutController : BasicController<LayoutEntity, string, ILayoutService>
    {
        private readonly IPageService _pageService;
        private readonly IZoneService _zoneService;
        public LayoutController(ILayoutService service, IPageService pageService, IZoneService zoneService)
            : base(service)
        {
            _pageService = pageService;
            _zoneService = zoneService;
        }

        [AdminTheme]
        public override ActionResult Index()
        {
            return View(Service.Get());
        }
        [AdminTheme]
        public ActionResult LayoutWidget(string LayoutID)
        {
            ViewBag.LayoutID = LayoutID;
            return View(Service.Get());
        }
        [HttpPost]
        public ActionResult LayoutZones(string ID)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            var layout = Service.Get(ID);
            var viewModel = new LayoutZonesViewModel
            {
                LayoutID = ID,
                Zones = _zoneService.GetZonesByLayoutId(ID),
                Widgets = widgetService.GetByLayoutId(ID),
                LayoutHtml = layout.Html
            };
            return View(viewModel);
        }
        [AdminTheme]
        public override ActionResult Create()
        {
            return View(new LayoutEntity { ImageUrl = LayoutEntity.DefaultThumbnial, ImageThumbUrl = LayoutEntity.DefaultThumbnial });
        }
        [AdminTheme, HttpPost]
        public override ActionResult Create(LayoutEntity entity)
        {
            base.Create(entity);
            return RedirectToAction("Design", new { entity.ID });
        }
        [AdminTheme]
        public override ActionResult Edit(string ID)
        {
            return base.Edit(ID);
        }
        [AdminTheme]
        [HttpPost]
        public override ActionResult Edit(LayoutEntity entity)
        {
            if (entity.ActionType == ActionType.Design)
            {
                return RedirectToAction("Design", new { ID = entity.ID });
            }
            return base.Edit(entity);
        }
        public ActionResult Design(string ID, string PageID)
        {
            LayoutEntity layout = null;
            if (ID.IsNotNullAndWhiteSpace())
            {
                layout = Service.Get(ID);
            }
            if (PageID.IsNotNullAndWhiteSpace())
            {
                layout.Page = new PageEntity { ID = PageID };
            }
            return View(layout ?? new LayoutEntity());
        }

        [ValidateInput(false)]
        public ActionResult SaveLayout(string[] html, LayoutEntity layout)
        {
            LayoutHtmlCollection htmls;
            var zones = Helper.GetZones(html, out htmls);
            layout.Zones = zones;
            layout.Html = htmls;
            Service.UpdateDesign(layout);
            if (layout.Page != null)
            {
                return RedirectToAction("Design", "Page", new { module = "admin", ID = layout.Page.ID });
            }
            return RedirectToAction("Index");
        }
        [PopUp]
        public ActionResult SelectZone(string layoutId, string pageId, string zoneId)
        {
            LayoutEntity layou = null;
            if (layoutId.IsNotNullAndWhiteSpace())
            {
                layou = Service.Get(layoutId);
            }
            else if (pageId.IsNotNullAndWhiteSpace())
            {
                layou = Service.Get(_pageService.Get(pageId).LayoutId);
            }
            ViewBag.ZoneId = zoneId;
            return View(layou);
        }
    }
}
