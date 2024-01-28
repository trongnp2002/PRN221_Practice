using EntityFramework.Models;
using FluentAPI.Models;
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
            Console.WriteLine("OnConfiguring >>>");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating >>>");
            base.OnModelCreating(modelBuilder);
            // entity => FluentAPI Product
            // var product = modelBuilder.Entity(typeof(Product));
            //tuong tu cai tren
            //var product = modelBuilder.Entity<Product>(); 
            //goi fluentAPI thong qua delegate
            modelBuilder.Entity<Product>(
                entity =>
                {
                    //Table mapping
                    entity.ToTable("Products");
                    //Khoa chinh
                    entity.HasKey(p => p.ProductId);
                    //Index
                    entity.HasIndex(p => p.ProductId)
                    .HasDatabaseName("index-product-id");
                    //Relative
                    entity.HasOne(p => p.Category)
                    .WithMany() // 1-n
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Khoa_Ngoai_Product_Category")
                    ;
                    entity.HasOne(p=>p.Category2)
                    .WithMany(c => c.Products)
                    .HasForeignKey("CategoryId2")
                    .OnDelete(DeleteBehavior.Cascade)
                    .OnDelete(DeleteBehavior.NoAction)
                    ;
                    entity.Property(p => p.Name)
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(60)
                    .IsRequired(false)
                    .HasDefaultValue("Ten san pham")
                    ;
                
                }
                );
            modelBuilder.Entity<CategoryDetails>(
                entity =>
                {
                    entity.HasOne(d => d.category)
                    .WithOne(c => c.Details)
                    .HasForeignKey<CategoryDetails>(c => c.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    ;
                }
                );
        }

    }
}
