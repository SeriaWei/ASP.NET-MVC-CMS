using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Models;
using Easy.MetaData;

namespace Easy.CMS.News.Models
{
    [DataConfigure(typeof(NewsEntityMeta))]
    public class NewsEntity : EditorEntity, IBasicEntity<long>
    {
        public long ID { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public int Counter { get; set; }
        public string Description { get; set; }
        public string NewsContent { get; set; }
        public int Status { get; set; }
    }
    class NewsEntityMeta : DataViewMetaData<NewsEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("News");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            
        }
    }

}