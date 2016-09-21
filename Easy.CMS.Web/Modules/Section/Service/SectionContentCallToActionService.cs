using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionContentCallToActionService : ServiceBase<SectionContentCallToAction>, ISectionContentService
    {
        public SectionContentBase.Types ContentType
        {
            get { return SectionContentBase.Types.CallToAction; }
        }

        public void AddContent(SectionContent content)
        {
            Add(content as SectionContentCallToAction);
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
            Update(content as SectionContentCallToAction);
        }
    }
}