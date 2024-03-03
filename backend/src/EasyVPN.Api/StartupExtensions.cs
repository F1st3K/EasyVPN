using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Service;

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

        var connectionExpireService = scope.ServiceProvider.GetRequiredService<ConnectionExpireService>();
        var expirationChecker = app.ApplicationServices.GetRequiredService<IExpirationChecker>();
        
        connectionExpireService.AddActiveConnectionsToTrackExpire();
        expirationChecker.Run();

        return app;
    }
}