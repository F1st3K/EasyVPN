using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<User[]>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(
        IUserRepository connectionRepository)
    {
        _userRepository = connectionRepository;
    }

    public async Task<ErrorOr<User[]>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _userRepository.GetAll().ToArray();
    }
}