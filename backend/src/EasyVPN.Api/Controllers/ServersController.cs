using EasyVPN.Api.Common;
using EasyVPN.Application.Servers.Queries.GetServer;
using EasyVPN.Application.Servers.Queries.GetServers;
using EasyVPN.Contracts.Servers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("servers")]
[Authorize]
public class ServersController : ApiController
{
    private readonly ISender _sender;

    public ServersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get list of servers. (any auth)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/servers
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetServers()
    {
        var serverResult = await _sender.Send(new GetServersQuery());
        return serverResult.Match(
            result => Ok(result.Select(c => new ServerResponse(
                c.Id,
                new ProtocolResponse(
                    c.Protocol.Id,
                    c.Protocol.Name,
                    c.Protocol.Icon
                ),
                c.Version.ToString()
            ))),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get server by id. (any auth)
    /// </summary>
    /// <param name="serverId">Connection ticket guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/servers/{{serverId}}
    /// </remarks>
    [HttpGet("{serverId:guid}")]
    public async Task<IActionResult> GetServer([FromRoute] Guid serverId)
    {
        var serverResult = await _sender.Send(new GetServerQuery(serverId));
        return serverResult.Match(
            result => Ok(new ServerResponse(
                result.Id,
                new ProtocolResponse(
                    result.Protocol.Id,
                    result.Protocol.Name,
                    result.Protocol.Icon
                ),
                result.Version.ToString()
            )),
            errors => Problem(errors));
    }
}
