using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Models
{
    public interface IUser : IEntity
    {
        string UserID { get; set; }
        string NickName { get; set; }
    }
}
