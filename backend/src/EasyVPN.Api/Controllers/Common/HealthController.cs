using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Common;

[Route("health")]
[AllowAnonymous]
public class HealthController : ApiControllerBase
{
    private static readonly string Version = Assembly
        .GetExecutingAssembly()
        .GetName()
        .Version?
        .ToString(3) ?? "unknown";
    
    private static readonly Stopwatch Uptime = Stopwatch.StartNew();
    
    /// <summary>
    /// Health api (anywhere)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return Ok(new
        {
            status = "Healthy",
            version = Version,
            uptime = Uptime.Elapsed.ToString(@"dd\.hh\:mm\:ss")
        });
    }
}
