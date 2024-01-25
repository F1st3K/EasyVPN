using EasyVPN.Application.Services.Authentication;
using EasyVPN.Contracts.Authentication;
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
        var response = new AuthenticationResponse(authResult.Id,
            authResult.FirstName, authResult.LastName,
            authResult.Login, authResult.Token);
        return Ok(response);
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Login, request.Password);
        var response = new AuthenticationResponse(authResult.Id,
            authResult.FirstName, authResult.LastName,
            authResult.Login, authResult.Token);
        return Ok(response);
    }
}