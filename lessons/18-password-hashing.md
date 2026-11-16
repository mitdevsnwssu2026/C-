# Lesson 18: Safe Password Hashing With PBKDF2

| | |
|---|---|
| **Part** | 4: Security and Accounts |
| **Time** | About 75 minutes |
| **Before this** | [Lesson 17](17-what-is-hashing.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Write a reusable `PasswordHasher` class that hashes passwords the **safe** way
- Understand every part of the saved hash: **iterations**, **salt**, and **hash**
- **Verify** a password at login
- Be ready to build a real account system in the next two lessons

---

## 🧠 The Big Idea

In Lesson 17 you learned the rules. A safe password hash needs:

1. A **random salt** for every user ✅
2. A **slow** hash, repeated many times ✅

**PBKDF2** (Password-Based Key Derivation Function 2) does both. It is built into .NET, it is trusted by governments and banks, and you only need **one line** to use it.

### What we will save in the database

Our `PasswordHasher` turns a password into **one** piece of text with **three parts**, separated by dots:

```text
600000.YfP/qcgBYfn2ogwpjbGHag==.K3Ff0YjQdFrLMTz1BO873eteLDFdtRsNrpIvIfLvesU=
└─┬──┘ └──────────┬───────────┘ └──────────────────┬───────────────────────┘
iterations       salt                              hash
```

| Part | What it is | Why we save it |
|---|---|---|
| `600000` | How many times the hash was repeated | To repeat it the **same** number of times at login |
| `YfP/qcg...` | The random **salt** for this user | To use the **same** salt at login |
| `K3Ff0Yj...` | The final **hash** | To compare with at login |

Everything needed to check the password later is in this one value, but the **password itself is not**. 🔒

---

## 🛠️ Let's Build It

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson18
cd Lesson18
code .
```

### Step 2: Create `PasswordHasher.cs`

Create a new file named **`PasswordHasher.cs`** and write:

```csharp
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
```

### Step 3: Write `Program.cs` to try it out

Replace everything in `Program.cs` with:

```csharp
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
```

### Step 4: Run it

```cmd
dotnet run
```

Type a password, and then type **the same password** again to log in.

✅ **You should see something like this:**

```text
===== PASSWORD HASHER DEMO =====
Create a password: MySecret123

This is what we would save in the database:
600000.YfP/qcgBYfn2ogwpjbGHag==.K3Ff0YjQdFrLMTz1BO873eteLDFdtRsNrpIvIfLvesU=

Hashing the SAME password again gives a DIFFERENT result (new salt):
600000.b05NOuV/WVaPHf3jk63l0A==.DYLmSxxFYWnnZg8wpBP8VlrOOTQs8aOdRNT//VJ6IFg=

Now type your password again to log in: MySecret123
Correct password. Access granted!
```

Your salts and hashes will be **different** from these, because the salt is random every time. That is correct.

**Run it again** and type a **wrong** password at the end (for example `mysecret123` with a small m):

```text
Now type your password again to log in: mysecret123
Wrong password. Access denied!
```

Did you notice a tiny pause after you typed the password? That is the **600,000 rounds** at work. It is about a quarter of a second for you, and a **huge** problem for a hacker. 🐢

> [!NOTE]
> In this demo, the password is **visible** while you type. In Lesson 19 you will make it show `*****` instead.

---

## 🔎 Line by Line: `PasswordHasher.cs`

### The settings

```cs
private const int SaltSize = 16;
private const int HashSize = 32;
private const int Iterations = 600_000;
```

- `const`: values that never change (Lesson 14).
- `private`: only this class uses them.
- `600_000` is the same as `600000`. C# lets you put `_` inside numbers to make them **easier to read**, like a comma when you write "600,000" by hand.
- **600,000** is the number recommended by **OWASP**, a respected worldwide organization for web security, for PBKDF2 with SHA-256.

### `Hash`: making a new hash

```cs
byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
```

Make 16 **secure random** bytes. Every call makes a new, different salt. That is why the same password gave two different results in the demo.

```cs
byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
    password,
    salt,
    Iterations,
    HashAlgorithmName.SHA256,
    HashSize);
```

This is the **one line** that does all the hard work. (`Rfc2898DeriveBytes` is .NET's name for PBKDF2. "RFC 2898" is the name of the official document that describes it.)

| What we give it | Meaning |
|---|---|
| `password` | The password to hash |
| `salt` | The random salt |
| `Iterations` | Repeat 600,000 times (the "slow on purpose" part) |
| `HashAlgorithmName.SHA256` | Use SHA-256 inside each round |
| `HashSize` | Give back 32 bytes |

The code is split over several lines only to make it easier to read. It is still **one** instruction, ending with `;`.

```cs
return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
```

Join the three parts with dots. **Base64** is a way to write bytes as normal text, using letters, numbers, `+`, `/`, and `=`. It is shorter than the hexadecimal you saw in Lesson 17. Because Base64 never uses a dot, the dot is a safe separator.

### `Verify`: checking a password at login

```cs
string[] parts = savedHash.Split('.');

if (parts.Length != 3)
{
    return false;
}
```

Split the saved value back into its 3 parts. If it does not have exactly 3 parts, something is wrong with the saved value, so the answer is "no match".

```cs
int iterations = int.Parse(parts[0]);
byte[] salt = Convert.FromBase64String(parts[1]);
byte[] expectedHash = Convert.FromBase64String(parts[2]);
```

Turn the text parts back into a number and bytes.

```cs
byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
    password,
    salt,
    iterations,
    HashAlgorithmName.SHA256,
    expectedHash.Length);
```

Hash the password the user **just typed**, using the **same salt** and the **same number of rounds** as when the account was created. If the password is the same, the hash will be the same.

```cs
return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
```

Compare the two hashes. `true` = correct password.

**Why not just use `==`?** A normal comparison stops at the **first** difference, so a wrong guess that starts out right takes a tiny bit **longer** to reject. Very clever hackers can measure those tiny time differences to learn things. `FixedTimeEquals` always takes the **same** amount of time, so the timing gives away nothing. It is a small detail, but security is made of small details.

> [!TIP]
> **Why save the iterations instead of always using 600,000?** Computers get faster every year, so in the future you may want to increase it to, say, 1,000,000. Because every saved hash remembers its **own** number, old accounts will still log in correctly while new accounts use the new number.

---

## ⚠️ Common Mistakes

### Mistake 1: Forgetting `using System.Security.Cryptography;`

```text
error CS0103: The name 'RandomNumberGenerator' does not exist in the current context
```

**Fix:** Add `using System.Security.Cryptography;` as the first line of `PasswordHasher.cs`.

### Mistake 2: Comparing new hashes with `==`

```cs
if (PasswordHasher.Hash(typedPassword) == savedHash)   // ❌ never true!
```

Every call to `Hash` uses a **new random salt**, so the result is always different. **Fix:** Always check passwords with `PasswordHasher.Verify(typedPassword, savedHash)`.

### Mistake 3: Making the iterations very small "to be faster"

With `Iterations = 1`, logins are instant, and so are the hacker's guesses. **Keep 600,000.** A quarter of a second at login is a fair price for safety.

---

## 🏋️ Practice

**Exercise 1.** Measure how long hashing takes. Add this to the top of `Program.cs` and run it:

```cs
System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
PasswordHasher.Hash("test1234");
Console.WriteLine($"Hashing took {timer.ElapsedMilliseconds} milliseconds.");
```

(A `Stopwatch` works like a real stopwatch: `StartNew()` starts it, and `ElapsedMilliseconds` tells you how much time has passed.)

Then temporarily change `Iterations` to `1_000` and run it again. What changes? (Change it back to `600_000` afterward!)

<details>
<summary>Show answer</summary>

With `600_000`, it takes a few hundred milliseconds (around a quarter of a second, depending on your computer). With `1_000`, it takes almost 0 milliseconds. That means a hacker could also guess about **600 times faster**. This is exactly why we use a high number.

</details>

**Exercise 2.** What does `Verify` return if the saved hash has been damaged? Try it:

```cs
Console.WriteLine(PasswordHasher.Verify("MySecret123", "this is not a real hash"));
```

<details>
<summary>Show answer</summary>

It shows `False`. The text has no dots, so `Split('.')` gives only 1 part, not 3, and `Verify` returns `false` right away.

</details>

**Exercise 3.** Explain in your own words: two users both choose the password `Summer2026`. Why are their saved values different, and how can the system still check each user's login correctly?

<details>
<summary>Show answer</summary>

Each user gets a **different random salt**, so their hashes are different, even with the same password. The salt is saved **inside** each user's saved value. At login, `Verify` takes **that user's** salt from the saved value, hashes the typed password with it, and compares. The result matches only if the typed password is correct.

</details>

---

## 📌 Recap

- **PBKDF2** = salt + many rounds of hashing. It is built into .NET: `Rfc2898DeriveBytes.Pbkdf2(...)`.
- We save `iterations.salt.hash` as one text value. The real password is **never** saved.
- `PasswordHasher.Hash(password)` → the value to save when an account is created.
- `PasswordHasher.Verify(typedPassword, savedValue)` → `true` or `false` at login.
- Compare hashes with `CryptographicOperations.FixedTimeEquals`, not `==`.
- Use **600,000** iterations (the OWASP recommendation for PBKDF2 with SHA-256).

---

[⬅️ Lesson 17](17-what-is-hashing.md) | [🏠 Course Home](../README.md) | [Next: Lesson 19 (Account System: Register) ➡️](19-account-system-register.md)
