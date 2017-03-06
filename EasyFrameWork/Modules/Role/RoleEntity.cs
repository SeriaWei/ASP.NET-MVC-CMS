/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Models;

namespace Easy.Modules.Role
{
    [DataConfigure(typeof(RoleMetaData))]
    public class RoleEntity : EditorEntity
    {
        public int ID { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }

    class RoleMetaData : DataViewMetaData<RoleEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Roles");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Permissions).SetReference<Permission, IPermissionService>((role, permission) => role.ID == permission.RoleId);
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required();
        }
    }
}
