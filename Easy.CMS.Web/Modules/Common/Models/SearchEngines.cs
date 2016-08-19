using System.Collections.Generic;
using System.Xml.Serialization;

namespace Easy.CMS.Common.Models
{
    [XmlRoot("SearchEngineList")]
    public class SearchEngines
    {
        [XmlElement("SearchEngine")]
        public List<SearchEngineItem> SearchEngine { get; set; }
    }

    public class SearchEngineItem
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Match")]
        public string Match { get; set; }
        [XmlElement("UserAgent")]
        public string UserAgent { get; set; }
    }
}