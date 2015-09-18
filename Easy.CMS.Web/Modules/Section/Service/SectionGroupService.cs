using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionGroupService : ServiceBase<SectionGroup>, ISectionGroupService
    {
        public override int Delete(params object[] primaryKeys)
        {
            var group = Get(primaryKeys);
            var contentService = new SectionContentProviderService();
            var contents = contentService.Get(new DataFilter().Where("SectionGroupId", OperatorType.Equal, group.ID));
            contents.Each(m =>
            {
                contentService.Delete(m.ID);
            });
            return base.Delete(primaryKeys);
        }
    }
}