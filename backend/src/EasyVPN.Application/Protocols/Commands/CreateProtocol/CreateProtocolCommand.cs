using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Commands.CreateProtocol;

public record CreateProtocolCommand(string Name, string Icon) : IRequest<ErrorOr<Created>>;