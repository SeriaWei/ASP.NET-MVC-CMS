using Easy.IOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Easy.Web.CMS.PackageManger
{
    public interface IPackageInstallerProvider : IDependency
    {
        IPackageInstaller CreateInstaller(string packageInstaller);
        IPackageInstaller CreateInstaller<T>(Stream stream, out T package) where T : Package;
    }
}
