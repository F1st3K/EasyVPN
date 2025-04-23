using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Common;

[Route("health")]
[AllowAnonymous]
public class HealthController : ApiControllerBase
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
