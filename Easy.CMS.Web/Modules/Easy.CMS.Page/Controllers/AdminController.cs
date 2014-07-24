using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;

namespace Easy.CMS.Page.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string pageId)
        {
            return View("Form");
        }

        public JsonResult GetPageTree(string id)
        {
            id = id ?? string.Empty;
            var pages = new PageService().Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.PageId, m => m.PageName, m => m.ParentId, "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
