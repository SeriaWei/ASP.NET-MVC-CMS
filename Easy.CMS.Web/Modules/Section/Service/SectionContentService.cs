using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Models;
using Easy.Models;
using Easy.Modules.DataDictionary;
using Easy.RepositoryPattern;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Section.Service
{
    public class SectionContentService : ServiceBase<SectionContent>
    {
        private readonly IEnumerable<ISectionContentService> _sectionContentServices;
        public SectionContentService()
        {
            _sectionContentServices = ServiceLocator.Current.GetAllInstances<ISectionContentService>();
        }
        public override void Add(SectionContent item)
        {
            base.Add(item);
            _sectionContentServices.First(m => (int)m.ContentType == item.SectionContentType).AddContent(item);
        }

        public override int Delete(params object[] primaryKeys)
        {
            var content = base.Get(primaryKeys);
            _sectionContentServices.First(m => (int)m.ContentType == content.SectionContentType).DeleteContent(content.ID);
            base.Delete(primaryKeys);
            return 1;
        }

        public SectionContent FillContent(SectionContent content)
        {
            return
                content.InitContent(
                    _sectionContentServices.First(m => (int)m.ContentType == content.SectionContentType)
                        .GetContent(content.ID));
        }
    }
}