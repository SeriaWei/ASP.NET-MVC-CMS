using System.Web.Mvc;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using Easy.Extend;
using Easy.Web.Extend;
using Easy.Web.Authorize;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
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
            if (ModelState.IsValid)
            {
                
            }
            var url = Request.SaveImage();
            if (url.IsNotNullAndWhiteSpace())
            {
                entity.PhotoUrl = url;
            }            
            return base.Edit(entity);
        }
    }
}
