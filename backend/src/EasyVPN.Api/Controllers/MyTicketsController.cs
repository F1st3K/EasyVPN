using EasyVPN.Api.Common;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using EasyVPN.Contracts.ConnectionTickets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("my/tickets")]
[Authorize(Roles = Roles.Client)]
public class MyTicketsController : ApiController
{
    private readonly ISender _sender;

    public MyTicketsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetConnectionTickets()
    {
        if (User.GetCurrentId() is not { } clientId)
            return Forbid();
        
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
}