using EasyVPN.Application.Authentication.Common;
using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using MediatR;
using ErrorOr;

namespace EasyVPN.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHashGenerator _hashGenerator;

    public LoginQueryHandler(
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

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByLogin(query.Login) is not { } user)
            return Errors.Authentication.InvalidCredentials;

        if (user.HashPassword != _hashGenerator.Hash(query.Password))
            return Errors.Authentication.InvalidCredentials;

        var roles = _userRoleRepository.GetRolesByUserId(user.Id);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);
        
        return new AuthenticationResult(user, token);
    }
}