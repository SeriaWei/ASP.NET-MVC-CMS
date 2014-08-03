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
        public override ActionResult Index()
        {
            return base.Index();
        }
        [AdminTheme]
        public JsonResult GetPageTree(string id)
        {
            id = id ?? string.Empty;
            var pages = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.ID, m => m.PageName, m => m.ParentId, "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
        [AdminTheme]
        public override ActionResult Create()
        {
            return base.Create();
        }
        [AdminTheme]
        public override ActionResult Create(PageEntity entity)
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
        public override ActionResult Edit(PageEntity entity)
        {
            return base.Edit(entity);
        }
        [EditWidget]
        public ActionResult Design()
        {
            return View();
        }
    }
}
