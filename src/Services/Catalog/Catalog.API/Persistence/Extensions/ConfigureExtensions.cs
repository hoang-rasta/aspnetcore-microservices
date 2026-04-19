using Catalog.API.Data;
using Catalog.API.Repository;
using Catalog.API.Repository.Interfaces;
using Catalog.API.Settings;
using Microsoft.Extensions.Options;

namespace Catalog.API.Persistence.Extensions
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            #region Configuration Dependencies

            services.Configure<CatalogDatabaseSettings>(
                configuration.GetSection(nameof(CatalogDatabaseSettings))
            );

            services.AddSingleton<ICatalogDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value
            );

            #endregion

            #region Database Context
            services.AddScoped<ICatalogContext, CatalogContext>();
            #endregion

            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion

            return services;
        }
    }
}
