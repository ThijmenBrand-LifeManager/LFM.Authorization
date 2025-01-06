using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace LFM.Authorization.Core.Extensions;

public static class OpenTelemetry
{
    public static void RegisterOpenTelementry(this IServiceCollection services, IConfiguration configuration,
        string applicationName)
    {
        var tracingOtpEndpoint = configuration.GetValue<string>("Logging:OpenTelemetry:Endpoint");
        var otel = services.AddOpenTelemetry();

        otel.ConfigureResource(resource => resource.AddService(serviceName: applicationName));
        otel.WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddPrometheusExporter();

            if (tracingOtpEndpoint != null)
            {
                metrics.AddOtlpExporter(oltpOptions =>
                {
                    oltpOptions.Endpoint = new Uri(tracingOtpEndpoint);
                    oltpOptions.ExportProcessorType = ExportProcessorType.Batch;
                    oltpOptions.Protocol = OtlpExportProtocol.Grpc;
                });
            }
        });

    otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            if (tracingOtpEndpoint != null)
            {
                tracing.AddOtlpExporter(oltpOptions =>
                {
                    oltpOptions.Endpoint = new Uri(tracingOtpEndpoint);
                    oltpOptions.ExportProcessorType = ExportProcessorType.Batch;
                    oltpOptions.Protocol = OtlpExportProtocol.Grpc;
                });
            }
            else 
            {
                tracing.AddConsoleExporter();
            }
        });
    }
}