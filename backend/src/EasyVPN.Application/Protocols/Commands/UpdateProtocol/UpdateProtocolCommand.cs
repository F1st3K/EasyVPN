using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Commands.UpdateProtocol;

public record UpdateProtocolCommand(Guid ProtocolId, string Name, string Icon) : IRequest<ErrorOr<Updated>>;