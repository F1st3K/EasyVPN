using EasyVPN.Application.Common.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EasyVPN.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddAppServices();
        return services;
    }
    
    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ConnectionExpireService>();
        
        return services;
    }
}