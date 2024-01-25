using EasyVPN.Application.Common.Interfaces.Authentication;

namespace EasyVPN.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(
        string firstName, string lastName, string login, string password)
    {
        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        
        return new AuthenticationResult(
            userId, firstName, lastName, login, token);
    }

    public AuthenticationResult Login(string login, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(), "firstName", "lastName", login, "token");
    }
}