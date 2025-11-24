using EasyZsV.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.UnitTests.ConnectionTickets.Commands.CreateConnectionTicket;

public static class CreateConnectionTicketUtils
{
    public static CreateConnectionTicketCommand CreateCommand()
        => new(Constants.Connection.Id,
            Constants.ConnectionTicket.Days,
            Constants.ConnectionTicket.Description,
            Constants.ConnectionTicket.Images);

    public static bool IsValid(this ConnectionTicket connectionTicket)
        => connectionTicket.ConnectionId == Constants.Connection.Id
           && connectionTicket.Client.Id == Constants.User.Id
           && connectionTicket.Status == ConnectionTicketStatus.Pending
           && connectionTicket.CreationTime == Constants.Time.Now
           && connectionTicket.Days == Constants.ConnectionTicket.Days
           && connectionTicket.PaymentDescription == Constants.ConnectionTicket.Description;
}
