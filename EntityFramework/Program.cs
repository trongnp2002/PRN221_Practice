using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework
{
    internal class Program
    {
        /*
         *  Reference Navigation -> Tao Foreign key (1-nhieu)
         *  Collect Navigation -> Khong tao Fk
         *  Muon tu dong lay du lieu Products trong Category hoac Category trong Product
         *  --> Tai pakage Proxies --> optionsBuilder.UseLazyLoadingProxies(); trong dbcontext
         * InverseProperty: Chỉ định thuộc tính này là khóa ngoại nối đến bảng nào, 
         * Sử dụng trong trường hợp mình muốn 1 sản phẩm có 2 category, tuy nhiên chỉ 1 category là
         * khóa ngoại chính, vì nếu không lúc Product lấy Category sẽ không biết lấy cái nào
         *  
        */
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
        static void InsertProduct()
        {
            using var dbcontext = new ShopDbContext();
            /*  var p1 = new Product()
              {
                  ProductName = "San pham 2",
                  Provider = "Cong ty 2"
              };*/
            /*            p1.ProductName = "San pham 1";
                        p1.Provider = "Cong ty 1";*/
            /*           dbcontext.Add(p1);
             *           
            */
     /*       var products = new object[]
            {
                new Product(){Name = "San pham 3", Provider = "Cong ty 3"},
                new Product(){ProductName = "San pham 4", Provider = "Cong ty 4"},
                new Product(){ProductName = "San pham 5", Provider = "Cong ty 5"}

            };*/
/*            dbcontext.AddRange(products);
*/            int number_rows = dbcontext.SaveChanges();
            Console.WriteLine($"Da chen {number_rows} dong");
        }
        static void ReadProduct()
        {
            using var dbcontext = new ShopDbContext();
            /*  var products = dbcontext.products.Where((p) => p.ProductId >= 3)
                  .OrderByDescending(p =>p.ProductId).ToList();

              products.ForEach((p) => p.PrinInfo());*/
            var product = dbcontext.products.Where((p) => p.ProductId == 2).FirstOrDefault();
            if (product != null)
            {
                product.PrinInfo();
            }
        }

        static void RenameProduct(int id, string name)
        {
            using var dbcontext = new ShopDbContext();
            var product = dbcontext.products.Where((p) => p.ProductId == id).FirstOrDefault();
            if (product != null)
            {
                // product -> DbContext
                // Giam sat su thay doi cua product trong DbContext
                EntityEntry<Product> entry = dbcontext.Entry(product);
                // Tach ra khong con nam trong su giam sat cua DbContext nua
                // SaveChanges cung khong giup update du lieu nua
                entry.State = EntityState.Deleted; 
/*                product.ProductName = name;
*/                int row = dbcontext.SaveChanges();
                Console.WriteLine($"Da cap nhat {row} dong du lieu");

            }
        }

        static void DeleteProduct(int id)
        {
            using var dbcontext = new ShopDbContext();
            var product = dbcontext.products.Where((p) => p.ProductId == id).FirstOrDefault();
            if (product != null)
            {
               dbcontext.Remove(product);
                int row = dbcontext.SaveChanges();
                Console.WriteLine($"Da xoa {row} dong du lieu");

            }
        }

        static void InsertProductData()
        {
            using var dbcontext = new ShopDbContext();
            var products = new List<Product>() {
                new Product(){Name="Iphone SE",CategoryId=2},
                new Product(){Name="MSI GL63 8rc",CategoryId=1},
                new Product(){Name="MSI Headphone GI62",CategoryId=3},
            };
            
            dbcontext.AddRange(products);
            dbcontext.SaveChanges();

        }

        static void InsertCategoryData()
        {
            using var dbcontext = new ShopDbContext();
            var categories = new List<Category>() {
                new Category(){Name="Laptop"},
                new Category(){Name="Smart phone"},
                new Category(){Name="Gaming gear"},
            };

            dbcontext.AddRange(categories);
            dbcontext.SaveChanges();
        }
        static void Main(string[] args)
        {
            DropDatabase();
            CreateDatabase();
            /*     InsertCategoryData();
                 InsertProductData();*/

/*            using var dbcontext = new ShopDbContext();
*/          /*  var product = dbcontext.products.Where(p => p.ProductId == 1).FirstOrDefault();
            var e = dbcontext.Entry(product);
            e.Reference((p) => p.Category).Load();

      */

/*            var category = dbcontext.categories.Where(c => c.Id == 1).FirstOrDefault();
*/    /*        var e = dbcontext.Entry(category);
            e.Collection(c => c.Products).Load();*/
/*            category.PrintInfo();
*/        }
    }
}
