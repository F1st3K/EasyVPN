using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.Common.Interfaces.Services;
using EasyVPN.Application.Common.Interfaces.Vpn;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Application.Vpn.Commands.CreateConnection;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Vpn.Commands.CreateConnection;

public class CreateConnectionTests
{
    [Fact]
    public async Task HandleCreateConnectionCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = CreateCommand();
        
        _mockUserRepository.Setup(x 
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(new User(){ Id = Constants.User.Id });
        
        _mockUserRoleRepository.Setup(x 
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(new List<RoleType>() { RoleType.Client });
        
        _mockServerRepository.Setup(x 
                => x.Get(It.IsAny<Guid>()))
            .Returns(new Server() { Id = Constants.Server.Id });
            
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        
        _mockConnectionRepository.Verify(x => 
            x.Add(It.Is<Connection>(connection => IsValid(connection) )));
        _mockVpnService.Verify(x => 
            x.CreateClient(It.Is<Connection>(connection => IsValid(connection) )));
    }

    [Fact]
    public async Task HandleCreateConnectionCommand_WhenUserNotFound_Error()
    {
        //Arrange
        var command = CreateCommand();
        
        _mockUserRepository.Setup(x 
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(() => null);
        
        _mockUserRoleRepository.Setup(x 
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(new List<RoleType>() { RoleType.Client });
        
        _mockServerRepository.Setup(x 
                => x.Get(It.IsAny<Guid>()))
            .Returns(new Server() { Id = Constants.Server.Id });
            
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.NotFound);
    }
    
    [Fact]
    public async Task HandleCreateConnectionCommand_WhenUserNotClient_Error()
    {
        //Arrange
        var command = CreateCommand();
        
        _mockUserRepository.Setup(x 
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(new User(){ Id = Constants.User.Id });
        
        _mockUserRoleRepository.Setup(x 
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(Enumerable.Empty<RoleType>());
        
        _mockServerRepository.Setup(x 
                => x.Get(It.IsAny<Guid>()))
            .Returns(new Server() { Id = Constants.Server.Id });
            
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Access.ClientsOnly);
    }
    
    [Fact]
    public async Task HandleCreateConnectionCommand_WhenServerNotFound_Error()
    {
        //Arrange
        var command = CreateCommand();
        
        _mockUserRepository.Setup(x 
                => x.GetUserById(It.IsAny<Guid>()))
            .Returns(new User(){ Id = Constants.User.Id });
        
        _mockUserRoleRepository.Setup(x 
                => x.GetRolesByUserId(It.IsAny<Guid>()))
            .Returns(new List<RoleType>() { RoleType.Client });
        
        _mockServerRepository.Setup(x 
                => x.Get(It.IsAny<Guid>()))
            .Returns(() => null);
            
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Server.NotFound);
    }

    private readonly Mock<IUserRepository> _mockUserRepository = new();
    private readonly Mock<IUserRoleRepository> _mockUserRoleRepository = new();
    private readonly Mock<IServerRepository> _mockServerRepository = new();
    private readonly Mock<IConnectionRepository> _mockConnectionRepository = new();
    private readonly Mock<IVpnService> _mockVpnService = new();

    private CreateConnectionCommandHandler CreateHandler()
    {
        var mockVpnServiceFactory = new Mock<IVpnServiceFactory>();
        mockVpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mockVpnService.Object);
        var mockDateTimeProvider = new Mock<IDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.UtcNow)
            .Returns(DateTime.MinValue);
        return new CreateConnectionCommandHandler(
            _mockUserRepository.Object,
            _mockUserRoleRepository.Object,
            _mockServerRepository.Object,
            _mockConnectionRepository.Object,
            mockVpnServiceFactory.Object,
            mockDateTimeProvider.Object
        );
    }
    
    private static CreateConnectionCommand CreateCommand()
        => new (Constants.User.Id,
            Constants.Server.Id,
            Constants.Connection.Days);

    private static bool IsValid(Connection connection)
        => connection.ClientId == Constants.User.Id
           && connection.ServerId == Constants.Server.Id
           && connection.ExpirationTime == DateTime.MinValue.AddDays(Constants.Connection.Days);
}
