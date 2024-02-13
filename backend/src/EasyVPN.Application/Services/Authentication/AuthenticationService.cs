using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;

namespace EasyVPN.Application.Services.Authentication;

public class AuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Register(
        string firstName, string lastName, string login, string password)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(login) is not null)
            return Errors.User.DuplicateLogin;

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = firstName,
            LastName = lastName,
            Login = login,
            HashPassword = password
        };
        _userRepository.Add(user);

        var role = new UserRole()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = RoleType.Client
        };
        _userRoleRepository.Add(role);
        
        var token = _jwtTokenGenerator.GenerateToken(user, new [] { role.Type });
        
        return new AuthenticationResult(user, token);
    }

    public async Task<ErrorOr<AuthenticationResult>> Login(string login, string password)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(login) is not { } user)
            return Errors.Authentication.InvalidCredentials;

        if (user.HashPassword != password)
            return Errors.Authentication.InvalidCredentials;

        var roles = _userRoleRepository.GetRolesByUserId(user.Id);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);
        
        return new AuthenticationResult(user, token);
    }
}