﻿using Azure.Core;
using LFM.Authorization.Application;
using LFM.Authorization.Core.Messages;
using LFM.Authorization.Repository;
using LFM.Azure.Common.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;


var builder = WebApplication.CreateBuilder(args);
var userManagedIdentityClientId = Environment.GetEnvironmentVariable("Identity__ClientId");
var tokenCredential = AzureCredentialFactory.GetCredential(userManagedIdentityClientId);
builder.Services.AddSingleton<TokenCredential>(tokenCredential);

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