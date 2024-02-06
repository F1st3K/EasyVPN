using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;

namespace EasyVPN.Application.Services.Authentication;

public class AuthenticationService
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

    public async Task<ErrorOr<AuthenticationResult>> Register(
        string firstName, string lastName, string login, string password)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(login) is not null)
            return Errors.User.DuplicateLogin;

        var user = new User
        {
            Roles = new [] { Roles.User, Roles.Administrator, Roles.PaymentReviewer },
            FirstName = firstName,
            LastName = lastName,
            Login = login,
            Password = password
        };
        _userRepository.Add(user);
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }

    public async Task<ErrorOr<AuthenticationResult>> Login(string login, string password)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(login) is not { } user)
            return Errors.Authentication.InvalidCredentials;

        if (user.Password != password)
            return Errors.Authentication.InvalidCredentials;
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }
}