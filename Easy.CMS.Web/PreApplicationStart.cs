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
            string dir = AppDomain.CurrentDomain.BaseDirectory + "Modules";
            DirectoryInfo directionarys = new DirectoryInfo(dir);
            foreach (DirectoryInfo item in directionarys.GetDirectories())
            {
                string dllPath = dir + string.Format(@"\{0}\bin\{0}.dll", item.Name);
                if (File.Exists(dllPath))
                {
                    BuildManager.AddReferencedAssembly(Assembly.LoadFile(dllPath));
                }
            }

        }
    }
}
