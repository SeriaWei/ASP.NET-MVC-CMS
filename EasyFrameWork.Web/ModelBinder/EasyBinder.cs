using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.ModelBinder
{
    public class EasyBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType.IsInterface || bindingContext.ModelType.IsAbstract)
            {
                return Easy.Reflection.ClassAction.GetModel(ServiceLocator.Current.GetInstance(bindingContext.ModelType).GetType(), controllerContext.RequestContext.HttpContext.Request.Form);
            }
            else
            {
                DefaultModelBinder binder = new DefaultModelBinder();
                return binder.BindModel(controllerContext, bindingContext);
            }
        }
    }

}