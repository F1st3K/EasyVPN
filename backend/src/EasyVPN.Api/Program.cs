using EasyVPN.Api;
using EasyVPN.Api.Controllers;
using EasyVPN.Application;
using EasyVPN.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler(ErrorsController.Route);
    
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.MigrateDatabase();
    app.StartExpireService();
    
    app.Run();
}



