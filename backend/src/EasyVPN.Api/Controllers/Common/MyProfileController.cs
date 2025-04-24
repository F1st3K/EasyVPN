using EasyVPN.Api.Common;
using EasyVPN.Application.Users.Commands.ChangePassword;
using EasyVPN.Application.Users.Queries.GetUser;
using EasyVPN.Contracts.Users;
using EasyVPN.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Common;

[Route("my/profile")]
[Authorize]
public class MyProfileController : ApiControllerBase
{
    private readonly ISender _sender;

    public MyProfileController(ISender sender)
    {
        _sender = sender;
    }
    
    /// <summary>
    /// Change profile information for current user. (any auth)
    /// </summary>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET {{host}}/my/profile
    /// "newPassword"
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (User.GetCurrentId() is not { } userId)
            return Problem(Errors.Access.NotIdentified);
        
        var userResult =
            await _sender.Send(new GetUserQuery(userId));

        return userResult.Match(
         u => Ok(new UserResponse(
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Icon,
                    u.Login,
                    u.Roles.Select(r => r.ToString()).ToArray()
                )),
            Problem);
    }
    
    /// <summary>
    /// Change profile information for current user. (any auth)
    /// </summary>
    /// <param name="request">New profile information.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/my/profile
    /// "newPassword"
    /// </remarks>
    /*
    [HttpPut]
    public async Task<IActionResult> Change([FromBody] UserRequest request)
    {
        if (User.GetCurrentId() is not { } userId)
            return Problem(Errors.Access.NotIdentified);
        
        var userResult =
            await _sender.Send(new ChangeUserInfoCommand(userId, request));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }
    */

    /// <summary>
    /// Change password for current user. (any auth)
    /// </summary>
    /// <param name="newPassword">New user password.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/my/profile/password
    /// "newPassword"
    /// </remarks>
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword([FromBody] string newPassword)
    {
        if (User.GetCurrentId() is not { } userId)
            return Problem(Errors.Access.NotIdentified);
        
        var userResult =
            await _sender.Send(new ChangePasswordCommand(userId, newPassword));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }
    
    /// <summary>
    /// Change login for current user. (any auth)
    /// </summary>
    /// <param name="newLogin">New user login.</param>
    /// <returns>Returns OK or error.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// PUT {{host}}/my/profile/login
    /// "newLogin"
    /// </remarks>
    /*
    [HttpPut("login")]
    public async Task<IActionResult> ChangeLogin([FromBody] string newLogin)
    {
        if (User.GetCurrentId() is not { } userId)
            return Problem(Errors.Access.NotIdentified);
        
        var userResult =
            await _sender.Send(new ChangeLoginCommand(userId, newLogin));

        return userResult.Match(
            _ => Ok(),
            Problem);
    }
*/
}
