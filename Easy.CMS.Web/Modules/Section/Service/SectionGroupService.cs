using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Section.Service
{
    public class SectionGroupService : ServiceBase<SectionGroup>
    {
        public override int Delete(params object[] primaryKeys)
        {
            return base.Delete(primaryKeys);
        }
    }
}