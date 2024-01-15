using Microsoft.Extensions.DependencyInjection;
using Play.Common.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Play.Common.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services)
    {
        services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());

                configure.UsingRabbitMq((context, cfg) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    cfg.Host(rabbitMQSettings.Host);
                    cfg.ConfigureEndpoints(context);
                    cfg.UseMessageRetry(retryConfig => retryConfig.Interval(3, 1000));
                });
            });
        return services;
    }
}