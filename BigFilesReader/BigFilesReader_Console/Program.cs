using System;
using System.IO;
using System.Linq;

namespace BigFilesReader_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter file path:");
            var filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                Console.WriteLine("For line by line press 1, For specific line press 2:");
                var choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    var fileReader = new StreamReader(File.OpenRead(filePath));
                    while ((Console.ReadKey().KeyChar) != 'q')
                        Console.WriteLine(fileReader.ReadLine());
                    fileReader.Close();
                    fileReader = null;
                }
                else
                {
                    Console.WriteLine("Please enter line number: ");
                    var lineNumber = int.Parse(Console.ReadLine());
                    var line = File.ReadLines(filePath).Skip(lineNumber-1).Take(1).FirstOrDefault();
                    if(line!= null)
                        Console.WriteLine("Your Line: \r\n"+line);
                    else Console.WriteLine("End of file!");
                }
            }
            else Console.WriteLine("File does not exist!");
        }
    }
}
