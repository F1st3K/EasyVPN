using EasyZsV.Application.Authentication.Commands.Register;
using EasyZsV.Application.Common.Interfaces.Authentication;
using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;
using Moq;

namespace EasyZsV.Application.UnitTests.Authentication.Commands.Register;

public class RegisterMocks
{
    public readonly Mock<IUserRepository> UserRepository = new();
    public readonly Mock<IJwtTokenGenerator> JwtTokenGenerator = new();
    public readonly Mock<IHashGenerator> HashGenerator = new();

    public RegisterCommandHandler CreateHandler()
    {
        JwtTokenGenerator.Setup(x =>
                x.GenerateToken(It.IsAny<User>()))
            .Returns(Constants.User.Token);
        HashGenerator.Setup(x =>
                x.Hash(It.IsAny<string>()))
            .Returns(Constants.User.HashPassword);
        return new RegisterCommandHandler(
            UserRepository.Object,
            JwtTokenGenerator.Object,
            HashGenerator.Object);
    }
}