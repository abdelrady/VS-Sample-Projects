using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URLDomainMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var ch = "";
            while ((ch = Console.ReadLine()) != "q")
            {
                var host = new System.Uri(ch).Host;
                //var domain = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
                Console.WriteLine(host);
                Console.WriteLine();
            }
        }
    }
}
