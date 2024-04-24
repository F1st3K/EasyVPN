using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.AddLifetimeConnection;

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

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());
        
        _mocks.VpnService.Verify(x 
            => x.EnableClient(Constants.Connection.Id));
        _mocks.ConnectionRepository.Verify(x 
            => x.Update(It.Is<Connection>(connection 
                => connection.ExtendIsValid())));
        _mocks.ExpireService.Verify(x
            => x.ResetTrackExpire(It.Is<Connection>(connection
                => connection.ExtendIsValid())));
        _mocks.ExpireService.Verify(x
            => x.AddTrackExpire(It.Is<Connection>(connection
                => connection.ExtendIsValid())));
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

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());
        
        _mocks.VpnService.Verify(x 
            => x.EnableClient(Constants.Connection.Id));
        _mocks.ConnectionRepository.Verify(x 
            => x.Update(It.Is<Connection>(connection 
                => connection.ActivateIsValid())));
        _mocks.ExpireService.Verify(x
            => x.AddTrackExpire(It.Is<Connection>(connection
                => connection.ActivateIsValid())));
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

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

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

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Server.FailedGetService);
    }
}
