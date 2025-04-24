using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string NewPassword) : IRequest<ErrorOr<Updated>>;