using Easy.CMS.Common.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Common.Service
{
    public interface IPageViewService : IService<PageView>
    {
        PageView GenerateReferer(PageView pageView, string referer);
    }
}