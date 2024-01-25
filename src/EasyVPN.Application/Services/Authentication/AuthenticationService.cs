namespace EasyVPN.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Register(
        string firstName, string lastName, string login, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(), firstName, lastName, login, "token");
    }

    public AuthenticationResult Login(string login, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(), "firstName", "lastName", login, "token");
    }
}