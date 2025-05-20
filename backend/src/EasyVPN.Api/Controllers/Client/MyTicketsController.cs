using EasyVPN.Api.Common;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using EasyVPN.Contracts.ConnectionTickets;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Client;

[Route("my/tickets")]
[Authorize(Roles = nameof(RoleType.Client))]
public class MyTicketsController : ApiControllerBase
{
    private readonly ISender _sender;

    public MyTicketsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get list of my conneciton tickets. (client)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/my/tickets
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetConnectionTickets()
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

        var getConnectionsResult =
            await _sender.Send(new GetConnectionTicketsQuery(clientId));

        return getConnectionsResult.Match(
            result => Ok(
                result.Select(c => new ConnectionTicketResponse(
                    c.Id,
                    c.ConnectionId,
                    new UserResponse(
                        c.Client.Id,
                        c.Client.FirstName,
                        c.Client.LastName,
                        c.Client.Icon,
                        c.Client.Login,
                        c.Client.Roles.Select(r => r.ToString()).ToArray()),
                    c.Status.ToString(),
                    c.CreationTime,
                    c.Days,
                    c.PaymentDescription,
                    c.Images.ToArray()))),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get connection ticket by id. (client)
    /// </summary>
    /// <param name="connectionTicketId">Connection ticket guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/my/tickets/{{connectionTicketId}}
    /// </remarks>
    [HttpGet("{connectionTicketId:guid}")]
    public async Task<IActionResult> GetConnection([FromRoute] Guid connectionTicketId)
    {
        if (User.GetCurrentId() is not { } clientId)
            return Problem(Errors.Access.NotIdentified);

        var connectionResult = await _sender.Send(new GetConnectionTicketQuery(connectionTicketId));
        return connectionResult.Match(
            result => result.Client.Id == clientId
                ? Ok(new ConnectionTicketResponse(
                    result.Id,
                    result.ConnectionId,
                    new UserResponse(
                        result.Client.Id,
                        result.Client.FirstName,
                        result.Client.LastName,
                        result.Client.Icon,
                        result.Client.Login,
                        result.Client.Roles.Select(r => r.ToString()).ToArray()),
                    result.Status.ToString(),
                    result.CreationTime,
                    result.Days,
                    result.PaymentDescription,
                    result.Images.ToArray()))
                : Problem(Errors.Access.AnotherUserOnly),
            errors => Problem(errors));
    }
}
