using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

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
    }
}