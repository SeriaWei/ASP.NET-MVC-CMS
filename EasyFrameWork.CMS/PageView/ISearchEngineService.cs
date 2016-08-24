using Easy.IOC;

namespace Easy.Web.CMS.PageView
{
    public interface ISearchEngineService : IDependency
    {
        SearchEngines GetSearchEngines();
    }
}