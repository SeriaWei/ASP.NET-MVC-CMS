/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Linq;
using System.Web.Mvc;
using Easy.CMS.Common.Models;
using Easy.CMS.Common.Service;
using Easy.Data;
using Easy.ViewPort.jsTree;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.Controller;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class NavigationController : BasicController<NavigationEntity, string, INavigationService>
    {
        public NavigationController(INavigationService service)
            : base(service)
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
        public JsonResult GetNavTree()
        {
            var navs = Service.Get(new DataFilter().OrderBy("DisplayOrder", OrderType.Ascending));
            var node = new Tree<NavigationEntity>().Source(navs).ToNode(m => m.ID, m => m.Title, m => m.ParentId, "#");
            return Json(node, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MoveNav(string id, string parentId, int position, int oldPosition)
        {
            Service.Move(id, parentId, position, oldPosition);
            return Json(true);
        }
    }
}
