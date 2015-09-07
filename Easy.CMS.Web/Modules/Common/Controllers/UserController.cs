using System.Web.Mvc;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, Authorize]
    public class UserController : BasicController<UserEntity, string, IUserService>
    {
        public UserController(IUserService userService)
            : base(userService)
        {

        }
    }
}
