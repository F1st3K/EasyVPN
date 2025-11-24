using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Commands.RemoveServer;

public record RemoveServerCommand(Guid ServerId) : IRequest<ErrorOr<Deleted>>;