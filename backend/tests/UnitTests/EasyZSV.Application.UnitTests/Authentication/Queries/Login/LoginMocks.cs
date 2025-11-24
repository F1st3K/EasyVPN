using EasyZsV.Application.Authentication.Queries.Login;
using EasyZsV.Application.Common.Interfaces.Authentication;
using EasyZsV.Application.Common.Interfaces.Persistence;
using EasyZsV.Application.UnitTests.CommonTestUtils.Constants;
using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;
using Moq;

namespace EasyZsV.Application.UnitTests.Authentication.Queries.Login;

public class LoginMocks
{
    public readonly Mock<IUserRepository> UserRepository = new();
    public readonly Mock<IJwtTokenGenerator> JwtTokenGenerator = new();
    public readonly Mock<IHashGenerator> HashGenerator = new();

    public LoginQueryHandler CreateHandler()
    {
        JwtTokenGenerator.Setup(x =>
                x.GenerateToken(It.IsAny<User>()))
            .Returns(Constants.User.Token);
        return new LoginQueryHandler(
            UserRepository.Object,
            JwtTokenGenerator.Object,
            HashGenerator.Object);
    }
}