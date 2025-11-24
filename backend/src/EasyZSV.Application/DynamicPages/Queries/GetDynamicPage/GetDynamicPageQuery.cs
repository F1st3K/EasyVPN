using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Queries.GetDynamicPage;

public record GetDynamicPageQuery(string Route) : IRequest<ErrorOr<DynamicPage>>;