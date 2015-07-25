using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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
            path += string.Format("\\{0}\\", DateTime.Now.ToString("yyyyMMdd"));
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
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
                    fileName = string.Format("{0}{1}", DateTime.Now.ToFileTime(), ext);
                    path += fileName;
                    request.Files[0].SaveAs(path);
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
                    fileName = string.Format("{0}{1}", DateTime.Now.ToFileTime(), ext);
                    path += fileName;
                    request.Files[name].SaveAs(path);
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
                    fileName = string.Format("{0}{1}", DateTime.Now.ToFileTime(), ext);
                    path += fileName;
                    request.Files[0].SaveAs(path);
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
                    fileName = string.Format("{0}{1}", DateTime.Now.ToFileTime(), ext);
                    path += fileName;
                    request.Files[0].SaveAs(path);
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }
    }
}
