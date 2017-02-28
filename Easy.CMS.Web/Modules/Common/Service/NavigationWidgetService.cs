/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Linq;
using System.Web;
using Easy.CMS.Common.Models;
using Easy.CMS.Common.ViewModels;
using Easy.Constant;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;

namespace Easy.CMS.Common.Service
{
    public class NavigationWidgetService : WidgetService<NavigationWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            var navs = ServiceLocator.Current.GetInstance<INavigationService>().Get(new DataFilter().OrderBy("DisplayOrder", OrderType.Ascending)).Where(m => m.Status == (int)RecordStatus.Active);
            string path = "~" + controllerContext.HttpContext.Request.Path.ToLower();
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