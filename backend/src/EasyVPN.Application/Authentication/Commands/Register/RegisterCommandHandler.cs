using EasyVPN.Application.Authentication.Common;
using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHashGenerator _hashGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IHashGenerator hashGenerator)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hashGenerator = hashGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(command.Login) is not null)
            return Errors.User.DuplicateLogin;

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Login = command.Login,
            HashPassword = _hashGenerator.Hash(command.Password)
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
}