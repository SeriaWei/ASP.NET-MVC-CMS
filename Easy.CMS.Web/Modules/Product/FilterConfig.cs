using Easy.CMS.Product.ActionFilter;
using Easy.CMS.Product.Controllers;
using Easy.Web.Filter;
namespace Easy.CMS.Product
{
    public class FilterConfig : ConfigureFilterBase
    {
        public FilterConfig(IFilterRegister register)
            : base(register)
        {
        }
        public override void Configure()
        {
            Registry.Register<ProductController, ViewData_ProductCategoryAttribute>(m => m.Index());
            Registry.Register<ProductController, ViewData_ProductCategoryAttribute>(m => m.Create());
            Registry.Register<ProductController, ViewData_ProductCategoryAttribute>(m => m.Create(null));
            Registry.Register<ProductController, ViewData_ProductCategoryAttribute>(m => m.Edit(0));
            Registry.Register<ProductController, ViewData_ProductCategoryAttribute>(m => m.Edit(null));
            Registry.Register<ProductController, ViewData_ProductCategoryAttribute>(m => m.GetList());
        }

    }
}