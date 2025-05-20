using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Commands.RemoveServer;

public record RemoveServerCommand(Guid ServerId) : IRequest<ErrorOr<Deleted>>;