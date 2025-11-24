using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string NewPassword, string? OldPassword = null) : IRequest<ErrorOr<Updated>>;