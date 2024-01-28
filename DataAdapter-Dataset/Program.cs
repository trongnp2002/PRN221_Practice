using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace DataAdapter_Dataset
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sqlStringBuilder = new SqlConnectionStringBuilder();
            sqlStringBuilder["Server"] = "localhost, 1433";
            sqlStringBuilder["Database"] = "xtlab";
            sqlStringBuilder["UID"] = "sa";
            sqlStringBuilder["PWD"] = "123456aA@";

            var sqlStringConnection = sqlStringBuilder.ToString();

            using var connection = new SqlConnection(sqlStringConnection);

            connection.Open();

            var dataset = new DataSet();
            /* var table = new DataTable("MyTable");

             dataset.Tables.Add(table);

             table.Columns.Add("STT");
             table.Columns.Add("HoTen");
             table.Columns.Add("Tuoi");

             table.Rows.Add(1, "Nguyen Phuc Trong", 21);
             table.Rows.Add(2, "Nguyen Van A", 22);
             table.Rows.Add(3, "Nguyen Van B", 23);
                        ShowDataTable(table);

 */
            var adapter = new SqlDataAdapter();

            adapter.TableMappings.Add("Table", "NhanVien"); // lay du lieu tu bang nhan vien

            adapter.SelectCommand = new SqlCommand("select NhanviennID, Ten, Ho from NhanVien", connection);
           
            //Thiet lap cho Insert Command
            adapter.InsertCommand = new SqlCommand("insert into NhanVien(Ho, Ten) values(@Ho, @Ten)",connection);
            adapter.InsertCommand.Parameters.Add("@Ho",SqlDbType.NVarChar, 255, "Ho");
            adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
            //Thiet lap cho Delete Command

            adapter.DeleteCommand = new SqlCommand("Delete from NhanVien where NhanviennID = @NhanvienId", connection);
            var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@NhanvienId",SqlDbType.Int));
            pr1.SourceColumn = "NhanviennID"; // Nguon lay du lieu tu cot nao
            pr1.SourceVersion = DataRowVersion.Original; // Lay phien ban nao cua du lieu cap nhat
           
            adapter.UpdateCommand = new SqlCommand("Update NhanVien set Ho = @Ho, Ten = @Ten where NhanviennID = @NhanvienId", connection);
            var pr2 = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@NhanvienId", SqlDbType.Int));
            var pr3 = adapter.UpdateCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
            var pr4 = adapter.UpdateCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");

            pr2.SourceColumn = "NhanviennID"; // Nguon lay du lieu tu cot nao
            pr2.SourceVersion = DataRowVersion.Original;

            adapter.Fill(dataset);// do vao dataset 

            DataTable dataTable = dataset.Tables["NhanVien"];

            ShowDataTable(dataTable);

          /*Insert
           * var row = dataTable.Rows.Add();
            row["Ten"] = "Abc";
            row["Ho"] = "Nguyen Van";
          */
          /*Delete
            var row10 = dataTable.Rows[10];
            row10.Delete();
          */

           /* Update
            * var r = dataTable.Rows[9];
            r["Ten"] = "AnhAnh";
           */

            adapter.Update(dataset);
            connection.Close();
        }

        static void ShowDataTable(DataTable dt)
        {
            int index = 0;
            int number_cols = dt.Columns.Count;
            Console.WriteLine($"Ten bang: {dt.TableName}");
            Console.Write($"{"Index", -10} ");
            foreach (DataColumn c in dt.Columns)
            {
                Console.Write($"{c.ColumnName,-20}");
            }
            Console.WriteLine();

            foreach (DataRow r in dt.Rows)
            {
                Console.Write($"{index,-10}");
                index++;
                for (int i = 0; i < number_cols; i++)
                {
                    Console.Write($"{r[i],-20}");
                }
                Console.WriteLine();
            }

        }
    }
}
