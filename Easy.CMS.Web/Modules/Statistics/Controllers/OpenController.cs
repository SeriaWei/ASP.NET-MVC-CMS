using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Statistics.Service;
using Easy.Data;

namespace Easy.CMS.Statistics.Controllers
{
    public class OpenController : Controller
    {

        public JsonResult Index()
        {
            var service = new StatisticsService();
            if (Request.UrlReferrer != null && service.Count(new DataFilter().Where("Host", OperatorType.Equal, Request.UrlReferrer.Host)) == 0)
            {
                service.Add(new Models.Statistics
                {
                    Host = Request.UrlReferrer.Host,
                    IpAddress = Request.UserHostAddress
                });
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
