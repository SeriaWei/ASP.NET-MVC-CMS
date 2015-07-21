using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Section.Service
{
    public class SectionWidgetService : WidgetService<SectionWidget>
    {
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
            widget.Groups = new SectionGroupService().Get("SectionWidgetId", OperatorType.Equal, widget.ID);
            var sectionContentService = new SectionContentService();
            var contents = sectionContentService.Get("SectionWidgetId", OperatorType.Equal, widget.ID).ToList();
            for (int i = 0; i < contents.Count; i++)
            {
                contents[i] = sectionContentService.FillContent(contents[i]);
            }
            widget.Groups.Each(m =>
            {
                m.SectionContents = contents.Where(n => n.SectionGroupId == m.ID);
            });
            return widget;
        }

        public override int Delete(params object[] primaryKeys)
        {
            return base.Delete(primaryKeys);
        }
    }
}