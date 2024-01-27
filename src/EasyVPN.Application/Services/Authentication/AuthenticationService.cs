using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Entities;

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

    public AuthenticationResult Register(
        string firstName, string lastName, string login, string password)
    {
        if (_userRepository.GetUserByLogin(login) is not null)
            throw new Exception($"User with given login already exist");

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Login = login,
            Password = password
        };
        _userRepository.Add(user);
        
        var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);
        
        return new AuthenticationResult(
            user.Id, firstName, lastName, login, token);
    }

    public AuthenticationResult Login(string login, string password)
    {
        if (_userRepository.GetUserByLogin(login) is not User user)
            throw new Exception("User with given login not exist");

        if (user.Password != password)
            throw new Exception("Invalid password");
        
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
        
        return new AuthenticationResult(
            user.Id, 
            user.FirstName, 
            user.LastName, 
            user.Login, 
            token);
    }
}