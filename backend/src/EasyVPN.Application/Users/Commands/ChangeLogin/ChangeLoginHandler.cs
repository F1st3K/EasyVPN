using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.ChangeLogin;

public class ChangeLoginHandler : IRequestHandler<ChangeLoginCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;

    public ChangeLoginHandler(
        IUserRepository connectionRepository)
    {
        _userRepository = connectionRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(ChangeLoginCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetById(command.UserId) is not { } user)
            return Errors.User.NotFound;

        if (_userRepository.GetByLogin(command.NewLogin) is not null)
            return Errors.User.DuplicateLogin;

        user.Login = command.NewLogin;
        _userRepository.Update(user);

        return Result.Updated;
    }
}