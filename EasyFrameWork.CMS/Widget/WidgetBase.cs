using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int Position { get; set; }
        public string LayoutID { get; set; }
        public string PageID { get; set; }
        public string ZoneID { get; set; }
        public string PartialView { get; set; }
        public string AssemblyName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ViewModelTypeName { get; set; }
        public string FormView { get; set; }
        public string StyleClass { get; set; }
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
        public Type GetServiceType()
        {
            StaticCache cache = new StaticCache();
            return cache.Get("TypeCache_" + this.ServiceTypeName, m =>
            {
                Type type = null;
                AppDomain.CurrentDomain.GetAssemblies().Each(n => n.GetTypes().Each(t =>
                {
                    if (type == null && t.FullName == this.ServiceTypeName)
                    {
                        type = t;
                    }
                }));
                return type;
            });
        }
        public WidgetBase ToWidgetBase()
        {
            return new WidgetBase
            {
                AssemblyName = this.AssemblyName,
                CreateBy = this.CreateBy,
                CreatebyName = this.CreatebyName,
                CreateDate = this.CreateDate,
                Description = this.Description,
                ID = this.ID,
                LastUpdateBy = this.LastUpdateBy,
                LastUpdateByName = this.LastUpdateByName,
                LastUpdateDate = this.LastUpdateDate,
                LayoutID = this.LayoutID,
                PageID = this.PageID,
                PartialView = this.PartialView,
                Position = this.Position,
                ServiceTypeName = this.ServiceTypeName,
                Status = this.Status,
                Title = this.Title,
                ViewModelTypeName = this.ViewModelTypeName,
                WidgetName = this.WidgetName,
                ZoneID = this.ZoneID,
                FormView = this.FormView,
                StyleClass = this.StyleClass
            };
        }
    }
    class WidgetBaseMetaData : DataViewMetaData<WidgetBase>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_WidgetBase");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }


}
