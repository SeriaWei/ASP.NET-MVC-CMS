using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionContentParagraphService : ServiceBase<SectionContentParagraph>, ISectionContentService
    {
        public SectionContent.Types ContentType
        {
            get { return SectionContent.Types.Paragraph; }
        }

        public void AddContent(SectionContent content)
        {
            this.Add(content as SectionContentParagraph);
        }

        public void UpdateContent(SectionContent content)
        {
            this.Update(content as SectionContentParagraph);
        }

        public void DeleteContent(int contentId)
        {
            this.Delete(contentId);
        }


        public SectionContent GetContent(int contentId)
        {
            return this.Get(contentId);
        }
    }
}