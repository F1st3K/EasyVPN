using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.ConnectionTickets.Commands.RejectConnectionTicket;

public class RejectConnectionTicketCommandHandler : IRequestHandler<RejectConnectionTicketCommand, ErrorOr<Success>>
{
    private readonly IConnectionTicketRepository _connectionTicketRepository;

    public RejectConnectionTicketCommandHandler(
        IConnectionTicketRepository connectionTicketRepository)
    {
        _connectionTicketRepository = connectionTicketRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(RejectConnectionTicketCommand command, CancellationToken cancellationToken)
    {   
        await Task.CompletedTask;

        if (_connectionTicketRepository.Get(command.Id) is not { } ticket)
            return Errors.ConnectionTicket.NotFound;
        
        if (ticket.Status != ConnectionTicketStatus.Pending)
            return Errors.ConnectionTicket.AlreadyProcessed;

        ticket.Status = ConnectionTicketStatus.Rejected;
        _connectionTicketRepository.Update(ticket);

        return new Success();
    }
}