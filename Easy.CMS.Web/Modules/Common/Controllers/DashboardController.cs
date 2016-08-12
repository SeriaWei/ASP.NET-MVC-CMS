using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Common.Models;
using Easy.CMS.Common.Service;
using Easy.CMS.Common.ViewModels;
using Easy.Data;
using Easy.Extend;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Article.Service;
using Easy.Web.CMS.Layout;
using Easy.Web.CMS.Media;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Product.Service;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class DashboardController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IPageService _pageService;
        private readonly IPageViewService _pageViewService;
        private readonly IProductService _productService;
        private readonly IArticleService _articleService;
        private readonly IMediaService _mediaService;
        public DashboardController(ILayoutService layoutService, IPageService pageService, IPageViewService pageViewService, IProductService productService, IArticleService articleService, IMediaService mediaService)
        {
            _layoutService = layoutService;
            _pageService = pageService;
            _pageViewService = pageViewService;
            _productService = productService;
            _articleService = articleService;
            _mediaService = mediaService;
        }

        public ActionResult Index()
        {
            var datetime = DateTime.Now.AddDays(-1);
            var viewMoldel = new DashboardViewModel
            {
                PageViewDate = new List<string>(),
                PageViewCount = new List<int>(),
                PageUniqueViewCount = new List<int>(),
                PageIpAddressCount = new List<int>(),
                Layouts = new List<string>(),
                LayoutUsage = new List<int>(),
                Products = _productService.Count(new DataFilter()),
                Articles = _articleService.Count(new DataFilter()),
                Medias = _mediaService.Count(new DataFilter()),
                UnPublishPage = _pageService.Get(m => m.IsPublishedPage == false && m.IsPublish == false),
                CurrentTop = _pageViewService.Get(new DataFilter().Where("CreateDate", OperatorType.GreaterThanOrEqualTo, datetime)
                .OrderBy("ID", OrderType.Descending)).GroupBy(m => m.PageUrl).OrderByDescending(m => m.Count()).Take(10)
                .Select(m =>
                    {
                        var pv = m.First();
                        pv.Sum = m.Count();
                        return pv;
                    })
            };
            var dateAgo = DateTime.Now.AddDays(-15);
            _pageViewService.Get(new DataFilter().Where("CreateDate", OperatorType.GreaterThan, dateAgo).OrderBy("ID", OrderType.Descending))
                .OrderBy(m => m.ID)
                .GroupBy(m => (m.CreateDate ?? DateTime.Now).ToString("yyyy-MM-dd"))
                .Each(
                    m =>
                    {
                        viewMoldel.PageViewDate.Add(m.Key);
                        viewMoldel.PageViewCount.Add(m.Count());
                        viewMoldel.PageUniqueViewCount.Add(m.GroupBy(n => n.SessionID).Count());
                        viewMoldel.PageIpAddressCount.Add(m.GroupBy(n => n.IPAddress).Count());
                    });


            var pages = _pageService.Get(m => m.IsPublishedPage == true);
            _layoutService.Get().Each(l =>
            {
                viewMoldel.Layouts.Add(l.LayoutName);
                viewMoldel.LayoutUsage.Add(pages.Count(m => m.LayoutId == l.ID));
            });
            viewMoldel.Pages = pages.Count();
            return View(viewMoldel);
        }

    }
}
