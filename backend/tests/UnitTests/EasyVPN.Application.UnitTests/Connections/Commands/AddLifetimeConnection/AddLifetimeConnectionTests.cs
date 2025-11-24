using EasyZsV.Application.Connections.Commands.DisableConnection;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Domain.Common.Errors;
using EasyZsV.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyZsV.Application.UnitTests.Connections.Commands.AddLifetimeConnection;

public class AddLifetimeConnectionTests
{
    private readonly AddLifetimeConnectionMocks _mocks = new();

    [Fact]
    public async Task HandleAddLifetimeConnectionCommand_WhenConnectionIsActive_Success()
    {
        //Arrange
        var command = AddLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.ZsvServiceFactory.Setup(x
                => x.GetZsvService(It.IsAny<Server>()))
            .Returns(_mocks.ZsvService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.ZsvService.Verify(x
            => x.EnableClient(Constants.Connection.Id));
        _mocks.ConnectionRepository.Verify(x
            => x.Update(It.Is<Connection>(connection
                => connection.ExtendIsValid())));
        _mocks.TaskRepository.Verify(x
            => x.TryPopTask<DisableConnectionCommand>(Constants.Connection.Id));
        _mocks.TaskRepository.Verify(x
            => x.PushTask(
                Constants.Connection.Id,
                Constants.Connection.ExpirationTime.AddDays(Constants.Connection.Days),
                It.Is<DisableConnectionCommand>(dc => dc.ConnectionId == Constants.Connection.Id)
                ));
    }

    [Fact]
    public async Task HandleAddLifetimeConnectionCommand_WhenZsvServiceError_Error()
    {
        //Arrange
        var command = AddLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.ZsvServiceFactory.Setup(x
                => x.GetZsvService(It.IsAny<Server>()))
            .Returns(_mocks.ZsvService.Object);

        _mocks.ZsvService.Setup(x
                => x.EnableClient(Constants.Connection.Id))
            .Returns(Constants.Connection.ZsvServiceError);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue(result.FirstError.ToString());
        result.FirstError.Should().Be(Constants.Connection.ZsvServiceError);

        _mocks.ZsvService.Verify(x
            => x.EnableClient(Constants.Connection.Id));
    }

    [Fact]
    public async Task HandleAddLifetimeConnectionCommand_WhenConnectionNotActive_Success()
    {
        //Arrange
        var command = AddLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
            });

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.ZsvServiceFactory.Setup(x
                => x.GetZsvService(It.IsAny<Server>()))
            .Returns(_mocks.ZsvService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.ZsvService.Verify(x
            => x.EnableClient(Constants.Connection.Id));
        _mocks.ConnectionRepository.Verify(x
            => x.Update(It.Is<Connection>(connection
                => connection.ActivateIsValid())));
        _mocks.TaskRepository.Verify(x
            => x.TryPopTask<DisableConnectionCommand>(Constants.Connection.Id));
        _mocks.TaskRepository.Verify(x
            => x.PushTask(
                Constants.Connection.Id,
                Constants.Time.Now.AddDays(Constants.Connection.Days),
                It.Is<DisableConnectionCommand>(dc => dc.ConnectionId == Constants.Connection.Id)
                ));
    }

    [Fact]
    public async Task HandleAddLifetimeConnectionCommand_WhenConnectionNotFound_Error()
    {
        //Arrange
        var command = AddLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => null);

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.ZsvServiceFactory.Setup(x
                => x.GetZsvService(It.IsAny<Server>()))
            .Returns(_mocks.ZsvService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Connection.NotFound);
    }

    [Fact]
    public async Task HandleAddLifetimeConnectionCommand_WhenFailedGetService_Success()
    {
        //Arrange
        var command = AddLifetimeConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
            });

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.ZsvServiceFactory.Setup(x
                => x.GetZsvService(It.IsAny<Server>()))
            .Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Server.FailedGetService);
    }
}
