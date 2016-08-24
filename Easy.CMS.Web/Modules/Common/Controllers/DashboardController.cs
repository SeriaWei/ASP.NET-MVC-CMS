using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Easy.CMS.Common.Models;
using Easy.CMS.Common.Service;
using Easy.CMS.Common.ViewModels;
using Easy.Data;
using Easy.Extend;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Article.Service;
using Easy.Web.CMS.Chart;
using Easy.Web.CMS.Layout;
using Easy.Web.CMS.Media;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Product.Service;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class DashboardController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IProductService _productService;
        private readonly IArticleService _articleService;
        private readonly IMediaService _mediaService;
        private readonly IChartProviderService _chartProviderService;
        public DashboardController(IPageService pageService, IProductService productService, 
            IArticleService articleService, IMediaService mediaService, IChartProviderService chartProviderService)
        {
            _pageService = pageService;
            _productService = productService;
            _articleService = articleService;
            _mediaService = mediaService;
            _chartProviderService = chartProviderService;
        }

        public ActionResult Index()
        {
            var datetime = DateTime.Now.AddDays(-1);
            var pages = _pageService.Get(m => m.IsPublishedPage == true);
            var viewMoldel = new DashboardViewModel
            {
                Products = _productService.Count(new DataFilter()),
                Articles = _articleService.Count(new DataFilter()),
                Medias = _mediaService.Count(new DataFilter()),
                UnPublishPage = _pageService.Get(m => m.IsPublishedPage == false && m.IsPublish == false),
                //CurrentTop =
                //    _pageViewService.Get(new DataFilter().Where("CreateDate", OperatorType.GreaterThanOrEqualTo,
                //        datetime)
                //        .OrderBy("ID", OrderType.Descending))
                //        .GroupBy(m => m.PageUrl)
                //        .OrderByDescending(m => m.Count())
                //        .Take(10)
                //        .Select(m =>
                //        {
                //            var pv = m.First();
                //            pv.Sum = m.Count();
                //            return pv;
                //        }),
                Charts = _chartProviderService.GetAvailableChart().OrderBy(m=>m.Order),
                Pages = pages.Count()
            };
            return View(viewMoldel);
        }

    }
}
