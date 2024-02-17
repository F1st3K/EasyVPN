using EasyVPN.Application.Common.Interfaces.Services;

namespace EasyVPN.Application.UnitTests.CommonTestUtils.Dependencies.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}