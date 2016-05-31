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
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme, Authorize]
    public class ProductController : BasicController<ProductEntity, long, IProductService>
    {
        public ProductController(IProductService service)
            : base(service)
        {
        }
        [HttpPost]
        public override ActionResult Create(ProductEntity entity)
        {
            var result = base.Create(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID ?? 0);
            }
            return result;
        }

        [HttpPost]
        public override ActionResult Edit(ProductEntity entity)
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
