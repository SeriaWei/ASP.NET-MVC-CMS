using Easy.CMS.Common.Models;
using Easy.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Common.Service;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, Authorize]
    public class CarouselController : BasicController<CarouselEntity, long, ICarouselService>
    {
        public CarouselController(ICarouselService service)
            : base(service)
        {

        }
    }
}
