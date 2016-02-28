using Easy.CMS.Common.Models;
using Easy.CMS.Common.ViewModels;
using Easy.Data;
using Easy.Web.CMS.Widget;
using System.Linq;
using System.Web;
using Easy.Constant;
using Easy.Extend;

namespace Easy.CMS.Common.Service
{
    public class NavigationWidgetService : WidgetService<NavigationWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var navs = new NavigationService().Get(new DataFilter().OrderBy("DisplayOrder", OrderType.Ascending)).Where(m => m.Status == (int)RecordStatus.Active);
            string path = "~" + httpContext.Request.Path.ToLower();
            NavigationEntity current = null;
            int length = 0;
            foreach (var navigationEntity in navs)
            {
                if (navigationEntity.Url.IsNotNullAndWhiteSpace()
                    && path.StartsWith(navigationEntity.Url.ToLower())
                    && length < navigationEntity.Url.Length)
                {
                    current = navigationEntity;
                    length = navigationEntity.Url.Length;
                }
            }
            if (current != null)
            {
                current.IsCurrent = true;
            }
            return widget.ToWidgetPart(new NavigationWidgetViewModel(navs, widget as NavigationWidget));
        }
    }
}