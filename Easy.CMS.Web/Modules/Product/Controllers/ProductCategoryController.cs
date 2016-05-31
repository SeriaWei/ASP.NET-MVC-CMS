using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Product.Models;
using Easy.CMS.Product.Service;
using Easy.Web.Controller;
using Easy.Web.Attribute;
using Easy.ViewPort.jsTree;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme, Authorize]
    public class ProductCategoryController : BasicController<ProductCategory, long, IProductCategoryService>
    {
        public ProductCategoryController(IProductCategoryService service)
            : base(service)
        {
        }

        public override ActionResult Create()
        {
            var parentId = ValueProvider.GetValue("ParentID");
            var productCategory = new ProductCategory { ParentID = 0 };
            if (parentId != null)
            {
                long id;
                if (long.TryParse(parentId.AttemptedValue, out id))
                {
                    productCategory.ParentID = id;
                }
            }
            return View(productCategory);
        }

        public JsonResult GetProductCategoryTree()
        {
            var pages = Service.Get(new Data.DataFilter());
            var node = new Tree<ProductCategory>().Source(pages).ToNode(m => m.ID.ToString(), m => m.Title, m => m.ParentID.ToString(), "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
