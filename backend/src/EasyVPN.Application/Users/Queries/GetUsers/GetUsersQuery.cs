using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<ErrorOr<User[]>>;