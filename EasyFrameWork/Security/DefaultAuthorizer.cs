/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.Linq;
using Easy.Constant;
using Easy.Data;
using Easy.Extend;
using Easy.Models;
using Easy.Modules.Role;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Security
{
    public class DefaultAuthorizer : IAuthorizer
    {
        private Dictionary<string, IEnumerable<Permission>> _userPermissions;
        public bool Authorize(string permission)
        {
            return Authorize(permission, ServiceLocator.Current.GetInstance<IApplicationContext>().CurrentUser);
        }

        public bool Authorize(string permission, IUser user)
        {
            if (permission.IsNullOrWhiteSpace())
            {
                return true;
            }
            if (_userPermissions != null && _userPermissions.ContainsKey(user.UserID))
            {
                return _userPermissions[user.UserID].Any(m => m.PermissionKey == permission);
            }
            if (user.Roles == null || !user.Roles.Any())
            {
                return false;
            }
            _userPermissions = _userPermissions ?? new Dictionary<string, IEnumerable<Permission>>();

            var roles = user.Roles.ToList(m => m.RoleID);
            List<Permission> permissions = new List<Permission>();
            ServiceLocator.Current.GetInstance<IRoleService>()
                 .Get(new DataFilter().Where("ID", OperatorType.In, roles).Where("Status", OperatorType.Equal, (int)RecordStatus.Active)).Each(r => permissions.AddRange(r.Permissions));
            _userPermissions.Add(user.UserID, permissions);
            return permissions.Any(m => m.PermissionKey == permission);
        }
    }
}