using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Extend;

namespace Easy.Web.CMS.Zone
{
    public class ZoneService : ServiceBase<ZoneEntity>
    {
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
            var page = new Easy.Web.CMS.Page.PageService().Get(pageId);
            var layout = new Easy.Web.CMS.Layout.LayoutService().Get(page.LayoutId);
            var zones = new Easy.Web.CMS.Zone.ZoneService().Get(new DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID).OrderBy("ID", OrderType.Ascending));
            return zones;
        }
        public IEnumerable<ZoneEntity> GetZonesByLayoutId(string layoutId)
        {
            var layout = new Easy.Web.CMS.Layout.LayoutService().Get(layoutId);
            var zones = new Easy.Web.CMS.Zone.ZoneService().Get(new DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID).OrderBy("ID", OrderType.Ascending));
            return zones;
        }
        public override int Delete(Data.DataFilter filter)
        {
            //var deletes = this.Get(filter).ToList(m => m.ID);
            //if (deletes.Any())
            //{
            //    Widget.WidgetService widgetService = new Widget.WidgetService();
            //    var widgets = widgetService.Get(new DataFilter().Where("ZoneId", OperatorType.In, deletes));
            //    widgets.Each(m =>
            //    {
            //        m.CreateServiceInstance().DeleteWidget(m.ID);
            //    });
            //}
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            //Widget.WidgetService widgetService = new Widget.WidgetService();
            //var widgets = widgetService.Get(new DataFilter().Where("ZoneId", OperatorType.Equal, primaryKeys[0]));
            //widgets.Each(m =>
            //{
            //    m.CreateServiceInstance().DeleteWidget(m.ID);
            //});
            return base.Delete(primaryKeys);
        }
    }
}
