/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.MetaData;
using Easy.Models;

namespace Easy.Modules.Role
{
    [DataConfigure(typeof(PermissionMetaData))]
    public class Permission : EditorEntity
    {
        public string PermissionKey { get; set; }
        public string Module { get; set; }
        public int RoleId { get; set; }
    }

    class PermissionMetaData : DataViewMetaData<Permission>
    {

        protected override void DataConfigure()
        {
            DataTable("Permission");
            DataConfig(m => m.PermissionKey).AsPrimaryKey();
            DataConfig(m => m.RoleId).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }
}