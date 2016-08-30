using Easy.MetaData;
using Easy.Models;

namespace Easy.Web.CMS.DataArchived
{
    [DataConfigure(typeof(DataArchivedMetaData))]
    public class DataArchived : EditorEntity
    {
        public string ID { get; set; }
        public string Data { get; set; }
    }

    class DataArchivedMetaData : DataViewMetaData<DataArchived>
    {

        protected override void DataConfigure()
        {
            DataTable("DataArchived");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            
        }
    }
}