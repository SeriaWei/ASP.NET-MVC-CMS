using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.PackageManger
{
    public class PackageInstallerProvider : IPackageInstallerProvider
    {
        private readonly IEnumerable<IPackageInstaller> _packageInstallers;
        public PackageInstallerProvider()
        {
            _packageInstallers = ServiceLocator.Current.GetAllInstances<IPackageInstaller>();
        }
        public IPackageInstaller CreateInstaller(string packageInstaller)
        {
            return _packageInstallers.FirstOrDefault(m => m.PackageInstaller == packageInstaller);
        }

        public IPackageInstaller CreateInstaller<T>(Stream stream, out T package) where T : Package
        {
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            package = JsonConvert.DeserializeObject<T>(content);
            package.Content = content;
            return CreateInstaller(package.PackageInstaller);
        }
    }
}
