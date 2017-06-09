using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BigSqlRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter file path:");
            var filePath = Console.ReadLine();

            Console.WriteLine("Please enter connection string:");
            var cnnString = Console.ReadLine();

            var cnn = new SqlConnection(cnnString);
            cnn.Open();
            var cmd = new SqlCommand { Connection = cnn };
            var logger = new StreamWriter(Directory.GetCurrentDirectory() + "\\logs.txt", false);

            var fileReader = new StreamReader(File.OpenRead(filePath));
            var statment = "";
            while ((statment = fileReader.ReadLine()) != null)
            {
                try
                {
                    cmd.CommandText = statment;//.Replace("''", "'''");
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    logger.WriteLine(statment);
                }
            }
            cnn.Close();
            fileReader.Close();
            logger.Flush();
            logger.Close();
            fileReader = null;
        }
    }
}
