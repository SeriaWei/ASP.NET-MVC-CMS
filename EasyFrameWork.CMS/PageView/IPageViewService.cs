using Easy.RepositoryPattern;

namespace Easy.Web.CMS.PageView
{
    public interface IPageViewService : IService<PageView>
    {
        PageView GenerateReferer(PageView pageView, string referer);
    }
}