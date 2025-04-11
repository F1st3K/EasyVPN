using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Queries.GetDynamicPages;

public class GetDynamicPagesQueryHandler : IRequestHandler<GetDynamicPagesQuery, ErrorOr<List<DynamicPage>>>
{
    private readonly IDynamicPageRepository _dynamicPageRepository;

    public GetDynamicPagesQueryHandler(IDynamicPageRepository dynamicPageRepository)
    {
        _dynamicPageRepository = dynamicPageRepository;
    }

    public async Task<ErrorOr<List<DynamicPage>>> Handle(GetDynamicPagesQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _dynamicPageRepository.GetAll().ToList();
    }
}