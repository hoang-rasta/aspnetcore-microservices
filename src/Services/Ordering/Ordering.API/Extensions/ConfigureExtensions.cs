using EventBus.Messages.Common;
using MassTransit;
using Microsoft.OpenApi;
using Ordering.API.EventBusConsumer;

namespace Ordering.API.Extensions
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection AddApplicationConfigureExtensionsServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            #region Project Dependencies

            services.AddAutoMapper(typeof(Program));
            services.AddScoped<BasketCheckoutConsumer>();
            #endregion

            #region MassTransit-RabbitMQ Configuration

            services.AddMassTransit(config =>
            {
                config.AddConsumer<BasketCheckoutConsumer>();

                config.UsingRabbitMq(
                    (ctx, cfg) =>
                    {
                        cfg.Host(configuration["EventBusSettings:HostAddress"]);

                        cfg.ReceiveEndpoint(
                            EventBusConstants.BasketCheckoutQueue,
                            c =>
                            {
                                c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                            }
                        );
                    }
                );
            });

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });
            });

            #endregion

            return services;
        }
    }
}
