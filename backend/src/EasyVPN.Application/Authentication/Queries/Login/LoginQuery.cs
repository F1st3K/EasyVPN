using EasyZsV.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Login,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;