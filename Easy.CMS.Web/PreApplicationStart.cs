using System;
using System.Reflection;
using System.Web.Compilation;
using System.Xml;

namespace Easy
{
    /// <summary>
    /// [assembly: PreApplicationStartMethod(typeof(Easy.Web.PreApplicationStart), "LoadModules")]
    /// </summary>
    public class PreApplicationStart
    {
        public static void LoadModules()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "modules.config";
            var doc = new XmlDocument();
            doc.Load(dir);
            var nodeList = doc.SelectNodes("Assemblys/Assembly");
            foreach (XmlNode item in nodeList)
            {
                string dllPath = AppDomain.CurrentDomain.BaseDirectory + item.Attributes.GetNamedItem("path").Value;
                Assembly target = Assembly.LoadFile(dllPath);
                BuildManager.AddReferencedAssembly(target);
            }
        }

       
    }
}
