using EasyVPN.Application.Authentication.Common;
using EasyVPN.Application.Authentication.Queries.Login;
using EasyVPN.Application.UnitTests.CommonTestUtils.Constants;
using EasyVPN.Domain.Entities;

namespace EasyVPN.Application.UnitTests.Authentication.Queries.Login;

public static class LoginUtils
{
    public static LoginQuery CreateQuery()
        => new(Constants.User.Login, Constants.User.Password);

    public static bool IsValid(this AuthenticationResult result)
        => result.User.IsValid()
           && result.Token == Constants.User.Token;

    public static bool IsValid(this User user)
        => user.Id == Constants.User.Id
           && user.Roles.Any(r => r == Constants.User.Role)
           && user.Roles.Count() == 1
           && user.FirstName == Constants.User.FirstName
           && user.LastName == Constants.User.LastName
           && user.Login == Constants.User.Login
           && user.HashPassword == Constants.User.HashPassword;
}