using EasyVPN.Application.Authentication.Commands.Register;
using EasyVPN.Application.Authentication.Common;
using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName, request.LastName, request.Login, request.Password);
        var authResult = await _sender.Send(command);
        
        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Login, request.Password);
        var authResult = await _sender.Send(query);
        
        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors));
    }
    
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Login,
            authResult.Token);
        return response;
    }
}