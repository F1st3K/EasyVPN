using EasyVPN.Api.Common;
using EasyVPN.Application.Protocols.Commands.CreateProtocol;
using EasyVPN.Application.Protocols.Commands.RemoveProtocol;
using EasyVPN.Application.Protocols.Commands.UpdateProtocol;
using EasyVPN.Application.Protocols.Queries.GetProtocol;
using EasyVPN.Application.Protocols.Queries.GetProtocols;
using EasyVPN.Contracts.Servers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("protocols")]
public class ProtocolsController : ApiController
{
    private readonly ISender _sender;

    public ProtocolsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get list of protocols. (any auth)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/protocols
    /// </remarks>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProtocols()
    {
        var serverResult = await _sender.Send(new GetProtocolsQuery());
        return serverResult.Match(
            result => Ok(result.Select(c => 
                new ProtocolResponse(
                    c.Id,
                    c.Name,
                    c.Icon
                ))),
            Problem);
    }

    /// <summary>
    /// Get protocol by id. (any auth)
    /// </summary>
    /// <param name="protocolId">Protocol guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/protocols/{{protocolId}}
    /// </remarks>
    [HttpGet("{protocolId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetProtocol([FromRoute] Guid protocolId)
    {
        var serverResult = await _sender.Send(new GetProtocolQuery(protocolId));
        return serverResult.Match(
            result => Ok(new ProtocolResponse(
                    result.Id,
                    result.Name,
                    result.Icon
            )),
            Problem);
    }
    
    /// <summary>
    /// Create new protocol. (server setuper)
    /// </summary>
    /// <param name="request">Protocol info.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST {{host}}/protocols
    /// </remarks>
    [HttpPost]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> CreateProtocol([FromBody] ProtocolRequest request)
    {
        var result = await _sender.Send(new CreateProtocolCommand(
                request.Name,
                request.Icon
            ));
        
        return result.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Update protocol. (server setuper)
    /// </summary>
    /// <param name="protocolId">Protocol guid.</param>
    /// <param name="request">Protocol info.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/protocols/{{protocolId}}
    /// </remarks>
    [HttpPut("{protocolId:guid}")]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> UpdateProtocol([FromRoute] Guid protocolId, [FromBody] ProtocolRequest request)
    {
        var result = await _sender.Send(new UpdateProtocolCommand(
                protocolId,
                request.Name,
                request.Icon
            ));
        
        return result.Match(
            _ => Ok(),
            Problem);
    }
    
    /// <summary>
    /// Remove protocol. (server setuper)
    /// </summary>
    /// <param name="protocolId">Protocol guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// DELETE {{host}}/protocols/{{protocolId}}
    /// </remarks>
    [HttpDelete("{protocolId:guid}")]
    [Authorize(Roles = Roles.ServerSetuper)]
    public async Task<IActionResult> DeleteProtocol([FromRoute] Guid protocolId)
    {
        var result = await _sender.Send(new RemoveProtocolCommand(protocolId));
        
        return result.Match(
            _ => Ok(),
            Problem);
    }
}
