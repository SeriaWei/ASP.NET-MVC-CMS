using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionContentParagraphService : ServiceBase<SectionContentParagraph>, ISectionContentService
    {
        public SectionContentBase.Types ContentType
        {
            get { return SectionContentBase.Types.Paragraph; }
        }

        public void AddContent(SectionContent content)
        {
            Add(content as SectionContentParagraph);
        }

        public void DeleteContent(int contentId)
        {
            Delete(contentId);
        }


        public SectionContent GetContent(int contentId)
        {
            return Get(contentId);
        }


        public void UpdateContent(SectionContent content)
        {
            Update(content as SectionContentParagraph);
        }
    }
}