/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization.Json;
using System.Text;

namespace Easy.Extend
{
    public static class ExtObject
    {
        public static string ToStringIgnoeNull(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        //public static string ToJson(this object obj)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        DataContractJsonSerializer ds = new DataContractJsonSerializer(obj.GetType());
        //        ds.WriteObject(ms, obj);
        //        string strReturn = Encoding.UTF8.GetString(ms.ToArray());
        //        ms.Close();
        //        return strReturn;
        //    }
        //}
    }
}
