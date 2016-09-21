using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Easy.Constant;
using Easy.Extend;
using Easy.Modules.Role;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS;
using Easy.Web.Controller;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class RolesController : BasicController<RoleEntity, string, IRoleService>
    {
        public RolesController(IRoleService userService)
            : base(userService)
        {

        }
        [NonAction]
        public override ActionResult Create(RoleEntity entity)
        {
            return base.Create(entity);
        }
        [HttpPost]
        public ActionResult Create(RoleEntity entity, List<PermissionDescriptor> PermissionSet)
        {
            entity.Permissions = new List<Permission>();
            PermissionSet.Where(m => m.Checked ?? false).Each(m =>
            {
                ((List<Permission>)entity.Permissions).Add(new Permission { PermissionKey = m.Key, Module = m.Module, Title = m.Title, ActionType = ActionType.Create });
            });
            Service.Add(entity);
            return RedirectToAction("Index");
        }
        [NonAction]
        public override ActionResult Edit(RoleEntity entity)
        {
            return base.Edit(entity);
        }
        [HttpPost]
        public ActionResult Edit(RoleEntity entity, List<PermissionDescriptor> PermissionSet)
        {
            entity.Permissions = Service.Get(entity.ID).Permissions;
            entity.Permissions.Each(m => m.ActionType = ActionType.Delete);
            PermissionSet.Where(m => m.Checked ?? false).Each(m =>
            {
                bool exists = false;
                foreach (var item in entity.Permissions)
                {
                    if (item.PermissionKey == m.Key)
                    {
                        item.ActionType = ActionType.Update;
                        exists = true;
                    }
                }
                if (!exists)
                {
                    ((List<Permission>)entity.Permissions).Add(new Permission { PermissionKey = m.Key, Module = m.Module, Title = m.Title, ActionType = ActionType.Create });
                }

            });
            Service.Update(entity);
            return RedirectToAction("Index");
        }
    }
}
