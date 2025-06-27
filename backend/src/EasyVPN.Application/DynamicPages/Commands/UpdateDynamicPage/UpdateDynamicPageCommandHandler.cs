using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Commands.UpdateDynamicPage;

public class UpdateDynamicPageCommandHandler : IRequestHandler<UpdateDynamicPageCommand, ErrorOr<Updated>>
{
    private readonly IDynamicPageRepository _dynamicPageRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateDynamicPageCommandHandler(IDynamicPageRepository dynamicPageRepository, IDateTimeProvider dateTimeProvider)
    {
        _dynamicPageRepository = dynamicPageRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateDynamicPageCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_dynamicPageRepository.Get(command.Route) is not { } page)
            return Errors.DynamicPage.NotFound;

        page.Title = command.Title;
        page.Content = command.Content;
        page.LastModified = _dateTimeProvider.UtcNow;

        if (page.Route == command.NewRoute)
        {
            _dynamicPageRepository.Update(page);
        }
        else
        {
            _dynamicPageRepository.Remove(page.Route);

            page.Route = command.NewRoute;
            _dynamicPageRepository.Add(page);
        }

        return Result.Updated;
    }
}