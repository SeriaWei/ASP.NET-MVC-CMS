using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Article.Service;
using Easy.CMS.Article.Models;
using Easy.Web.Attribute;
using Easy.Web.CMS.Article.Models;
using Easy.Web.CMS.Article.Service;
using Easy.Web.Authorize;

namespace Easy.CMS.Article.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class ArticleTypeController : BasicController<ArticleType, long, IArticleTypeService>
    {
        public ArticleTypeController(IArticleTypeService service)
            : base(service)
        {
        }

        public override ActionResult Create()
        {
            var parentId = ValueProvider.GetValue("ParentID");
            var articleType = new ArticleType { ParentID = 0 };
            if (parentId != null)
            {
                long id;
                if (long.TryParse(parentId.AttemptedValue, out id))
                {
                    articleType.ParentID = id;
                }
            }
            return View(articleType);
        }

        public JsonResult GetArticleTypeTree()
        {
            var allNodes = Service.Get();
            var node = new ViewPort.jsTree.Tree<ArticleType>().Source(allNodes).ToNode(m => m.ID.ToString(), m => m.Title, m => m.ParentID.ToString(), "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
