using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;
using Easy.Web.Controller;

namespace Easy.CMS.Page.Controllers
{
    [Admin]
    public class AdminController : BasicController<PageEntity, string, PageService>
    {
        public JsonResult GetPageTree(string id)
        {
            id = id ?? string.Empty;
            var pages = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.ID, m => m.PageName, m => m.ParentId, "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
