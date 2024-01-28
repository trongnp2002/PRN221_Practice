using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Database
{
    internal class Program
    {
        /*
         command.ExecuteReader(): Dùng khi kết quả trả về có nhiều dòng;
         command.ExecuteScalar(): Chỉ trả về giá trị ở dòng 1 cột 1; (Count, sum , avarage v.v)
         command.ExecuteNonQuery(): Không lấy tập kết quả truy vấn được từ server mà trả về tổng số dòng bị tác động
        bới câu truy vấn đó (Insert, Update, Delete)

         
        */
        static void SelectTopKhacHang(SqlConnection connection, int top, int id)
        {
            try
            {
                connection.Open();
                using DbCommand command = new SqlCommand();
                Console.WriteLine("Ket noi thanh cong");
                command.Connection = connection;
                command.CommandText = "SELECT TOP (@top) * FROM [xtlab].[dbo].[Khachhang] where KhachhangId > @id";
                /*     var topParameter = new SqlParameter("@top",5);
                     command.Parameters.Add(topParameter);*/
                var topCommand = command.Parameters.Add(new SqlParameter("@top", top));
                var idCommand = command.Parameters.Add(new SqlParameter("@id", id));
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"{dataReader["HoTen"],-20} Quoc gia {dataReader["QuocGia"],-8}");
                    }
                }
                else
                {
                    Console.WriteLine("Khong co gia tri");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ket noi that bai");
                Console.WriteLine(ex.Message);

            }

        }
        static void procedure(SqlConnection connection, int id)
        {
            try
            {
                connection.Open();
                Console.WriteLine("Ket noi thanh cong");
                using var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "getProductInfo";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", id));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    var tensp = reader[0];
                    var tendm = reader[1];
                    Console.WriteLine($"ten san pham: {tensp} - ten danh muc: {tendm}");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ket noi that bai");
                Console.WriteLine(ex.Message);

            }

        }

        static void Main(string[] args)
        {
            var sqlStringBuilder = new SqlConnectionStringBuilder();
            sqlStringBuilder["Server"] = "localhost, 1433";
            sqlStringBuilder["Database"] = "xtlab";
            sqlStringBuilder["UID"] = "sa";
            sqlStringBuilder["PWD"] = "123456aA@";

            var sqlStringConnection = sqlStringBuilder.ToString();

            using var connection = new SqlConnection(sqlStringConnection);

            SelectTopKhacHang(connection, 10, 1000);
            procedure(connection, 4);




        }
    }
}
