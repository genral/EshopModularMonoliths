 using Catalog.Data.Seed; 
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Shared.Data;
using Shared.Data.Interceptors;
using Shared.Data.Seed;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            // add services to the container

            // Api endpoints services

            // Application usecase services 

            // Data- infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

            services.AddDbContext<CatalogDbContext>((sp, opt) =>
            {
                opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opt.UseNpgsql(connectionString);
            });

            services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            // configure the HTTP request pipeline

            // 1. use api endpoint services

            // 2. use application use case servcies

            // 3. use data- infrastructure services
            app.UseMigration<CatalogDbContext>();  
            //InitialiseDatabaseAsnyc(app).GetAwaiter().GetResult();

            return app;
        }

        //private static async Task InitialiseDatabaseAsnyc(IApplicationBuilder app)
        //{
        //    var scope=app.ApplicationServices.CreateScope();

        //    var context=scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        //    await context.Database.MigrateAsync();
        //}
    }
}
