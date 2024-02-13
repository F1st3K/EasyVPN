using EasyVPN.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("connections")]
public class Connections : ApiController
{
    [HttpGet]
    public IActionResult GetConnections()
    {
        return Ok();
    }
}