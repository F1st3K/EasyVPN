using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.Servers.Queries.GetServers;

public record GetServersQuery() : IRequest<ErrorOr<List<Server>>>;
