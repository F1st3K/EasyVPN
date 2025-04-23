using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Protocols.Commands.RemoveProtocol;

public record RemoveProtocolCommand(Guid ProtocolId) : IRequest<ErrorOr<Deleted>>;