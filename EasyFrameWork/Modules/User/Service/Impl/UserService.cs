using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Modules.User.Models;
using Easy.Constant;

namespace Easy.Modules.User.Service
{
    public class UserService : ServiceBase<UserEntity>, IUserService
    {
        public UserService()
        {

        }

        public UserEntity Login(string userID, string passWord)
        {
            passWord = Easy.EncryptionTool.Encryption(passWord);
            var result = this.Get(new Data.DataFilter().Where("UserID", OperatorType.Equal, userID)
                .Where("PassWord", OperatorType.Equal, passWord));
            if (result.Any())
            {
                var user = result.First();
                user.LastLoginDate = DateTime.Now;
                this.Update(user);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
