using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Statistics.Models
{
    [DataConfigure(typeof(StatisticsMeteData))]
    public class Statistics : EditorEntity
    {
        public int StatisticsId { get; set; }
        public string Host { get; set; }
        public string ContactName { get; set; }
        public string Tel { get; set; }
        public string IpAddress { get; set; }
    }

    class StatisticsMeteData : DataViewMetaData<Statistics>
    {

        protected override void DataConfigure()
        {
            DataTable("Statistics");
            DataConfig(m => m.StatisticsId).AsIncreasePrimaryKey();
            DataConfig(m => m.Title).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.StatisticsId).AsHidden();
            ViewConfig(m => m.Title).AsHidden();
        }
    }
}