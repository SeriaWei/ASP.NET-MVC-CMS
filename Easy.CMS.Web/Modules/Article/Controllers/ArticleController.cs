using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Article.ActionFilter;
using Easy.Constant;
using Easy.Web.Controller;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.Service;
using Easy.Web.Attribute;
using Easy.Web.CMS.Article.Models;
using Easy.Web.CMS.Article.Service;
using Easy.Web.Authorize;

namespace Easy.CMS.Article.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class ArticleController : BasicController<ArticleEntity, long, IArticleService>
    {
        public ArticleController(IArticleService service)
            : base(service)
        {
        }
        [HttpPost]
        public override ActionResult Create(ArticleEntity entity)
        {
            var result = base.Create(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
        [HttpPost]
        public override ActionResult Edit(ArticleEntity entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
      
    }
}
