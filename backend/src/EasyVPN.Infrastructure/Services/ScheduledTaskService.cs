using EasyVPN.Application.Common.Interfaces.Persistence;
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
    IDateTimeProvider dateTimeProvider,
    ITaskRepository taskRepository)
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(expirationOptions.Value.CheckMinutes);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var t in taskRepository.GetTasks()
                         .Where(t => t.Value.execTime <= dateTimeProvider.UtcNow))
                try
                {
                    using var scope = serviceProvider.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                    var result = (IErrorOr)(await sender.Send(t.Value.request, stoppingToken))!;

                    if (result.IsError == false)
                    {
                        taskRepository.RemoveTaskIfExist(t.Key.id, t.Key.typeRequest);
                        logger.LogInformation("Task {Key},\n\tof type {Request},\nreturned:\n\t{Result}.",
                            t.Key.id,
                            t.Key.typeRequest.FullName,
                            t.Value.typeResponse.FullName
                        );
                    }
                    else
                    {
                        logger.LogError("Task {Key},\n\tof type {Request},\nreturned errors:\n\t{Errors}.",
                            t.Key.id,
                            t.Key.typeRequest.FullName,
                            string.Join(", \n\t",
                                result.Errors?.Select(e => $"{e.Code}: {e.Description}") ??
                                Enumerable.Empty<string>())
                        );
                    }
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Unhandled exception");
                }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}