/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.Constant;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;
using Easy.Web.Controller;
using Easy.Extend;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme, DefaultAuthorize]
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
                Service.Publish(entity.ID);
            }
            return result;
        }

        [HttpPost]
        public override ActionResult Edit(ProductEntity entity)
        {
            var result = base.Edit(entity);
            if (entity.ActionType == ActionType.Publish)
            {
                Service.Publish(entity.ID);
            }
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (returnUrl.IsNotNullAndWhiteSpace())
            {
                return Redirect(returnUrl);
            }
            return result;
        }
    }
}
