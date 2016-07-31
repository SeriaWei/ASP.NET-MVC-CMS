using Easy.CMS.Common.Models;
using Easy.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Common.Service;
using Easy.Web.Authorize;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class CarouselController : BasicController<CarouselEntity, long, ICarouselService>
    {
        public CarouselController(ICarouselService service)
            : base(service)
        {

        }
    }
}
