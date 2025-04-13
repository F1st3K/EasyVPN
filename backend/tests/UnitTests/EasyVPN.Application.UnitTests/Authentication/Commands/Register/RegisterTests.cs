using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EasyVPN.Application.UnitTests.Authentication.Commands.Register;

public class RegisterTests
{
    private readonly RegisterMocks _mocks = new();

    [Fact]
    public async Task HandleRegisterCommand_WhenIsAllValid_Success()
    {
        //Arrange
        var command = RegisterUtils.CreateCommand();

        _mocks.UserRepository.Setup(x =>
                x.GetByLogin(Constants.User.Login))
            .Returns(() => null);

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.IsValid().Should().BeTrue();

        _mocks.UserRepository.Verify(x =>
            x.Add(It.Is<User>(u => u.IsValid())));
    }

    [Fact]
    public async Task HandleRegisterCommand_WhenUserExist_Error()
    {
        //Arrange
        var command = RegisterUtils.CreateCommand();

        _mocks.UserRepository.Setup(x =>
                x.GetByLogin(Constants.User.Login))
            .Returns(new User());

        //Act
        var handler = _mocks.CreateHandler();
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.DuplicateLogin);
    }
}