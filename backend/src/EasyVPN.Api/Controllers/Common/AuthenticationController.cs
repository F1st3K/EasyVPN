using EasyVPN.Api.Common;
using EasyVPN.Application.Authentication.Commands.Register;
using EasyVPN.Application.Authentication.Common;
using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Application.Users.Queries.GetUser;
using EasyVPN.Contracts.Authentication;
using EasyVPN.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers.Common;

[Route("auth")]
public class AuthenticationController : ApiControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Registers a new user. (anywhere)
    /// </summary>
    /// <param name="request">The registration request containing the user's first name, last name, login, and password.</param>
    /// <returns>Returns the result of the registration.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST /auth/register
    /// {
    ///     "firstName": "Freak",
    ///     "lastName": "Fister",
    ///     "login": "F1st3K",
    ///     "password": "fisty123"
    /// }
    /// </remarks>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName, request.LastName, request.Login, request.Password);
        var authResult = await _sender.Send(command);

        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            Problem);
    }

    /// <summary>
    /// Logs in a user. (anywhere)
    /// </summary>
    /// <param name="request">The login request containing the user's login and password.</param>
    /// <returns>Returns the result of the login, including the token.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST /auth/login
    /// {
    ///     "login": "F1st3K",
    ///     "password": "fisty123"
    /// }
    /// </remarks>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Login, request.Password);
        var authResult = await _sender.Send(query);

        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            Problem);
    }

    /// <summary>
    /// Checks the validity of the token. (any auth)
    /// </summary>
    /// <returns>Returns user information if the token is valid.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET /auth/check
    /// Authorization: Bearer {token}
    /// </remarks>
    [HttpGet("check")]
    [Authorize]
    public async Task<IActionResult> Check()
    {
        if (User.GetCurrentId() is not { } id
            || Request.GetCurrentToken() is not { } token)
            return Unauthorized();

        var query = new GetUserQuery(id);
        var userResult = await _sender.Send(query);

        return userResult.Match(
            result => Ok(MapAuthResult(new AuthenticationResult(result, token))),
            Problem);
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Icon,
            authResult.User.Login,
            authResult.User.Roles.Select(r => r.ToString()).ToArray(),
            authResult.Token);
        return response;
    }
}
