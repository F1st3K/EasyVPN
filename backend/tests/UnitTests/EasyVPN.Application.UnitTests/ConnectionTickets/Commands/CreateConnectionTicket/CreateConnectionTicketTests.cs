using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.CreateConnectionTicket;

public class CreateConnectionTicketTests
{
    private readonly CreateConnectionTicketMocks _mocks = new();

    [Fact]
    public async Task HandleCreateConnectionTicketCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = CreateConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(new Connection() { Id = Constants.Connection.Id, Client = new User() { Id = Constants.User.Id } });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.ConnectionTicketRepository.Verify(x =>
            x.Add(It.Is<ConnectionTicket>(connection => connection.IsValid())));
    }

    [Fact]
    public async Task HandleCreateConnectionTicketCommand_WhenConnectionNotFound_Error()
    {
        //Arrange
        var command = CreateConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => null);
        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Connection.NotFound);
    }

}
