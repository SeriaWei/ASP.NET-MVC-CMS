using System.Web;
using EasyZip;

namespace Easy.Web.CMS.Widget
{
    public interface IWidgetPartDriver
    {
        void AddWidget(WidgetBase widget);
        void DeleteWidget(string widgetId);
        void UpdateWidget(WidgetBase widget);
        void Publish(WidgetBase widget);
        WidgetBase GetWidget(WidgetBase widget);
        WidgetPart Display(WidgetBase widget, HttpContextBase httpContext);
        ZipFile PackWidget(WidgetBase widget);
        WidgetBase UnPackWidget(ZipFileInfoCollection pack);
    }

}
