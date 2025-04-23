using EasyVPN.Application.Connections.Commands.AddLifetimeConnection;
using EasyVPN.Application.Connections.Commands.CreateConnection;
using EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;
using EasyVPN.Application.Connections.Queries.GetConfig;
using EasyVPN.Application.Connections.Queries.GetConnection;
using EasyVPN.Application.Connections.Queries.GetConnections;
using EasyVPN.Contracts.Connections;
using EasyVPN.Contracts.Servers;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.ConnectionRegulator;

[Route("connections")]
public class ConnectionsController : ApiControllerBase
{
    private readonly ISender _sender;

    public ConnectionsController(ISender sender)
    {
        _sender = sender;
    }


    /// <summary>
    /// Permanent get list connections. (connection regulator)
    /// </summary>
    /// <param name="clientId">Filter connection with this client.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/connections?clientId={{clientId}}
    /// </remarks>
    [HttpGet]
    [Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
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
            Problem);
    }


    /// <summary>
    /// Permanent get connection by guid. (connection regulator, payment reviewer)
    /// </summary>
    /// <param name="connectionId">The guid of connection.</param>
    /// <returns>Returns information for this connection.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/connections/{{connectionId}}
    /// </remarks>
    [HttpGet("{connectionId:guid}")]
    [Authorize(Roles = $"{nameof(RoleType.ConnectionRegulator)}, {nameof(RoleType.PaymentReviewer)})]")]
    public async Task<IActionResult> GetConnection([FromRoute] Guid connectionId)
    {
        var configResult =
            await _sender.Send(new GetConnectionQuery(connectionId));
        return configResult.Match(
            result => Ok(new ConnectionResponse(
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
                    result.ExpirationTime)),
            Problem);
    }

    /// <summary>
    /// Permanent create new connection for client. (connection regulator)
    /// </summary>
    /// <param name="serverId">Guid of the server to which the connection is created.</param>
    /// <param name="clientId">Client guid for which the connection is being created.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/connections/?serverId={{serverId}}clientId={{clientId}}
    /// </remarks>
    [HttpPost]
    [Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
    public async Task<IActionResult> CreateConnection(
        [FromQuery] Guid serverId, [FromQuery] Guid clientId)
    {
        var createConnectionResult =
            await _sender.Send(new CreateConnectionCommand(
                clientId,
                serverId));

        return createConnectionResult.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Permanent get config connection by guid. (connection regulator)
    /// </summary>
    /// <param name="connectionId">The guid of connection.</param>
    /// <returns>Returns config information for this connection.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/connections/{{connectionId}}/config
    /// </remarks>
    [HttpGet("{connectionId:guid}/config")]
    [Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid connectionId)
    {
        var configResult =
            await _sender.Send(new GetConfigQuery(connectionId));
        return configResult.Match(
            result => Ok(new ConnectionConfigResponse(result.ClientId, result.Config)),
            Problem);
    }

    /// <summary>
    /// Permanent reset lifetime connection by guid. (connection regulator)
    /// </summary>
    /// <param name="connectionId">The guid of connection.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/connections/{{connectionId}}/reset
    /// </remarks>
    [HttpPut("{connectionId:guid}/reset")]
    [Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
    public async Task<IActionResult> Reset([FromRoute] Guid connectionId)
    {
        var confirmResult = await _sender.Send(
            new ResetLifetimeConnectionCommand(connectionId));

        return confirmResult.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Permanent extend lifetime connection by guid. (connection regulator)
    /// </summary>
    /// <param name="connectionId">The guid of connection.</param>
    /// <param name="days">The count days connection extended.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    /// PUT {{host}}/connections/{{connectionId}}/extend?days=3
    /// </remarks>
    [HttpPut("{connectionId:guid}/extend")]
    [Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
    public async Task<IActionResult> Extend([FromRoute] Guid connectionId, [FromQuery] uint days = 0)
    {
        var confirmResult = await _sender.Send(
            new AddLifetimeConnectionCommand(connectionId, (int)days));

        return confirmResult.Match(
            _ => Ok(),
            Problem);
    }
}
