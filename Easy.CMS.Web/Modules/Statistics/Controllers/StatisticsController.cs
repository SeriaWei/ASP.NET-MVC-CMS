using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Statistics.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;

namespace Easy.CMS.Statistics.Controllers
{
    [AdminTheme, Authorize]
    public class StatisticsController : BasicController<Models.Statistics, Int32, StatisticsService>
    {
        public StatisticsController(StatisticsService service)
            : base(service)
        {
        }
    }
}
