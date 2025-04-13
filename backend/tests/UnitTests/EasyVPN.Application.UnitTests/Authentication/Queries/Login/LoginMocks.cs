using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Authentication.Queries.Login;

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