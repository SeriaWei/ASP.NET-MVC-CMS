/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Data;
using Easy.Models;
using Easy.Constant;
using Easy.Modules.Role;

namespace Easy.Modules.User.Models
{
    [DataConfigure(typeof(UserMetaData))]
    public class UserEntity : HumanBase, IUser
    {
        public string UserID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        public string PassWordNew { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 登陆IP
        /// </summary>
        public string LoginIP { get; set; }
        public string PhotoUrl { get; set; }
        public int? UserTypeCD { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        public string UserName { get; set; }

        public string ApiLoginToken { get; set; }
        public IEnumerable<UserRoleRelation> Roles { get; set; }
    }
    class UserMetaData : DataViewMetaData<UserEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Users");
            DataConfig(m => m.UserID).Update(false).Insert(true).AsPrimaryKey();
            DataConfig(m => m.LastLoginDate).Insert(false).Update(false);
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.PassWordNew).Ignore();
            DataConfig(m => m.Roles)
                .SetReference<UserRoleRelation, IUserRoleRelationService>((user, relation) => user.UserID == relation.UserID);
        }

        protected override void ViewConfigure()
        {
            ViewConfig(p => p.PassWord).AsHidden();
            ViewConfig(p => p.PassWordNew).AsTextBox().HideInGrid();
            ViewConfig(p => p.UserID).AsTextBox().Required().Order(1);
            ViewConfig(p => p.NickName).AsTextBox().Required().Order(2);
            ViewConfig(p => p.Age).AsTextBox().RegularExpression(RegularExpression.Integer).HideInGrid();
            ViewConfig(p => p.LastName).AsTextBox().HideInGrid();
            ViewConfig(p => p.FirstName).AsTextBox().HideInGrid();
            ViewConfig(p => p.Birthday).AsTextBox().HideInGrid().FormatAsDate();
            ViewConfig(p => p.Birthplace).AsTextBox().HideInGrid().MaxLength(200);
            ViewConfig(p => p.Address).AsTextBox().HideInGrid().MaxLength(200);
            ViewConfig(p => p.ZipCode).AsTextBox().HideInGrid().RegularExpression(RegularExpression.ZipCode);
            ViewConfig(p => p.School).AsTextBox().HideInGrid().MaxLength(100);
            ViewConfig(p => p.LoginIP).AsTextBox().Hide().HideInGrid();
            ViewConfig(p => p.Timestamp).AsHidden();
            ViewConfig(p => p.LastLoginDate).AsTextBox().Hide().FormatAsDate();
            ViewConfig(p => p.Sex).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.MaritalStatus).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.Roles).AsListEditor();
            ViewConfig(p => p.Description).AsTextArea();
            ViewConfig(p => p.PhotoUrl).AsFileInput().HideInGrid();
            ViewConfig(p => p.UserTypeCD).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.Title).AsHidden();
            ViewConfig(m => m.ApiLoginToken).AsTextBox().ReadOnly().HideInGrid().Hide();
        }
    }
}
