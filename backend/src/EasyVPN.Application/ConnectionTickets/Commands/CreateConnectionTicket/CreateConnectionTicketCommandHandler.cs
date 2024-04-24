using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;

public class CreateConnectionTicketCommandHandler : IRequestHandler<CreateConnectionTicketCommand, ErrorOr<Created>>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IConnectionTicketRepository _connectionTicketRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateConnectionTicketCommandHandler(
        IDateTimeProvider dateTimeProvider,
        IConnectionRepository connectionRepository, 
        IConnectionTicketRepository connectionTicketRepository)
    {
        _dateTimeProvider = dateTimeProvider;
        _connectionRepository = connectionRepository;
        _connectionTicketRepository = connectionTicketRepository;
    }
    
    public async Task<ErrorOr<Created>> Handle(CreateConnectionTicketCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;

        if (_connectionRepository.Get(command.ConnectionId) is not { } connection)
            return Errors.Connection.NotFound;

        var ticket = new ConnectionTicket
        {
            Id = Guid.NewGuid(),
            ConnectionId = connection.Id,
            Client = connection.Client,
            Status = ConnectionTicketStatus.Pending,
            CreationTime = _dateTimeProvider.UtcNow,
            Days = command.Days,
            PaymentDescription = command.Description,
            Images = command.Images
        };
        _connectionTicketRepository.Add(ticket);

        return Result.Created;
    }
}