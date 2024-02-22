namespace EasyVPN.Application.UnitTests.CommonTestUtils.Constants;

public static partial class Constants
{
    public static class User
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Login = "login";
        public const string Password = "pass1234";
        public const string HashPassword = "hashpass1234";
        public const string Token = "token";
        
        public static IEnumerable<Guid> GetMore(int start = 0, int count = 10)
        {
            for (int i = start; i < count + start; i++)
                yield return GuidGenerator.GuidByIndex(i, Id);
        }
    }
}