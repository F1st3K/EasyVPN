using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.RejectConnectionTicket;

public class RejectConnectionTicketTests
{
    private readonly RejectConnectionTicketMocks _mocks = new();
    
    [Fact]
    public async Task HandleRejectConnectionTicketCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = ConfirmConnectionTicket.ConfirmConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionTicketRepository.Setup(x
                => x.Get(Constants.ConnectionTicket.Id))
            .Returns(new ConnectionTicket() { Id = Constants.ConnectionTicket.Id });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.ConnectionTicketRepository.Verify(x =>
            x.Update(It.Is<ConnectionTicket>(connection => ConfirmConnectionTicket.ConfirmConnectionTicketUtils.IsValid(connection))));
    }

    [Fact]
    public async Task HandleRejectConnectionTicketCommand_WhenConnectionTicketNotFound_Error()
    {
        //Arrange
        var command = ConfirmConnectionTicket.ConfirmConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionTicketRepository.Setup(x
                => x.Get(Constants.ConnectionTicket.Id))
            .Returns(() => null);
        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.ConnectionTicket.NotFound);
    }

}
