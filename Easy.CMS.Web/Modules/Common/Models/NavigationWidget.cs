/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.MetaData;
using Easy.Web.CMS;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using Easy.CMS.Common.Service;
using Easy.Extend;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(NavigationWidgetMetaData)), Serializable]
    public class NavigationWidget : WidgetBase
    {
        public string Logo { get; set; }
        public string CustomerClass { get; set; }
        public string AlignClass { get; set; }
        public bool IsTopFix { get; set; }
        public string RootID { get; set; }
    }
    class NavigationWidgetMetaData : WidgetMetaData<NavigationWidget>
    {
        protected override void DataConfigure()
        {
            base.DataConfigure();
            DataConfig(m => m.Title).Ignore(false);
        }
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.CustomerClass).AsDropDownList().DataSource(() =>
            {
                return new Dictionary<string, string>
                {
                     {"container","居中"},
                     {"container-fluid","自适应"}
                };
            }).Order(NextOrder());
            ViewConfig(m => m.AlignClass).AsDropDownList().DataSource(() =>
            {
                return new Dictionary<string, string>
                {
                     {"navbar-left","左对齐"},
                     {"navbar-right","右对齐"}
                };
            }).Order(NextOrder());
            ViewConfig(m => m.IsTopFix).AsHidden();
            ViewConfig(m => m.Logo).AsTextBox().Order(NextOrder()).AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.RootID).AsDropDownList().Order(NextOrder()).AddClass("select").AddProperty("data-url", "/admin/Navigation/Select").DataSource(() =>
            {
                Dictionary<string, string> navigations = new Dictionary<string, string>();
                navigations.Add("root", "导航");
                ServiceLocator.Current.GetInstance<INavigationService>().Get().Each(navigation => {
                    navigations.Add(navigation.ID, navigation.Title);
                });
                return navigations;
            });
        }
    }

}