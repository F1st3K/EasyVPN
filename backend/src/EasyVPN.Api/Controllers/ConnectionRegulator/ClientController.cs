using EasyVPN.Application.Connections.Commands.CreateConnection;
using EasyVPN.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.ConnectionRegulator;

[Route("client")]
[Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
public class ClientController : ApiControllerBase
{
    private readonly ISender _sender;

    public ClientController(ISender sender)
    {
        _sender = sender;
    }


    /// <summary>
    /// Permanent create new connection for client. (administrator)
    /// </summary>
    /// <param name="serverId">Guid of the server to which the connection is created.</param>
    /// <param name="clientId">Client guid for which the connection is being created.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/client/{{clientId}}/connections?serverId={{serverId}}
    /// </remarks>
    [HttpPost("{clientId:guid}/connections")]
    public async Task<IActionResult> CreateConnection(
        [FromQuery] Guid serverId, [FromRoute] Guid clientId)
    {
        var createConnectionResult =
            await _sender.Send(new CreateConnectionCommand(
                clientId,
                serverId));

        return createConnectionResult.Match(
            _ => Ok(),
            Problem);
    }
}
