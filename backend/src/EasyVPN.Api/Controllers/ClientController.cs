using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Application.Vpn.Queries.GetConfig;
using EasyVPN.Application.Vpn.Queries.GetConnections;
using EasyVPN.Contracts.Connections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("client")]
[Authorize(Roles = Roles.Administrator)]
public class ClientController : ApiController
{
    private readonly ISender _sender;

    public ClientController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("{clientId:guid}/connections")]
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