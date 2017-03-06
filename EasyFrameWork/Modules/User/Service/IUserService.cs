/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.IOC;
using Easy.RepositoryPattern;
using Easy.Modules.User.Models;

namespace Easy.Modules.User.Service
{
    public interface IUserService : IService<UserEntity>, IFreeDependency
    {
        UserEntity Login(string userID, string passWord, string ip);
    }
}
