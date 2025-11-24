using EasyZsV.Application.Connections.Commands.DisableConnection;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using ErrorOr;
using FluentAssertions;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.ResetLifetimeConnection;

public class ResetLifetimeConnectionTests
{
    private readonly ResetLifetimeConnectionMocks _mocks = new();

    [Fact]
    public async Task HandleResetLifetimeConnectionCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = ResetLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.ConnectionRepository.Verify(x
            => x.Update(It.Is<Connection>(connection
                => connection.IsValid())));
        _mocks.TaskRepository.Verify(x
            => x.TryPopTask<DisableConnectionCommand>(Constants.Connection.Id));
        _mocks.Sender.Verify(x
            => x.Send(It.Is<DisableConnectionCommand>(c
                => c.ConnectionId == Constants.Connection.Id),
                It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task HandleResetLifetimeConnectionCommand_WhenDisableError_Error()
    {
        //Arrange
        var command = ResetLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });

        _mocks.Sender.Setup(x
                => x.Send(It.Is<DisableConnectionCommand>(c
                    => c.ConnectionId == Constants.Connection.Id),
                    It.IsAny<CancellationToken>()))
            .Returns(Task.Run<ErrorOr<Updated>>(() => Constants.Connection.ZsvServiceError));

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue(result.FirstError.ToString());
        result.FirstError.Should().Be(Constants.Connection.ZsvServiceError);
    }

    [Fact]
    public async Task HandleResetLifetimeConnectionCommand_WhenConnectionNotFound_Error()
    {
        //Arrange
        var command = ResetLifetimeConnectionUtils.CreateCommand();

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
