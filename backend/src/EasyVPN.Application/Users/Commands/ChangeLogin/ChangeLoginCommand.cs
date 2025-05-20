using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.ChangeLogin;

public record ChangeLoginCommand(
        Guid UserId,
        string NewLogin
    ) : IRequest<ErrorOr<Updated>>;