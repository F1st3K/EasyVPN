using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.ConnectionTickets.Commands.ConfirmConnectionTicket;

public class ConfirmConnectionTicketTests
{
    private readonly ConfirmConnectionTicketMocks _mocks = new();
    
    [Fact]
    public async Task HandleConfirmConnectionTicketCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = ConfirmConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionTicketRepository.Setup(x
                => x.Get(Constants.ConnectionTicket.Id))
            .Returns(new ConnectionTicket()
            {
                Id = Constants.ConnectionTicket.Id,
                Status = ConnectionTicketStatus.Pending
            });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.ConnectionTicketRepository.Verify(x =>
            x.Update(It.Is<ConnectionTicket>(connection => connection.IsValid())));
    }

    [Fact]
    public async Task HandleConfirmConnectionTicketCommand_WhenConnectionTicketNotFound_Error()
    {
        //Arrange
        var command = ConfirmConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionTicketRepository.Setup(x
                => x.Get(Constants.ConnectionTicket.Id))
            .Returns(() => null);
        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.ConnectionTicket.NotFound);
    }

    [Fact]
    public async Task HandleConfirmConnectionTicketCommand_WhenConnectionTicketAlreadyProcessed_Error()
    {
        //Arrange
        var command = ConfirmConnectionTicketUtils.CreateCommand();

        _mocks.ConnectionTicketRepository.Setup(x
                => x.Get(Constants.ConnectionTicket.Id))
            .Returns(new ConnectionTicket()
            {
                Id = Constants.ConnectionTicket.Id,
                Status = ConnectionTicketStatus.Rejected
            });
        
        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.ConnectionTicket.AlreadyProcessed);
    }
}
