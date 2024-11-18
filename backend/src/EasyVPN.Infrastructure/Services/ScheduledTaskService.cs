using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Application.Protocols.Queries.GetProtocols;
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

    private readonly Dictionary<Guid, (DateTime expirationTime, IBaseRequest request)> _tasks = new() {
        };
    
    public void AddOrUpdateTask<TResponse>(Guid taskId, (DateTime expirationTime, IRequest<ErrorOr<TResponse>> request) task) =>
        _tasks[taskId] = (task.expirationTime, task.request);
    
    public void RemoveTaskIfExists(Guid taskId) => _tasks.Remove(taskId);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        AddOrUpdateTask( new Guid("00000000-0000-0000-0000-000000000000"), (
                new DateTime(2024, 11, 18, 1, 55, 0),
                new GetProtocolsQuery()
            ));
        AddOrUpdateTask( new Guid("00000001-0000-0000-0000-000000000000"), (
                new DateTime(2024, 11, 18, 1, 55, 0),
                new LoginQuery("admin", "admin")
            ));
        AddOrUpdateTask( new Guid("00000002-0000-0000-0000-000000000000"), (
                new DateTime(2024, 11, 18, 1, 55, 0),
                new CreateConnectionTicketCommand(new Guid("00000000-0000-0000-0000-000000000000"),
                    30, 
                    "Automaticly created",
                    ["automatic"]
                )
            ));
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var t in _tasks
                         .Where(t => t.Value.expirationTime <= dateTimeProvider.UtcNow))
                try
                {
                    using var scope = serviceProvider.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                    
                    var result = (IErrorOr) (await sender.Send(t.Value.request, stoppingToken))!;
                    
                    object r = ((IErrorOr<object?>) result).Value!;

                    if (result.IsError == false)
                    {
                        _tasks.Remove(t.Key);
                        logger.LogInformation("Task {Key} of type {Request} returned: \n{Result}.",
                            t.Key,
                            t.Value.request.GetType().FullName,
                            result.GetType().FullName 
                        );
                    }
                    else
                    {
                        logger.LogError("Task {Key} of type {Request} returned errors: \n\t{Errors}.",
                            t.Key,
                            t.Value.request.GetType().FullName,
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