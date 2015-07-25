using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Article.Service;
using Easy.CMS.Article.Models;
using Easy.Web.Attribute;

namespace Easy.CMS.Article.Controllers
{
    [AdminTheme]
    public class ArticleTypeController : BasicController<ArticleType, ArticleTypeService>
    {
        public ArticleTypeController() : base(new ArticleTypeService())
        {
        }

        public JsonResult GetArticleTypeTree()
        {
            var pages = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<ArticleType>().Source(pages).ToNode(m => m.ID.ToString(), m => m.Title, m => m.ParentID.ToString(), "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
