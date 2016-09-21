using System;
using System.Collections.Generic;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Layout;
using Easy.Web.CMS.Page;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Zone
{
    public class ZoneService : ServiceBase<ZoneEntity>, IZoneService
    {
        private IPageService _pageService;

        public IPageService PageService
        {
            get { return _pageService ?? (_pageService = ServiceLocator.Current.GetInstance<IPageService>()); }
        }
        private ILayoutService _layoutService;

        public ILayoutService LayoutService
        {
            get { return _layoutService ?? (_layoutService = ServiceLocator.Current.GetInstance<ILayoutService>()); }
        }
        public override void Add(ZoneEntity item)
        {
            if (item.ID.IsNullOrEmpty())
            {
                item.ID = Guid.NewGuid().ToString("N");
            }
            base.Add(item);
        }
        public IEnumerable<ZoneEntity> GetZonesByPageId(string pageId)
        {
            var page = PageService.Get(pageId);
            var layout = LayoutService.Get(page.LayoutId);
            var zones = Get(new DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID).OrderBy("ID", OrderType.Ascending));
            return zones;
        }
        public IEnumerable<ZoneEntity> GetZonesByLayoutId(string layoutId)
        {
            var layout = LayoutService.Get(layoutId);
            var zones = Get(new DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID).OrderBy("ID", OrderType.Ascending));
            return zones;
        }
    }
}
