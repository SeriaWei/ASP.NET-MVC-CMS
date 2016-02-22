using System.Web.Mvc;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using Easy.Web.Extend;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, Authorize]
    public class UserController : BasicController<UserEntity, string, IUserService>
    {
        public UserController(IUserService userService)
            : base(userService)
        {

        }
        [HttpPost]
        public override ActionResult Create(UserEntity entity)
        {
            entity.PhotoUrl = Request.SaveImage();
            return base.Create(entity);
        }
        [HttpPost]
        public override ActionResult Edit(UserEntity entity)
        {
            entity.PhotoUrl = Request.SaveImage();
            return base.Edit(entity);
        }
    }
}
