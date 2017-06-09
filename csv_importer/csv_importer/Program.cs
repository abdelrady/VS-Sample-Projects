using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv_importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                @"Password=p@$$w0rd;Persist Security Info=True;User ID=sa;Initial Catalog=g;Data Source=.\sql2016";
            foreach (var file in Directory.GetFiles(@"C:\Users\abdel\Downloads\staar_to_upload\staar_to_upload\", "*.csv", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);
                var dt = GetDataTableFromCsv(file, true);
                using (var bulkCopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                    foreach (DataColumn col in dt.Columns)
                    {
                        var columnName = string.IsNullOrEmpty(col.ColumnName) ? "_" : col.ColumnName;
                        bulkCopy.ColumnMappings.Add(columnName, columnName);
                    }

                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "merge2";
                    bulkCopy.WriteToServer(dt);
                }
            }
        }


        static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                      @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                      ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
