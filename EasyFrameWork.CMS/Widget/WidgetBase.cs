using System;
using System.Collections.Generic;
using Easy.Cache;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;
using Easy.Web.CMS.ExtendField;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Widget
{
    [DataConfigure(typeof(WidgetBaseMetaData)), Serializable]
    public class WidgetBase : EditorEntity, IExtendField
    {
        [NonSerialized]
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

        [NonSerialized] 
        private IWidgetPartDriver _partDriver;
        public IWidgetPartDriver CreateServiceInstance()
        {
            if (_partDriver != null) return _partDriver;
            StaticCache cache = new StaticCache();
            var type = cache.Get("WidgetPart_" + AssemblyName + ServiceTypeName, source =>
              {
                  _partDriver = _partDriver ??
                            (_partDriver = Activator.CreateInstance(AssemblyName, ServiceTypeName).Unwrap() as IWidgetPartDriver);
                  return _partDriver.GetType();
              });
            return _partDriver ?? (_partDriver = ServiceLocator.Current.GetInstance(type) as IWidgetPartDriver);
        }

        [NonSerialized] 
        private WidgetBase _widgetBase;
        public WidgetBase CreateViewModelInstance()
        {
            StaticCache cache = new StaticCache();
            var type = cache.Get("WidgetBase_" + AssemblyName + ViewModelTypeName, source =>
            {
                _widgetBase = _widgetBase ??
                   (_widgetBase = Activator.CreateInstance(AssemblyName, ViewModelTypeName).Unwrap() as WidgetBase);
                return _widgetBase.GetType();
            });
            return _widgetBase ?? (_widgetBase = ServiceLocator.Current.GetInstance(type) as WidgetBase);
        }
        public Type GetViewModelType()
        {
            if (KnownWidgetModel.ContainsKey(ViewModelTypeName))
            {
                return KnownWidgetModel[ViewModelTypeName];
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
