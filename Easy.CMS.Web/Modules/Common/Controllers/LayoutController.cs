using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Layout;
using Easy.Web.Attribute;
using Easy.Extend;
using Easy.Constant;

namespace Easy.CMS.Common.Controllers
{

    public class LayoutController : BasicController<LayoutEntity, string, LayoutService>
    {
        [AdminTheme]
        public override System.Web.Mvc.ActionResult Index(ParamsContext<string> context)
        {
            return View(Service.Get(new Data.DataFilter()));
        }
        [AdminTheme]
        public override ActionResult Create(ParamsContext<string> context)
        {
            return base.Create(context);
        }
        [AdminTheme]
        public override ActionResult Create(LayoutEntity entity)
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
        public override ActionResult Edit(LayoutEntity entity, ActionType? actionType)
        {
            if (actionType.HasValue && actionType.Value == ActionType.Design)
            {
                return RedirectToAction("Design", new { ID = entity.ID });
            }
            return base.Edit(entity, actionType);
        }
        public ActionResult Design(ParamsContext<string> context)
        {
            LayoutEntity layout = null;
            if (!context.ID.IsNullOrEmpty())
            {
                layout = new LayoutService().Get(context.ID);
            }
            return View(layout ?? new LayoutEntity());
        }

        [ValidateInput(false)]
        public ActionResult SaveLayout(string[] html, LayoutEntity layout)
        {
            LayoutHtmlCollection htmls;
            var zones = Easy.CMS.Zone.Helper.GetZones(html, out htmls);
            layout.Zones = zones;
            layout.Html = htmls;
            Service.UpdateDesign(layout);
            return RedirectToAction("Edit", new { ID = layout.ID, module = "Common" });
        }
    }
}
