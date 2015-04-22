using Easy.CMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Common.ViewModels
{
    public class NavigationWidgetViewModel
    {
        public NavigationWidgetViewModel(IEnumerable<NavigationEntity> navigation, NavigationWidget widget)
        {
            Navigations = navigation;
            Widget = widget;
        }
        public IEnumerable<NavigationEntity> Navigations { get; set; }
        public NavigationWidget Widget { get; set; }
    }
}