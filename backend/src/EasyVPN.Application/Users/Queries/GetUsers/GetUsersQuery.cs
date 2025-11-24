using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Queries.GetUsers;

public record GetUsersQuery(RoleType? Role = null) : IRequest<ErrorOr<User[]>>;