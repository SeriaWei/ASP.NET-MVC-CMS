using System.Web.Mvc;
using Easy.Extend;
using Easy.Web.CMS.Widget;

namespace Easy.Web.CMS.ModelBinder
{
    public class WidgetBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = base.BindModel(controllerContext, bindingContext);
            var widgetBase = model as WidgetBase;
            if (!widgetBase.ViewModelTypeName.IsNullOrEmpty())
            {
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => widgetBase.CreateViewModelInstance(), widgetBase.GetViewModelType());
                model = base.BindModel(controllerContext, bindingContext);
            }
            return model;
        }
    }
}
