// Lesson 18: Trying out the PasswordHasher

Console.WriteLine("===== PASSWORD HASHER DEMO =====");
Console.Write("Create a password: ");
string password = Console.ReadLine() ?? "";

string savedHash = PasswordHasher.Hash(password);
Console.WriteLine();
Console.WriteLine("This is what we would save in the database:");
Console.WriteLine(savedHash);

string secondHash = PasswordHasher.Hash(password);
Console.WriteLine();
Console.WriteLine("Hashing the SAME password again gives a DIFFERENT result (new salt):");
Console.WriteLine(secondHash);

Console.WriteLine();
Console.Write("Now type your password again to log in: ");
string attempt = Console.ReadLine() ?? "";

if (PasswordHasher.Verify(attempt, savedHash))
{
    Console.WriteLine("Correct password. Access granted!");
}
else
{
    Console.WriteLine("Wrong password. Access denied!");
}
