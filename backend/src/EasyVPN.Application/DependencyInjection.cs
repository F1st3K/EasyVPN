using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Expire;
using EasyVPN.Domain.Entities;
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
        services.AddScoped<IExpireService<Connection>, ConnectionExpireService>();
        
        return services;
    }
}