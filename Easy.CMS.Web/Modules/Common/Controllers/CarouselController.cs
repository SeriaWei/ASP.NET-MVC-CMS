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
    [AdminTheme]
    public class CarouselController : BasicController<CarouselEntity, CarouselService>
    {
        public CarouselController() : base(new CarouselService())
        {
            
        }
    }
}
