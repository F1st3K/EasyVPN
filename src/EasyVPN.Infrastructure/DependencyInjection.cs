using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace EasyVPN.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
}