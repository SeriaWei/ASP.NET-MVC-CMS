using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Models;
using Easy.MetaData;

namespace Easy.Web.CMS.Media
{
    [DataConfigure(typeof(MediaEntityMetaData))]
    public class MediaEntity : EditorEntity
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public int MediaType { get; set; }
        public string Url { get; set; }

        public string MediaTypeImage
        {
            get { return ((CMS.MediaType)MediaType).ToString(); }
        }
    }

    class MediaEntityMetaData : DataViewMetaData<MediaEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Media");
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.MediaTypeImage).Ignore();
        }

        protected override void ViewConfigure()
        {

        }
    }
}
