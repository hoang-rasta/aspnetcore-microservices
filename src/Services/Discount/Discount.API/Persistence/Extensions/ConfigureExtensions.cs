using Discount.API.Repositories;
using Discount.API.Repositories.Interfaces;
using Microsoft.OpenApi;

namespace Discount.API.Persistence.Extensions
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            #region Project Dependencies

            services.AddTransient<IDiscountRepository, DiscountRepository>();

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Discount API",
                        Version = "v1",
                        Description = "Discount Microservice API",
                        Contact = new OpenApiContact { Name = "HA", Email = "ha@example.com" },
                    }
                );
            });

            #endregion

            return services;
        }
    }
}
