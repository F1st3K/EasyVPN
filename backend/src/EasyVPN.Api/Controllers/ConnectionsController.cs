using EasyVPN.Api.Common;
using EasyVPN.Application.Vpn.Queries.GetConfig;
using EasyVPN.Contracts.Connections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("connections")]
[Authorize(Roles = Roles.Administrator)]
public class ConnectionsController : ApiController
{
    private readonly ISender _sender;

    public ConnectionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}/config")]
    public async Task<IActionResult> GetConnectionConfig([FromRoute] Guid id)
    {
        var configResult = await _sender.Send(new GetConfigQuery(id));
        return configResult.Match(
        result => Ok(new ConnectionConfigResponse(result.ClientId, result.Config)),
        errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateConnection([FromRoute] Guid id)
    {
        return Ok();
    }

}