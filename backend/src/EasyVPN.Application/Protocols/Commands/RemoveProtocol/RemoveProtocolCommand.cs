using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Protocols.Commands.RemoveProtocol;

public record RemoveProtocolCommand(Guid ProtocolId) : IRequest<ErrorOr<Deleted>>;