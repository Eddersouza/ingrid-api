namespace IP.Shared.Abstractions.Extensions;

public static class GuidExtensions
{
    public static DateTime GetDateTime(this Guid Value)
    {
        byte[] bytes = new byte[8];
        Value.ToByteArray(true)[0..6].CopyTo(bytes, 2);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        long ms = BitConverter.ToInt64(bytes);
        return DateTimeOffset.FromUnixTimeMilliseconds(ms).DateTime;
    }
}