using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Data;
using Easy.Models;
using Easy.Constant;

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
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 登陆IP
        /// </summary>
        public string LoginIP { get; set; }
        public string PhotoUrl { get; set; }
        public int UserTypeCD { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        public string ID { get; set; }

        public string UserName { get; set; }

        public string ApiLoginToken { get; set; }
    }
    public class UserMetaData : DataViewMetaData<UserEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Users");
            DataConfig(m => m.UserID).Update(false).Insert(true).AsPrimaryKey();
            DataConfig(m => m.LastLoginDate).Insert(false);
            DataConfig(m => m.ID).Ignore();
            DataConfig(m => m.Title).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(p => p.PassWord).AsPassWord().HideInGrid().Required();
            ViewConfig(p => p.NickName).AsTextBox().Required().Order(2);
            ViewConfig(p => p.UserID).AsTextBox().Required().Order(1);
            ViewConfig(p => p.Age).AsTextBox().RegularExpression(Easy.Constant.RegularExpression.Integer).HideInGrid();
            ViewConfig(p => p.Birthday).AsTextBox().HideInGrid().FormatAsDate();
            ViewConfig(p => p.Birthplace).AsTextBox().HideInGrid().MaxLength(200);
            ViewConfig(p => p.Address).AsTextBox().HideInGrid().MaxLength(200);
            ViewConfig(p => p.ZipCode).AsTextBox().HideInGrid().RegularExpression(Easy.Constant.RegularExpression.ZipCode);
            ViewConfig(p => p.School).AsTextBox().HideInGrid().MaxLength(100);
            ViewConfig(p => p.LoginIP).AsTextBox().Hide();
            ViewConfig(p => p.Timestamp).AsHidden();
            ViewConfig(p => p.LastLoginDate).AsTextBox().Hide().FormatAsDate();
            ViewConfig(p => p.Sex).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.MaritalStatus).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.Description).AsMutiLineTextBox();
            ViewConfig(p => p.PhotoUrl).AsFileUp().HideInGrid();
            ViewConfig(p => p.UserTypeCD).AsDropDownList().DataSource(Easy.Constant.SourceType.Dictionary);
            ViewConfig(p => p.Title).AsTextBox().Ignore();
            ViewConfig(p => p.ID).AsHidden();
            ViewConfig(m => m.ApiLoginToken).AsTextBox().ReadOnly();
        }
    }
}
