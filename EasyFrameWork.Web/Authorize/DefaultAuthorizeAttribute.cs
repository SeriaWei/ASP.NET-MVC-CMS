/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Easy.Security;
using Easy.Web.HttpActionResult;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.Authorize
{
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
    {
        public DefaultAuthorizeAttribute()
        {

        }

        public DefaultAuthorizeAttribute(string permisstionKey)
        {
            PermissionKey = permisstionKey;
        }
        public string PermissionKey { get; set; }

        public void SetPermissionKey(string key)
        {
            PermissionKey = key;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                filterContext.Result = new HttpForbiddenResult();
            }

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            IPrincipal user = httpContext.User;

            var applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>();
            if (applicationContext.CurrentUser == null)
            {
                return false;
            }
            return user.Identity.IsAuthenticated && ServiceLocator.Current.GetInstance<IAuthorizer>()
                .Authorize(PermissionKey, applicationContext.CurrentUser);
        }
    }
}