using EasyVPN.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Login, 
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;