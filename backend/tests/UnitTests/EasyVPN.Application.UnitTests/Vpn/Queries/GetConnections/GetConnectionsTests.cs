using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Queries.GetConnections;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;

namespace EasyVPN.Application.UnitTests.Vpn.Queries.GetConnections;

public class GetConnectionsTests
{
    private readonly GetConnectionsMocks _mocks = new();
    
    [Fact]
    public async Task HandleGetConnectionsQuery_WhenPassClientId_Success()
    {
        //Arrange
        var query = new GetConnectionsQuery();

        _mocks.ConnectionRepository.Setup(x =>
                x.Select())
            .Returns(Constants.Connection.GetMore(15)
                    .Select(id => new Connection()).ToList());

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Validate(Constants.Connection.GetMore(15)
            .Select(id => new Connection()).ToList());
    }
    
    [Fact]
    public async Task HandleGetConnectionsQuery_WhenClientIdAllValid_Success()
    {
        //Arrange
        var query = new GetConnectionsQuery(Constants.User.Id);
        
        _mocks.ConnectionRepository.Setup(x =>
                x.Select(Constants.User.Id))
            .Returns(Constants.Connection.GetMore()
                .Select(id => new Connection() {Id = id}).ToList());

        _mocks.UserRepository.Setup(x =>
                x.GetUserById(Constants.User.Id))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.UserRoleRepository.Setup(x =>
                x.GetRolesByUserId(Constants.User.Id))
            .Returns(new[] { RoleType.Client });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Validate(Constants.Connection.GetMore()
            .Select(id => new Connection() {Id = id}).ToList());
    }
    
    [Fact]
    public async Task HandleGetConnectionsQuery_WhenClientIdUserNotFound_Error()
    {
        //Arrange
        var query = new GetConnectionsQuery(Constants.User.Id);
        
        _mocks.ConnectionRepository.Setup(x =>
                x.Select(Constants.User.Id))
            .Returns(Constants.Connection.GetMore()
                .Select(id => new Connection() {Id = id}).ToList());

        _mocks.UserRepository.Setup(x =>
                x.GetUserById(Constants.User.Id))
            .Returns(() => null);

        _mocks.UserRoleRepository.Setup(x =>
                x.GetRolesByUserId(Constants.User.Id))
            .Returns(new[] { RoleType.Client });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.User.NotFound);
    }
    
    [Fact]
    public async Task HandleGetConnectionsQuery_WhenClientIdIsNotClient_Error()
    {
        //Arrange
        var query = new GetConnectionsQuery(Constants.User.Id);
        
        _mocks.ConnectionRepository.Setup(x =>
                x.Select(Constants.User.Id))
            .Returns(Constants.Connection.GetMore()
                .Select(id => new Connection() {Id = id}).ToList());

        _mocks.UserRepository.Setup(x =>
                x.GetUserById(Constants.User.Id))
            .Returns(new User() { Id = Constants.User.Id });

        _mocks.UserRoleRepository.Setup(x =>
                x.GetRolesByUserId(Constants.User.Id))
            .Returns(Enumerable.Empty<RoleType>());

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.FirstError.Should().Be(Errors.Access.ClientsOnly);
    }
}