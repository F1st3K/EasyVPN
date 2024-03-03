using System.Text;
using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Infrastructure.Authentication;
using EasyVPN.Infrastructure.Persistence;
using EasyVPN.Infrastructure.Services;
using EasyVPN.Infrastructure.Vpn;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EasyVPN.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
                .AddExpirationChecker(configuration);
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IServerRepository, ServerRepository>();
        services.AddScoped<IConnectionRepository, ConnectionRepository>();
        services.AddScoped<IVpnServiceFactory, VpnServiceFactory>();
        
        return services;
    }
    
    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var hashSettings = new Settings.Options.Hash();
        configuration.Bind(Settings.Options.Jwt.SectionName, hashSettings);
        services.AddSingleton(Options.Create(hashSettings));
        services.AddSingleton<IHashGenerator, HashGenerator>();
        
        var jwtSettings = new Settings.Options.Jwt();
        configuration.Bind(Settings.Options.Jwt.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new ()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        
        return services;
    }

    private static IServiceCollection AddExpirationChecker(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var expirationSettings = new Settings.Options.Expiration();
        configuration.Bind(Settings.Options.Expiration.SectionName, expirationSettings);
        services.AddSingleton(Options.Create(expirationSettings));
        services.AddSingleton<IExpirationChecker, ExpirationChecker>();
        
        return services;
    }
}