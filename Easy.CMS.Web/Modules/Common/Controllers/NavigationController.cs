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
    public class NavigationController : BasicController<NavigationEntity, NavigationService>
    {
        public override ActionResult Index(ParamsContext context)
        {
            return base.Index(context);
        }
        public override ActionResult Create(ParamsContext context)
        {
            var navication = new NavigationEntity
            {
                ParentId = context.ParentID,
                DisplayOrder = Service.Get("ParentID", Constant.OperatorType.Equal, context.ParentID).Count() + 1
            };
            return View(navication);
        }
        public JsonResult GetPageTree(ParamsContext context)
        {
            var navs = Service.Get(new Data.DataFilter().OrderBy("DisplayOrder", Constant.OrderType.Ascending));
            var node = new Easy.HTML.jsTree.Tree<NavigationEntity>().Source(navs).ToNode(m => m.ID, m => m.Title, m => m.ParentId, "#");
            return Json(node, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MovePage(string id, string parentId, int position, int oldPosition)
        {
            Service.Move(id, parentId, position, oldPosition);
            return Json(true);
        }
    }
}
