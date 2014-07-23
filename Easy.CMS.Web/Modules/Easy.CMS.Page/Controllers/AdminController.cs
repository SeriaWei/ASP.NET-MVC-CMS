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
            var pages = new PageService().Get(new Data.DataFilter().Where("ParentId", Constant.DataEnumerate.OperatorType.Equal, id));
            return Json(new Easy.HTML.zTree.Tree<PageEntity>().Source(pages).Parent(m => m.ParentId).Value(m => m.PageId).Text(m => m.PageName).ToNode(true));
        }
    }
}
