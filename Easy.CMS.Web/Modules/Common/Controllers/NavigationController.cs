using Easy.CMS.Common.Models;
using Easy.CMS.Common.Service;
using Easy.Data;
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
    [AdminTheme, Authorize]
    public class NavigationController : BasicController<NavigationEntity, string, NavigationService>
    {
        public NavigationController()
            : base(new NavigationService())
        {

        }
        [NonAction]
        public override ActionResult Create()
        {
            return base.Create();
        }

        public ActionResult Create(string ParentID)
        {
            var navication = new NavigationEntity
            {
                ParentId = ParentID,
                DisplayOrder = Service.Get("ParentID", OperatorType.Equal, ParentID).Count() + 1
            };
            return View(navication);
        }
        public JsonResult GetPageTree()
        {
            var navs = Service.Get(new Data.DataFilter().OrderBy("DisplayOrder", OrderType.Ascending));
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
