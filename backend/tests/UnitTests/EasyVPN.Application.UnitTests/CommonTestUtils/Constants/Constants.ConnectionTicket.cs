namespace EasyVPN.Application.UnitTests.CommonTestUtils.Constants;

public static partial class Constants
{
    public static class ConnectionTicket
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly int Days = 30;
        public static readonly string Description = "description";
        public static readonly IEnumerable<string> Images = new[] { "image1", "image2", "image3" };

        public static IEnumerable<Guid> GetMore(int start = 0, int count = 10)
        {
            for (int i = start; i < count + start; i++)
                yield return GuidGenerator.GuidByIndex(i, Id);
        }
    }
}