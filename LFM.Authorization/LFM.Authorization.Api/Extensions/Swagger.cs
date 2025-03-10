using Microsoft.OpenApi.Models;

namespace LFM.Authorization.Extensions;

public static class Swagger
{
    public static void AddSwagger(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "LFM.Authorization", Version = "v1"});
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
            
            var openApiUrl = configuration.GetValue<string>("OpenApi:Url");
            options.AddServer(new OpenApiServer
            {
                Url = openApiUrl
            });
        });
    }
}