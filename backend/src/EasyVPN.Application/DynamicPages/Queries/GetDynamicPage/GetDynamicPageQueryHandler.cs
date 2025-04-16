using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Queries.GetDynamicPage;

public class GetDynamicPageQueryHandler : IRequestHandler<GetDynamicPageQuery, ErrorOr<DynamicPage>>
{
    private readonly IDynamicPageRepository _dynamicPageRepository;

    public GetDynamicPageQueryHandler(IDynamicPageRepository dynamicPageRepository)
    {
        _dynamicPageRepository = dynamicPageRepository;
    }

    public async Task<ErrorOr<DynamicPage>> Handle(GetDynamicPageQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_dynamicPageRepository.Get(query.Route) is not { } page)
            return Errors.DynamicPage.NotFound;

        return page;
    }
}