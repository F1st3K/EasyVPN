using EasyVPN.Application.Services.Authentication;
using EasyVPN.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace EasyVPN.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName, request.LastName, request.Login, request.Password);
        
        return authResult.MatchFirst(
            result => Ok(MapAuthResult(result)),
            error => Problem(error.Code));
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

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Login, request.Password);
        /*var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName, 
            authResult.User.LastName,
            authResult.User.Login,
            authResult.Token);*/
        return Ok();
    }
}