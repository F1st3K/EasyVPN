using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.DeleteConnection;

public class DeleteConnectionTests
{
    private readonly DeleteConnectionMocks _mocks = new();
    
    [Fact]
    public async Task HandleDeleteConnectionCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = DeleteConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(new Connection()
            {
                Id = Constants.Connection.Id,
                ServerId = Constants.Server.Id,
                ExpirationTime = Constants.Time.Now
            });
        
        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();

        _mocks.VpnService.Verify(x 
            => x.DeleteClient(Constants.Connection.Id));
        _mocks.ConnectionRepository.Verify(x 
            => x.Remove(Constants.Connection.Id));
    }
    
    [Fact]
    public async Task HandleDeleteConnectionCommand_WhenConnectionNotExpired_Error()
    {
        //Arrange
        var command = DeleteConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(new Connection()
            {
                Id = Constants.Connection.Id,
                ServerId = Constants.Server.Id,
                ExpirationTime = Constants.Connection.ExpirationTime
            });
        
        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Connection.NotExpired);
    }

    [Fact]
    public async Task HandleDeleteConnectionCommand_WhenConnectionNotFound_Error()
    {
        //Arrange
        var command = DeleteConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => null);
        
        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Connection.NotFound);
    }
    
    [Fact]
    public async Task HandleDeleteConnectionCommand_WhenServerNotFound_Error()
    {
        //Arrange
        var command = DeleteConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(new Connection() { Id = Constants.Connection.Id, ServerId = Constants.Server.Id });
        
        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(() => null);

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Server.NotFound);
    }
    
    [Fact]
    public async Task HandleDeleteConnectionCommand_WhenFailedGetService_Error()
    {
        //Arrange
        var command = DeleteConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(new Connection() { Id = Constants.Connection.Id, ServerId = Constants.Server.Id });
        
        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Server.FailedGetService);
    }
}
