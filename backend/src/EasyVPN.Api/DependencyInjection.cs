using EasyVPN.Api.Common.Errors;
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
            op.AddServer(new OpenApiServer { Url = "/api/" });
            op.AddServer(new OpenApiServer { Url = "/" });
        });

        return services;
    }
}