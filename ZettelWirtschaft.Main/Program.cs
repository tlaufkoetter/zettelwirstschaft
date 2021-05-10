using System.Reflection;
using System;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using ZettelWirtschaft.Engine.Zettel;
using ZettelWirtschaft.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ZettelWirtschaft.AvaloniaUI;

namespace ZettelWirtschaft.Main
{
    public class Program
    {
        public class BloggingContextFactory : IDesignTimeDbContextFactory<ZettelDbContext>
        {
            public static string connectionString = "Data Source=zettelkasten.db";
            public ZettelDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ZettelDbContext>();
                optionsBuilder.UseSqlite();

                return new ZettelDbContext(optionsBuilder.Options);
            }
        }
        public static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddMediatR(typeof(ZettelEntity));
            serviceCollection.AddAutoMapper(typeof(DataMappingProfile), typeof(UiMappingProfile));
            serviceCollection.AddDbContext<ZettelDbContext>(options => options.UseSqlite(BloggingContextFactory.connectionString), ServiceLifetime.Singleton);
            serviceCollection.AddTransient<IGetMultipleZettelRepository, GetMultipleZettelRepository>();
            serviceCollection.AddTransient<IGetZettelDetailQueryRepository, GetZettelDetailQueryRepository>();
            serviceCollection.AddTransient<IUpdateZettelRepository, UpdateZettelRepository>();
            serviceCollection.AddTransient<IZettelCreationRepository, ZettelCreationRepository>();
            var startup = new Startup();
            startup.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            DataSeed.SeedData(serviceProvider.GetRequiredService<ZettelDbContext>()).Wait();

            startup.Start(args);
        }
    }
}
