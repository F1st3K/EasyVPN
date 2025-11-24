using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Protocols.Commands.UpdateProtocol;

public record UpdateProtocolCommand(Guid ProtocolId, string Name, string Icon) : IRequest<ErrorOr<Updated>>;