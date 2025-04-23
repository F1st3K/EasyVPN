using EasyVPN.Api.Common;
using EasyVPN.Application.Servers.Commands.CreateServer;
using EasyVPN.Application.Servers.Commands.RemoveServer;
using EasyVPN.Application.Servers.Commands.UpdateServer;
using EasyVPN.Application.Servers.Queries.GetServer;
using EasyVPN.Application.Servers.Queries.GetServers;
using EasyVPN.Application.Servers.Queries.TestConnection;
using EasyVPN.Contracts.Servers;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.ServerSetuper;

[Route("servers")]
public class ServersController : ApiControllerBase
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
    [Authorize]
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
            Problem);
    }

    /// <summary>
    /// Get server by id. (any auth)
    /// </summary>
    /// <param name="serverId">Server guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/servers/{{serverId}}
    /// </remarks>
    [HttpGet("{serverId:guid}")]
    [Authorize]
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
            Problem);
    }

    /// <summary>
    /// Setup new server. (server setuper)
    /// </summary>
    /// <param name="request">Server info.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/servers
    /// {
    ///     "protocolId": "0e9e6fd7-c73e-4961-93a1-1406fdec51ed",
    ///     "connection": {
    ///         "auth": "username:password",
    ///         "endpoint": "http://easyvpn.host.com:8000/v1/"
    ///     },
    ///     "version": "V1"
    /// }
    /// </remarks>
    [HttpPost]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> SetupServer([FromBody] ServerRequest request)
    {
        var testResult = await _sender.Send(new TestConnectionQuery(
            request.Connection.Auth,
            request.Connection.Endpoint,
            Enum.Parse<VpnVersion>(request.Version, ignoreCase: true)
        ));
        if (testResult.IsError) Problem(testResult.Errors);

        var result = await _sender.Send(new CreateServerCommand(
            new ConnectionString
            {
                Auth = request.Connection.Auth,
                Endpoint = request.Connection.Endpoint
            },
            request.ProtocolId,
            Enum.Parse<VpnVersion>(request.Version, ignoreCase: true)
        ));

        return result.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Config server. (server setuper)
    /// </summary>
    /// <param name="serverId">Server guid.</param>
    /// <param name="request">New server info.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/servers/{{serverId}}
    /// {
    ///     "protocolId": "0e9e6fd7-c73e-4961-93a1-1406fdec51ed",
    ///     "connection": {
    ///         "auth": "username:password",
    ///         "endpoint": "http://easyvpn.host.com:8000/v1/"
    ///     },
    ///     "version": "V1"
    /// }
    /// </remarks>
    [HttpPut("{serverId:guid}")]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> ConfigServer([FromRoute] Guid serverId, [FromBody] ServerRequest request)
    {
        var testResult = await _sender.Send(new TestConnectionQuery(
            request.Connection.Auth,
            request.Connection.Endpoint,
            Enum.Parse<VpnVersion>(request.Version, ignoreCase: true)
        ));
        if (testResult.IsError) Problem(testResult.Errors);

        var result = await _sender.Send(new UpdateServerCommand(
            serverId,
            new ConnectionString
            {
                Auth = request.Connection.Auth,
                Endpoint = request.Connection.Endpoint
            },
            request.ProtocolId,
            Enum.Parse<VpnVersion>(request.Version, ignoreCase: true)
        ));

        return result.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Remove server. (server setuper)
    /// </summary>
    /// <param name="serverId">Server guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// DELETE {{host}}/servers/{{serverId}}
    /// </remarks>
    [HttpDelete("{serverId:guid}")]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> DeleteServer([FromRoute] Guid serverId)
    {
        var result = await _sender.Send(new RemoveServerCommand(serverId));

        return result.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Test connection to server. (server setuper)
    /// </summary>
    /// <param name="request">Connection string to test server.</param>
    /// <param name="version">Version API test server.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/servers/test/{{version}}
    /// {
    ///     "auth": "username:password",
    ///     "endpoint": "http://easyvpn.host.com:8000/v1/"
    /// }
    /// </remarks>
    [HttpPost("test/{version}")]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> TestConnection([FromRoute] string version, [FromBody] ConnectionRequest request)
    {
        var testResult = await _sender.Send(new TestConnectionQuery(
            request.Auth,
            request.Endpoint,
            Enum.Parse<VpnVersion>(version, ignoreCase: true)
        ));


        return testResult.Match(
            _ => Ok(),
            Problem);
    }
}
