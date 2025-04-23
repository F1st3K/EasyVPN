using EasyVPN.Api.Common;
using EasyVPN.Application.Connections.Commands.CreateConnection;
using EasyVPN.Application.Connections.Commands.DeleteConnection;
using EasyVPN.Application.Connections.Queries.GetConfig;
using EasyVPN.Application.Connections.Queries.GetConnection;
using EasyVPN.Application.Connections.Queries.GetConnections;
using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Contracts.Connections;
using EasyVPN.Contracts.Servers;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Client;

[Route("my/connections")]
[Authorize(Roles = Roles.Client)]
public class MyConnectionsController : ApiControllerBase
{
    private readonly ISender _sender;

    public MyConnectionsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get list of my connecitons. (client)
    /// </summary>
    /// <returns>List connections.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/my/connections
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetConnections()
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

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

    /// <summary>
    /// Create new connection for me. (client)
    /// </summary>
    /// <param name="request">Info for accepted ticket on create new connection.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/my/connections
    /// {
    ///     "serverId": "00000000-0000-0000-0000-000000000000",
    ///     "Days": 30,
    ///     "Description": "I am payment this",
    ///     "Images" : [
    ///       "image1",
    ///       "image2",
    ///       "image3"
    ///     ]
    /// }
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> CreateConnection(CreateConnectionRequest request)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

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
                request.Description,
                request.Images));

        return createTicketResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Create extend ticket for me. (client)
    /// </summary>
    /// <param name="request">Info for accepted ticket on extend connection.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/my/connections/extend
    /// {
    ///     "connectionId": "00000001-0000-0000-0000-000000000000",
    ///     "Days": 30,
    ///     "Description": "I am payment this",
    ///     "Images": [
    ///       "image1",
    ///       "image2",
    ///       "image3"
    ///     ]
    /// }
    /// </remarks>
    [HttpPost("extend")]
    public async Task<IActionResult> CreateExtendConnectionTicket(ExtendConnectionRequest request)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

        var getConnectionResult =
            await _sender.Send(new GetConnectionQuery(request.ConnectionId));
        if (getConnectionResult.IsError)
            return Problem(getConnectionResult.ErrorsOrEmptyList);

        if (getConnectionResult.Value.Client.Id != clientId)
            return Problem(Errors.Access.AnotherUserOnly);

        var createTicketResult =
            await _sender.Send(new CreateConnectionTicketCommand(
                request.ConnectionId,
                request.Days,
                request.Description,
                request.Images));

        return createTicketResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Remove my connection if that expire. (client)
    /// </summary>
    /// <param name="connectionId">Connection guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// DELETE {{host}}/my/connections/{{connectionId}}
    /// </remarks>
    [HttpDelete("{connectionId:guid}")]
    public async Task<IActionResult> DeleteConnection([FromRoute] Guid connectionId)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

        var getConnectionResult =
            await _sender.Send(new GetConnectionQuery(connectionId));
        if (getConnectionResult.IsError)
            return Problem(getConnectionResult.ErrorsOrEmptyList);

        if (getConnectionResult.Value.Client.Id != clientId)
            return Problem(Errors.Access.AnotherUserOnly);

        var confirmResult = await _sender.Send(
            new DeleteConnectionCommand(connectionId));

        return confirmResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get configuration of connection. (client)
    /// </summary>
    /// <param name="connectionId">Connection guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/my/connections/{{connectionId}}/config
    /// </remarks>
    [HttpGet("{connectionId:guid}/config")]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid connectionId)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

        var configResult = await _sender.Send(new GetConfigQuery(connectionId));
        return configResult.Match(
            result => result.ClientId == clientId
                ? Ok(new ConnectionConfigResponse(result.ClientId, result.Config))
                : Problem(Errors.Access.AnotherUserOnly),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get connection info by id. (client)
    /// </summary>
    /// <param name="connectionId">Connection guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/my/connections/{{connectionId}}
    /// </remarks>
    [HttpGet("{connectionId:guid}")]
    public async Task<IActionResult> GetConnection([FromRoute] Guid connectionId)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

        var connectionResult = await _sender.Send(new GetConnectionQuery(connectionId));
        return connectionResult.Match(
            result => result.Client.Id == clientId
                ? Ok(new ConnectionResponse(
                    result.Id,
                    new UserResponse(
                        result.Client.Id,
                        result.Client.FirstName,
                        result.Client.LastName,
                        result.Client.Login,
                        result.Client.Roles.Select(r => r.ToString()).ToArray()),
                    new ServerResponse(
                        result.Server.Id,
                        new ProtocolResponse(
                            result.Server.Protocol.Id,
                            result.Server.Protocol.Name,
                            result.Server.Protocol.Icon),
                        result.Server.Version.ToString()),
                    result.ExpirationTime))
                : Problem(Errors.Access.AnotherUserOnly),
            errors => Problem(errors));
    }
}
