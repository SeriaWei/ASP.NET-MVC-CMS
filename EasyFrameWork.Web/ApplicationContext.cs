/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Models;
using Easy.Modules.User.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using System.Web;
using System.Collections;

namespace Easy.Web
{
    public class ApplicationContext : IApplicationContext
    {
        IUser _CurrentUser;
        public IUser CurrentUser
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (_CurrentUser == null)
                    {
                        IUserService userService = ServiceLocator.Current.GetInstance<IUserService>();
                        if (userService != null)
                        {
                            _CurrentUser = userService.Get(HttpContext.Current.User.Identity.Name);
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
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
