using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.Service;
using Easy.Web.Attribute;
using Easy.CMS.Article.ActionFilter;

namespace Easy.CMS.Article.Controllers
{
    [AdminTheme]
    public class ArticleController : BasicController<ArticleEntity, ArticleService>
    {
        [ViewData_ArticleType]
        public override ActionResult Create(ParamsContext context)
        {
            return base.Create(context);
        }
        [HttpPost, ViewData_ArticleType]
        public override ActionResult Create(ArticleEntity entity)
        {
            return base.Create(entity);
        }
        [ViewData_ArticleType]
        public override ActionResult Edit(ParamsContext context)
        {
            return base.Edit(context);
        }
        [HttpPost, ViewData_ArticleType]
        public override ActionResult Edit(ArticleEntity entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == Constant.ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
        [ViewData_ArticleType]
        public override JsonResult GetList()
        {
            return base.GetList();
        }
    }
}
