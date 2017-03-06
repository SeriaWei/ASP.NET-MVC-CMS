/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using Easy.Storage;
using Easy.Extend;

namespace Easy.Web.Extend
{
    public static class ExHttpRequest
    {
        const string ImagePath = "~/UpLoad/Images";
        const string FilePath = "~/UpLoad/Files";

        public static string InitPath(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            path += string.Format("\\{0}\\", DateTime.Now.ToString("yyyyMM"));
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                var storage = ServiceLocator.Current.GetInstance<IStorageService>();
                if (storage != null)
                {
                    storage.CreateFolder(path);
                }
            }
            return path;
        }
        /// <summary>
        /// 保存图片到UpLoad/Images
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string SaveImage(this HttpRequestBase request)
        {
            if (request.Files.Count > 0 && request.Files[0].ContentLength > 0)
            {
                string path = InitPath(request.MapPath(ImagePath));
                string fileName = request.Files[0].FileName;
                string ext = System.IO.Path.GetExtension(fileName);
                if (Common.IsImage(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Files[0].SaveAs(path);

                    var storage = ServiceLocator.Current.GetInstance<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }
        public static string SaveImage(this HttpRequestBase request, string name)
        {
            if (request.Files.Count > 0 && request.Files[name].ContentLength > 0)
            {
                string path = InitPath(request.MapPath(ImagePath));
                string fileName = request.Files[name].FileName;
                string ext = System.IO.Path.GetExtension(fileName);
                if (Common.IsImage(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Files[name].SaveAs(path);
                    var storage = ServiceLocator.Current.GetInstance<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 保存文件到UpLoad/Files
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string SaveFile(this HttpRequestBase request)
        {
            if (request.Files.Count > 0 && request.Files[0].ContentLength > 0)
            {
                string path = InitPath(request.MapPath(FilePath));
                string fileName = request.Files[0].FileName;
                string ext = System.IO.Path.GetExtension(fileName);
                if (Common.FileCanUp(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Files[0].SaveAs(path);
                    var storage = ServiceLocator.Current.GetInstance<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }
        public static string SaveFile(this HttpRequestBase request, string name)
        {
            if (request.Files.Count > 0 && request.Files[0].ContentLength > 0)
            {
                string path = InitPath(request.MapPath(FilePath));
                string fileName = request.Files[0].FileName;
                string ext = System.IO.Path.GetExtension(fileName);
                if (Common.FileCanUp(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Files[0].SaveAs(path);
                    var storage = ServiceLocator.Current.GetInstance<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }

        public static void DeleteFile(this HttpRequestBase request, string filePath)
        {
            try
            {
                string file = request.MapPath(filePath);
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
                var storage = ServiceLocator.Current.GetInstance<IStorageService>();
                if (storage != null)
                {
                    storage.DeleteFile(file);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
    }
}
