using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Modules.User.Models;

namespace Easy.Modules.User.Service
{
    public interface IUserService : IService
    {
        UserEntity Login(string userID, string passWord);
    }
}
