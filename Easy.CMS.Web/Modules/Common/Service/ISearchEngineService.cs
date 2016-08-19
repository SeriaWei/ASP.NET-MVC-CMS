using Easy.CMS.Common.Models;
using Easy.IOC;

namespace Easy.CMS.Common.Service
{
    public interface ISearchEngineService : IDependency
    {
        SearchEngines GetSearchEngines();
    }
}