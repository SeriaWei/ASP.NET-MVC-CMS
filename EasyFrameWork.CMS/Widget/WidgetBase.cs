using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Easy.Cache;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;
using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Widget
{
    [DataConfigure(typeof(WidgetBaseMetaData))]
    public class WidgetBase : EditorEntity
    {

        private static readonly Regex StyleRegex = new Regex("^style=\"(.+?)\"$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public string ID { get; set; }
        public string WidgetName { get; set; }
        public int? Position { get; set; }
        public string LayoutID { get; set; }
        public string PageID { get; set; }
        public string ZoneID { get; set; }
        public bool IsTemplate { get; set; }
        public string Thumbnail { get; set; }
        public bool IsSystem { get; set; }
        public string PartialView { get; set; }
        public string AssemblyName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ViewModelTypeName { get; set; }
        public string FormView { get; set; }
        public string StyleClass { get; set; }

        public IHtmlString StyleClassResult(bool design = false)
        {
            if (!design)
            {
                return new HtmlString(StyleRegex.IsMatch(StyleClass??"") ? StyleClass +" class=\"widget\"": "class=\"widget " + StyleClass + "\"");
            }
            return
                new HtmlString(StyleRegex.IsMatch(StyleClass ?? "")
                    ? StyleClass + " class=\"widget widget-design\""
                    : "class=\"widget widget-design " + StyleClass + "\"");
        }
        public WidgetPart ToWidgetPart()
        {
            return new WidgetPart
            {
                Widget = this,
                ViewModel = this
            };
        }
        public WidgetPart ToWidgetPart(object viewModel)
        {
            return new WidgetPart
            {
                Widget = this,
                ViewModel = viewModel
            };
        }
        public IWidgetPartDriver CreateServiceInstance()
        {
            return Activator.CreateInstance(this.AssemblyName, this.ServiceTypeName).Unwrap() as IWidgetPartDriver;
        }
        public WidgetBase CreateViewModelInstance()
        {
            return Activator.CreateInstance(this.AssemblyName, this.ViewModelTypeName).Unwrap() as WidgetBase;
        }
        public Type GetViewModelType()
        {
            StaticCache cache = new StaticCache();
            return cache.Get("TypeCache_" + this.ViewModelTypeName, m =>
            {
                Type type = null;
                AppDomain.CurrentDomain.GetAssemblies().Each(n => n.GetTypes().Each(t =>
                {
                    if (type == null && t.FullName == this.ViewModelTypeName)
                    {
                        type = t;
                    }
                }));
                return type;
            });
        }
    }
    class WidgetBaseMetaData : DataViewMetaData<WidgetBase>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_WidgetBase");
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.IsSystem).Update(false).Insert(false);
        }

        protected override void ViewConfigure()
        {

        }
    }


}
