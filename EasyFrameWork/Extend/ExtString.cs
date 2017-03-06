/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
//using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
//using Microsoft.International.Converters.PinYinConverter;
using System.Security.Cryptography;
using System.Net;

namespace Easy.Extend
{
    public static class ExtString
    {
        /// <summary>
        /// 获取匹配的内部内容
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetInnerContent(this string value, string start, string end, int index)
        {
            List<string> result = value.GetInnerContent(start, end);
            if (result.Count > 0)
                return result[index];
            else return string.Empty;
        }
        /// <summary>
        /// 获取全部匹配的内部内容
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<string> GetInnerContent(this string value, string start, string end)
        {
            List<string> result = new List<string>();
            int startIndex = 0;
            int endIndex = 0;
            while (endIndex >= startIndex)
            {
                startIndex = value.IndexOf(start, startIndex, StringComparison.InvariantCultureIgnoreCase);
                if (startIndex < 0)
                    break;
                endIndex = value.IndexOf(end, startIndex + start.Length + 1, StringComparison.InvariantCultureIgnoreCase);
                if (endIndex < 0)
                    break;
                if (endIndex > startIndex)
                {
                    result.Add(value.Substring(startIndex + start.Length, endIndex - startIndex - start.Length));
                    startIndex = endIndex;
                }
            }
            return result;
        }

        public static string GetHtmlInner(this string value, string tag, int? index = 0)
        {
            var end = string.Format("</{0}>", tag);
            tag = string.Format("<{0}", tag);
            int reIndex = 0;
            string result = string.Empty;
            var indexStart = 0;
            while (reIndex <= index)
            {
                indexStart = value.IndexOf(tag, indexStart, StringComparison.InvariantCultureIgnoreCase);
                indexStart = value.IndexOf(">", indexStart + 1, StringComparison.InvariantCultureIgnoreCase) + 1;
                if (indexStart >= 0)
                {
                    var tempSatart = value.IndexOf(tag, indexStart + 1, StringComparison.InvariantCultureIgnoreCase);
                    var indexEnd = value.IndexOf(end, indexStart + 1, StringComparison.InvariantCultureIgnoreCase);
                    while (tempSatart < indexEnd && tempSatart >= 0)
                    {
                        tempSatart = value.IndexOf(tag, tempSatart + 1, StringComparison.InvariantCultureIgnoreCase);
                        var tempEnd = value.IndexOf(end, indexEnd + 1, StringComparison.InvariantCultureIgnoreCase);
                        if (tempEnd >= 0)
                            indexEnd = tempEnd;
                    }
                    if (indexStart < indexEnd)
                    {
                        result = value.Substring(indexStart, indexEnd - indexStart);
                        indexStart = indexEnd - indexStart;
                    }
                    reIndex++;
                }
                else return string.Empty;
            }
            return result;
        }
        public static int GetMatchCount(this string value, string target, StringComparison comparison)
        {
            var reIndex = 0;
            var indexStart = -1;
            do
            {
                indexStart = value.IndexOf(target, indexStart + 1, comparison);
                reIndex++;
            }
            while (indexStart >= 0);
            return --reIndex;
        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="Htmlstring">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(this string Htmlstring)
        {
            //删除脚本
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "\\s{2,}", "", RegexOptions.IgnoreCase);
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("\n", "");
            // Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        /// <summary>
        /// 转为合法的文件名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFileName(this string value)
        {
            return value.Replace("/", "_")
                .Replace("\\", "_")
                .Replace(":", "_")
                .Replace("*", "_")
                .Replace("?", "_")
                .Replace("\"", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace("|", "_");
        }
        /// <summary>
        /// 字符串转为byte字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByte(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }


        public static string HtmlDecode(this string value)
        {
            return System.Net.WebUtility.HtmlDecode(value);
        }

        public static string HtmlEncode(this string value)
        {
            return System.Net.WebUtility.HtmlEncode(value);
        }

        public static string UrlEncode(this string value)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(value);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }
        /// <summary>
        /// 转为简体中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static string ToSimpleChinese(this string value)
        //{
        //    return ChineseConverter.Convert(value, ChineseConversionDirection.TraditionalToSimplified);
        //}
        /// <summary>
        /// 转为繁体中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static string ToTraditionalChinese(this string value)
        //{
        //    return ChineseConverter.Convert(value, ChineseConversionDirection.SimplifiedToTraditional);
        //}
        /// <summary>
        /// 获取汉字拼音
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static string GetPinyin(this string value)
        //{
        //    string r = string.Empty;
        //    foreach (char obj in value)
        //    {
        //        try
        //        {
        //            ChineseChar chineseChar = new ChineseChar(obj);
        //            string t = chineseChar.Pinyins[0].ToString();
        //            r += t.Substring(0, t.Length - 1);
        //        }
        //        catch
        //        {
        //            r += obj.ToString();
        //        }
        //    }
        //    return r;
        //}
        /// <summary>
        /// 获取拼音首字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static string GetFirstPinyin(this string value)
        //{
        //    string r = string.Empty;
        //    foreach (char obj in value)
        //    {
        //        try
        //        {
        //            ChineseChar chineseChar = new ChineseChar(obj);
        //            string t = chineseChar.Pinyins[0].ToString();
        //            r += t.Substring(0, 1);
        //        }
        //        catch
        //        {
        //            r += obj.ToString();
        //        }
        //    }
        //    return r;
        //}

        public static string ToUnicode(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append("\\u" + ((int)value[i]).ToString("x"));
            }
            return builder.ToString();
        }

        private static readonly Regex emailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex webUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex stripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);


        public static string FormatWith(this string instance, params object[] args)
        {


            return string.Format(instance, args);
        }

        public static string Hash(this string instance)
        {

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.Unicode.GetBytes(instance);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        public static T ToEnum<T>(this string instance, T defaultValue) where T : struct, IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrWhiteSpace(instance) && !Enum.TryParse(instance.Trim(), true, out convertedValue))
            {
                convertedValue = defaultValue;
            }

            return convertedValue;
        }

        public static T ToEnum<T>(this int instance, T defaultValue) where T : struct, IComparable, IFormattable
        {
            T convertedValue;

            if (!Enum.TryParse(instance.ToString(), true, out convertedValue))
            {
                convertedValue = defaultValue;
            }

            return convertedValue;
        }

        public static string StripHtml(this string instance)
        {
            return stripHTMLExpression.Replace(instance, string.Empty);
        }

        public static bool IsEmail(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && emailExpression.IsMatch(instance);
        }

        public static bool IsWebUrl(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && webUrlExpression.IsMatch(instance);
        }

        public static bool IsIPAddress(this string instance)
        {
            IPAddress ip;

            return !string.IsNullOrWhiteSpace(instance) && IPAddress.TryParse(instance, out ip);
        }

        public static bool AsBool(this string instance)
        {
            bool result = false;
            bool.TryParse(instance, out result);
            return result;
        }

        public static DateTime AsDateTime(this string instance)
        {
            DateTime result = DateTime.MinValue;
            DateTime.TryParse(instance, out result);
            return result;
        }

        public static Decimal AsDecimal(this string instance)
        {
            var result = (decimal)0.0;
            Decimal.TryParse(instance, out result);
            return result;
        }

        public static int AsInt(this string instance)
        {
            var result = (int)0;
            int.TryParse(instance, out result);
            return result;
        }

        public static bool IsInt(this string instance)
        {
            int result;
            return int.TryParse(instance, out result);
        }

        public static bool IsDateTime(this string instance)
        {
            DateTime result;
            return DateTime.TryParse(instance, out result);
        }

        public static bool IsFloat(this string instance)
        {
            float result;
            return float.TryParse(instance, out result);
        }

        public static bool IsNullOrWhiteSpace(this string instance)
        {
            return string.IsNullOrWhiteSpace(instance);
        }

        public static bool IsNotNullAndWhiteSpace(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance);
        }

        /// Like linq take - takes the first x characters
        public static string Take(this string theString, int count, bool ellipsis = false)
        {
            int lengthToTake = Math.Min(count, theString.Length);
            var cutDownString = theString.Substring(0, lengthToTake);

            if (ellipsis && lengthToTake < theString.Length)
                cutDownString += "...";

            return cutDownString;
        }

        //like linq skip - skips the first x characters and returns the remaining string
        public static string Skip(this string theString, int count)
        {
            int startIndex = Math.Min(count, theString.Length);
            var cutDownString = theString.Substring(startIndex - 1);

            return cutDownString;
        }

        //reverses the string... pretty obvious really
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        // "a string".IsNullOrEmpty() beats string.IsNullOrEmpty("a string")
        public static bool IsNullOrEmpty(this string theString)
        {
            return string.IsNullOrEmpty(theString);
        }

        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        //splits string into array with chunks of given size. not really that useful..
        public static string[] SplitIntoChunks(this string toSplit, int chunkSize)
        {
            if (string.IsNullOrEmpty(toSplit))
                return new string[] { "" };

            int stringLength = toSplit.Length;

            int chunksRequired = (int)Math.Ceiling((decimal)stringLength / (decimal)chunkSize);
            var stringArray = new string[chunksRequired];

            int lengthRemaining = stringLength;

            for (int i = 0; i < chunksRequired; i++)
            {
                int lengthToUse = Math.Min(lengthRemaining, chunkSize);
                int startIndex = chunkSize * i;
                stringArray[i] = toSplit.Substring(startIndex, lengthToUse);

                lengthRemaining = lengthRemaining - lengthToUse;
            }

            return stringArray;
        }

        //public static string Join(this IEnumerable<object> array, string seperator)
        //{
        //    if (array == null)
        //        return "";

        //    return string.Join(seperator, array.ToArray());
        //}

        public static string Join(this object[] array, string seperator)
        {
            if (array == null)
                return "";

            return string.Join(seperator, array);
        }


    }
}
