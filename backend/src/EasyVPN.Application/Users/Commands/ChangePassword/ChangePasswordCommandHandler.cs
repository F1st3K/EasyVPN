using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashGenerator _hasher;

    public ChangePasswordCommandHandler(
        IUserRepository connectionRepository,
        IHashGenerator hasher)
    {
        _userRepository = connectionRepository;
        _hasher = hasher;
    }

    public async Task<ErrorOr<Updated>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetById(command.UserId) is not { } user)
            return Errors.User.NotFound;

        user.HashPassword = _hasher.Hash(command.NewPassword);
        _userRepository.Update(user);

        return Result.Updated;
    }
}