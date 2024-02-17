namespace EasyVPN.Application.UnitTests.CommonTestUtils.Constants;

public static partial class Constants
{
    public static class Connection
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly int Days = 30;

        public static IEnumerable<Guid> GetMoreId(int count)
        {
            for (int i = 0; i < count; i++)
                yield return Guid.NewGuid();
        }
    }
}