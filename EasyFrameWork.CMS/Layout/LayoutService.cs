using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Cache;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Extend;
using Easy.Web.CMS.Zone;
using Easy.Constant;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Layout
{
    public class LayoutService : ServiceBase<LayoutEntity>, ILayoutService
    {
        public const string LayoutChanged = "LayoutChanged";
        private IPageService _pageService;
        public IPageService PageService
        {
            get { return _pageService ?? (_pageService = ServiceLocator.Current.GetInstance<IPageService>()); }
        }
        private IZoneService _zoneService;
        public IZoneService ZoneService
        {
            get { return _zoneService ?? (_zoneService = ServiceLocator.Current.GetInstance<IZoneService>()); }
        }

        private IWidgetService _widgetService;

        public IWidgetService WidgetService
        {
            get { return _widgetService ?? (_widgetService = ServiceLocator.Current.GetInstance<IWidgetService>()); }
        }
        public override void Add(LayoutEntity item)
        {
            item.ID = Guid.NewGuid().ToString("N");
            base.Add(item);
            if (item.Zones != null)
            {
                item.Zones.Each(m =>
                {
                    m.LayoutId = item.ID;
                    ZoneService.Add(m);
                });
            }
            if (item.Html != null)
            {
                LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
                item.Html.Each(m =>
                {
                    m.LayoutId = item.ID;
                    layoutHtmlService.Add(m);
                });
            }
        }

        public void UpdateDesign(LayoutEntity item)
        {
            this.Update(item, new Data.DataFilter(new List<string> { "ContainerClass" }).Where("ID", OperatorType.Equal, item.ID));
            if (item.Zones != null)
            {
                var zones = ZoneService.Get(m => m.LayoutId == item.ID);

                item.Zones.Where(m => zones.All(n => n.ID != m.ID)).Each(m =>
                {
                    m.LayoutId = item.ID;
                    ZoneService.Add(m);
                });
                item.Zones.Where(m => zones.Any(n => n.ID == m.ID)).Each(m =>
                {
                    m.LayoutId = item.ID;
                    ZoneService.Update(m);
                });
                zones.Where(m => item.Zones.All(n => n.ID != m.ID)).Each(m => ZoneService.Delete(m.ID));
            }
            if (item.Html != null)
            {
                var layoutHtmlService = new LayoutHtmlService();
                layoutHtmlService.Delete(m => m.LayoutId == item.ID);
                item.Html.Each(m =>
                {
                    m.LayoutId = item.ID;
                    layoutHtmlService.Add(m);
                });
            }

        }
        public override bool Update(LayoutEntity item, Data.DataFilter filter)
        {
            Signal.Trigger(LayoutChanged);
            return base.Update(item, filter);
        }
        public override bool Update(LayoutEntity item, params object[] primaryKeys)
        {
            Signal.Trigger(LayoutChanged);
            return base.Update(item, primaryKeys);
        }
        public override LayoutEntity Get(params object[] primaryKeys)
        {
            var cache = new StaticCache();
            var layout = cache.Get("Layout_" + primaryKeys[0], m =>
            {
                m.When(LayoutChanged);
                LayoutEntity entity = base.Get(primaryKeys);
                if (entity == null)
                    return null;
                IEnumerable<ZoneEntity> zones = ZoneService.Get(new Data.DataFilter().Where("LayoutId", OperatorType.Equal, entity.ID));
                entity.Zones = new ZoneCollection();
                zones.Each(entity.Zones.Add);
                IEnumerable<LayoutHtml> htmls = new LayoutHtmlService().Get(new Data.DataFilter().OrderBy("LayoutHtmlId", OrderType.Ascending).Where("LayoutId", OperatorType.Equal, entity.ID));
                entity.Html = new LayoutHtmlCollection();
                htmls.Each(entity.Html.Add);
                return entity;
            });
            layout.Page = null;
            return layout;
        }
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any())
            {
                var layoutHtmlService = new LayoutHtmlService();
                layoutHtmlService.Delete(new Data.DataFilter().Where("LayoutId", OperatorType.In, deletes));

                ZoneService.Delete(new Data.DataFilter().Where("LayoutId", OperatorType.In, deletes));


                PageService.Delete(new Data.DataFilter().Where("LayoutId", OperatorType.In, deletes));

                var widgets = WidgetService.Get(new Data.DataFilter().Where("LayoutId", OperatorType.In, deletes));
                widgets.Each(m =>
                {
                    m.CreateServiceInstance().DeleteWidget(m.ID);
                });

            }
            Signal.Trigger(LayoutChanged);
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            LayoutEntity layout = Get(primaryKeys);
            var layoutHtmlService = new LayoutHtmlService();
            layoutHtmlService.Delete(m => m.LayoutId == layout.ID);

            ZoneService.Delete(new Data.DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID));


            PageService.Delete(new DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID));

            var widgets = WidgetService.Get(new DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID));
            widgets.Each(m =>
            {
                m.CreateServiceInstance().DeleteWidget(m.ID);
            });
            Signal.Trigger(LayoutChanged);
            return base.Delete(primaryKeys);
        }
    }
}
