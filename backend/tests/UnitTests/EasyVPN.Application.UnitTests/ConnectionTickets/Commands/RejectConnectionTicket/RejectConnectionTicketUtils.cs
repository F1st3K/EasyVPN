using EasyZsV.Application.ConnectionTickets.Commands.ConfirmConnectionTicket;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.UnitTests.ConnectionTickets.Commands.RejectConnectionTicket;

public static class RejectConnectionTicketUtils
{
    public static ConfirmConnectionTicketCommand CreateCommand()
        => new(Constants.ConnectionTicket.Id);

    public static bool IsValid(this ConnectionTicket connectionTicket)
        => connectionTicket.Id == Constants.ConnectionTicket.Id
           && connectionTicket.Status == ConnectionTicketStatus.Rejected;
}
