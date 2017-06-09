using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeFilesIntoTable
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportDiffFilesToTable(new[]{@"C:\Users\abdel\Downloads\staar_to_upload\staar_to_upload", "*.csv", @".\sql2016", "DBName",
                "ResultTableName"});
        }


        private static void ImportDiffFilesToTable(string[] args)
        {
            string location = args[0],
                extension = args[1],
                server = args[2],
                database = args[3],
                masterTableName = args[4];

            var cnn = new SqlConnection("Data Source=" + server + ";Database=" + database + ";integrated security=true");
            cnn.Open();

            var filesHeaders = new Dictionary<string, string>();
            var files = Directory.GetFiles(location, extension, SearchOption.AllDirectories);
            Console.WriteLine("Found Files: " + files.Length);
            foreach (var file in files)
            {
                var sr = new StreamReader(File.OpenRead(file));
                var fileHeader = sr.ReadLine();
                fileHeader = fileHeader.Replace(" ", "").Replace(",,", ",_,");
                if (fileHeader.EndsWith(","))
                    fileHeader += "_";
                fileHeader = fileHeader.Replace(",", "], [");
                fileHeader = "[" + fileHeader + "]";
                sr.Close();
                filesHeaders.Add(file, fileHeader);
                var fileName = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - file.LastIndexOf("\\") - 1);
                Console.WriteLine("Processing File: " + fileName);

                fileHeader = fileHeader.Replace(",", " VARCHAR(100),");
                var dropTableScript = "IF OBJECT_ID('dbo." + fileName + "', 'U') IS NOT NULL DROP TABLE dbo." + fileName;
                var createTableScript = "CREATE TABLE " + fileName + "(" + fileHeader + " VARCHAR(100))";
                var insertDataScript = "EXECUTE stp_CommaBulkInsert @filePath,@tableName";
                new SqlCommand(dropTableScript, cnn).ExecuteNonQuery();
                new SqlCommand(createTableScript, cnn).ExecuteNonQuery();
                var insertCmd = new SqlCommand(insertDataScript, cnn);
                insertCmd.Parameters.AddWithValue("filePath", file);
                insertCmd.Parameters.AddWithValue("tableName", fileName);
                insertCmd.CommandTimeout = 0; //wait indefinitely for the procedure to finish import
                insertCmd.ExecuteNonQuery();
            }

            Console.WriteLine("Done Processing files!");

            var masterTableHeader = "";
            if (filesHeaders.All(x => x.Value == filesHeaders.ElementAt(0).Value))
                masterTableHeader = filesHeaders.ElementAt(0).Value;
            else
            {
                var columns = new List<string>();
                foreach (var filesHeader in filesHeaders)
                    columns.AddRange(filesHeader.Value.Split(','));

                masterTableHeader = string.Join(",", columns.Distinct());
            }

            CreateTableFromHeader(server, database, masterTableName, masterTableHeader);
            Console.WriteLine("Done Creating Master Table!");

            Console.WriteLine("Transferring Data to Master Table!");

            foreach (var file in files)
            {
                var fileHeader = filesHeaders[file];
                var fileName = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - file.LastIndexOf("\\") - 1);
                Console.WriteLine("Transferring File: " + fileName);

                var insertDataScript = $"insert into {masterTableName} ({fileHeader}) select {fileHeader} from {fileName}";
                var insertCmd = new SqlCommand(insertDataScript, cnn);
                insertCmd.CommandTimeout = 0; //wait indefinitely for the procedure to finish import
                insertCmd.ExecuteNonQuery();
            }

            Console.WriteLine("Done transferring files!");

            Console.WriteLine("Cleaning tables!");
            foreach (var file in files)
            {
                var fileName = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - file.LastIndexOf("\\") - 1);
                Console.WriteLine("Dropping table: " + fileName);

                var dropTableScript = "IF OBJECT_ID('dbo." + fileName + "', 'U') IS NOT NULL DROP TABLE dbo." + fileName;
                new SqlCommand(dropTableScript, cnn) { CommandTimeout = 0 }.ExecuteNonQuery();
            }
            Console.WriteLine("Done!");

            cnn.Close();
            cnn.Dispose();
        }


        private static void CreateTableFromHeader(string server, string database, string tableName, string fileHeader)
        {
            var cnn = new SqlConnection("Data Source=" + server + ";Database=" + database + ";integrated security=true");
            cnn.Open();

            //DROP & CREATE TABLE
            fileHeader = fileHeader.Replace(",", " VARCHAR(100), ");
            var dropTableScript = "IF OBJECT_ID('dbo." + tableName + "', 'U') IS NOT NULL DROP TABLE dbo." + tableName;
            var createTableScript = "CREATE TABLE " + tableName + "(" + fileHeader + " VARCHAR(100))";
            new SqlCommand(dropTableScript, cnn).ExecuteNonQuery();
            new SqlCommand(createTableScript, cnn).ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
        }



    }
}
