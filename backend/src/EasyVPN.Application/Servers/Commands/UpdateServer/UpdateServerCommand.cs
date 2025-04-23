using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Commands.UpdateServer;

public record UpdateServerCommand(
    Guid ServerId,
    ConnectionString ConnectionString,
    Guid ProtocolId,
    VpnVersion Version
    ) : IRequest<ErrorOr<Created>>;