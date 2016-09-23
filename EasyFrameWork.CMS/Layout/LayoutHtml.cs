/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.ObjectModel;
using Easy.MetaData;
using Easy.Models;

namespace Easy.Web.CMS.Layout
{
    [DataConfigure(typeof(LayoutHtmlMetaData))]
    public class LayoutHtml : EditorEntity
    {

        public int LayoutHtmlId { get; set; }
        public string LayoutId { get; set; }
        public string Html { get; set; }

        public string NoStyleHtml
        {
            get { return CustomRegex.StyleRegex.Replace(Html, ""); }
        }
    }
    public class LayoutHtmlCollection : Collection<LayoutHtml>
    {

    }
    class LayoutHtmlMetaData : DataViewMetaData<LayoutHtml>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_LayoutHtml");
            DataConfig(m => m.LayoutHtmlId).AsIncreasePrimaryKey();
            DataConfig(m => m.Status).Ignore();
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.NoStyleHtml).Ignore();
        }

        protected override void ViewConfigure()
        {

        }
    }

}
