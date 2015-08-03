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

namespace Easy.CMS.Article.Controllers
{
    [AdminTheme, Authorize]
    public class ArticleController : BasicController<ArticleEntity, long, ArticleService>
    {
        public ArticleController()
            : base(new ArticleService())
        {
        }

        [ViewData_ArticleType]
        public override ActionResult Index()
        {
            return base.Index();
        }

        [ViewData_ArticleType]
        public override ActionResult Create()
        {
            return base.Create();
        }
        [HttpPost, ViewData_ArticleType]
        public override ActionResult Create(ArticleEntity entity)
        {
            return base.Create(entity);
        }
        [ViewData_ArticleType]
        public override ActionResult Edit(long Id)
        {
            return base.Edit(Id);
        }
        [HttpPost, ViewData_ArticleType]
        public override ActionResult Edit(ArticleEntity entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
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
