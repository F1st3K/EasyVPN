using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Users.Queries.GetUser;

public record GetUserQuery(
    Guid UserId
    ) : IRequest<ErrorOr<User>>;