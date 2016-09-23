/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.CMS.Section.Models;

namespace Easy.CMS.Section.Service
{
    interface ISectionContentService
    {
        SectionContentBase.Types ContentType { get; }
        void AddContent(SectionContent content);
        void UpdateContent(SectionContent content);
        SectionContent GetContent(int contentId);
        void DeleteContent(int contentId);
    }
}
