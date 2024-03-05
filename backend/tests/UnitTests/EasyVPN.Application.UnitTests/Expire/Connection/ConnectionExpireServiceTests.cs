using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Entities;
using ErrorOr;
using Moq;

namespace EasyVPN.Application.UnitTests.Expire.Connection;

public class ConnectionExpireServiceTests
{
    private readonly ConnectionExpireServiceMocks _mocks = new ();
    
    [Fact]
    public async Task AddTrackExpire_WhenIsAllValid_Success()
    {
        await Task.CompletedTask;
        
        //Arrange
        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(ConnectionExpireServiceUtils.GetConnection);

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var connectionExpireService = _mocks.Create();
        connectionExpireService.AddTrackExpire(
            ConnectionExpireServiceUtils.GetConnection());

        //Assert
        _mocks.VpnService.Verify(x 
            => x.DisableClient(Constants.Connection.Id));
    }
    
    [Fact]
    public async Task AddActiveConnectionsToTrackExpire_WhenIsAllValid_Success()
    {
        await Task.CompletedTask;
        
        //Arrange
        _mocks.ConnectionRepository.Setup(x
                => x.GetAll())
            .Returns(Constants.Connection.GetMore(start: 0, count: 5)
                .Select(id => new Domain.Entities.Connection() 
                    { Id = id, ExpirationTime = Constants.Connection.ExpirationTime })
                .Concat(Constants.Connection.GetMore(start: 5, count: 10)
                .Select(id => new Domain.Entities.Connection() 
                    { Id = id, ExpirationTime = Constants.Time.Now })));
        
        //Act
        var connectionExpireService = _mocks.Create();
        connectionExpireService.AddAllToTrackExpire();

        //Assert
        _mocks.ConnectionRepository.Verify();
        _mocks.ExpirationChecker.Verify(x
            => x.NewExpire(It.IsIn(Constants.Connection.GetMore(0, 5)),
                Constants.Connection.ExpirationTime,
                It.IsAny<Func<ErrorOr<Success>>>()),
            Times.Exactly(5));
        _mocks.ExpirationChecker.Verify(x
                => x.TryRemoveExpire(It.IsIn(Constants.Connection.GetMore(5, 10))),
            Times.Exactly(10));
    }

    
    [Fact]
    public async Task AddTrackExpire_WhenConnectionNotFound_Nothing()
    {
        await Task.CompletedTask;
        //Arrange
        
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
        var connectionExpireService = _mocks.Create();
        connectionExpireService.AddTrackExpire(
            ConnectionExpireServiceUtils.GetConnection());

        //Assert
        _mocks.VpnService.Verify(x 
            => x.DisableClient(It.IsAny<Guid>()), Times.Never);
        _mocks.ConnectionRepository.Verify(x 
            => x.Update(It.IsAny<Domain.Entities.Connection>()), Times.Never);
    }
    
    [Fact]
    public async Task AddTrackExpire_WhenServerNotFound_Nothing()
    {
        await Task.CompletedTask;
        //Arrange
        
        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(ConnectionExpireServiceUtils.GetConnection);

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(() => null);

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(_mocks.VpnService.Object);

        //Act
        var connectionExpireService = _mocks.Create();
        connectionExpireService.AddTrackExpire(
            ConnectionExpireServiceUtils.GetConnection());

        //Assert
        _mocks.VpnService.Verify(x 
            => x.DisableClient(It.IsAny<Guid>()), Times.Never);
        _mocks.ConnectionRepository.Verify(x 
            => x.Update(It.IsAny<Domain.Entities.Connection>()), Times.Never);
    }
    
    [Fact]
    public async Task AddTrackExpire_WhenFailedGetVpnService_Nothing()
    {
        await Task.CompletedTask;
        //Arrange
        
        _mocks.ConnectionRepository.Setup(x
                => x.Get(Constants.Connection.Id))
            .Returns(() => null);

        _mocks.ServerRepository.Setup(x
                => x.Get(Constants.Server.Id))
            .Returns(new Server() { Id = Constants.Server.Id });

        _mocks.VpnServiceFactory.Setup(x
                => x.GetVpnService(It.IsAny<Server>()))
            .Returns(() => null);

        //Act
        var connectionExpireService = _mocks.Create();
        connectionExpireService.AddTrackExpire(
            ConnectionExpireServiceUtils.GetConnection());

        //Assert
        _mocks.VpnService.Verify(x 
            => x.DisableClient(It.IsAny<Guid>()), Times.Never);
        _mocks.ConnectionRepository.Verify(x 
            => x.Update(It.IsAny<Domain.Entities.Connection>()), Times.Never);
    }
}