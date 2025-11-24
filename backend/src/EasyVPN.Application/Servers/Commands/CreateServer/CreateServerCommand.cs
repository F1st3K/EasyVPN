using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Commands.CreateServer;

public record CreateServerCommand(
    ConnectionString ConnectionString,
    Guid ProtocolId,
    ZsvVersion Version
    ) : IRequest<ErrorOr<Created>>;