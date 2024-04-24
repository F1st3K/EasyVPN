using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Connections.Queries.GetConnections;
using EasyVPN.Domain.Entities;
using FluentAssertions;

namespace EasyVPN.Application.UnitTests.Connections.Queries.GetConnections;

public class GetConnectionsTests
{
    private readonly GetConnectionsMocks _mocks = new();
    
    [Fact]
    public async Task HandleGetConnectionsQuery_WhenPassClientId_Success()
    {
        //Arrange
        var query = new GetConnectionsQuery();

        _mocks.ConnectionRepository.Setup(x => x.GetAll())
            .Returns(
                Constants.Connection.GetMore(5).Zip(Constants.User.GetMore(5))
                    .Select(ids => 
                        new Connection() { Id = ids.First, Client = new User() {Id = ids.Second } })
            );

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Validate(Constants.Connection.GetMore(5).Zip(Constants.User.GetMore(5))
            .Select(ids => new Connection() { Id = ids.First, Client = new User() {Id = ids.Second } }).ToList());
    }
    
    [Fact]
    public async Task HandleGetConnectionsQuery_WhenClientIdAllValid_Success()
    {
        //Arrange
        var query = new GetConnectionsQuery(Constants.User.Id);
        
        _mocks.ConnectionRepository.Setup(x => x.GetAll())
            .Returns(
                Constants.Connection.GetMore(5).Zip(Constants.User.GetMore(5))
                    .Select(ids => 
                        new Connection() { Id = ids.First, Client = new User() {Id = ids.Second }})
                    .Concat(Constants.Connection.GetMore(5, 5)
                        .Select(id => 
                            new Connection(){Id = id, Client = new User() {Id = Constants.User.Id }})
                    )
            );

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Validate(Constants.Connection.GetMore(5, 5)
            .Select(id => 
                new Connection(){Id = id, Client = new User() {Id = Constants.User.Id } }).ToList());
    }
}