using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Commands.ChangeLogin;

public record ChangeLoginCommand(
        Guid UserId,
        string NewLogin
    ) : IRequest<ErrorOr<Updated>>;