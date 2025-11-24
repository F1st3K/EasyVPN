using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.Common.Interfaces.Services;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Commands.CreateDynamicPage;

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

        if (_dynamicPageRepository.Get(command.Route) is { } existing)
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