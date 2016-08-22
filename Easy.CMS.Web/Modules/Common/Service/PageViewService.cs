using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Easy.CMS.Common.Models;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS;

namespace Easy.CMS.Common.Service
{
    public class PageViewService : ServiceBase<PageView>, IPageViewService
    {

        private const string Path = "~/Modules/Common/Config/Referer.config";
        private readonly IApplicationContext _applicationContext;

        public PageViewService(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public PageView GenerateReferer(PageView pageView, string referer)
        {
            if (referer.IsNullOrWhiteSpace())
            {
                return pageView;
            }

            pageView.Referer = referer;
            var uri = new Uri(referer);
            var config = GetRefererConfig().RefererConfigs.FirstOrDefault(m => m.Host == uri.Host);
            if (config != null)
            {
                pageView.RefererName = config.Name;
                pageView.KeyWords = HttpUtility.ParseQueryString(uri.Query)[config.KeyWordsQuery];
            }
            else
            {
                pageView.RefererName = uri.Host;
            }
            return pageView;
        }


        RefererConfig GetRefererConfig()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RefererConfig));
            var fileStream = new FileStream((_applicationContext as CMSApplicationContext).MapPath(Path), FileMode.Open);
            var result = serializer.Deserialize(new StreamReader(fileStream));
            fileStream.Close();
            fileStream.Dispose();
            return result as RefererConfig;
        }
    }
}