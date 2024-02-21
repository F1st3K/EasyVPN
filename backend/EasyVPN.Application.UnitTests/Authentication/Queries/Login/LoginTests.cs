using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Authentication.Queries.Login;

public class LoginTests
{
    private readonly LoginMocks _mocks = new();
    
    [Fact]
    public async Task HandleLoginQuery_WhenIsAllValid_Success()
    {
        //Arrange
        var query = LoginUtils.CreateQuery();

        _mocks.UserRepository.Setup(x =>
                x.GetUserByLogin(Constants.User.Login))
            .Returns(new User
            {
                Id = Constants.User.Id,
                FirstName = Constants.User.FirstName,
                LastName = Constants.User.LastName,
                Login = Constants.User.Login,
                HashPassword = Constants.User.HashPassword
            });
        
        _mocks.HashGenerator.Setup(x => x.Hash(It.IsAny<string>()))
            .Returns(Constants.User.HashPassword);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.IsValid().Should().BeTrue();
    }
    
    [Fact]
    public async Task HandleLoginQuery_WhenInvalidLogin_Error()
    {
        //Arrange
        var query = LoginUtils.CreateQuery();

        _mocks.UserRepository.Setup(x =>
                x.GetUserByLogin(Constants.User.Login))
            .Returns(() => null);
        
        _mocks.HashGenerator.Setup(x => x.Hash(It.IsAny<string>()))
            .Returns(Constants.User.HashPassword);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Authentication.InvalidCredentials);
    }
    
    [Fact]
    public async Task HandleLoginQuery_WhenPasswordInvalid_Error()
    {
        //Arrange
        var query = LoginUtils.CreateQuery();

        _mocks.UserRepository.Setup(x =>
                x.GetUserByLogin(Constants.User.Login))
            .Returns(new User
            {
                Id = Constants.User.Id,
                FirstName = Constants.User.FirstName,
                LastName = Constants.User.LastName,
                Login = Constants.User.Login,
                HashPassword = Constants.User.HashPassword
            });

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Authentication.InvalidCredentials);
    }
}