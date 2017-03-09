/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.CMS.Message.Controllers;
using Easy.Web.Authorize;
using Easy.Web.Filter;

namespace Easy.CMS.Message
{
    public class FilterConfig : ConfigureFilterBase
    {
        public FilterConfig(IFilterRegister register)
            : base(register)
        {
        }
        public override void Configure()
        {

            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewMessage));
            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageMessage));
            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageMessage));
            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.Edit(0), auth => auth.SetPermissionKey(PermissionKeys.ManageMessage));
            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.Edit(null), auth => auth.SetPermissionKey(PermissionKeys.ManageMessage));
            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.Delete(null), auth => auth.SetPermissionKey(PermissionKeys.ManageMessage));
            Registry.Register<MessageController, DefaultAuthorizeAttribute>(m => m.GetList(), auth => auth.SetPermissionKey(PermissionKeys.ViewMessage));
        }

    }
}