using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MigrationAndScaffold.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationAndScaffold
{
    public class WebContext:DbContext
    {
        public DbSet<Article> articles { set; get; }        // bảng article
        public DbSet<Tag> tags { set; get; }                // bảng tag

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
            builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning);

        });
        // chuỗi kết nối với tên db sẽ làm  việc đặt là webdb
        public const string ConnectStrring = @"Data Source=localhost,1433;Initial Catalog=webdb;User ID=SA;Password=123456aA@";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectStrring);
            optionsBuilder.UseLoggerFactory(loggerFactory);       // bật logger
        }

/*        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                    builder.AddConsole()
                           .AddFilter(DbLoggerCategory.Database.Command.Name,
                                    LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }*/
    }
}
