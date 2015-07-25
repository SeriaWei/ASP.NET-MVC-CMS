using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Easy.Web
{
    public static class Common
    {
        /// <summary>
        /// 判断文件是否能上传
        /// </summary>
        /// <param name="Ext">文件扩展名</param>
        /// <returns></returns>
        public static bool FileCanUp(string Ext)
        {
            Ext = Ext.ToLower();
            if (Ext == ".aspx" || Ext == ".asp" || Ext == ".exe" || Ext == ".php" || Ext == ".jsp" || Ext == ".htm" || Ext == ".html" || Ext == ".xhtml" || Ext == string.Empty || Ext == null
                || Ext == ".cs" || Ext == ".bat" || Ext == ".jar" || Ext == ".dll" || Ext == ".com")
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// 判断是否为图片
        /// </summary>
        /// <param name="Ext">扩展名</param>
        /// <returns>返回Bool值，是则返回true</returns>
        public static bool IsImage(string Ext)
        {
            Ext = Ext.ToLower();
            if (Ext == ".gif" || Ext == ".jpg" || Ext == ".png" || Ext == ".jpeg" || Ext == ".bmp")
            {
                return true;
            }
            else return false;
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static string Md5Encoder(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
    }
}
