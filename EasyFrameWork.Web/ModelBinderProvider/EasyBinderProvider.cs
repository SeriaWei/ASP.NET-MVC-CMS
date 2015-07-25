using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.Web.ModelBinder
{
    public class EasyBinderProvider : IModelBinderProvider
    {

        public IModelBinder GetBinder(Type modelType)
        {
            return new EasyBinder();
        }
    }


}