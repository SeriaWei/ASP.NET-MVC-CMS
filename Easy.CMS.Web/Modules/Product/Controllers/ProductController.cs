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
using Easy.CMS.Product.Models;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme, Authorize]
    public class ProductController : BasicController<Models.Product, long, ProductService>
    {
        public ProductController()
            : base(new ProductService())
        {
        }
        [HttpPost]
        public override ActionResult Create(Models.Product entity)
        {
            var result = base.Create(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID ?? 0);
            }
            return result;
        }

        [HttpPost]
        public override ActionResult Edit(Models.Product entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID ?? 0);
            }
            return result;
        }
    }
}
