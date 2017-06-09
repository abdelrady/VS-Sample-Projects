using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JSON_TO_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            var json =
                "[{\"Text\":\"ABC\",\"Choice\":\"A\",\"Yes/No\":true},{\"Text\":\"EFG\",\"Choice\":\"B\",\"Yes/No\":false}]";
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

            var json2 = "[{\"Text2\":\"ABC\",\"Choice2\":\"B\",\"Question\":false}]";
            DataTable dt2 = (DataTable)JsonConvert.DeserializeObject(json2, (typeof(DataTable)));

            var dt3 = new DataTable("Concat");
            dt3.Merge(dt);
            dt3.Merge(dt2);

            var dt4 = ContactTables(dt2, dt);

            //Result table
            //Build new columns
            //DataColumn[] newcolumns = new DataColumn[First.Columns.Count];
            //for (int i = 0; i < First.Columns.Count; i++)
            //{
            //    newcolumns[i] = new DataColumn(First.Columns[i].ColumnName, First.Columns[i].DataType);
            //}
            //add new columns to result table
            DataTable table = new DataTable("Union");
            DataColumn[] newcolumns = new DataColumn[2];
            newcolumns[0] = new DataColumn("Text", typeof(string));
            newcolumns[1] = new DataColumn("Yes/No", typeof(string));
            table.Columns.AddRange(newcolumns);

            Union(dt, ref table);
            Union(dt2, ref table);

            Console.WriteLine(table.Rows.Count);

        }

        public static DataTable ContactTables(DataTable dt1, DataTable dt2)
        {
            var dt3 = new DataTable();

            var columns = dt1.Columns.Cast<DataColumn>()
                              .Concat(dt2.Columns.Cast<DataColumn>());

            foreach (var column in columns)
            {
                dt3.Columns.Add(column.ColumnName, column.DataType);
            }

            var rowsCount = dt1.Rows.Count > dt2.Rows.Count ? dt1.Rows.Count : dt2.Rows.Count;

            for (int i = 0; i < rowsCount; i++)
            {
                var row = dt3.NewRow();
                row.ItemArray = (dt1.Rows.Count == i ? new object[dt1.Columns.Count] : dt1.Rows[i].ItemArray)
                    .Concat((dt2.Rows.Count == i ? new object[0] : dt2.Rows[i].ItemArray)).ToArray();

                dt3.Rows.Add(row);
            }

            return dt3;
        }


        public static void Union(DataTable input, ref DataTable table)
        {
            foreach (DataRow row in input.Rows)
            {
                var r = table.NewRow();
                foreach (DataColumn column in table.Columns)
                {
                    r[column] = row[column.ColumnName];
                }
                table.Rows.Add(r);
            }
        }

        public static DataTable Union(DataTable First, DataTable Second)
        {
            //Result table
            DataTable table = new DataTable("Union");
            //Build new columns
            DataColumn[] newcolumns = new DataColumn[First.Columns.Count];
            for (int i = 0; i < First.Columns.Count; i++)
            {
                newcolumns[i] = new DataColumn(First.Columns[i].ColumnName, First.Columns[i].DataType);
            }
            //add new columns to result table
            table.Columns.AddRange(newcolumns);
            table.BeginLoadData();
            //Load data from first table
            foreach (DataRow row in First.Rows)
            {
                table.LoadDataRow(row.ItemArray, true);
            }
            //Load data from second table
            foreach (DataRow row in Second.Rows)
            {
                table.LoadDataRow(row.ItemArray, true);
            }
            table.EndLoadData();
            return table;
        }

        private static DataTable JoinDataTables(DataTable t1, DataTable t2, params Func<DataRow, DataRow, bool>[] joinOn)
        {
            DataTable result = new DataTable();
            foreach (DataColumn col in t1.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataColumn col in t2.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataRow row1 in t1.Rows)
            {
                var joinRows = t2.AsEnumerable().Where(row2 =>
                {
                    foreach (var parameter in joinOn)
                    {
                        if (!parameter(row1, row2)) return false;
                    }
                    return true;
                });
                foreach (DataRow fromRow in joinRows)
                {
                    DataRow insertRow = result.NewRow();
                    foreach (DataColumn col1 in t1.Columns)
                    {
                        insertRow[col1.ColumnName] = row1[col1.ColumnName];
                    }
                    foreach (DataColumn col2 in t2.Columns)
                    {
                        insertRow[col2.ColumnName] = fromRow[col2.ColumnName];
                    }
                    result.Rows.Add(insertRow);
                }
            }
            return result;
        }
    }
}
