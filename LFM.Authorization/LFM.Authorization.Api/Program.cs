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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRepositoryModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (enableSwagger)
{
    app.UseSwagger().UseAuthentication();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();
app.MapIdentityApi<IdentityUser>();

app.Run();