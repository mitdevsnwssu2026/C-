# Lesson 24: Safe Password Hashing With PBKDF2

**Part 4: Security and Accounts** · About 75 minutes

## What You Will Learn

- How to write a `PasswordHasher` that hashes passwords the **safe** way
- The three parts of a saved hash: **rounds**, **salt**, and **hash**
- How to **check** a password at login

After this lesson, you are ready to build a real account system.

## The Idea

In Lesson 23, you learned the rules. A safe password hash needs:

1. A **random salt** for every user
2. A **slow** hash, repeated many times

**PBKDF2** does both. It is built into C#, it is trusted by banks and governments, and using it takes just **one instruction**.

### What we will save in the database

Our `PasswordHasher` turns a password into **one** piece of text with **three parts**, separated by dots:

```text
600000.YfP/qcgBYfn2ogwpjbGHag==.K3Ff0YjQdFrLMTz1BO873eteLDFdtRsNrpIvIfLvesU=
└─┬──┘ └──────────┬───────────┘ └──────────────────┬───────────────────────┘
rounds           salt                              hash
```

- **`600000`** → how many times the hashing was repeated. Saved so that login can repeat it the **same** number of times.
- **The salt** → this user's random salt. Saved so that login can use the **same** salt.
- **The hash** → the final fingerprint, to compare with at login.

Everything needed to check the password later is in this one value. But the **password itself is not**. 🔒

---

## Step by Step

### Step 1: Create a new project named `Lesson24`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson24`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the file `PasswordHasher.cs`

1. In the **Solution Explorer**, right-click the project line **Lesson24**.
2. Move to **Add**, then click **Class...**
3. In the **Name** box, type `PasswordHasher.cs`. Click **Add**.
4. In the new **PasswordHasher.cs** tab, press **Ctrl + A**, then **Delete**.
5. Put in this code:

## The Code

**File: `PasswordHasher.cs`**

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

### Step 3: Put the test program in `Program.cs`

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

**File: `Program.cs`**

```csharp
// Lesson 24: Trying out the PasswordHasher

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

1. Press **Ctrl + F5**.
2. Type a password and press **Enter**.
3. When it asks you to log in, type **the same password** again.

## The Output

✅ **You will see something like this:**

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

Your salts and hashes will be **different** from these, because a salt is random. That is correct.

**Run it again.** This time, type a **wrong** password at the end (for example, `mysecret123` with a small m):

```text
Now type your password again to log in: mysecret123
Wrong password. Access denied!
```

Did you notice a tiny pause after typing the password? That is the **600,000 rounds** at work. It is a quarter of a second for you, and a **huge** problem for a hacker. 🐢

> In this demo, the password is **visible** while you type. In Lesson 25, you will make it show `*****` instead.

---

## What the Code Means

### The settings

```cs
private const int SaltSize = 16;
private const int HashSize = 32;
private const int Iterations = 600_000;
```

- `const` → values that never change (Lesson 20).
- `private` → only this class uses them (Lesson 21).
- `600_000` is the same as `600000`. C# lets you put `_` inside a number to make it easier to read, like the comma in "600,000".
- **Iterations** is the programmer's word for "rounds". **600,000** is the number recommended by **OWASP**, a respected worldwide group for web security.

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

This is the **one instruction** that does all the hard work. (`Rfc2898DeriveBytes` is C#'s long name for PBKDF2.) You give it five things:

1. `password` → the password to hash
2. `salt` → the random salt
3. `Iterations` → repeat 600,000 times (the "slow on purpose" part)
4. `HashAlgorithmName.SHA256` → use SHA-256 inside each round
5. `HashSize` → give back 32 bytes

It is spread over several lines only to be easier to read. It is still **one** instruction, ending with `;`.

```cs
return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
```

Join the three parts with dots. **Base64** is a way to write bytes as normal text, using letters, numbers, `+`, `/`, and `=`. Base64 never uses a dot, so the dot is a safe separator.

### `Verify`: checking a password at login

```cs
string[] parts = savedHash.Split('.');

if (parts.Length != 3)
{
    return false;
}
```

Cut the saved value back into its 3 parts. If there are not exactly 3 parts, something is wrong with the saved value, so the answer is "no match".

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

Hash the password the user **just typed**, with the **same salt** and the **same number of rounds** as before. If the password is the same, the hash will be the same.

```cs
return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
```

Compare the two hashes. `true` means the password is correct.

**Why not just use `==`?** A normal comparison stops at the **first** difference. So some wrong guesses take a tiny bit longer to reject than others, and very clever hackers can measure that. `FixedTimeEquals` always takes the **same** time, so the timing tells them nothing. It is a small detail, but security is made of small details.

---

## If Something Goes Wrong

**Error List: `CS0103  The name 'RandomNumberGenerator' does not exist in the current context`**
The `using` line is missing. `PasswordHasher.cs` must have this line near the top:

```cs
using System.Security.Cryptography;
```

**Error List: `CS0103  The name 'PasswordHasher' does not exist in the current context`**
Check that `PasswordHasher.cs` is in the Solution Explorer, that its code has `public static class PasswordHasher`, and that you deleted **all** the starter code before pasting.

**I compared two hashes with `==`, and it is never true.**

```cs
if (PasswordHasher.Hash(typedPassword) == savedHash)   // ❌ never true!
```

Every call to `Hash` uses a **new random salt**, so the result is different every time. Always check a password with `PasswordHasher.Verify(typedPassword, savedHash)`.

---

## Exercise

**Exercise 1.** What does `Verify` give back when the saved hash is damaged? Add this line at the end of `Program.cs`, run it, and see:

```cs
Console.WriteLine(PasswordHasher.Verify("MySecret123", "this is not a real hash"));
```

<details>
<summary>Show answer</summary>

It shows `False`. The text has no dots, so `Split('.')` gives only 1 part, not 3. `Verify` gives back `false` right away.

</details>

**Exercise 2.** Two users both choose the password `Summer2026`. Explain in your own words: why are their saved values different? And how can the system still check each user's login correctly?

<details>
<summary>Show answer</summary>

Each user gets a **different random salt**, so their hashes are different, even with the same password. The salt is saved **inside** each user's saved value. At login, `Verify` takes **that user's** salt out of the saved value, hashes the typed password with it, and compares. They match only when the typed password is correct.

</details>

## ⭐ Bonus Challenge

**Measure the slowness.** Add these three lines at the **top** of `Program.cs`, then run the program:

```cs
System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
PasswordHasher.Hash("test1234");
Console.WriteLine($"Hashing took {timer.ElapsedMilliseconds} milliseconds.");
```

A `Stopwatch` works like a real stopwatch: `StartNew()` starts it, and `ElapsedMilliseconds` tells you how much time has passed. (1,000 milliseconds = 1 second.)

Now open `PasswordHasher.cs`, change `600_000` to `1_000`, and run again. What changes? What does that mean for a hacker?

**Important: change it back to `600_000` when you are done!**

<details>
<summary>Show answer</summary>

With `600_000`, hashing takes a few hundred milliseconds (about a quarter of a second, depending on your computer). With `1_000`, it takes almost 0 milliseconds.

For a hacker, that means they could guess about **600 times faster**. This is exactly why we use a high number of rounds.

</details>

---

## Remember

- **PBKDF2** = salt + many rounds of hashing. In C#: `Rfc2898DeriveBytes.Pbkdf2(...)`
- We save `rounds.salt.hash` as one text value. The real password is **never** saved.
- `PasswordHasher.Hash(password)` → the value to save when an account is created.
- `PasswordHasher.Verify(typedPassword, savedValue)` → `true` or `false` at login.
- Never compare two new hashes with `==`. Always use `Verify`.
- Use **600,000** rounds.

---

[⬅️ Lesson 23](23-what-is-hashing.md) | [🏠 Course Home](../README.md) | [Next: Lesson 25 (Account System: Register) ➡️](25-account-system-register.md)
