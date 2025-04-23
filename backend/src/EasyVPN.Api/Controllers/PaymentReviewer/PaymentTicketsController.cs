using EasyVPN.Application.Connections.Commands.AddLifetimeConnection;
using EasyVPN.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Commands.RejectConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using EasyVPN.Contracts.ConnectionTickets;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.PaymentReviewer;

[Route("payment/tickets")]
[Authorize(Roles = nameof(RoleType.PaymentReviewer))]
public class PaymentTicketsController : ApiControllerBase
{
    private readonly ISender _sender;

    public PaymentTicketsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get list of conneciton tickets for confirm. (payment reviewer)
    /// </summary>
    /// <returns>Returns list of connection tickets.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/payment/tickets
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetConnectionTickets([FromQuery] Guid? clientId)
    {
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
    /// Confirm connection ticket by guid, on days. (payment reviewer)
    /// </summary>
    /// <param name="connectionTicketId">Connection ticket guid.</param>
    /// <param name="days">Confirm with days.</param>
    /// <returns>Returns OR or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/payment/tickets/{{connectionTicketId}}/confirm
    /// </remarks>
    [HttpPut("{connectionTicketId:guid}/confirm")]
    public async Task<IActionResult> Confirm([FromRoute] Guid connectionTicketId, [FromQuery] int? days)
    {
        var confirmTicketResult =
            await _sender.Send(new ConfirmConnectionTicketCommand(connectionTicketId));
        if (confirmTicketResult.IsError)
            return Problem(confirmTicketResult.ErrorsOrEmptyList);

        var ticketResult =
            await _sender.Send(new GetConnectionTicketQuery(connectionTicketId));
        if (ticketResult.IsError)
            return Problem(ticketResult.ErrorsOrEmptyList);

        var confirmResult =
            await _sender.Send(new AddLifetimeConnectionCommand(
                ticketResult.Value.ConnectionId, days ?? ticketResult.Value.Days));

        return confirmResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Reject connection ticket by Guid. (payment reviewer)
    /// </summary>
    /// <returns>Returns OR or error.</returns>
    /// <param name="connectionTicketId">Connection ticket guid.</param>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/payment/tickets/{{connectionTicketId}}/reject
    /// </remarks>
    [HttpPut("{connectionTicketId:guid}/reject")]
    public async Task<IActionResult> Reject([FromRoute] Guid connectionTicketId)
    {
        var rejectTicketResult =
            await _sender.Send(new RejectConnectionTicketCommand(connectionTicketId));

        return rejectTicketResult.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get connection ticket by id. (payment reviewer)
    /// </summary>
    /// <param name="connectionTicketId">Connection ticket guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/payment/tickets/{{connectionTicketId}}
    /// </remarks>
    [HttpGet("{connectionTicketId:guid}")]
    public async Task<IActionResult> GetConnection([FromRoute] Guid connectionTicketId)
    {
        var connectionResult = await _sender.Send(new GetConnectionTicketQuery(connectionTicketId));
        return connectionResult.Match(
            result => Ok(new ConnectionTicketResponse(
                    result.Id,
                    result.ConnectionId,
                    new UserResponse(
                        result.Client.Id,
                        result.Client.FirstName,
                        result.Client.LastName,
                        result.Client.Login,
                        result.Client.Roles.Select(r => r.ToString()).ToArray()),
                    result.Status.ToString(),
                    result.CreationTime,
                    result.Days,
                    result.PaymentDescription,
                    result.Images.ToArray())),
            errors => Problem(errors));
    }
}
