using EasyVPN.Application.Authentication.Commands.Register;
using EasyVPN.Application.Common.Interfaces.Authentication;
using EasyVPN.Application.Common.Interfaces.Persistence;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using Moq;

namespace EasyVPN.Application.UnitTests.Authentication.Commands.Register;

public class RegisterMocks
{
    public readonly Mock<IUserRepository> UserRepository = new();
    public readonly Mock<IJwtTokenGenerator> JwtTokenGenerator  = new();
    public readonly Mock<IHashGenerator> HashGenerator  = new();
    
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