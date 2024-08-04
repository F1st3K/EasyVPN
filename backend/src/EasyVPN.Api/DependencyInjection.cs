using System.Reflection;
using EasyVPN.Api.Common.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace EasyVPN.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwagger();
        
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, ApiProblemsDetailsFactory>();

        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(op =>
        {
            op.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = Assembly.GetExecutingAssembly().GetName().Name,
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)
            });
            
            op.AddServer(new OpenApiServer { Url = "/" });
            op.AddServer(new OpenApiServer { Url = "/api/" });
            
            op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = 
                    "Enter 'Bearer' [space] and then your token in the text input below. " +
                    "Example: 'Bearer abc123'",
            });

            op.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}