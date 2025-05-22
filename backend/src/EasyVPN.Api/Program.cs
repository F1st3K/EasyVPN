using EasyVPN.Api;
using EasyVPN.Api.Controllers.Common;
using EasyVPN.Application;
using EasyVPN.Infrastructure;
using Microsoft.Extensions.Options;
using Options = EasyVPN.Infrastructure.Settings.Options;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.Configure<Options.Features>(
        builder.Configuration.GetSection(Options.Features.SectionName));

    builder.Services.AddCors(options =>
        options.AddPolicy("AllowAll", policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()));

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    var f = app.Services.GetRequiredService<IOptions<Options.Features>>().Value;

    if (f.UseDocumentationEndpoint)
        app.UseDocumentationEndpoint();

    if (f.UseExceptionHandler)
        app.UseExceptionHandler(ErrorsController.Route);

    if (f.UseCors)
        app.UseCors("AllowAll");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    if (f.MigrateDatabase)
        app.MigrateDatabase();

    if (f.AddScheduledTasks)
        app.AddScheduledTasks();

    app.Run();
}
