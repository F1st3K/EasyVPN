using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.ResetLifetimeConnection;

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
            => x.DisableClient(Constants.Connection.Id));
        _mocks.ConnectionRepository.Verify(x 
            => x.Update(It.Is<Connection>(connection 
                => connection.IsValid())));
        _mocks.ExpireService.Verify(x
            => x.ResetTrackExpire(It.Is<Connection>(connection
                => connection.IsValid())));
    }
    
    [Fact]
    public async Task HandleResetLifetimeConnectionCommand_WhenConnectionNotFound_Error()
    {
        //Arrange
        var command = ResetLifetimeConnectionUtils.CreateCommand();

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
    public async Task HandleResetLifetimeConnectionCommand_WhenFailedGetService_Error()
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