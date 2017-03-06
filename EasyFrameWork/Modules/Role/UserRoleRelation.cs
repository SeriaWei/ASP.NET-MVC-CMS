/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Linq;
using Easy.IOC;
using Easy.MetaData;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Modules.Role
{
    [DataConfigure(typeof(UserRoleRelationMetaData))]
    public class UserRoleRelation : EditorEntity
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string UserID { get; set; }
    }

    class UserRoleRelationMetaData : DataViewMetaData<UserRoleRelation>
    {

        protected override void DataConfigure()
        {
            DataTable("UserRoleRelation");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();

            DataConfig(m => m.Status).Ignore();
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.CreateBy).Ignore();
            DataConfig(m => m.CreatebyName).Ignore();
            DataConfig(m => m.CreateDate).Ignore();
            DataConfig(m => m.LastUpdateBy).Ignore();
            DataConfig(m => m.LastUpdateByName).Ignore();
            DataConfig(m => m.LastUpdateDate).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.Description).AsHidden();
            ViewConfig(m => m.Status).AsHidden();
            ViewConfig(m => m.RoleID).AsDropDownList().DataSource(() =>
            {
                return ServiceLocator.Current.GetInstance<IRoleService>()
                    .Get()
                    .ToDictionary(m => m.ID.ToString(), n => n.Title);
            });
            ViewConfig(m => m.UserID).AsHidden();
        }
    }
}