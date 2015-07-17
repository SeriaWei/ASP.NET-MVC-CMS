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
            var contents = new SectionContentService().Get("SectionWidgetId", OperatorType.Equal, widget.ID).ToList();
            for (int i = 0; i < contents.Count; i++)
            {
                switch ((SectionContent.Types)contents[i].SectionContentType)
                {
                    case SectionContent.Types.CallToAction:
                        {
                            contents[i] = contents[i].InitContent(new SectionContentCallToActionService().Get(contents[i].SectionContentId));
                            break;
                        }
                    case SectionContent.Types.Image:
                        {
                            contents[i] = contents[i].InitContent(new SectionContentImageService().Get(contents[i].SectionContentId));
                            break;
                        }
                    case SectionContent.Types.Paragraph:
                        {
                            contents[i] = contents[i].InitContent(new SectionContentParagraphService().Get(contents[i].SectionContentId));
                            break;
                        }
                    case SectionContent.Types.Title:
                        {
                            contents[i] = contents[i].InitContent(new SectionContentTitleService().Get(contents[i].SectionContentId));
                            break;
                        }
                }
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