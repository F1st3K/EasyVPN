namespace EasyVPN.Application.UnitTests.CommonTestUtils.Constants;

public static partial class Constants
{
    public static class ConnectionTicket
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly int Days = 30;
        public static readonly string Description = "description";
    }
}