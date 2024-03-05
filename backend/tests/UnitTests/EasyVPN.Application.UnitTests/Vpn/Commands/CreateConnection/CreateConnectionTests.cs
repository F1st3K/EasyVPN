using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.CreateConnection;

public class CreateConnectionTests
{
    private readonly CreateConnectionMocks _mocks = new();
    
    [Fact]
    public async Task HandleCreateConnectionCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.UserRepository.Setup(x
                => x.GetUserById(Constants.User.Id))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.UserRoleRepository.Setup(x
                => x.GetRolesByUserId(Constants.User.Id))
            .Returns(new List<RoleType>() { RoleType.Client });

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

        _mocks.ConnectionRepository.Verify(x =>
            x.Add(It.Is<Connection>(connection => connection.IsValid())));
        _mocks.VpnService.Verify(x =>
            x.CreateClient(It.Is<Connection>(connection => connection.IsValid())));
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenUserNotFound_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.UserRepository.Setup(x
                => x.GetUserById(Constants.User.Id))
            .Returns(() => null);

        _mocks.UserRoleRepository.Setup(x
                => x.GetRolesByUserId(Constants.User.Id))
            .Returns(new List<RoleType>() { RoleType.Client });

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
        result.FirstError.Should().Be(Errors.User.NotFound);
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenUserNotClient_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.UserRepository.Setup(x
                => x.GetUserById(Constants.User.Id))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.UserRoleRepository.Setup(x
                => x.GetRolesByUserId(Constants.User.Id))
            .Returns(Enumerable.Empty<RoleType>());

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
        result.FirstError.Should().Be(Errors.Access.ClientsOnly);
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenServerNotFound_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.UserRepository.Setup(x
                => x.GetUserById(Constants.User.Id))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.UserRoleRepository.Setup(x
                => x.GetRolesByUserId(Constants.User.Id))
            .Returns(new List<RoleType>() { RoleType.Client });

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
    public async Task HandleCreateConnectionCommand_WhenGetVpnServiceFailed_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.UserRepository.Setup(x
                => x.GetUserById(Constants.User.Id))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.UserRoleRepository.Setup(x
                => x.GetRolesByUserId(Constants.User.Id))
            .Returns(new List<RoleType>() { RoleType.Client });

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
