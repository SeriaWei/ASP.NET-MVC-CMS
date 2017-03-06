/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Easy.RSS
{
    public class RssService
    {
        XmlNamespaceManager namespaceMgr;
        public RssEntity Get(string feed)
        {
            XmlTextReader reader = new XmlTextReader(feed);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            namespaceMgr = new XmlNamespaceManager(doc.NameTable);
            XmlNodeList channels = doc.SelectNodes("rss/channel");
            RssEntity entity = new RssEntity();

            foreach (XmlNode item in channels)
            {
                RssChannel channel = new RssChannel();
                channel.description = GetText(item, "description");
                channel.generator = GetText(item, "generator");
                channel.language = GetText(item, "language");
                string lastBuildDate = GetText(item, "lastBuildDate");
                if (!string.IsNullOrEmpty(lastBuildDate))
                {
                    channel.lastBuildDate = Convert.ToDateTime(lastBuildDate);
                }
                channel.link = GetText(item, "link");
                channel.title = GetText(item, "title");
                channel.Items = new List<RssItem>();
                XmlNodeList items = item.SelectNodes("item");
                if (items == null) continue;
                foreach (XmlNode rssItem in items)
                {
                    RssItem ritem = new RssItem();
                    foreach (XmlNode itemDetail in rssItem.ChildNodes)
                    {
                        Easy.Reflection.ClassAction.SetPropertyValue<RssItem>(ritem, itemDetail.LocalName, itemDetail.InnerText);
                    }
                    channel.Items.Add(ritem);
                }
                entity.Channels.Add(channel);
            }

            return entity;
        }
        private string GetText(XmlNode node, string nodeName)
        {
            XmlNode tempNode = node.SelectSingleNode(nodeName, namespaceMgr);
            if (tempNode != null)
                return tempNode.InnerText;
            return string.Empty;
        }
    }
}
