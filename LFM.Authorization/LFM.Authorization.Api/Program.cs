using Azure.Core;
using FluentValidation;
using LFM.Authorization.Application;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.Core;
using LFM.Authorization.Core.Extensions;
using LFM.Authorization.Core.Messages;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Extensions;
using LFM.Authorization.Repository;
using LFM.Azure.Common.Authentication;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var userManagedIdentityClientId = Environment.GetEnvironmentVariable("Identity__ClientId");
var tokenCredential = AzureCredentialFactory.GetCredential(userManagedIdentityClientId);
builder.Services.AddSingleton<TokenCredential>(tokenCredential);

builder.Services.AddEndpointsApiExplorer();

var enableSwagger = builder.Configuration.GetValue<bool>("OpenApi:ShowDocument");
if (enableSwagger)
{
    builder.Services.AddSwagger(builder.Configuration);
}

builder.Services.RegisterOpenTelementry(builder.Configuration, builder.Environment.ApplicationName);
builder.Services.RegisterSerilog(builder.Configuration, builder.Environment.ApplicationName);

builder.Services.AddCoreModule(builder.Configuration);
builder.Services.AddRepositoryModule(builder.Configuration);
builder.Services.AddApplicationModule(builder.Configuration);

builder.Services.AddIdentity<LfmUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 12;
    })
    .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddLfmAuthorization(builder.Configuration);

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.RegisterMasstransit(builder.Configuration, enableQueueListener: false);

const string CorsDevelopmentPolicy = "local_development";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsDevelopmentPolicy, policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (enableSwagger)
{
    app.UseSwagger().UseAuthentication();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.MapControllers();
app.MapPrometheusScrapingEndpoint();

app.UseCors(CorsDevelopmentPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.Run();