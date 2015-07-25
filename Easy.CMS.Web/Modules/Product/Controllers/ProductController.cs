using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Product.ActionFilter;
using Easy.CMS.Product.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using Easy.Constant;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme]
    public class ProductController : BasicController<Models.Product, ProductService>
    {
        public ProductController() : base(new ProductService())
        {
        }

        [ViewData_ProductCategory]
        public override ActionResult Index(ParamsContext context)
        {
            return base.Index(context);
        }

        [ViewData_ProductCategory]
        public override ActionResult Create(ParamsContext context)
        {
            return base.Create(context);
        }
        [HttpPost, ViewData_ProductCategory]
        public override ActionResult Create(Models.Product entity)
        {
            return base.Create(entity);
        }
        [ViewData_ProductCategory]
        public override ActionResult Edit(ParamsContext context)
        {
            return base.Edit(context);
        }
        [HttpPost, ViewData_ProductCategory]
        public override ActionResult Edit(Models.Product entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            return result;
        }
        [HttpPost, ViewData_ProductCategory]
        public override JsonResult GetList()
        {
            return base.GetList();
        }
    }
}
