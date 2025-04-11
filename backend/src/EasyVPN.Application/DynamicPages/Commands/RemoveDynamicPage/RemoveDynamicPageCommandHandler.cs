using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Commands.RemoveDynamicPage;

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

        if (_dynamicPageRepository.Get(command.Route) is not {} page)
            return Errors.DynamicPage.NotFound;
        
        _dynamicPageRepository.Remove(page.Route);

        return Result.Deleted;
    }
}