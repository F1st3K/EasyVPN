using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.Servers.Queries.GetServers;

public record GetServersQuery() : IRequest<ErrorOr<List<Server>>>;
