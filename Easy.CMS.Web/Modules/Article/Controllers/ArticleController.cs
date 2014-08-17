using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.Service;
using Easy.Web.Attribute;

namespace Easy.CMS.Article.Controllers
{
    [AdminTheme]
    public class ArticleController : BasicController<ArticleEntity, long, ArticleService>
    {
        public override ActionResult Edit(ArticleEntity entity, Constant.ActionType? actionType)
        {
            var result = base.Edit(entity, actionType);
            if (entity.IsPublish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
    }
}
