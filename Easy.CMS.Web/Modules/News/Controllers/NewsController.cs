using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.News.Models;
using Easy.CMS.News.Service;
using Easy.Web.Attribute;

namespace Easy.CMS.News.Controllers
{
    [AdminTheme]
    public class NewsController : BasicController<NewsEntity, long, NewsService>
    {
        
    }
}
