using EasyZsV.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Commands.RolesUpdate;

public record RolesUpdateCommand(Guid UserId, IEnumerable<RoleType> NewRoles) : IRequest<ErrorOr<Updated>>;