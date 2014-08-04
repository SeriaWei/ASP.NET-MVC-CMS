using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Layout;
using Easy.Web.Attribute;
using Easy.Extend;

namespace Easy.CMS.Common.Controllers
{

    public class LayoutController : BasicController<LayoutEntity, string, LayoutService>
    {
        [AdminTheme]
        public override System.Web.Mvc.ActionResult Index()
        {
            return View(Service.Get(new Data.DataFilter()));
        }
        [AdminTheme]
        public override ActionResult Create()
        {
            return base.Create();
        }
        [AdminTheme]
        public override ActionResult Create(LayoutEntity entity)
        {
            return base.Create(entity);
        }
        [AdminTheme]
        public override ActionResult Edit(string id)
        {
            return base.Edit(id);
        }
        [AdminTheme]
        [HttpPost]
        public override ActionResult Edit(LayoutEntity entity)
        {
            return base.Edit(entity);
        }
        public ActionResult Design(string ID)
        {
            LayoutEntity layout = null;
            if (!ID.IsNullOrEmpty())
            {
                layout = new LayoutService().Get(ID);
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
            return RedirectToAction("Edit", new { ID = layout.ID, module = "layout" });
        }
    }
}
