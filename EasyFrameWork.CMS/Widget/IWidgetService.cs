/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.IO;
using System.Web;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Page;
using System.Web.Mvc;

namespace Easy.Web.CMS.Widget
{
    public interface IWidgetService : IService<WidgetBase>
    {
        IEnumerable<WidgetBase> GetByLayoutId(string layoutId);
        IEnumerable<WidgetBase> GetByPageId(string pageId);
        IEnumerable<WidgetBase> GetAllByPageId(string pageId);
        IEnumerable<WidgetBase> GetAllByPage(PageEntity page);
        WidgetPart ApplyTemplate(WidgetBase widget, ControllerContext httpContext);
        MemoryStream PackWidget(string widgetId);
        WidgetBase InstallPackWidget(Stream stream);
    }
}