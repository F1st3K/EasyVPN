using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.CreateConnectionTicket;

public static class CreateConnectionTicketUtils
{
    public static CreateConnectionTicketCommand CreateCommand()
        => new (Constants.Connection.Id,
            Constants.ConnectionTicket.Days,
            Constants.ConnectionTicket.Description,
            Constants.ConnectionTicket.Images);

    public static bool IsValid(this ConnectionTicket connectionTicket)
        => connectionTicket.ConnectionId == Constants.Connection.Id
           && connectionTicket.ClientId == Constants.User.Id
           && connectionTicket.Status == ConnectionTicketStatus.Pending
           && connectionTicket.CreationTime == Constants.Time.Now
           && connectionTicket.Days == Constants.ConnectionTicket.Days
           && connectionTicket.PaymentDescription == Constants.ConnectionTicket.Description;
}
