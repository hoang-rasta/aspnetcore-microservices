using Basket.API.Data;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.OpenApi;
using StackExchange.Redis;

namespace Basket.API.Persistence.Extensions
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            #region Redis Dependencies

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var redisConfiguration = ConfigurationOptions.Parse(
                    configuration.GetConnectionString("Redis"),
                    true
                );

                return ConnectionMultiplexer.Connect(redisConfiguration);
            });

            #endregion

            #region Project Dependencies

            services.AddTransient<IBasketContext, BasketContext>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(Program));

            #endregion

            #region GrpcSettings

            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
                o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"])
            );

            services.AddScoped<DiscountGrpcService>();

            #endregion


            #region MassTransit-RabbitMQ Configuration

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq(
                    (ctx, cfg) =>
                    {
                        cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    }
                );
            });

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket API", Version = "v1" });
            });

            #endregion

            return services;
        }
    }
}
