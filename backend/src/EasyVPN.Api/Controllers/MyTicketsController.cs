using EasyVPN.Api.Common;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using EasyVPN.Contracts.ConnectionTickets;
using EasyVPN.Contracts.Users;
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
            return Forbid();

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
}
