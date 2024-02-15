using System.Security.Claims;
using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Queries.GetConfig;
using EasyVPN.Contracts.Connections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("client/connections")]
[Authorize(Roles = Roles.Client)]
public class ClientConnectionsController : ApiController
{
    private readonly ISender _sender;

    public ClientConnectionsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet("{id:guid}/config")]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid id)
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idClaim is null)
            return Forbid();
        var userId = Guid.Parse(idClaim);
        
        var configResult = await _sender.Send(new GetConfigQuery(id));
        return configResult.Match(
            result => result.ClientId == userId 
                ? Ok(new ConnectionConfigResponse(result.ClientId, result.Config))
                : Forbid(),
            errors => Problem(errors));
    }
}