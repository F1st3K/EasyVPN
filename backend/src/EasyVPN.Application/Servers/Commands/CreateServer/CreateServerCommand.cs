using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Commands.CreateServer;

public record CreateServerCommand(
    ConnectionString ConnectionString,
    Guid ProtocolId,
    VpnVersion Version
    ) : IRequest<ErrorOr<Created>>;