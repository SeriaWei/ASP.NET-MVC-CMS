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
using Microsoft.Practices.ServiceLocation;
using Easy.IOC;
using Easy.Web.CMS.ExtendField;

namespace Easy.Web.CMS.Widget
{
    [DataConfigure(typeof(WidgetBaseMetaData))]
    public class WidgetBase : EditorEntity, IExtendField
    {
        public static Dictionary<string, Type> KnownWidgetModel;
        static WidgetBase()
        {
            KnownWidgetModel = new Dictionary<string, Type>();
        }
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

        public IWidgetPartDriver PartDriver { get; set; }
        public IWidgetPartDriver CreateServiceInstance()
        {
            if (PartDriver != null) return PartDriver;
            StaticCache cache = new StaticCache();
            var type = cache.Get("WidgetPart_" + this.AssemblyName + this.ServiceTypeName, source =>
              {
                  PartDriver = PartDriver ??
                            (PartDriver = Activator.CreateInstance(this.AssemblyName, this.ServiceTypeName).Unwrap() as IWidgetPartDriver);
                  return PartDriver.GetType();
              });
            return PartDriver ?? (PartDriver = ServiceLocator.Current.GetInstance(type) as IWidgetPartDriver);
        }

        private WidgetBase _widgetBase;
        public WidgetBase CreateViewModelInstance()
        {
            StaticCache cache = new StaticCache();
            var type = cache.Get("WidgetBase_" + this.AssemblyName + this.ViewModelTypeName, source =>
            {
                _widgetBase = _widgetBase ??
                   (_widgetBase = Activator.CreateInstance(this.AssemblyName, this.ViewModelTypeName).Unwrap() as WidgetBase);
                return _widgetBase.GetType();
            });
            return _widgetBase ?? (_widgetBase = ServiceLocator.Current.GetInstance(type) as WidgetBase);
        }
        public Type GetViewModelType()
        {
            if (KnownWidgetModel.ContainsKey(this.ViewModelTypeName))
            {
                return KnownWidgetModel[this.ViewModelTypeName];
            }
            return null;
        }

        public IEnumerable<ExtendFieldEntity> ExtendFields { get; set; }
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
            DataConfig(m => m.ExtendFields).SetReference<ExtendFieldEntity, IExtendFieldService>((widget, field) => field.OwnerModule == TargetType.Name && field.OwnerID == widget.ID);
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.StyleClass).AsTextBox().MaxLength(1000);
            ViewConfig(m => m.CustomClass).AsHidden().Ignore();
            ViewConfig(m => m.CustomStyle).AsHidden().Ignore();
            ViewConfig(m => m.ExtendFields).AsHidden().Ignore();
        }
    }


}
