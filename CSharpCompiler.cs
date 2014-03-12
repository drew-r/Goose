using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;

namespace Goose
{
    static class CSharpCompiler
    {

        public static Assembly Compile(string assemblyName, string[] src) 
        {
            return Compile(assemblyName, src, null);
        }
        public static Assembly Compile(string outputPath, string[] src, params string[] referencedAssemblies)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
        
            CompilerParameters paras = new CompilerParameters() { OutputAssembly = outputPath };            
            
            if (referencedAssemblies != null) 
            {
                foreach (string referencedAssembly in referencedAssemblies)
                {
                    paras.ReferencedAssemblies.Add(referencedAssembly);                      
                }
            }

            CompilerResults results = codeProvider.CompileAssemblyFromSource(paras,src);
            
            if (results.Errors.Count > 0)  
            {
                string msg = "";
                foreach (CompilerError err in results.Errors)
                {
                    msg = String.Concat(msg, err.ErrorNumber, ": ", err.ErrorText," @ ", 
                    err.FileName , " L" , err.Line , " C" , err.Column);
                }
                throw new Exception("Compilation error(s)..." + msg);
            }
            return results.CompiledAssembly;         
            
        }


      
        
        
    }
}
