using EasyVPN.Api.Common;
using EasyVPN.Application.Connections.Commands.ResetLifetimeConnection;
using EasyVPN.Application.Connections.Queries.GetConfig;
using EasyVPN.Application.Connections.Queries.GetConnection;
using EasyVPN.Application.Connections.Queries.GetConnections;
using EasyVPN.Contracts.Connections;
using EasyVPN.Contracts.Servers;
using EasyVPN.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Administrator;

[Route("connections")]
public class ConnectionsController : ApiControllerBase
{
    private readonly ISender _sender;

    public ConnectionsController(ISender sender)
    {
        _sender = sender;
    }


    /// <summary>
    /// Permanent get list connections. (administrator)
    /// </summary>
    /// <param name="clientId">Filter connection with this client.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/connections?clientId={{clientId}}
    /// </remarks>
    [HttpGet]
    [Authorize(Roles = Roles.Administrator)]
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


    /// <summary>
    /// Permanent get connection by guid. (administrator, payment reviewer)
    /// </summary>
    /// <param name="connectionId">The guid of conneciton.</param>
    /// <returns>Returns information for this connection.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/connections/{{connectionId}}
    /// </remarks>
    [HttpGet("{connectionId:guid}")]
    [Authorize(Roles = Roles.Administrator + "," + Roles.PaymentReviewer)]
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
            errors => Problem(errors));
    }


    /// <summary>
    /// Permanent get config connection by guid. (administrator)
    /// </summary>
    /// <param name="connectionId">The guid of conneciton.</param>
    /// <returns>Returns config information for this connection.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/connections/{{connectionId}}/config
    /// </remarks>
    [HttpGet("{connectionId:guid}/config")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid connectionId)
    {
        var configResult =
            await _sender.Send(new GetConfigQuery(connectionId));
        return configResult.Match(
            result => Ok(new ConnectionConfigResponse(result.ClientId, result.Config)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Permanent reset life time connection by guid. (administrator)
    /// </summary>
    /// <param name="connectionId">The guid of conneciton.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/connections/{{connectionId}}/reset
    /// </remarks>
    [HttpPut("{connectionId:guid}/reset")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Reset([FromRoute] Guid connectionId)
    {
        var confirmResult = await _sender.Send(
            new ResetLifetimeConnectionCommand(connectionId));

        return confirmResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}
