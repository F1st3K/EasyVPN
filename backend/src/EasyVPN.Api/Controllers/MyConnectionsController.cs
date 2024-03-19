using System.Security.Claims;
using EasyVPN.Api.Common;
using EasyVPN.Application.Connections.Commands.CreateConnection;
using EasyVPN.Application.Connections.Commands.DeleteConnection;
using EasyVPN.Application.Connections.Queries.GetConfig;
using EasyVPN.Application.Connections.Queries.GetConnection;
using EasyVPN.Application.Connections.Queries.GetConnections;
using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Contracts.Connections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("my/connections")]
[Authorize(Roles = Roles.Client)]
public class MyConnectionsController : ApiController
{
    private readonly ISender _sender;

    public MyConnectionsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetConnections()
    {
        if (User.GetCurrentId() is not { } clientId)
            return Forbid();
        
        var getConnectionsResult = 
            await _sender.Send(new GetConnectionsQuery(clientId));
        
        return getConnectionsResult.Match(
            result => Ok(
                result.Select(c => new ConnectionResponse(
                    c.Id, c.ClientId, c.ServerId, c.ExpirationTime))),
            errors => Problem(errors));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateConnection(CreateConnectionRequest request)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Forbid();
        
        var createConnectionResult = 
            await _sender.Send(new CreateConnectionCommand(
                clientId,
                request.ServerId));
        if (createConnectionResult.IsError)
            return Problem(createConnectionResult.ErrorsOrEmptyList);
        
        var createTicketResult =
            await _sender.Send(new CreateConnectionTicketCommand(
                createConnectionResult.Value.Id,
                request.Days,
                request.Description));
        
        return createTicketResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
    
    [HttpPost("extend")]
    public async Task<IActionResult> CreateExtendConnectionTicket(ExtendConnectionRequest request)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Forbid();

        var getConnectionResult =
            await _sender.Send(new GetConnectionQuery(request.ConnectionId));
        if (getConnectionResult.IsError)
            return Problem(getConnectionResult.ErrorsOrEmptyList);

        if (getConnectionResult.Value.ClientId != clientId)
            return NotFound();
        
        var createTicketResult =
            await _sender.Send(new CreateConnectionTicketCommand(
                request.ConnectionId,
                request.Days,
                request.Description));
        
        return createTicketResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
    
    [HttpDelete("{connectionId:guid}")]
    public async Task<IActionResult> DeleteConnection([FromRoute] Guid connectionId)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Forbid();
        
        var getConnectionResult =
            await _sender.Send(new GetConnectionQuery(connectionId));
        if (getConnectionResult.IsError)
            return Problem(getConnectionResult.ErrorsOrEmptyList);

        if (getConnectionResult.Value.ClientId != clientId)
            return NotFound();
        
        var confirmResult = await _sender.Send(
            new DeleteConnectionCommand(connectionId));
        
        return confirmResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
    
    [HttpGet("{connectionId:guid}/config")]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid connectionId)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Forbid();
        
        var configResult = await _sender.Send(new GetConfigQuery(connectionId));
        return configResult.Match(
            result => result.ClientId == clientId 
                ? Ok(new ConnectionConfigResponse(result.ClientId, result.Config))
                : Forbid(),
            errors => Problem(errors));
    }
    
    public  Guid? UserGetCurrentId()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is not null && Guid.TryParse(id, out var guid))
            return guid;
        
        return null;
    }
}