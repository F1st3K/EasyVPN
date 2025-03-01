using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Queries.GetDynamicPage;

public record GetDynamicPageQuery(string Route) : IRequest<ErrorOr<DynamicPage>>;