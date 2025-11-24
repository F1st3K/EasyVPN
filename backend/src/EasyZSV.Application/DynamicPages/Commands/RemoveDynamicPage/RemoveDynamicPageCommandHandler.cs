using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Commands.RemoveDynamicPage;

public class RemoveDynamicPageCommandHandler : IRequestHandler<RemoveDynamicPageCommand, ErrorOr<Deleted>>
{
    private readonly IDynamicPageRepository _dynamicPageRepository;

    public RemoveDynamicPageCommandHandler(IDynamicPageRepository dynamicPageRepository)
    {
        _dynamicPageRepository = dynamicPageRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(RemoveDynamicPageCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_dynamicPageRepository.Get(command.Route) is not { } page)
            return Errors.DynamicPage.NotFound;

        _dynamicPageRepository.Remove(page.Route);

        return Result.Deleted;
    }
}