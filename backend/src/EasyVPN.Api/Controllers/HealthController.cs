using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("health")]
[AllowAnonymous]
public class HealthController : ApiController
{
    /// <summary>
    /// Health api (any)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return Ok();
    }
}
