using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Connections.Queries.GetConfig;

public class GetConfigTests
{
    private readonly GetConfigMocks _mocks = new();

    [Fact]
    public async Task HandleGetConfigQuery_WhenIsAllValid_Success()
    {
        //Arrange
        var query = GetConfigUtils.CreateQuery();

        _mocks.ConnectionRepository.Setup(x =>
                x.Get(Constants.Connection.Id))
            .Returns(new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                Client = new User() { Id = Constants.User.Id }
            });

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        _mocks.VpnService.Setup(x =>
                x.GetConfig(It.IsAny<Guid>()))
            .Returns(Constants.Connection.Config);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.IsValid().Should().BeTrue();
    }

    [Fact]
    public async Task HandleGetConfigQuery_WhenVpnServiceError_Error()
    {
        //Arrange
        var query = GetConfigUtils.CreateQuery();

        _mocks.ConnectionRepository.Setup(x =>
                x.Get(Constants.Connection.Id))
            .Returns(new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                Client = new User() { Id = Constants.User.Id }
            });

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        _mocks.VpnService.Setup(x
                => x.GetConfig(Constants.Connection.Id))
            .Returns(Constants.Connection.VpnServiceError);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert

        _mocks.VpnService.Verify(x
            => x.GetConfig(Constants.Connection.Id));
        result.IsError.Should().BeTrue(result.FirstError.ToString());
        result.FirstError.Should().Be(Constants.Connection.VpnServiceError);
    }

    [Fact]
    public async Task HandleGetConfigQuery_WhenConnectionNotFound_Error()
    {
        //Arrange
        var query = GetConfigUtils.CreateQuery();

        _mocks.ConnectionRepository.Setup(x =>
                x.Get(Constants.Connection.Id))
            .Returns(() => null);

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        _mocks.VpnService.Setup(x =>
                x.GetConfig(It.IsAny<Guid>()))
            .Returns(Constants.Connection.Config);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Connection.NotFound);
    }

    [Fact]
    public async Task HandleGetConfigQuery_WhenGetVpnServiceFailed_Error()
    {
        //Arrange
        var query = GetConfigUtils.CreateQuery();

        _mocks.ConnectionRepository.Setup(x =>
                x.Get(Constants.Connection.Id))
            .Returns(new Connection()
            {
                Id = Constants.Connection.Id,
                Server = new() { Id = Constants.Server.Id },
                Client = new User() { Id = Constants.User.Id }
            });

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(() => null);

        _mocks.VpnService.Setup(x =>
                x.GetConfig(It.IsAny<Guid>()))
            .Returns(Constants.Connection.Config);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Server.FailedGetService);
    }
}
