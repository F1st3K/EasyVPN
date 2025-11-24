using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Queries.GetDynamicPages;

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