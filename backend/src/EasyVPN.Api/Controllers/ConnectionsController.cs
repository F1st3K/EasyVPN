using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Queries.GetConfig;
using EasyVPN.Application.Vpn.Queries.GetConnections;
using EasyVPN.Contracts.Connections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("connections")]
[Authorize(Roles = Roles.Administrator)]
public class ConnectionsController : ApiController
{
    private readonly ISender _sender;

    public ConnectionsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetConnections()
    {
        var getConnectionsResult = 
            await _sender.Send(new GetConnectionsQuery());
        
        return getConnectionsResult.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
    
    
    [HttpGet("{connectionId:guid}/config")]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid connectionId)
    {
        var configResult = 
            await _sender.Send(new GetConfigQuery(connectionId));
        return configResult.Match(
            result => Ok(new ConnectionConfigResponse(result.ClientId, result.Config)),
            errors => Problem(errors));
    }
}