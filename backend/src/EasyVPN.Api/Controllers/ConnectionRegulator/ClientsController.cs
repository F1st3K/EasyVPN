using EasyVPN.Application.Users.Queries.GetUsers;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.ConnectionRegulator;

[Route("clients")]
public class ClientsController : ApiControllerBase
{
    private readonly ISender _sender;

    public ClientsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Permanent get list clients. (connection regulator)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/clients
    /// </remarks>
    [HttpGet]
    [Authorize(Roles = nameof(RoleType.ConnectionRegulator))]
    public async Task<IActionResult> GetClients()
    {
        var getClientsResult =
            await _sender.Send(new GetUsersQuery(RoleType.Client));

        return getClientsResult.Match(
            result => Ok(
                result.Select(c => new ClientResponse(
                        c.Id,
                        c.FirstName,
                        c.LastName,
                        c.Icon,
                        c.Login
                    ))),
            Problem);
    }
}
