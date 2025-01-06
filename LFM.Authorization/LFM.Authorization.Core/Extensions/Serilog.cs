using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.Destructurers;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.Refit.Destructurers;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;

namespace LFM.Authorization.Core.Extensions;

public static class Serilog
{
    public static void RegisterSerilog(this IServiceCollection services, IConfiguration configuration, string appname)
    {
        var lokiUrl = configuration["Serilog:LokiUrl"];

        var logger = new LoggerConfiguration();
        logger.ReadFrom.Configuration(configuration);
        logger.Enrich.FromLogContext();
        logger.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/metrics")));
        logger.Enrich.WithProperty("Application", appname);
        logger.Enrich.WithEnvironmentName();
        logger.Enrich.WithExceptionDetails(
            new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers([
                    new DbUpdateExceptionDestructurer(),
                    new ApiExceptionDestructurer()
            ]));
        logger.Enrich.WithDemystifiedStackTraces();
        
        logger.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
        logger.WriteTo.Console(new RenderedCompactJsonFormatter());

        logger.WriteTo.GrafanaLoki(lokiUrl, new List<LokiLabel>
        {
            new() { Key = "app", Value = appname },
        }, ["app"]);

        Log.Logger = logger.CreateLogger();

        services.AddSerilog();
        Log.Information("Logger configured");
    }
}