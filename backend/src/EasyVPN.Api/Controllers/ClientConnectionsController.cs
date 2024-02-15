using System.Security.Claims;
using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
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
        if (GetCurrentId() is not { } clientId)
            return Forbid();
        
        var configResult = await _sender.Send(new GetConfigQuery(id));
        return configResult.Match(
            result => result.ClientId == clientId 
                ? Ok(new ConnectionConfigResponse(result.ClientId, result.Config))
                : Forbid(),
            errors => Problem(errors));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateConnection(CreateConnectionRequest request)
    {
        if (GetCurrentId() is not { } clientId)
            return Forbid();
        
        var createConnectionResult = 
            await _sender.Send(new CreateConnectionCommand(
                clientId,
                request.ServerId,
                request.CountDays));
        
        return createConnectionResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    private Guid? GetCurrentId()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is not null && Guid.TryParse(id, out var guid))
            return guid;
        
        return null;
    }
}