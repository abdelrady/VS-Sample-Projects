using System;
using System.IO;
using System.Linq;

namespace BigFilesReader_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var columnSeparator = ',';

            Console.WriteLine("Please enter file path:");
            var filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                Console.WriteLine("Enter Column Name: ");
                var columnName = Console.ReadLine().ToLower();
                Console.WriteLine("Enter Column value: ");
                var columnValue = Console.ReadLine().ToLower();
                if (!string.IsNullOrEmpty(columnName))
                {
                    var fileReader = new StreamReader(File.OpenRead(filePath));
                    var header = fileReader.ReadLine();
                    if (header != null)
                    {
                        var columnIndex = header.ToLower().Split(columnSeparator).ToList().IndexOf(columnName);
                        var valueFound = false;
                        while (true)
                        {
                            var line = fileReader.ReadLine();
                            if (line == null)
                                break;
                            var cell = line.Split(columnSeparator)[columnIndex].ToLower();
                            if (cell == columnValue)
                            {
                                valueFound = true;
                                break;
                            }
                        }
                        Console.WriteLine(valueFound ? "Value found in file!" : "Value not found in file!");
                    }
                    else
                    {
                        Console.WriteLine("File is empty!");
                    }

                    fileReader.Close();
                    fileReader = null;
                }

            }
            else Console.WriteLine("File does not exist!");
        }
    }
}
