using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

namespace PlugWeb
{
    /// <summary>
    /// [assembly: PreApplicationStartMethod(typeof(Easy.Web.PreApplicationStart), "LoadModules")]
    /// </summary>
    public class PreApplicationStart
    {
        public static void LoadModules()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "modules.config";
            var doc = new System.Xml.XmlDocument();
            doc.Load(dir);
            var nodeList = doc.SelectNodes("Assemblys/Assembly");
            foreach (System.Xml.XmlNode item in nodeList)
            {
                string dllPath = AppDomain.CurrentDomain.BaseDirectory + item.Attributes.GetNamedItem("path").Value;
                Assembly target = Assembly.LoadFile(dllPath);
                BuildManager.AddReferencedAssembly(target);
            }
        }

       
    }
}
