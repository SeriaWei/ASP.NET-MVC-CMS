using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.ViewModels
{
    public class DesignWidgetViewModel:WidgetPart
    {
        public DesignWidgetViewModel(WidgetPart widgetPart, string pageId)
        {
            PageID = pageId;
            ViewModel = widgetPart.ViewModel;
            Widget = widgetPart.Widget;
        }
        public string PageID { get; set; }
    }
}