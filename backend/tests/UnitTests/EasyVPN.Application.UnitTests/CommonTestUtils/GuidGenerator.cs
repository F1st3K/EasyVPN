namespace EasyVPN.Application.UnitTests.CommonTestUtils;

public static class GuidGenerator
{
    public static Guid GuidByIndex(int index, Guid guid)
    {
        var gb = guid.ToByteArray();
        var ib = BitConverter.GetBytes(index);
        var guidBytes = new byte[16] 
            {
                ib[0], ib[1], ib[2], ib[3],
                gb[4], gb[5], gb[6], gb[7],
                gb[8], gb[9], gb[10], gb[11],
                gb[12], gb[13], gb[14], gb[15]
            };
        return new Guid(guidBytes);
    }
}