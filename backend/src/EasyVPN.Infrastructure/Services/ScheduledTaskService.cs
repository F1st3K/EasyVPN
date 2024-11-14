using Microsoft.Extensions.Hosting;

namespace EasyVPN.Infrastructure.Services;

public class ScheduledTaskService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    private async Task Do(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await someService.DoSomeWorkAsync();
            }
            catch (Exception ex)
            {
                // обработка ошибки однократного неуспешного выполнения фоновой задачи
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}