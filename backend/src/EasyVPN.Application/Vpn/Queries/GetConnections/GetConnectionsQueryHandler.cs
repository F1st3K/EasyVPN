using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Vpn.Queries.GetConnections;

public class GetConnectionsQueryHandler : IRequestHandler<GetConnectionsQuery, ErrorOr<List<Connection>>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetConnectionsQueryHandler(
        IConnectionRepository connectionRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository)
    {
        _connectionRepository = connectionRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }
    
    public async Task<ErrorOr<List<Connection>>> Handle(GetConnectionsQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (query.ClientId is not { } clientId) 
            return _connectionRepository.Select();
        
        if (_userRepository.GetUserById(clientId) is null)
            return Errors.User.NotFound;

        if (_userRoleRepository.GetRolesByUserId(clientId)
                .Any(r => r == RoleType.Client) == false)
            return Errors.Access.ClientsOnly;
            
        return _connectionRepository.Select(query.ClientId.Value);
    }
}