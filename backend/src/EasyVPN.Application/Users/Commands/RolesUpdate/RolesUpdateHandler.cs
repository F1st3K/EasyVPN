using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Commands.RolesUpdate;

public class RolesUpdateHandler : IRequestHandler<RolesUpdateCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;

    public RolesUpdateHandler(
        IUserRepository connectionRepository)
    {
        _userRepository = connectionRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(RolesUpdateCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetById(command.UserId) is not { } user)
            return Errors.User.NotFound;

        user.Roles = command.NewRoles;
        _userRepository.Update(user);

        return Result.Updated;
    }
}