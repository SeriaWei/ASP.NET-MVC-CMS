/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Modules.User.Models;
using Easy.Constant;
using Easy.Extend;
using Easy.Modules.Role;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Modules.User.Service
{
    public class UserService : ServiceBase<UserEntity>, IUserService
    {
        public override void Add(UserEntity item)
        {
            if (item.PassWordNew.IsNotNullAndWhiteSpace())
            {
                item.PassWord = EncryptionTool.Encryption(item.PassWordNew);
            }
            base.Add(item);
        }

        public override bool Update(UserEntity item, params object[] primaryKeys)
        {
            if (item.PassWordNew.IsNotNullAndWhiteSpace())
            {
                item.PassWord = EncryptionTool.Encryption(item.PassWordNew);
            }
            return base.Update(item, primaryKeys);
        }

        public UserEntity Login(string userID, string passWord, string ip)
        {
            passWord = EncryptionTool.Encryption(passWord);
            var result = Get(new DataFilter().Where("UserID", OperatorType.Equal, userID)
                .Where("PassWord", OperatorType.Equal, passWord));
            if (result.Any())
            {
                var user = result.First();
                user.LastLoginDate = DateTime.Now;
                user.LoginIP = ip;
                Update(user, new DataFilter(new List<string> { "LastLoginDate", "LoginIP" }).Where("UserID", OperatorType.Equal, user.UserID));
                return user;
            }
            return null;
        }
    }
}
