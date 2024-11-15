using EasyVPN.Application.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Options;
using Options = EasyVPN.Infrastructure.Settings.Options;

namespace EasyVPN.Infrastructure.Services;

public class ScheduledTaskService(
    IOptions<Options.Expiration> expirationOptions,
    ILogger<ScheduledTaskService> logger,
    IServiceProvider serviceProvider,
    IDateTimeProvider dateTimeProvider)
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(expirationOptions.Value.CheckMinutes);

    private readonly Dictionary<Guid, (DateTime expirationTime, IRequest<IErrorOr> request)> _tasks
        = new Dictionary<Guid, (DateTime expirationTime, IRequest<IErrorOr> request)>();
    
    public void AddOrUpdateTask(Guid taskId, (DateTime expirationTime, IRequest<IErrorOr> request) task) =>
        _tasks[taskId] = (task.expirationTime, task.request);
    
    public void RemoveTaskIfExists(Guid taskId) => _tasks.Remove(taskId);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Background Service is running.");
            foreach (var t in _tasks
                         .Where(t => t.Value.expirationTime <= dateTimeProvider.UtcNow))
                try
                {
                    using var scope = serviceProvider.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                    
                    var result = await sender.Send(t.Value.request, stoppingToken);

                    if (result.IsError == false)
                        _tasks.Remove(t.Key);
                    else
                        logger.LogWarning("Task returned the following errors: {Errors}.",
                            string.Join(", ", result.Errors?.Select(e => e.Code) ?? Enumerable.Empty<string>()));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unhandled exception");
                }
         
            await Task.Delay(_interval, stoppingToken);
        }
    }
}