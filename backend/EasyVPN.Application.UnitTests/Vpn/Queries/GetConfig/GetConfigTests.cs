using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Queries.GetConfig;

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
                ServerId = Constants.Server.Id,
                ClientId = Constants.User.Id
            });

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.IsValid().Should().BeTrue();
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

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Connection.NotFound);
    }
    
    [Fact]
    public async Task HandleGetConfigQuery_WhenServerNotFound_Error()
    {
        //Arrange
        var query = GetConfigUtils.CreateQuery();

        _mocks.ConnectionRepository.Setup(x =>
                x.Get(Constants.Connection.Id))
            .Returns(new Connection()
            {
                Id = Constants.Connection.Id,
                ServerId = Constants.Server.Id,
                ClientId = Constants.User.Id
            });

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(() => null);
        
        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Server.NotFound);
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
                ServerId = Constants.Server.Id,
                ClientId = Constants.User.Id
            });

        _mocks.ServerRepository.Setup(x =>
                x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x =>
                x.GetVpnService(It.IsAny<Server>()))
            .Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Server.FailedGetService);
    }
}