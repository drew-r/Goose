using Goose;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            VM vm = new VM();
            vm.Compile("GooseFeatures", new string[]{
@"
using System;
namespace GooseInYourFaceWhatchaGonnaDo 
{
        public class MotherGoose 
        { 
            public MotherGoose()
            {
                Console.WriteLine(""Moo!"");
            }
            
            public string WhatsUpMotherGoose()
            {                                   
                return ""Space and stuff"";
            }
        }    
}" 
            });

            vm.DoString("m = MotherGoose()");
            Console.WriteLine(vm.DoString("return m:WhatsUpMotherGoose()")[0]);



            
            dynamic motherGoose = vm.DoString("return m")[0];
            string whatsUp = motherGoose.WhatsUpMotherGoose();
            Console.WriteLine(whatsUp);

            LuaFunction func = vm.DoString("return function(whatsUp) Console.WriteLine(whatsUp) end")[0] as LuaFunction;
            func.Call(whatsUp);

            vm.DoString("HelloWorld()");


            Console.ReadLine();
            
        }
    }
}
