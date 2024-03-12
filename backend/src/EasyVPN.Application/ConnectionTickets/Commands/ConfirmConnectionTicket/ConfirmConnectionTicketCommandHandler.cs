using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;

public class ConfirmConnectionTicketCommandHandler : IRequestHandler<ConfirmConnectionTicketCommand, ErrorOr<Success>>
{
    private readonly IConnectionTicketRepository _connectionTicketRepository;

    public ConfirmConnectionTicketCommandHandler(
        IConnectionTicketRepository connectionTicketRepository)
    {
        _connectionTicketRepository = connectionTicketRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(ConfirmConnectionTicketCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;

        if (_connectionTicketRepository.Get(command.Id) is not { } ticket)
            return Errors.ConnectionTicket.NotFound;

        if (ticket.Status != ConnectionTicketStatus.Pending)
            return Errors.ConnectionTicket.AlreadyProcessed;

        ticket.Status = ConnectionTicketStatus.Confirmed;
        _connectionTicketRepository.Update(ticket);

        return new Success();
    }
}