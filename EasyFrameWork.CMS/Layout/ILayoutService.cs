using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Layout
{
    public interface ILayoutService:IService<LayoutEntity>
    {
        void UpdateDesign(LayoutEntity item);
    }
}