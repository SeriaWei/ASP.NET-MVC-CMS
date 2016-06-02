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
        private string _customClass;

        public string CustomClass
        {
            get
            {
                if (_customClass != null)
                {
                    return _customClass;
                }
                InitStyleClass();
                return _customClass;
            }
        }
        private string _customStyle;
        public string CustomStyle
        {
            get
            {
                if (_customStyle != null)
                {
                    return _customStyle;
                }
                InitStyleClass();
                return _customStyle;
            }
        }
        private void InitStyleClass()
        {
            if (StyleClass.IsNullOrWhiteSpace())
            {
                _customClass = _customStyle = string.Empty;
            }
            else
            {
                _customClass = CustomRegex.StyleRegex.Replace(StyleClass, evaluator =>
                {
                    _customStyle = evaluator.Groups[1].Value;
                    return string.Empty;
                });
            }
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
            DataConfig(m => m.CustomClass).Ignore();
            DataConfig(m => m.CustomStyle).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.StyleClass).AsTextBox().MaxLength(1000);
            ViewConfig(m => m.CustomClass).AsHidden().Ignore();
            ViewConfig(m => m.CustomStyle).AsHidden().Ignore();
        }
    }


}
