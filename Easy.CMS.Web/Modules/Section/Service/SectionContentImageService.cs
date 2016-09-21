using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionContentImageService : ServiceBase<SectionContentImage>, ISectionContentService
    {
        public SectionContentBase.Types ContentType
        {
            get { return SectionContentBase.Types.Image; }
        }

        public void AddContent(SectionContent content)
        {
            Add(content as SectionContentImage);
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
            Update(content as SectionContentImage);
        }
    }
}