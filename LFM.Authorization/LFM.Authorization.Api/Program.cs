using LFM.Authorization.AspNetCore;
using LFM.Authorization.Core;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Extensions;
using LFM.Authorization.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var enableSwagger = builder.Configuration.GetValue<bool>("OpenApi:ShowDocument");
if (enableSwagger)
{
    builder.Services.AddSwagger();
}
builder.Services.AddCoreModule(builder.Configuration);
builder.Services.AddRepositoryModule(builder.Configuration);

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

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();