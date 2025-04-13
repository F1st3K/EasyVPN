using EasyVPN.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.RejectConnectionTicket;

public static class RejectConnectionTicketUtils
{
    public static ConfirmConnectionTicketCommand CreateCommand()
        => new(Constants.ConnectionTicket.Id);

    public static bool IsValid(this ConnectionTicket connectionTicket)
        => connectionTicket.Id == Constants.ConnectionTicket.Id
           && connectionTicket.Status == ConnectionTicketStatus.Rejected;
}
