namespace EasyVPN.Application.Services.Authentication;

public interface IAuthenticationService
{
    public AuthenticationResult Register(
        string firstName, string lastName, string login, string password);

    public AuthenticationResult Login(string login, string password);
}