using System.IO;
using System.Xml.Serialization;
using Easy.CMS.Common.Models;
using Easy.Web.CMS;

namespace Easy.CMS.Common.Service
{
    public class SearchEngineService : ISearchEngineService
    {
        private const string Path = "~/Modules/Common/Config/SearchEngines.config";
        private readonly IApplicationContext _applicationContext;
        public SearchEngineService(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public SearchEngines GetSearchEngines()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SearchEngines));
            var fileStream = new FileStream((_applicationContext as CMSApplicationContext).MapPath(Path), FileMode.Open);
            var result = serializer.Deserialize(new StreamReader(fileStream));
            fileStream.Close();
            fileStream.Dispose();
            return result as SearchEngines;
        }
    }
}