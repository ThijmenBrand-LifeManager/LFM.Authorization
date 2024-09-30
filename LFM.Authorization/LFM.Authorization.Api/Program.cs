using LFM.Authorization.Extensions;
using LFM.Authorization.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme).AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRepositoryModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();
app.MapIdentityApi<IdentityUser>();

app.Run();