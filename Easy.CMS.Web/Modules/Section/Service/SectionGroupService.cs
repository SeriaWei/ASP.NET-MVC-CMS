using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using System.IO;
using System.Xml;
using Microsoft.Practices.ServiceLocation;
using Easy.Reflection;

namespace Easy.CMS.Section.Service
{
    public class SectionGroupService : ServiceBase<SectionGroup>, ISectionGroupService
    {
        private IEnumerable<SectionContent> GenerateContentFromConfig(SectionGroup group)
        {
            string configFile = AppDomain.CurrentDomain.BaseDirectory + @"Modules\Section\Views\Thumbnail\{0}.xml".FormatWith(group.PartialView);
            List<SectionContent> contents = new List<SectionContent>();
            if (File.Exists(configFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile);
                var nodes = doc.SelectNodes("/required/item");
                foreach (XmlNode item in nodes)
                {
                    var attr = item.Attributes["type"];
                    if (attr != null && attr.Value.IsNotNullAndWhiteSpace())
                    {
                        try
                        {
                            var content = Activator.CreateInstance("Easy.CMS.Section", attr.Value).Unwrap();
                            var properties = item.SelectNodes("property");
                            foreach (XmlNode property in properties)
                            {
                                var name = property.Attributes["name"];
                                if (name != null && name.Value.IsNotNullAndWhiteSpace() && property.InnerText.IsNotNullAndWhiteSpace())
                                {
                                    ClassAction.SetObjPropertyValue(content, name.Value, property.InnerText);
                                }
                            }
                            contents.Add(content as SectionContent);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }
            }
            return contents;
        }
        public override void Add(SectionGroup item)
        {
            base.Add(item);
            if (item.SectionContents != null && item.SectionContents.Any())
            {
                var contentService = new SectionContentProviderService();
                item.SectionContents.Each(m =>
                {
                    m.SectionGroupId = item.ID;
                    m.SectionWidgetId = item.SectionWidgetId;
                    contentService.Add(m);
                });
            }
            var contents = GenerateContentFromConfig(item);
            if (contents != null && contents.Any())
            {
                ISectionContentProviderService contentService = ServiceLocator.Current.GetInstance<ISectionContentProviderService>();
                contents.Each(c =>
                {
                    c.SectionGroupId = item.ID;
                    c.SectionWidgetId = item.SectionWidgetId;
                    contentService.Add(c);
                });
            }
        }

        public override int Delete(params object[] primaryKeys)
        {
            var group = Get(primaryKeys);
            var contentService = new SectionContentProviderService();
            var contents = contentService.Get(new DataFilter().Where("SectionGroupId", OperatorType.Equal, group.ID));
            contents.Each(m =>
            {
                contentService.Delete(m.ID);
            });
            return base.Delete(primaryKeys);
        }
    }
}