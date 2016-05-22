using System.Collections.Generic;
using System.Web;
using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Widget
{
    public interface IWidgetService : IService<WidgetBase>
    {
        IEnumerable<WidgetBase> GetByLayoutId(string layoutId);
        IEnumerable<WidgetBase> GetByPageId(string pageId);
        IEnumerable<WidgetBase> GetAllByPageId(string pageId);
        IEnumerable<WidgetBase> GetAllByPage(Page.PageEntity page);
        WidgetPart ApplyTemplate(WidgetBase widget, HttpContextBase httpContext);
    }
}