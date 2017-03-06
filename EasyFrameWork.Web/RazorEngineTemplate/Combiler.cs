/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Razor;
using Microsoft.CSharp;

namespace Easy.Web.RazorEngineTemplate
{
    public class Combiler
    {
        public Combiler()
        {
            CacheAssembly = true;
        }
        
        public bool CacheAssembly { get; set; }
        public List<string> NamespaceImports { get; set; }
        public string GetTemplateResult<T>(T model, string viewPath)
        {
            var combiledAssembly = Combile(typeof(T), viewPath);
            var razorTemplate = (RazorTemplateBase<T>)combiledAssembly.CreateInstance("Easy.Web.RazorEngineTemplate.RazorTemplate");
            razorTemplate.Model = model;
            razorTemplate.Execute();
            return razorTemplate.Buffer.ToString();
        }
        public Assembly Combile(Type modelType, string viewPath)
        {
            string razorTemplateFolder = AppDomain.CurrentDomain.BaseDirectory + "RazorTemplate\\";
            if (!Directory.Exists(razorTemplateFolder))
            {
                Directory.CreateDirectory(razorTemplateFolder);
            }
            string templateAssembly = razorTemplateFolder + Path.GetFileName(viewPath) + ".dll";
            if (CacheAssembly && File.Exists(templateAssembly))
            {
                return Assembly.LoadFile(templateAssembly);
            }
            var codeProvider = new CSharpCodeProvider();
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                codeProvider.GenerateCodeFromCompileUnit(GenerateCode(modelType, viewPath).GeneratedCode, writer, new CodeGeneratorOptions());
            }
            var result = codeProvider.CompileAssemblyFromSource(BuildCompilerParameters(), new[] { builder.ToString() });
            if (result.Errors.Count > 0)
            {
                throw new CombileFailException(result.Errors[0].ToString());
            }
            if (!string.IsNullOrEmpty(result.PathToAssembly) && CacheAssembly)
            {
                if (File.Exists(result.PathToAssembly))
                {
                    File.Copy(result.PathToAssembly, templateAssembly);
                }
                else
                {
                    throw new CombileFailException();
                }
            }
            return result.CompiledAssembly;
        }
        private GeneratorResults GenerateCode(Type modelType, string viewPath)
        {
            var host = new RazorEngineHost(new CSharpRazorCodeLanguage())
            {
                DefaultBaseClass = string.Format("Easy.Web.RazorEngineTemplate.RazorTemplateBase<{0}>", modelType.FullName),
                DefaultNamespace = "Easy.Web.RazorEngineTemplate",
                DefaultClassName = "RazorTemplate"
            };
            host.NamespaceImports.Add("System");
            if (NamespaceImports != null)
            {
                foreach (string item in NamespaceImports)
                {
                    host.NamespaceImports.Add(item);
                }
            }
            GeneratorResults razorResult = null;
            using (TextReader reader = new StringReader(new StreamReader(viewPath).ReadToEnd()))
            {
                razorResult = new RazorTemplateEngine(host).GenerateCode(reader);
            }
            return razorResult;
        }

        private CompilerParameters BuildCompilerParameters()
        {
            var param = new CompilerParameters();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.IsDynamic &&
                    assembly.ManifestModule.Name != "<In Memory Module>")
                    param.ReferencedAssemblies.Add(assembly.Location);
            }
            param.GenerateInMemory = !CacheAssembly;
            param.IncludeDebugInformation = false;
            param.GenerateExecutable = false;
            param.CompilerOptions = "/target:library /optimize";
            return param;
        }
    }
}
