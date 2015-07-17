using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionContentService : ServiceBase<SectionContent>
    {
        public override int Delete(params object[] primaryKeys)
        {
            var content = Get(primaryKeys);
            switch ((SectionContent.Types)content.SectionContentType)
            {
                case SectionContent.Types.CallToAction:
                    {
                        new SectionContentCallToActionService().Delete(content.SectionContentId);
                        break;
                    }
                case SectionContent.Types.Image:
                    {
                        new SectionContentImageService().Delete(content.SectionContentId);
                        break;
                    }
                case SectionContent.Types.Paragraph:
                    {
                        new SectionContentParagraphService().Delete(content.SectionContentId);
                        break;
                    }
                case SectionContent.Types.Title:
                    {
                        new SectionContentTitleService().Delete(content.SectionContentId);
                        break;
                    }
            }
            return base.Delete(primaryKeys);
        }
    }
}