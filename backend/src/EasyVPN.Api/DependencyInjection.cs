using EasyVPN.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EasyVPN.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, ApiProblemsDetailsFactory>();

        return services;
    }
}