/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public interface ISectionGroupService:IService<SectionGroup>
    {
        SectionGroup GenerateContentFromConfig(SectionGroup group);
    }
}