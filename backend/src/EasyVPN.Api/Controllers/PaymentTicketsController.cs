using EasyVPN.Api.Common;
using EasyVPN.Application.Connections.Commands.AddLifetimeConnection;
using EasyVPN.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Commands.RejectConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTicket;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using EasyVPN.Contracts.ConnectionTickets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("payment/tickets")]
[Authorize(Roles = Roles.PaymentReviewer)]
public class PaymentTicketsController : ApiController
{
    private readonly ISender _sender;
    
    public PaymentTicketsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetConnectionTickets([FromQuery] Guid? clientId)
    {
        var getConnectionsResult = 
            await _sender.Send(new GetConnectionTicketsQuery(clientId));
        
        return getConnectionsResult.Match(
            result => Ok(result.Select(c =>
                new ConnectionTicketResponse(c.Id,
                    c.ConnectionId,
                    c.ClientId,
                    c.Status.ToString(),
                    c.CreationTime,
                    c.Days,
                    c.PaymentDescription))),
            errors => Problem(errors));
    }
    
    [HttpPut("{connectionTicketId:guid}/confirm")]
    public async Task<IActionResult> Confirm([FromRoute] Guid connectionTicketId)
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
                ticketResult.Value.ConnectionId, ticketResult.Value.Days));
        
        return confirmResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
    
    [HttpPut("{connectionTicketId:guid}/reject")]
    public async Task<IActionResult> Reject([FromRoute] Guid connectionTicketId)
    {
        var rejectTicketResult =
            await _sender.Send(new RejectConnectionTicketCommand(connectionTicketId));
        
        return rejectTicketResult.Match(
            _ => Ok(), 
            errors => Problem(errors));
    }
}