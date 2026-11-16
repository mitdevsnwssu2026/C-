// Lesson 17: What is hashing? (A learning demo)
// IMPORTANT: Plain SHA-256 is NOT safe for passwords.
// This lesson shows WHY. Lesson 18 shows the safe way.

using System.Security.Cryptography;
using System.Text;

// ----- Part 1: Same input = same hash. Small change = totally different hash. -----
Console.WriteLine("===== PART 1: HASHING =====");
ShowHash("hello");
ShowHash("hello");
ShowHash("Hello");
ShowHash("hello!");
ShowHash("This is a much longer sentence, but the hash is still 64 characters.");

// ----- Part 2: Why a plain hash is NOT enough for passwords -----
Console.WriteLine();
Console.WriteLine("===== PART 2: GUESSING A PLAIN HASH =====");

// Imagine a hacker stole this hash from a database:
string stolenHash = Sha256("sunshine");
Console.WriteLine($"Stolen hash: {stolenHash}");

// Hackers have lists of the most common passwords. They just try each one.
string[] commonPasswords = { "123456", "password", "qwerty", "iloveyou", "sunshine", "admin123" };

foreach (string guess in commonPasswords)
{
    string guessHash = Sha256(guess);

    if (guessHash == stolenHash)
    {
        Console.WriteLine($"Trying \"{guess}\"... MATCH! The password is \"{guess}\".");
        break;
    }

    Console.WriteLine($"Trying \"{guess}\"... no match.");
}

// ----- Part 3: Salt makes the same password produce different hashes -----
Console.WriteLine();
Console.WriteLine("===== PART 3: ADDING SALT =====");
Console.WriteLine("Two users both choose the password \"sunshine\":");

for (int user = 1; user <= 2; user++)
{
    byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
    string salt = Convert.ToHexString(saltBytes);
    string saltedHash = Sha256(salt + "sunshine");

    Console.WriteLine($"User {user} salt: {salt}");
    Console.WriteLine($"User {user} hash: {saltedHash}");
}

Console.WriteLine("Same password, but different hashes! A hacker can no longer crack everyone at once.");


// ----- Methods -----

string Sha256(string text)
{
    byte[] textBytes = Encoding.UTF8.GetBytes(text);
    byte[] hashBytes = SHA256.HashData(textBytes);
    return Convert.ToHexString(hashBytes);
}

void ShowHash(string text)
{
    Console.WriteLine($"\"{text}\"");
    Console.WriteLine($"   -> {Sha256(text)}");
}
