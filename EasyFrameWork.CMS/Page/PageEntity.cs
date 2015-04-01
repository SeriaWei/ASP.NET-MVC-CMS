using Easy.Constant;
using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Extend;

namespace Easy.Web.CMS.Page
{
    [DataConfigure(typeof(PageBaseMetaData))]
    public class PageEntity : EditorEntity
    {
        public string ID { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        string _PageUrl;
        public string PageUrl
        {
            get
            {
                if (!this.Url.IsNullOrEmpty())
                {
                    return this.Url.Substring(this.Url.LastIndexOf("/") + 1, this.Url.Length - this.Url.LastIndexOf("/") - 1);
                }
                return this.Url;
            }
            set { _PageUrl = value; }
        }
        public int DisplayOrder { get; set; }
        public string LayoutId { get; set; }
        public string PageName { get; set; }
        public string Content { get; set; }
        public string MetaKeyWorlds { get; set; }
        public string MetaDescription { get; set; }
        public string Script { get; set; }
        public string Style { get; set; }
        public bool IsHomePage { get; set; }

        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }
    }
    class PageBaseMetaData : DataViewMetaData<PageEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Page");
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.PageUrl).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.PageName).AsTextBox().Order(1).Required();
            ViewConfig(m => m.PageUrl).AsTextBox().Order(2).Required();
            ViewConfig(m => m.Url).AsTextBox().ReadOnly();
            ViewConfig(m => m.LayoutId).AsDropDownList().DataSource(ViewDataKeys.Layouts, SourceType.ViewData);
            ViewConfig(m => m.ParentId).AsHidden();
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Content).AsHidden();
            ViewConfig(m => m.PublishDate).AsTextBox().Hide();
            ViewConfig(m => m.IsPublish).AsTextBox().Hide();
        }
    }

}
