using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Widget;
using EasyZip;
using Microsoft.Practices.ServiceLocation;
using Easy.Web.CMS;
using System.IO;
using Newtonsoft.Json;
using Easy.CMS.Section.ContentJsonConvert;
using System.Text;

namespace Easy.CMS.Section.Service
{
    public class SectionWidgetService : WidgetService<SectionWidget>, ISectionWidgetService
    {
        private readonly ISectionGroupService _sectionGroupService;
        private readonly ISectionContentProviderService _sectionContentProviderService;

        public SectionWidgetService()
        {
            _sectionGroupService = ServiceLocator.Current.GetInstance<ISectionGroupService>();
            _sectionContentProviderService = ServiceLocator.Current.GetInstance<ISectionContentProviderService>();
        }
        public SectionWidgetService(ISectionGroupService sectionGroupService, ISectionContentProviderService sectionContentProviderService)
        {
            _sectionGroupService = sectionGroupService;
            _sectionContentProviderService = sectionContentProviderService;
        }

        public override WidgetBase GetWidget(WidgetBase widget)
        {
            SectionWidget sectionWidget = base.GetWidget(widget) as SectionWidget;

            return InitSectionWidget(sectionWidget);
        }

        public override SectionWidget Get(params object[] primaryKeys)
        {
            SectionWidget widget = base.Get(primaryKeys);
            widget = InitSectionWidget(widget);
            return widget;
        }

        private SectionWidget InitSectionWidget(SectionWidget widget)
        {
            widget.Groups = _sectionGroupService.Get("SectionWidgetId", OperatorType.Equal, widget.ID);
            var contents = _sectionContentProviderService.Get("SectionWidgetId", OperatorType.Equal, widget.ID).ToList();
            for (int i = 0; i < contents.Count; i++)
            {
                contents[i] = _sectionContentProviderService.FillContent(contents[i]);
            }
            widget.Groups.Each(m =>
            {
                m.SectionContents = contents.Where(n => n.SectionGroupId == m.ID).ToList();
            });
            return widget;
        }

        public override void DeleteWidget(string widgetId)
        {
            Get(widgetId).Groups.Each(m =>
            {
                _sectionGroupService.Delete(m.ID);
            });
            base.DeleteWidget(widgetId);
        }

        public override int Delete(params object[] primaryKeys)
        {
            Get(primaryKeys).Groups.Each(m =>
            {
                _sectionGroupService.Delete(m.ID);
            });
            return base.Delete(primaryKeys);
        }

        public override void Add(SectionWidget item)
        {
            base.Add(item);
            if (item.Groups != null && item.Groups.Any())
            {
                item.Groups.Each(m =>
                {
                    m.SectionWidgetId = item.ID;
                    _sectionGroupService.Add(m);
                });
            }
        }

        public override ZipFile PackWidget(WidgetBase widget)
        {
            var sectionWidget = GetWidget(widget) as SectionWidget;
            var zipFile = base.PackWidget(widget);
            var rootPath = (ApplicationContext as CMSApplicationContext).MapPath("~/");
            var files = new[]
               {
                "~/Modules/Section/Views/{0}.cshtml",
                "~/Modules/Section/Views/Thumbnail/{0}.png",
                "~/Modules/Section/Views/Thumbnail/{0}.xml",
                "~/Modules/Section/Views/Thumbnail/{0}.json"
            };
            sectionWidget.Groups.Each(g =>
            {
                var template = ServiceLocator.Current.GetInstance<ISectionTemplateService>().Get(g.PartialView);
                string infoFile = (ApplicationContext as CMSApplicationContext).MapPath("~/Modules/Section/Views/Thumbnail/{0}.json".FormatWith(template.TemplateName));
                using (var writer = System.IO.File.CreateText(infoFile))
                {
                    writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(template));
                }
                zipFile.AddFile(new System.IO.FileInfo(infoFile), Path.GetDirectoryName(infoFile.Replace(rootPath, @"\")));

                files.Each(f =>
                {
                    string file = (ApplicationContext as CMSApplicationContext).MapPath(f.FormatWith(template.TemplateName));
                    if (File.Exists(file))
                    {
                        zipFile.AddFile(new FileInfo(file), Path.GetDirectoryName(file.Replace(rootPath, @"\")));
                    }
                });

            });

            return zipFile;
        }
        public override WidgetBase UnPackWidget(ZipFileInfoCollection pack)
        {
            SectionWidget widget = null;
            foreach (ZipFileInfo item in pack)
            {
                if (item.RelativePath.EndsWith("-widget.json"))
                {
                    widget = JsonConvert.DeserializeObject<SectionWidget>(Encoding.UTF8.GetString(item.FileBytes), new SectionContentJsonConverter());
                    continue;
                }

                using (var fs = File.Create((ApplicationContext as CMSApplicationContext).MapPath("~" + item.RelativePath)))
                {
                    fs.Write(item.FileBytes, 0, item.FileBytes.Length);
                }

                if (item.RelativePath.EndsWith(".json"))
                {
                    var template = JsonConvert.DeserializeObject<SectionTemplate>(Encoding.UTF8.GetString(item.FileBytes));
                    var sectionTemplateService =
                        ServiceLocator.Current.GetInstance<ISectionTemplateService>();
                    if (sectionTemplateService.Count(m => m.TemplateName == template.TemplateName) > 0)
                    {
                        sectionTemplateService.Update(template);
                    }
                    else
                    {
                        sectionTemplateService.Add(template);
                    }
                }

            }
            return widget;
        }
    }
}