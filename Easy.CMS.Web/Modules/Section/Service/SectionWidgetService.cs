/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Easy.CMS.Section.ContentJsonConvert;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS;
using Easy.Web.CMS.Widget;
using EasyZip;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            return InitSectionWidget(widget);
        }

        private SectionWidget InitSectionWidget(SectionWidget widget)
        {
            widget.Groups = _sectionGroupService.Get("SectionWidgetId", OperatorType.Equal, widget.ID);
            var contents = _sectionContentProviderService.Get("SectionWidgetId", OperatorType.Equal, widget.ID);
            List<SectionContent> filled = new List<SectionContent>();
            contents.AsParallel().Each(content =>
            {
                filled.Add(_sectionContentProviderService.FillContent(content));
            });

            widget.Groups.Each(m =>
            {
                m.SectionContents = filled.Where(n => n.SectionGroupId == m.ID).ToList();
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

        public override WidgetPackage PackWidget(WidgetBase widget)
        {
            var package = base.PackWidget(widget);
            var sectionWidget = package.Widget as SectionWidget;

            var rootPath = (ApplicationContext as CMSApplicationContext).MapPath("~/");
            var files = new[]
               {
                "~/Modules/Section/Views/{0}.cshtml",
                "~/Modules/Section/Views/Thumbnail/{0}.png",
                "~/Modules/Section/Views/Thumbnail/{0}.xml"
            };
            sectionWidget.Groups.Each(g =>
            {
                sectionWidget.Template = ServiceLocator.Current.GetInstance<ISectionTemplateService>().Get(g.PartialView);
                files.Each(f =>
                {
                    string file = (ApplicationContext as CMSApplicationContext).MapPath(f.FormatWith(sectionWidget.Template.TemplateName));
                    if (File.Exists(file))
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        package.Files.Add(new Web.CMS.PackageManger.FileInfo { FileName = fileInfo.Name, FilePath = fileInfo.FullName.Replace(rootPath, "~/"), Content = File.ReadAllBytes(file) });
                    }
                });

            });
            return package;
        }
        public override void InstallWidget(WidgetPackage pack)
        {
            pack.Widget = null;
            base.InstallWidget(pack);
            var widget = JsonConvert.DeserializeObject<SectionWidget>(JObject.Parse(pack.Content.ToString()).GetValue("Widget").ToString(), new SectionContentJsonConverter());
            var sectionTemplateService = ServiceLocator.Current.GetInstance<ISectionTemplateService>();
            if (sectionTemplateService.Count(new DataFilter().Where("TemplateName", OperatorType.Equal, widget.Template.TemplateName)) == 0)
            {
                sectionTemplateService.Add(widget.Template);
            }
            widget.PageID = null;
            widget.LayoutID = null;
            widget.ZoneID = null;
            widget.IsSystem = false;
            widget.IsTemplate = true;
            AddWidget(widget);
        }
    }
}