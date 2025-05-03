using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Queries.GetUsers;

public record GetUsersQuery(RoleType? Role = null) : IRequest<ErrorOr<User[]>>;