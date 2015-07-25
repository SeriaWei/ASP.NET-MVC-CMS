using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.RSS
{
    public class RssEntity
    {
        public RssEntity()
        {
            Channels = new List<RssChannel>();
        }
        public List<RssChannel> Channels { get; set; }
    }
    public class RssChannel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public DateTime lastBuildDate { get; set; }
        public string generator { get; set; }
        public List<RssItem> Items { get; set; }
    }
    public class RssItem
    {
        public string title { get; set; }
        public string description { get; set; }
        public string encoded { get; set; }
        public string link { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public string guid { get; set; }
        public string comments { get; set; }
        public string source { get; set; }
        public DateTime pubDate { get; set; }
    }
}
