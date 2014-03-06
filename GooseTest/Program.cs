using Goose;
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
        }    
}" 
            });

            vm.DoString("MotherGoose()");

            Console.ReadLine();
            
        }
    }
}
