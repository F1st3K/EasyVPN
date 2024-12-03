using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Commands.DisableConnection;

public class DisableConnectionTests
{
    private readonly DisableConnectionMocks _mocks = new();

    [Fact]
    public async Task HandleDisableConnectionCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = DisableConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });
        
        _mocks.VpnServiceFactory.Setup(x 
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse(result.FirstError.ToString());

        _mocks.VpnService.Verify(x => x.DisableClient(Constants.Connection.Id));
    }

    [Fact]
    public async Task HandleDisableConnectionCommand_WhenVpnServiceError_Error()
    {
        //Arrange
        var command = DisableConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });
        
        _mocks.VpnServiceFactory.Setup(x 
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        _mocks.VpnService.Setup(x 
                => x.DisableClient(Constants.Connection.Id))
            .Returns(Constants.Connection.VpnServiceError);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue(result.FirstError.ToString());
        result.FirstError.Should().Be(Constants.Connection.VpnServiceError);
    }

    [Fact]
    public async Task HandleDisableConnectionCommand_WhenConnectionNotFound_Error()
    {
        //Arrange
        var command = DisableConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => null);
        
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
    public async Task HandleDisableConnectionCommand_WhenFailedGetService_Error()
    {
        //Arrange
        var command = DisableConnectionUtils.CreateCommand();

        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                ExpirationTime = Constants.Connection.ExpirationTime
            });

        _mocks.VpnServiceFactory.Setup(x
            => x.GetVpnService(It.IsAny<Server>())).Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Server.FailedGetService);
    }
}
