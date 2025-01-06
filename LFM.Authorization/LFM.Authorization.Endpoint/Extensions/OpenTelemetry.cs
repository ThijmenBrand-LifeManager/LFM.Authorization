using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace LFM.Authorization.Endpoint.Extensions;

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
            tracing.AddAspNetCoreInstrumentation(options =>
            {
                options.Filter = httpContext => !httpContext.Request.Path.Equals("/metrics") &&
                                                !httpContext.Request.Path.Equals("/health");
            });
            tracing.AddHttpClientInstrumentation(options =>
            {
                options.FilterHttpRequestMessage = (request) => !request.RequestUri!.Host.Equals("lfm_loki");
            });
            tracing.AddEntityFrameworkCoreInstrumentation(options =>
            {
                options.SetDbStatementForText = true;
                options.SetDbStatementForStoredProcedure = true;
            });
            tracing.AddSource("MassTransit");
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