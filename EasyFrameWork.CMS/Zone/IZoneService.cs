using System.Collections.Generic;
using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Zone
{
    public interface IZoneService : IService<ZoneEntity>
    {
        IEnumerable<ZoneEntity> GetZonesByPageId(string pageId);
        IEnumerable<ZoneEntity> GetZonesByLayoutId(string layoutId);
    }
}