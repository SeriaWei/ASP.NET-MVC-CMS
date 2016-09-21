using System.Collections.Generic;
using System.Linq;
using Easy.CMS.Section.Models;
using Easy.Data;
using Easy.RepositoryPattern;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Section.Service
{
    public class SectionContentProviderService : ServiceBase<SectionContent>, ISectionContentProviderService
    {
        private readonly IEnumerable<ISectionContentService> _sectionContentServices;
        public SectionContentProviderService()
        {
            _sectionContentServices = ServiceLocator.Current.GetAllInstances<ISectionContentService>();
        }
        public override void Add(SectionContent item)
        {
            if (!item.Order.HasValue || item.Order.Value == 0)
            {
                item.Order =
                    Get(
                        new DataFilter().Where("SectionWidgetId", OperatorType.Equal, item.SectionWidgetId)
                            .Where("SectionGroupId", OperatorType.Equal, item.SectionGroupId)).Count() + 1;
            }
            base.Add(item);
            _sectionContentServices.First(m => (int)m.ContentType == item.SectionContentType).AddContent(item);
        }

        public override bool Update(SectionContent item, params object[] primaryKeys)
        {
            _sectionContentServices.First(m => (int)m.ContentType == item.SectionContentType).UpdateContent(item);
            return true;
        }

        public override SectionContent Get(params object[] primaryKeys)
        {
            var item = base.Get(primaryKeys);
            if (item != null)
            {
                var result = _sectionContentServices.First(m => (int)m.ContentType == item.SectionContentType).GetContent(item.ID ?? 0);
                result.Order = item.Order;
                result.SectionGroupId = item.SectionGroupId;
                result.SectionWidgetId = item.SectionWidgetId;
                return result;
            }
            return null;
        }

        public override int Delete(params object[] primaryKeys)
        {
            var content = base.Get(primaryKeys);
            _sectionContentServices.First(m => (int)m.ContentType == content.SectionContentType).DeleteContent(content.ID ?? 0);
            base.Delete(primaryKeys);
            return 1;
        }

        public SectionContent FillContent(SectionContent content)
        {
            return
                content.InitContent(
                    _sectionContentServices.First(m => (int)m.ContentType == content.SectionContentType)
                        .GetContent(content.ID ?? 0));
        }
    }
}