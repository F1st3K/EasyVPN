using EasyVPN.Application.Authentication.Commands.Register;
using EasyVPN.Application.Authentication.Common;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Authentication.Commands.Register;

public static class RegisterUtils
{
    public static Application.Authentication.Commands.Register.RegisterCommand CreateCommand()
        => new Application.Authentication.Commands.Register.RegisterCommand(
            Constants.User.FirstName,
            Constants.User.LastName,
            Constants.User.Login,
            Constants.User.Password);

    public static bool IsValid(this AuthenticationResult result)
        => result.User.IsValid()
           && result.Token == Constants.User.Token;

    public static bool IsValid(this User user)
        => user.Roles.Any(r => r == Constants.User.Role)
           && user.Roles.Count() == 1
           && user.FirstName == Constants.User.FirstName
           && user.LastName == Constants.User.LastName
           && user.Login == Constants.User.Login
           && user.HashPassword == Constants.User.HashPassword;
}