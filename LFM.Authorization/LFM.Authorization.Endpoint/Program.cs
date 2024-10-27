using LFM.Authorization.Core.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;


var builder = WebApplication.CreateBuilder(args);

var appSettingsFilePath = Environment.GetEnvironmentVariable("APPSETTINGS_FILEPATH");
if (!string.IsNullOrWhiteSpace(appSettingsFilePath))
    builder.Configuration.AddJsonFile(appSettingsFilePath);
    
builder.Services.RegisterMasstransit(builder.Configuration, true);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.Run();