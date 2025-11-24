using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Users.Queries.GetUser;

public record GetUserQuery(
    Guid UserId
    ) : IRequest<ErrorOr<User>>;