using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BigFilesReader_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = args[0], filesSearchPattern = args[1];
            var files = Directory.GetFiles(folderPath, filesSearchPattern,
                args[2] == "all" ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            var filesHeaders = new Dictionary<string, string>();

            if (files.Any())
            {
                foreach (var file in files)
                {
                    var fileReader = new StreamReader(File.OpenRead(file));
                    var firstLine = fileReader.ReadLine();
                    fileReader.Close();
                    fileReader = null;
                    filesHeaders.Add(file, firstLine);
                }
                var resultFile = folderPath + "\\scanresults.txt";
                File.WriteAllLines(resultFile,
                    new List<string> { "Result: " + filesHeaders.All(x => x.Value == filesHeaders.ElementAt(0).Value) });

                var grp = filesHeaders.GroupBy(x => x.Value);
                foreach (var g in grp)
                {
                    File.AppendAllLines(resultFile, new List<string> { g.Key }.Concat(g.ToList().Select(x => x.Key)));
                    File.AppendAllLines(resultFile, new List<string> { "\r\n" });
                }

                var columns = new List<string>();
                foreach (var filesHeader in filesHeaders)
                    columns.AddRange(filesHeader.Value.Split(','));
                
                File.AppendAllLines(resultFile, new List<string> { "\r\nColums: \r\n", string.Join(",", columns.Distinct()) });
            }
            else Console.WriteLine("Files does not exist!");
        }
    }
}
