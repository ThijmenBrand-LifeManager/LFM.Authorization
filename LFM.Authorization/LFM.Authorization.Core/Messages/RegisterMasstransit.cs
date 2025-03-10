using System.Reflection;
using Azure;
using Azure.Core;
using Azure.Identity;
using LFM.Azure.Common.Helpers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.Core.Messages;

public static class MassTransitExtension
{
    public static IServiceCollection RegisterMasstransit(this IServiceCollection services, IConfiguration configuration,
        bool enableQueueListener)
    {
        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);
            
            Console.WriteLine("Environment: " + configuration.GetValue<string>("Environment"));

            if (configuration.GetValue<string>("Environment") == "Development")
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["ServiceBus:Host"] ?? throw new NullReferenceException("ServiceBus:Host not defined"), "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    ConfigureServiceBus(ctx, cfg, enableQueueListener, configuration);
                });
            }
            else
            {
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    var connectionString = configuration["ServiceBus:ConnectionString"] ??
                                           throw new NullReferenceException(
                                               "ServiceBus:ConnectionString is not defined");
                    cfg.Host(connectionString);

                    ConfigureServiceBus(context, cfg, enableQueueListener, configuration);
                });
            }
        });

        services.Configure<MassTransitHostOptions>(x => { x.WaitUntilStarted = true; });

        return services;
    }

    private static void ConfigureServiceBus(IBusRegistrationContext ctx, IBusFactoryConfigurator cfg,
        bool enableQueueListener, IConfiguration configuration)
    {
        cfg.UseSendFilter(typeof(SendWorkstreamIdFilter<>), ctx);

        if (enableQueueListener)
        {
            var workstreamQueue = configuration["ServiceBus:WorkstreamQueueName"] ??
                                  throw new NullReferenceException("ServiceBus:WorkstreamQueueName is not defined");
            cfg.ReceiveEndpoint(workstreamQueue, e => { e.ConfigureConsumers(ctx); });
        }
    }
}