using ErrorOr;

namespace EasyVPN.Application.Services.Authentication;

public interface IAuthenticationService
{
    public ErrorOr<AuthenticationResult> Register(
        string firstName, string lastName, string login, string password);

    public ErrorOr<AuthenticationResult> Login(string login, string password);
}