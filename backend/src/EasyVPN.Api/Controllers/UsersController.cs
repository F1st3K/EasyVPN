using EasyVPN.Api.Common;
using EasyVPN.Application.Users.Queries.GetUser;
using EasyVPN.Application.Users.Queries.GetUsers;
using EasyVPN.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("users")]
[Authorize(Roles = Roles.Administrator)]
public class UsersController : ApiController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var userResult =
            await _sender.Send(new GetUsersQuery());

        return userResult.Match(
            us => Ok(us.Select(u => new UserResponse(
                    u.Id,
                    u.FirstName, 
                    u.LastName,
                    u.Login,
                    u.Roles.Select(r => r.ToString()).ToArray()
                ))),
            Problem);
    }
        
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var userResult =
            await _sender.Send(new GetUserQuery(userId));

        return userResult.Match(
            u => Ok(new UserResponse(
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Login,
                    u.Roles.Select(r => r.ToString()).ToArray()
                )),
            Problem);
    }
    
    [HttpPut("{userId:guid}/roles")]
    public async Task<IActionResult> UpdateRoles([FromRoute] Guid userId, [FromBody] string[] roles)
    {
        var userResult =
            await _sender.Send(new GetUserQuery(userId));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }
    
    [HttpPut("{userId:guid}/password")]
    public async Task<IActionResult> ChangePassword([FromRoute] Guid userId, [FromBody] string newPassword)
    {
        var userResult =
            await _sender.Send(new GetUserQuery(userId));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }
}
