using Easy.CMS.Common.Models;
using Easy.CMS.Common.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme]
    public class NavigationController : BasicController<NavigationEntity, string, NavigationService>
    {
        public override ActionResult Index(ParamsContext<string> context)
        {
            return base.Index(context);
        }
        public override ActionResult Create(ParamsContext<string> context)
        {
            if (context == null || context.ParentID.IsNullOrEmpty())
            {
                return base.Create(context);
            }
            var parent = Service.Get(context.ParentID);
            if (parent != null)
            {
                var navication = new NavigationEntity
                {
                    ParentId = parent.ID,
                    Url = parent.Url
                };
                return View(navication);
            }
            return base.Create(context);
        }
        public JsonResult GetPageTree(ParamsContext<string> context)
        {
            var navs = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<NavigationEntity>().Source(navs).ToNode(m => m.ID, m => m.Title, m => m.ParentId, "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
