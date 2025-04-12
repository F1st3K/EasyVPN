using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.ConnectionTickets.Queries.GetConnectionTickets;
using EasyVPN.Domain.Entities;
using FluentAssertions;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Queries.GetConnectionTickets;

public class GetConnectionTicketsTests
{
    private readonly GetConnectionTicketsMocks _mocks = new();

    [Fact]
    public async Task HandleGetConnectionsQuery_WhenPassClientId_Success()
    {
        //Arrange
        var query = new GetConnectionTicketsQuery();

        _mocks.ConnectionTicketRepository.Setup(x => x.GetAll())
            .Returns(
                Constants.ConnectionTicket.GetMore(5).Zip(Constants.User.GetMore(5))
                    .Select(ids =>
                        new ConnectionTicket() { Id = ids.First, Client = new() { Id = ids.Second } })
            );

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Validate(Constants.ConnectionTicket.GetMore(5).Zip(Constants.User.GetMore(5))
            .Select(ids => new ConnectionTicket() { Id = ids.First, Client = new() { Id = ids.Second } }).ToList());
    }

    [Fact]
    public async Task HandleGetConnectionsQuery_WhenClientIdAllValid_Success()
    {
        //Arrange
        var query = new GetConnectionTicketsQuery(Constants.User.Id);

        _mocks.ConnectionTicketRepository.Setup(x => x.GetAll())
            .Returns(
                Constants.ConnectionTicket.GetMore(5).Zip(Constants.User.GetMore(5))
                    .Select(ids =>
                        new ConnectionTicket() { Id = ids.First, Client = new() { Id = ids.Second } })
                    .Concat(Constants.ConnectionTicket.GetMore(5, 5)
                        .Select(id =>
                            new ConnectionTicket() { Id = id, Client = new() { Id = Constants.User.Id } })
                    )
            );

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Validate(Constants.ConnectionTicket.GetMore(5, 5)
            .Select(id =>
                new ConnectionTicket() { Id = id, Client = new() { Id = Constants.User.Id } }).ToList());
    }
}