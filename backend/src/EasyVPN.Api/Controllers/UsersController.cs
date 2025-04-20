using EasyVPN.Api.Common;
using EasyVPN.Application.Users.Commands.ChangePassword;
using EasyVPN.Application.Users.Commands.RolesUpdate;
using EasyVPN.Application.Users.Queries.GetUser;
using EasyVPN.Application.Users.Queries.GetUsers;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("users")]
[Authorize(Roles = Roles.SecurityKeeper)]
public class UsersController : ApiController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get list of users. (security keeper)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/users
    /// </remarks>
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

    /// <summary>
    /// Get user by id. (security keeper)
    /// </summary>
    /// <param name="userId">User guid.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/users/{{userId}}
    /// </remarks>
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

    /// <summary>
    /// Set new list roles for user. (security keeper)
    /// </summary>
    /// <param name="userId">User guid.</param>
    /// <param name="roles">List user roles.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/users/{{userId}}/roles
    /// [
    ///     "Client",
    ///     "PageModerator",
    /// ]
    /// </remarks>
    [HttpPut("{userId:guid}/roles")]
    public async Task<IActionResult> UpdateRoles([FromRoute] Guid userId, [FromBody] string[] roles)
    {
        var userResult =
            await _sender.Send(new RolesUpdateCommand(userId, roles.Select(Enum.Parse<RoleType>)));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }

    /// <summary>
    /// Change password for user. (security keeper)
    /// </summary>
    /// <param name="userId">User guid.</param>
    /// <param name="newPassword">New user password.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/users/{{userId}}/password
    /// "newPassword"
    /// </remarks>
    [HttpPut("{userId:guid}/password")]
    public async Task<IActionResult> ChangePassword([FromRoute] Guid userId, [FromBody] string newPassword)
    {
        var userResult =
            await _sender.Send(new ChangePasswordCommand(userId, newPassword));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }
}
