using EasyVPN.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace EasyVPN.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Login,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;