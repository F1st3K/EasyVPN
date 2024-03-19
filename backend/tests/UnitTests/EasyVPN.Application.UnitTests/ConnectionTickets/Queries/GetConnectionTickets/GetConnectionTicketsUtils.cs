using EasyVPN.Domain.Entities;
using FluentAssertions;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Queries.GetConnectionTickets;

public static class GetConnectionTicketsUtils
{
    public static void Validate(this List<ConnectionTicket> result, List<ConnectionTicket> valid)
        => result.Zip(valid).ToList()
            .ForEach(pair => Validate(pair.First, pair.Second));

    public static void Validate(ConnectionTicket first, ConnectionTicket second)
    {
        first.Id.Should().Be(second.Id);
    }
    
}