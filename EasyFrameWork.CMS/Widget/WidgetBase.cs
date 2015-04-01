using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return Loader.CreateInstance<IWidgetPartDriver>(this.AssemblyName, this.ServiceTypeName);
        }
        public WidgetBase CreateViewModelInstance()
        {
            return Loader.CreateInstance<WidgetBase>(this.AssemblyName, this.ViewModelTypeName);
        }
        public Type GetViewModelType()
        {
            return Loader.GetType(this.ViewModelTypeName);
        }
        public Type GetServiceType()
        {
            return Loader.GetType(this.ServiceTypeName);
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
                ZoneID = this.ZoneID
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
