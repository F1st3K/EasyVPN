using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Application.Common.Interfaces.Expire;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Application.Protocols.Queries.GetProtocols;
using EasyVPN.Domain.Entities;
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
        
        taskRepository.AddTask( new Guid("00000000-0000-0000-0000-000000000000"),
                new DateTime(2024, 11, 18, 1, 55, 0),
                new GetProtocolsQuery()
            );
        taskRepository.AddTask( new Guid("00000001-0000-0000-0000-000000000000"),
                new DateTime(2024, 11, 18, 1, 55, 0),
                new LoginQuery("admin", "admin")
            );
        taskRepository.AddTask( new Guid("00000002-0000-0000-0000-000000000000"),
                new DateTime(2024, 11, 18, 1, 55, 0),
                new CreateConnectionTicketCommand(new Guid("b045a69e-33a9-4fb6-9e47-1f41afe43c4b"),
                    30, 
                    "Automaticly updated",
                    ["automatic"]
                )
            );
        taskRepository.AddTask( new Guid("00000003-0000-0000-0000-000000000000"),
                new DateTime(2024, 11, 18, 1, 55, 0),
                new CreateConnectionTicketCommand(new Guid("00000000-0000-0000-0000-000000000000"),
                    30, 
                    "Automaticly updated",
                    ["automatic"]
                )
            );

        return app;
    }
}
