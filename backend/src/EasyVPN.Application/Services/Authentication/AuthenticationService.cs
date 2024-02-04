using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;

namespace EasyVPN.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(
        string firstName, string lastName, string login, string password)
    {
        if (_userRepository.GetUserByLogin(login) is not null)
            return Errors.User.DuplicateLogin;

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Login = login,
            Password = password
        };
        _userRepository.Add(user);
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Login(string login, string password)
    {
        if (_userRepository.GetUserByLogin(login) is not User user)
            throw new Exception("User with given login not exist");

        if (user.Password != password)
            throw new Exception("Invalid password");
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }
}