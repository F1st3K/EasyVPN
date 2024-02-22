using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Application.Vpn.Queries.GetConfig;
using EasyVPN.Application.Vpn.Queries.GetConnections;
using EasyVPN.Contracts.Connections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("client/{clientId:guid}/connections")]
[Authorize(Roles = Roles.Administrator)]
public class ClientConnectionsController : ApiController
{
    private readonly ISender _sender;

    public ClientConnectionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetConnections([FromRoute] Guid clientId)
    {
        var getConnectionsResult = 
            await _sender.Send(new GetConnectionsQuery(clientId));
        
        return getConnectionsResult.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateConnection(
        CreateConnectionRequest request, [FromRoute] Guid clientId)
    {
        var createConnectionResult = 
            await _sender.Send(new CreateConnectionCommand(
                clientId,
                request.ServerId,
                request.CountDays));
        
        return createConnectionResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}