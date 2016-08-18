using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(PageViewMetaData))]
    public class PageView : EditorEntity
    {
        public int ID { get; set; }
        public string PageUrl { get; set; }
        public string PageTitle { get; set; }
        public string IPAddress { get; set; }
        public string SessionID { get; set; }
        public string UserAgent { get; set; }
        public int Sum { get; set; }
    }

    class PageViewMetaData : DataViewMetaData<PageView>
    {

        protected override void DataConfigure()
        {
            DataTable("PageView");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
            DataConfig(m => m.Sum).Ignore();
        }

        protected override void ViewConfigure()
        {
           
        }
    }
}