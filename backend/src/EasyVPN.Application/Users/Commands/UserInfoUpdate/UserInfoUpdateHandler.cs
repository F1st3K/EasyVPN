using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.UserInfoUpdate;

public class UserInfoUpdateHandler : IRequestHandler<UserInfoUpdateCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;

    public UserInfoUpdateHandler(
        IUserRepository connectionRepository)
    {
        _userRepository = connectionRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UserInfoUpdateCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetById(command.UserId) is not { } user)
            return Errors.User.NotFound;

        user.FirstName = command.FirstName;
        user.LastName = command.LastName;
        user.Icon = command.Icon;
        _userRepository.Update(user);

        return Result.Updated;
    }
}