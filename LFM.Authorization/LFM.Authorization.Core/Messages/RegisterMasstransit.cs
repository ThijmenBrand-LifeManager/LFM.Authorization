using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.Core.Messages;

public static class MassTransitExtension
{
    public static IServiceCollection RegisterMasstransit(this IServiceCollection services, IConfiguration configuration, bool enableQueueListener)
    {
        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                var host = configuration["RabbitMq:Host"] ??
                           throw new NullReferenceException("RabbitMq:Host is not defined");

                cfg.Host(host, x => {});

                cfg.UseSendFilter(typeof(SendWorkstreamIdFilter<>), context);

                if (enableQueueListener)
                {
                    var workstreamQueue = configuration["RabbitMq:WorkstreamQueueName"] ??
                                          throw new NullReferenceException("RabbitMq:WorkstreamQueueName is not defined");

                    cfg.ReceiveEndpoint(workstreamQueue, e =>
                    {
                        e.ConfigureConsumers(context);
                    });
                }
            });
        });

        services.Configure<MassTransitHostOptions>(x =>
        {
            x.WaitUntilStarted = true;
        });

        return services;
    }
}