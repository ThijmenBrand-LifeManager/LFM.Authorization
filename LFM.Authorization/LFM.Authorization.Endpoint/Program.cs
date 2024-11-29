using LFM.Authorization.Application;
using LFM.Authorization.Core.Messages;
using LFM.Authorization.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;


var builder = WebApplication.CreateBuilder(args);

var appSettingsFilePath = Environment.GetEnvironmentVariable("APPSETTINGS_FILEPATH");
if (!string.IsNullOrWhiteSpace(appSettingsFilePath))
    builder.Configuration.AddJsonFile(appSettingsFilePath);

builder.Services.AddApplicationModule(builder.Configuration);
builder.Services.AddRepositoryModule(builder.Configuration);
    
builder.Services.RegisterMasstransit(builder.Configuration, true);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.Run();