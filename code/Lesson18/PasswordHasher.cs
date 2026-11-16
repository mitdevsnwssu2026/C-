// PasswordHasher.cs - safely turns passwords into hashes, and checks them later.
// Uses PBKDF2: a SLOW hash with a random SALT. This is the safe way to store passwords.

using System.Security.Cryptography;

public static class PasswordHasher
{
    private const int SaltSize = 16;          // 16 random bytes of salt
    private const int HashSize = 32;          // 32 bytes of hash
    private const int Iterations = 600_000;   // how many times to repeat the hashing (makes it slow on purpose)

    // Turns a password into one text value that is safe to save in the database.
    // The result looks like:  600000.SALT.HASH
    public static string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    // Checks if a typed password matches the saved hash.
    public static bool Verify(string password, string savedHash)
    {
        // Split "600000.SALT.HASH" into its 3 parts
        string[] parts = savedHash.Split('.');

        if (parts.Length != 3)
        {
            return false;
        }

        int iterations = int.Parse(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] expectedHash = Convert.FromBase64String(parts[2]);

        // Hash the typed password using the SAME salt and SAME number of iterations
        byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            expectedHash.Length);

        // Compare the two hashes safely
        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
}
