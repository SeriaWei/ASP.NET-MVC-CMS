/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using EasyZip;
using System.Web.Mvc;

namespace Easy.Web.CMS.Widget
{
    public interface IWidgetPartDriver
    {
        void AddWidget(WidgetBase widget);
        void DeleteWidget(string widgetId);
        void UpdateWidget(WidgetBase widget);
        void Publish(WidgetBase widget);
        WidgetBase GetWidget(WidgetBase widget);
        WidgetPart Display(WidgetBase widget, ControllerContext controllerContext);
        WidgetPackage PackWidget(WidgetBase widget);
        void InstallWidget(WidgetPackage pack);
    }

}
