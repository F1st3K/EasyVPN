using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Commands.UpdateServer;

public record UpdateServerCommand(
    Guid ServerId,
    ConnectionString ConnectionString,
    Guid ProtocolId,
    ZsvVersion Version
    ) : IRequest<ErrorOr<Created>>;