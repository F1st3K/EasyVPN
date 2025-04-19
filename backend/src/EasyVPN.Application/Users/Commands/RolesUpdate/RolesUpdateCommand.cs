using EasyVPN.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Commands.RolesUpdate;

public record RolesUpdateCommand(Guid UserId, IEnumerable<RoleType> Roles) : IRequest<ErrorOr<Updated>>;