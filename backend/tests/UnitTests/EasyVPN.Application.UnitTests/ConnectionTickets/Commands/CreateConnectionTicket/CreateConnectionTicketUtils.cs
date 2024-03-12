using EasyVPN.Application.ConnectionTickets.Commands.CreateConnectionTicket;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.CreateConnectionTicket;

public static class CreateConnectionTicketUtils
{
    public static CreateConnectionTicketCommand CreateCommand()
        => new (Constants.Connection.Id,
            Constants.ConnectionTicket.Price,
            Constants.ConnectionTicket.Description);

    public static bool IsValid(this ConnectionTicket connectionTicket)
        => connectionTicket.ConnectionId == Constants.Connection.Id
           && connectionTicket.Status == ConnectionTicketStatus.Pending
           && connectionTicket.CreationTime == Constants.Time.Now
           && Math.Abs(connectionTicket.Price - Constants.ConnectionTicket.Price) < 0.001f
           && connectionTicket.PaymentDescription == Constants.ConnectionTicket.Description;
}
