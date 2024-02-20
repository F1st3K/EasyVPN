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

        _mocks.MockUserRepository.Setup(x
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.MockUserRoleRepository.Setup(x
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(new List<RoleType>() { RoleType.Client });

        _mocks.MockServerRepository.Setup(x
                => x.Get(It.IsAny<Guid>()))
            .Returns(new Server() { Id = Constants.Server.Id });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();

        _mocks.MockConnectionRepository.Verify(x =>
            x.Add(It.Is<Connection>(connection => connection.IsValid())));
        _mocks.MockVpnService.Verify(x =>
            x.CreateClient(It.Is<Connection>(connection => connection.IsValid())));
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenUserNotFound_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.MockUserRepository.Setup(x
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(() => null);

        _mocks.MockUserRoleRepository.Setup(x
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(new List<RoleType>() { RoleType.Client });

        _mocks.MockServerRepository.Setup(x
                => x.Get(It.IsAny<Guid>()))
            .Returns(new Server() { Id = Constants.Server.Id });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.NotFound);
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenUserNotClient_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.MockUserRepository.Setup(x
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.MockUserRoleRepository.Setup(x
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(Enumerable.Empty<RoleType>());

        _mocks.MockServerRepository.Setup(x
                => x.Get(It.IsAny<Guid>()))
            .Returns(new Server() { Id = Constants.Server.Id });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Access.ClientsOnly);
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenServerNotFound_Error()
    {
        //Arrange
        var command = CreateConnectionUtils.CreateCommand();

        _mocks.MockUserRepository.Setup(x
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.MockUserRoleRepository.Setup(x
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(new List<RoleType>() { RoleType.Client });

        _mocks.MockServerRepository.Setup(x
                => x.Get(It.IsAny<Guid>()))
            .Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Server.NotFound);
    }

}
