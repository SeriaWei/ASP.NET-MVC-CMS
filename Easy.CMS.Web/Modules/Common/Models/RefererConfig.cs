using System.Collections.Generic;
using System.Xml.Serialization;

namespace Easy.CMS.Common.Models
{
    [XmlRoot("RefererList")]
    public class RefererConfig
    {
        [XmlElement("Referer")]
        public List<RefererConfigItem> RefererConfigs { get; set; }
    }

    public class RefererConfigItem
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Host")]
        public string Host { get; set; }
        [XmlElement("KeyWordsQuery")]
        public string KeyWordsQuery { get; set; }
    }
}