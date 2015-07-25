using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Product.Models;
using Easy.CMS.Product.Service;
using Easy.Web.Controller;
using Easy.Web.Attribute;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme]
    public class ProductCategoryController : BasicController<ProductCategory, ProductCategoryService>
    {
        public ProductCategoryController() : base(new ProductCategoryService())
        {
        }

        public JsonResult GetProductCategoryTree()
        {
            var pages = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<ProductCategory>().Source(pages).ToNode(m => m.ID.ToString(), m => m.Title, m => m.ParentID.ToString(), "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
