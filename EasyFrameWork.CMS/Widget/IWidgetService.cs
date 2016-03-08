using System.Collections.Generic;
using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Widget
{
    public interface IWidgetService : IService<WidgetBase>
    {
        IEnumerable<WidgetBase> GetByLayoutId(string layoutId);
        IEnumerable<WidgetBase> GetByPageId(string pageId);
        IEnumerable<WidgetBase> GetAllByPageId(string pageId);
        WidgetBase GetWidget(string widgetId);
    }
}