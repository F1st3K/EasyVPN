using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.UserInfoUpdate;

public record UserInfoUpdateCommand(
        Guid UserId,
        string FirstName,
        string LastName,
        string Icon
    ) : IRequest<ErrorOr<Updated>>;