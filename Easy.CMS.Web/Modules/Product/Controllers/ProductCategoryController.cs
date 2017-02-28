/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.Data;
using Easy.ViewPort.jsTree;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;
using Easy.Web.Controller;

namespace Easy.CMS.Product.Controllers
{
    [AdminTheme, DefaultAuthorize]
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
                int id;
                if (int.TryParse(parentId.AttemptedValue, out id))
                {
                    productCategory.ParentID = id;
                }
            }
            return View(productCategory);
        }

        public JsonResult GetProductCategoryTree()
        {
            var pages = Service.Get(new DataFilter());
            var node = new Tree<ProductCategory>().Source(pages).ToNode(m => m.ID.ToString(), m => m.Title, m => m.ParentID.ToString(), "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
    }
}
