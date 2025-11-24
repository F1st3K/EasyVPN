using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Commands.UserInfoUpdate;

public record UserInfoUpdateCommand(
        Guid UserId,
        string FirstName,
        string LastName,
        string Icon
    ) : IRequest<ErrorOr<Updated>>;