/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Models;
using System;
using System.Collections;
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
