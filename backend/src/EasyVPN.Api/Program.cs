using EasyVPN.Api;
using EasyVPN.Api.Controllers;
using EasyVPN.Application;
using EasyVPN.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.CreateDocumentationEndpoint();
    app.UseExceptionHandler(ErrorsController.Route);
    app.UseCors("AllowAll");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.MigrateDatabase();
    app.AddScheduledTasks();

    app.Run();
}





