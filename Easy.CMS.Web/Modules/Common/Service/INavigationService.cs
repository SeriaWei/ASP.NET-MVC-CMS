/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.CMS.Common.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Common.Service
{
    public interface INavigationService : IService<NavigationEntity>
    {
        void Move(string id, string parentId, int position, int oldPosition);
    }
}