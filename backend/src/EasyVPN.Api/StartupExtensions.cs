using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Application.Protocols.Queries.GetProtocols;
using EasyVPN.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Api;

public static class StartupExtensions
{
    public static IApplicationBuilder CreateDocumentationEndpoint(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<EasyVpnDbContext>();

        appContext.Database.Migrate();

        return app;
    }

    public static IApplicationBuilder AddScheduledTasks(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var taskRepository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
        

        return app;
    }
}
