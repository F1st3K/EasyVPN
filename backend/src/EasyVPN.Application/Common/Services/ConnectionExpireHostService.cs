using EasyVPN.Application.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyVPN.Application.Common.Services;

public class ConnectionExpireHostService : IHostedService
{
    private readonly IExpirationChecker _expirationChecker;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ConnectionExpireHostService(
        IExpirationChecker expirationChecker,
        IServiceScopeFactory serviceScopeFactory)
    {
        _expirationChecker = expirationChecker;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var services = scope.ServiceProvider;
            var connectionExpireService = services.GetRequiredService<ConnectionExpireService>();
            connectionExpireService.AddActiveConnectionsToTrackExpire();
        }
        
        _expirationChecker.Run();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _expirationChecker.Stop();
        return Task.CompletedTask;
    }

    
}