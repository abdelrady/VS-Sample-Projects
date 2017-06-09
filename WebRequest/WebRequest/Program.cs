using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Url: ");
            var url = Console.ReadLine();
            var sb = new StringBuilder();

            foreach (var line in File.ReadAllLines("d:\\MMP_Links.txt"))
            {
                sb.AppendLine(line + "|" + (new WebClient().DownloadData(url + line).Length / 1024));
                Console.WriteLine(line);
                Thread.Sleep(10);
            }
            File.WriteAllText("D:\\MMP_Results.txt", sb.ToString());
        }
    }
}
