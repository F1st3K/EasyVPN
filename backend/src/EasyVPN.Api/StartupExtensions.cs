using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Entities;
using EasyVPN.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Api;

public static class StartupExtensions
{
    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<EasyVpnDbContext>();
        
        appContext.Database.Migrate();

        return app; 
    }

    public static IApplicationBuilder StartExpireService(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var connectionExpireService = scope.ServiceProvider.GetRequiredService<IExpireService<Connection>>();
        var expirationChecker = app.ApplicationServices.GetRequiredService<IExpirationChecker>();
        
        connectionExpireService.AddAllToTrackExpire();
        expirationChecker.Run();

        return app;
    }
}