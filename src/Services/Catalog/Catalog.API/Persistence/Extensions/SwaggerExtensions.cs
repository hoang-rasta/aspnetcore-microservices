using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace Catalog.API.Persistence.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Catalog API",
                        Version = "v1",
                        Description = "Catalog Microservice API",
                        Contact = new OpenApiContact { Name = "HA", Email = "ha@example.com" },
                    }
                );

                // XML comments
                var xmlFile =
                    $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }

        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
                    c.RoutePrefix = string.Empty; // Swagger UI root path
                });
            }

            return app;
        }
    }
}
