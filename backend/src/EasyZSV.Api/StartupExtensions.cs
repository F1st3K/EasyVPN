using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Connections.Commands.DisableConnection;
using EasyZsV.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyZsV.Api;

public static class StartupExtensions
{
    public static IApplicationBuilder UseDocumentationEndpoint(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<EasyZsvDbContext>();

        appContext.Database.Migrate();

        return app;
    }

    public static IApplicationBuilder AddScheduledTasks(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var connectionRepository = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();
        var taskRepository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();

        foreach (var c in connectionRepository.GetAll())
            taskRepository.PushTask(c.Id, c.ExpirationTime, new DisableConnectionCommand(c.Id));

        return app;
    }
}
