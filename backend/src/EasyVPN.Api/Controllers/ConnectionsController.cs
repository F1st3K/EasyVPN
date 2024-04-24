using EasyVPN.Api.Common;
using EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;
using EasyVPN.Application.Connections.Queries.GetConfig;
using EasyVPN.Application.Connections.Queries.GetConnections;
using EasyVPN.Contracts.Connections;
using EasyVPN.Contracts.Servers;
using EasyVPN.Contracts.Users;
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
    public async Task<IActionResult> GetConnections([FromQuery] Guid? clientId)
    {
        var getConnectionsResult = 
            await _sender.Send(new GetConnectionsQuery(clientId));
        
        return getConnectionsResult.Match(
            result => Ok(
                result.Select(c => new ConnectionResponse(
                    c.Id, 
                    new UserResponse(
                        c.Client.Id,
                        c.Client.FirstName,
                        c.Client.LastName,
                        c.Client.Login,
                        c.Client.Roles.Select(r => r.ToString()).ToArray()),
                    new ServerResponse(
                        c.Server.Id,
                        new ProtocolResponse(
                            c.Server.Protocol.Id,
                            c.Server.Protocol.Name,
                            c.Server.Protocol.Icon),
                        c.Server.Version.ToString()),
                    c.ExpirationTime))),
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
    
    [HttpPut("{connectionId:guid}/reset")]
    public async Task<IActionResult> Reset([FromRoute] Guid connectionId)
    {
        var confirmResult = await _sender.Send(
            new ResetLifetimeConnectionCommand(connectionId));

        return confirmResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}