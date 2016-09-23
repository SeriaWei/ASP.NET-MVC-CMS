/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.MetaData;
using Easy.Models;
using Easy.Web.CMS.Widget;

namespace Easy.Web.CMS.WidgetTemplate
{
    [DataConfigure(typeof(WidgetTemplateMetaData))]
    public class WidgetTemplateEntity : EditorEntity
    {
        public long? ID { get; set; }
        public string GroupName { get; set; }

        public string PartialView { get; set; }
        public string AssemblyName { get; set; }
        public string ServiceTypeName { get; set; }
        public string Thumbnail { get; set; }
        public string ViewModelTypeName { get; set; }
        public int? Order { get; set; }

        public string FormView { get; set; }

        private void CopyToWidget(WidgetBase widget)
        {
            widget.AssemblyName = AssemblyName;
            widget.Description = Description;
            widget.PartialView = PartialView;
            widget.ViewModelTypeName = ViewModelTypeName;
            widget.WidgetName = Title;
            widget.ServiceTypeName = ServiceTypeName;
        }
        public WidgetBase CreateWidgetInstance()
        {
            var widget = Activator.CreateInstance(AssemblyName, ViewModelTypeName).Unwrap() as WidgetBase;
            CopyToWidget(widget);
            return widget;
        }
    }
    class WidgetTemplateMetaData : DataViewMetaData<WidgetTemplateEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_WidgetTemplate");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }

}
