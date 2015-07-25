using Easy.Models;
using Easy.Modules.User.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web
{
    public class ApplicationContext : IApplicationContext
    {
        IUser _CurrentUser;
        public IUser CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (_CurrentUser == null)
                    {
                        IUserService userService = ServiceLocator.Current.GetInstance<IUserService>();
                        if (userService != null)
                        {
                            _CurrentUser = userService.GetGeneric<Easy.Modules.User.Models.UserEntity>(System.Web.HttpContext.Current.User.Identity.Name);
                        }
                    }
                }
                return _CurrentUser;
            }
        }


        public string VirtualPath
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    return System.Web.HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
