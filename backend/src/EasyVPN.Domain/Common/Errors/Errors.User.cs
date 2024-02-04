namespace EasyVPN.Domain.Common.Errors;
using ErrorOr;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateLogin => Error.Conflict(
            code: "User.DuplicateLogin",
            description: "Login is already in use");
    }
}