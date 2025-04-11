using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Commands.CreateDynamicPage;

public class CreateDynamicPageCommandHandler : IRequestHandler<CreateDynamicPageCommand, ErrorOr<Created>>
{
    private readonly IDynamicPageRepository _dynamicPageRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateDynamicPageCommandHandler(IDynamicPageRepository dynamicPageRepository, IDateTimeProvider dateTimeProvider)
    {
        _dynamicPageRepository = dynamicPageRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Created>> Handle(CreateDynamicPageCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_dynamicPageRepository.GetAll().Any(p => p.Route == command.Route))
            return Errors.DynamicPage.AlreadyExist;
        
        var page = new DynamicPage
        {
            Route = command.Route,
            Title = command.Title,
            Content = command.Content,
            Created = _dateTimeProvider.UtcNow,
            LastModified = _dateTimeProvider.UtcNow,
        };
        _dynamicPageRepository.Add(page);

        return Result.Created;
    }
}