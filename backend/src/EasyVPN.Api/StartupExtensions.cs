using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Api;

public static class StartupExtensions
{
    public static IApplicationBuilder UseOnRun(this IApplicationBuilder app)
    {
        app.StartExpireService();
        
        return app;
    }

    private static IApplicationBuilder StartExpireService(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var connectionExpireService = scope.ServiceProvider.GetRequiredService<IExpireService<Connection>>();
        var expirationChecker = app.ApplicationServices.GetRequiredService<IExpirationChecker>();
        
        connectionExpireService.AddAllToTrackExpire();
        expirationChecker.Run();

        return app;
    }
}