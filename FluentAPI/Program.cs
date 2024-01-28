using EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;

namespace FluentAPI
{
    //FluentAPI > Attribute
    internal class Program
    {
        static void CreateDatabase()
        {
            try
            {
                using var dbcontext = new ShopDbContext();
                string dbname = dbcontext.Database.GetDbConnection().Database;
                var kq = dbcontext.Database.EnsureCreated();// kiểm tra nếu không tồn tại database sẽ tạo ra database đó
                if (kq)
                {
                    Console.WriteLine($"Tao co so du lieu {dbname} thanh cong");
                }
                else
                {
                    Console.WriteLine($"Tao co so du lieu {dbname} that bai");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void DropDatabase()
        {
            try
            {
                using var dbcontext = new ShopDbContext();
                string dbname = dbcontext.Database.GetDbConnection().Database;
                var kq = dbcontext.Database.EnsureDeleted();// kiểm tra nếu không tồn tại database sẽ tạo ra database đó
                if (kq)
                {
                    Console.WriteLine($"Xoa co so du lieu {dbname} thanh cong");
                }
                else
                {
                    Console.WriteLine($"Xoa co so du lieu {dbname} that bai");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void Main(string[] args)
        {
            DropDatabase();
            CreateDatabase();
        }
    }
}
