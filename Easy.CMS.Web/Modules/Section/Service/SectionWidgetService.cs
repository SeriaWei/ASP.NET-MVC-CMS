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
        public override int Delete(params object[] primaryKeys)
        {
            return base.Delete(primaryKeys);
        }

        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var sectionWidget = widget as SectionWidget;
            sectionWidget.Groups = new SectionGroupService().Get("SectionWidgetId", OperatorType.Equal, sectionWidget.ID);
            var contents = new SectionContentService().Get("SectionWidgetId", OperatorType.Equal, sectionWidget.ID);
            contents.Each(m =>
            {
                switch ((SectionContent.Types) m.SectionContentType)
                {
                    case SectionContent.Types.CallToAction:
                    {
                        m = m.InitContent(new SectionContentCallToActionService().Get(m.SectionContentId));
                        break;
                    }
                    case SectionContent.Types.Image:
                    {
                        m = m.InitContent(new SectionContentImageService().Get(m.SectionContentId));
                        break;
                    }
                    case SectionContent.Types.Paragraph:
                    {
                        m = m.InitContent(new SectionContentParagraphService().Get(m.SectionContentId));
                        break;
                    }
                    case SectionContent.Types.Title:
                    {
                        m = m.InitContent(new SectionContentTitleService().Get(m.SectionContentId));
                        break;
                    }
                }
            });
            sectionWidget.Groups.Each(m =>
            {
                m.SectionContents = contents.Where(n => n.SectionGroupId == m.ID);
            });
            return widget.ToWidgetPart(widget);
        }
    }
}