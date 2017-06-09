using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Calc.Utils.Add(1, 5));
            Console.WriteLine(typeof(Calc.Utils).Assembly.FullName);
            Console.WriteLine(typeof(Calc.Utils).Assembly.CodeBase);
            Console.WriteLine(typeof(Calc.Utils).AssemblyQualifiedName);

        }
    }
}
