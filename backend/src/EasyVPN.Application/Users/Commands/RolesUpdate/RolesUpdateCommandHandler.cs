using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.RolesUpdate;

public class RolesUpdateCommandHandler : IRequestHandler<RolesUpdateCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;

    public RolesUpdateCommandHandler(
        IUserRepository connectionRepository)
    {
        _userRepository = connectionRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(RolesUpdateCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetById(command.UserId) is not { } user)
            return Errors.User.NotFound;

        user.Roles = command.Roles;
        _userRepository.Update(user);

        return Result.Updated;
    }
}