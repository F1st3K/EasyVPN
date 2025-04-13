using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
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

        var newPage = new DynamicPage
        {
            Route = command.NewRoute,
            Title = command.Title,
            Content = command.Content,
            Created = page.Created,
            LastModified = _dateTimeProvider.UtcNow,
        };

        if (page.Route == newPage.Route)
        {
            _dynamicPageRepository.Update(newPage);
        }
        else
        {
            _dynamicPageRepository.Remove(page.Route);
            _dynamicPageRepository.Add(newPage);
        }

        return Result.Updated;
    }
}