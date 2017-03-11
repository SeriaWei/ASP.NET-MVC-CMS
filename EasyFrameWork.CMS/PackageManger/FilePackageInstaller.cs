using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Easy.Extend;

namespace Easy.Web.CMS.PackageManger
{
    public class FilePackageInstaller : IPackageInstaller
    {
        public virtual string PackageInstaller
        {
            get
            {
                return "FilePackageInstaller";
            }

        }

        public virtual object Install(Package package)
        {
            var filePackage = package as FilePackage;
            if (filePackage != null)
            {
                if (filePackage.Files != null)
                {
                    filePackage.Files.ForEach(file =>
                    {
                        string filePath = HostingEnvironment.MapPath(file.FilePath);
                        var directory = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        File.WriteAllBytes(filePath, file.Content);
                    });
                }
            }
            return package;
        }

        public virtual Package Pack(object obj)
        {
            FilePackage package = CreatePackage();
            var directory = obj as DirectoryInfo;
            if (directory != null)
            {
                CollectFiles(directory, package);
            }
            else
            {
                var files = obj as IEnumerable<System.IO.FileInfo>;
                if (files != null)
                {
                    files.Each(file =>
                    {
                        package.Files.Add(new FileInfo { FileName = file.Name, FilePath = file.FullName.Replace(HostingEnvironment.MapPath("~/"), "~/"), Content = File.ReadAllBytes(file.FullName) });
                    });
                }
            }
            return package;
        }
        public virtual FilePackage CreatePackage()
        {
            return new FilePackage(PackageInstaller);
        }
        public void CollectFiles(DirectoryInfo directory, FilePackage package)
        {
            directory.GetDirectories().Each(dir => CollectFiles(dir, package));
            directory.GetFiles().Each(file =>
            {
                package.Files.Add(new FileInfo { FileName = file.Name, FilePath = file.FullName.Replace(HostingEnvironment.MapPath("~/"), "~/"), Content = File.ReadAllBytes(file.FullName) });
            });
        }
    }
}
