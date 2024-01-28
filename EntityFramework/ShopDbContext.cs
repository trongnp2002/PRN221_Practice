using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    public class ShopDbContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();

        });
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        private const string connectString = @"
                Data Source=127.0.0.1,1433;
                Initial Catalog=shopdata;
                User ID=sa;Password=123456aA@;
                TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
