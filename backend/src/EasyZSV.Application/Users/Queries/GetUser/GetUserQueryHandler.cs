using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(
        IUserRepository connectionRepository)
    {
        _userRepository = connectionRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetById(query.UserId) is not { } user)
            return Errors.User.NotFound;

        return user;
    }
}