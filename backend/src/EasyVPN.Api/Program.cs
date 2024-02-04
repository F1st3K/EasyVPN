using EasyVPN.Api.Common.Errors;
using EasyVPN.Application;
using EasyVPN.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, ApiProblemsDetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
