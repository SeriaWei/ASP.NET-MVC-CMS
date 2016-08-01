using Easy.CMS.Product.ActionFilter;
using Easy.CMS.Product.Controllers;
using Easy.Web.Authorize;
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



            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewProduct));
            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageProduct));
            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageProduct));
            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.Edit(0), auth => auth.SetPermissionKey(PermissionKeys.ManageProduct));
            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.Edit(null), auth => auth.SetPermissionKey(PermissionKeys.ManageProduct));
            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.Delete(null), auth => auth.SetPermissionKey(PermissionKeys.ManageProduct));
            Registry.Register<ProductController, DefaultAuthorizeAttribute>(m => m.GetList(), auth => auth.SetPermissionKey(PermissionKeys.ViewProduct));

            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewProductCategory));
            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageProductCategory));
            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageProductCategory));
            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.Edit(0), auth => auth.SetPermissionKey(PermissionKeys.ManageProductCategory));
            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.Edit(null), auth => auth.SetPermissionKey(PermissionKeys.ManageProductCategory));
            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.Delete(null), auth => auth.SetPermissionKey(PermissionKeys.ManageProductCategory));
            Registry.Register<ProductCategoryController, DefaultAuthorizeAttribute>(m => m.GetList(), auth => auth.SetPermissionKey(PermissionKeys.ManageProductCategory));
        }

    }
}