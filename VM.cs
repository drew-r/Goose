using NLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Goose  //because Maverick depends on Goose...
{
    public class VM : Lua
    {
        public VM() : base()
        {
            AssemblyLocator.Initialize();
            DoString("require \"lib/CLRPackage\"");
            Import(Assembly.GetAssembly(this.GetType()));
            Import(AssemblyLocator.ResolveAssembly(AssemblyLocator.ResolveReference("mscorlib"), true));
            Import(AssemblyLocator.ResolveAssembly(AssemblyLocator.ResolveReference("System"),true));
        }


        public void Import(Assembly assembly)
        {
            var assemblyNamespaces = (from type in assembly.GetExportedTypes() select new { Namespace = type.Namespace, Assembly = assembly.FullName }).Distinct();
            foreach (var assemblyNamespace in assemblyNamespaces)
            {
                DoString(String.Format("import('{0}','{1}')", assemblyNamespace.Assembly, assemblyNamespace.Namespace));
            }

        }


        public Assembly Compile(string assemblyName,string[] src, params string[] references)
        {
            List<string> source = new List<string>(src);
            for (int i = 0; i<source.Count;i++)
            {
                string item = source[i];
                if (File.Exists(item))
                {
                    source[i] = File.ReadAllText(item);
                    continue;
                }
                if (Directory.Exists(item))
                {
                    source.RemoveAt(i);
                    foreach (string file in Directory.GetFiles(item, "*.cs"))
                    {
                        source.Add(File.ReadAllText(file));                                                                                                
                    }
                    continue;
                }                
            }

            for (int i = 0; i < references.Length; i++)
            {
                references[i] = AssemblyLocator.ResolveReference(references[i]);
            }
                        
            Assembly assembly = CSharpCompiler.Compile(assemblyName, source.ToArray(), references);            
            Import(assembly);
            foreach (string reference in references)
            {
                Import(AssemblyLocator.ResolveAssembly(reference,true));
            }
            return assembly;
        }





    }
}
