using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy
{
    public interface IApplicationContext
    {
        IUser CurrentUser { get; }
        string VirtualPath { get; }
    }
}
