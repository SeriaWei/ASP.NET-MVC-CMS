using Easy.CMS.Common.Models;
using Easy.CMS.Common.Service;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.Controller;

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
