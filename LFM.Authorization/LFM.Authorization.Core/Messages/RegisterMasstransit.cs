using System.Reflection;
using Azure;
using Azure.Identity;
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

            x.UsingAzureServiceBus((context, cfg) =>
            {
                var host = configuration["ServiceBus:Host"] ??
                           throw new NullReferenceException("ServiceBus:Host is not defined");
                var clientId = configuration["Identity:ClientId"] ??
                               throw new NullReferenceException("Identity:ClientId is not defined");

                cfg.Host(new Uri(host), h =>
                {
                    h.TokenCredential = new ManagedIdentityCredential(clientId);
                });

                cfg.UseSendFilter(typeof(SendWorkstreamIdFilter<>), context);

                if (enableQueueListener)
                {
                    var workstreamQueue = configuration["ServiceBus:WorkstreamQueueName"] ??
                                          throw new NullReferenceException("ServiceBus:WorkstreamQueueName is not defined");

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