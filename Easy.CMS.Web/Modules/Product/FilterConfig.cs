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
            Registry.Register<ProductController, ViewDataProductCategoryAttribute>(m => m.Index());
            Registry.Register<ProductController, ViewDataProductCategoryAttribute>(m => m.Create());
            Registry.Register<ProductController, ViewDataProductCategoryAttribute>(m => m.Create(null));
            Registry.Register<ProductController, ViewDataProductCategoryAttribute>(m => m.Edit(0));
            Registry.Register<ProductController, ViewDataProductCategoryAttribute>(m => m.Edit(null));
            Registry.Register<ProductController, ViewDataProductCategoryAttribute>(m => m.GetList());
        }

    }
}