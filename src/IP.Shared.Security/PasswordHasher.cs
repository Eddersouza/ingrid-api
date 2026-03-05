using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace IP.Shared.Security;

public interface IPasswordHasher
{
    string CreateSecurePassword();

    string CreateSecurePasswordEncripted();

    string Hash(string password);

    bool Verify(string hashedPassword, string password);
}

internal sealed class PasswordHasher : IPasswordHasher
{
    private static readonly string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private static readonly string Numbers = "0123456789";
    private static readonly string SpecialChars = "!@#$%^&*()-_=+[]{}<>?/|";
    private static readonly string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private readonly int ITERATIONS = 100_000;
    private readonly byte[] salt = new byte[16];

    public string CreateSecurePassword()
    {
        Random random = new();

        int length = random.Next(8, 17);

        StringBuilder password = new();

        password.Append(Uppercase[random.Next(Uppercase.Length)]);
        password.Append(Lowercase[random.Next(Lowercase.Length)]);
        password.Append(Numbers[random.Next(Numbers.Length)]);
        password.Append(SpecialChars[random.Next(SpecialChars.Length)]);

        string allChars = Uppercase + Lowercase + Numbers + SpecialChars;
        for (int i = password.Length; i < length; i++)
        {
            password.Append(allChars[random.Next(allChars.Length)]);
        }

        return new string([.. password.ToString().OrderBy(c => random.Next())]);
    }

    public string CreateSecurePasswordEncripted() => Hash(CreateSecurePassword());

    public string Hash(string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        byte[] bytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: ITERATIONS,
            numBytesRequested: 32);

        byte[] dst = new byte[49];
        Buffer.BlockCopy(salt, 0, dst, 1, 16);
        Buffer.BlockCopy(bytes, 0, dst, 17, 32);
        return Convert.ToBase64String(dst);
    }

    public bool Verify(string hashedPassword, string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        if (hashedPassword == null) return false;

        byte[] src = Convert.FromBase64String(hashedPassword);
        if (src.Length != 49) return false;

        byte[] bytes = new byte[32];
        Buffer.BlockCopy(src, 1, salt, 0, 16);
        Buffer.BlockCopy(src, 17, bytes, 0, 32);

        byte[] verifiedBytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: ITERATIONS,
            numBytesRequested: 32);

        return ByteArraysEqual(bytes, verifiedBytes);
    }

    private static bool ByteArraysEqual(byte[] bytesA, byte[] bytesB)
    {
        if (bytesA == bytesB) return true;
        if (bytesA == null || bytesB == null) return false;
        if (bytesA.Length != bytesB.Length) return false;

        for (int indexBytesA = 0; indexBytesA < bytesA.Length; indexBytesA++)
            if (bytesA[indexBytesA] != bytesB[indexBytesA]) return false;

        return true;
    }
}