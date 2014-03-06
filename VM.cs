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
    /// <summary>
    /// Interface to the Goose Lua VM & C# Compiler
    /// </summary>
    public class VM : Lua
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VM"/> class.
        /// </summary>
        public VM() : base()
        {
            AssemblyLocator.Initialize();
            DoString(Goose.Properties.Resources.CLRPackage);
            DoString(Goose.Properties.Resources.Util);
            Import(Assembly.GetAssembly(this.GetType()));
            Import(AssemblyLocator.ResolveAssembly(AssemblyLocator.ResolveReference("mscorlib"), true));
            Import(AssemblyLocator.ResolveAssembly(AssemblyLocator.ResolveReference("System"),true));
        }



        /// <summary>
        /// Imports all namespaces within the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void Import(Assembly assembly)
        {
            var assemblyNamespaces = (from type in assembly.GetExportedTypes() select new { Namespace = type.Namespace, Assembly = assembly.FullName }).Distinct();
            foreach (var assemblyNamespace in assemblyNamespaces)
            {
                DoString(String.Format("import('{0}','{1}')", assemblyNamespace.Assembly, assemblyNamespace.Namespace));
            }

        }


        /// <summary>
        /// Compile and import an assembly. Referenced assemblies will also be imported.
        /// </summary>
        /// <param name="assemblyName">Name the assembly.</param>
        /// <param name="src">The source - a mix of directory paths, file paths and plain C#.</param>
        /// <param name="references">Any CLR references.</param>
        /// <returns>The compiled assembly.</returns>
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
