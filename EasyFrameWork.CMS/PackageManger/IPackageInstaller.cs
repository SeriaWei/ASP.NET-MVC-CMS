using Easy.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.PackageManger
{
    public interface IPackageInstaller : IDependency
    {
        string PackageInstaller { get; }
        object Install(Package package);
        Package Pack(object obj);
    }
}
