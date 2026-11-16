# Lesson 17: What Is Hashing? (Why We Never Save Real Passwords)

| | |
|---|---|
| **Part** | 4: Security and Accounts |
| **Time** | About 75 minutes |
| **Before this** | [Lesson 16](16-uploading-csv-data.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Understand why saving real passwords in a database is **dangerous**
- Know what a **hash** is, and its four important properties
- See with your own eyes how hackers **guess** plain hashes
- Understand what **salt** is and why passwords need a **slow** hash

This lesson is about **understanding**. In the next lesson, you will write the real, safe code.

---

## 🧠 The Big Idea

### The danger

Imagine your school's system saves passwords like this:

| Username | Password |
|---|---|
| juan_dc | `sunshine123` |
| maria | `Maria2008!` |

One day, a hacker steals a copy of the database. (This happens to big companies every year.) Now the hacker knows **everyone's password**. And because many people use the **same password everywhere**, the hacker can now try them on Facebook, email, and online banking too.

> [!CAUTION]
> **Never save real passwords in a database.** Not in a file, not anywhere. Not even "just for now." This is one of the most important rules in programming.

### Then how can we check a password at login?

We save a **hash** of the password instead.

A **hash** is like a **fingerprint** of some text. A **hash function** takes any text and turns it into a fixed-length "fingerprint" made of letters and numbers:

```text
"sunshine"  ──► hash function ──►  A941A4C4FD0C01CDDEF61B8BE963BF4C1E2B0811C037CE3F1835FDDF6EF6C223
```

Think of a **blender**. You put in a mango and it becomes a mango shake. You can make the shake from a mango, but you can **never** turn the shake back into the mango.

### The four properties of a hash

| Property | What it means | Why it matters |
|---|---|---|
| **One-way** | You **cannot** turn a hash back into the original text | A stolen hash does not directly reveal the password |
| **Same input → same hash** | `"sunshine"` always gives the same hash | We can check a password by hashing it again and comparing |
| **Tiny change → totally different hash** | `"sunshine"` and `"Sunshine"` look nothing alike | You cannot "get close" to a password by looking at hashes |
| **Fixed length** | A 1-letter word and a whole book both give a hash of the same length | The hash reveals nothing about the password's length |

### How login works with hashes

```text
REGISTER:   password "sunshine"  ──► hash ──►  save A941A4C4...  in the database

LOG IN:     user types "sunshine" ──► hash ──►  A941A4C4...
            Does it match the saved hash?  YES ──► Correct password! ✅

            user types "sunshin3" ──► hash ──►  4C5514A1...
            Does it match the saved hash?  NO  ──► Wrong password! ❌
```

The system **never** needs to know the real password. It only compares fingerprints.

---

## 🛠️ Let's See It: A Hashing Demo

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson17
cd Lesson17
code .
```

(No package needed! Hashing tools are built into .NET.)

### Step 2: Write the code

Replace everything in `Program.cs` with:

```csharp
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
```

### Step 3: Run it

```cmd
dotnet run
```

✅ **You should see:**

```text
===== PART 1: HASHING =====
"hello"
   -> 2CF24DBA5FB0A30E26E83B2AC5B9E29E1B161E5C1FA7425E73043362938B9824
"hello"
   -> 2CF24DBA5FB0A30E26E83B2AC5B9E29E1B161E5C1FA7425E73043362938B9824
"Hello"
   -> 185F8DB32271FE25F561A6FC938B2E264306EC304EDA518007D1764826381969
"hello!"
   -> CE06092FB948D9FFAC7D1A376E404B26B7575BCC11EE05A4615FEF4FEC3A308B
"This is a much longer sentence, but the hash is still 64 characters."
   -> 362823C7EACBB910E48A8EACE0E77354303808D88BD2A1CD0791895FC2438A45

===== PART 2: GUESSING A PLAIN HASH =====
Stolen hash: A941A4C4FD0C01CDDEF61B8BE963BF4C1E2B0811C037CE3F1835FDDF6EF6C223
Trying "123456"... no match.
Trying "password"... no match.
Trying "qwerty"... no match.
Trying "iloveyou"... no match.
Trying "sunshine"... MATCH! The password is "sunshine".

===== PART 3: ADDING SALT =====
Two users both choose the password "sunshine":
User 1 salt: FD0724D8B5C850D9F9A49A2AFB461CD0
User 1 hash: F2369B731CF0506EEDFB8B19184DD6281DFD1DD44D7B6BB9DC581BB850E8014A
User 2 salt: 4BA8E752696B87D57F1274681BA1D84B
User 2 hash: 2712D65C7276AB10752C5E6C60F5B1799E1DDBAA915CAE4E68C071A48945DCFB
Same password, but different hashes! A hacker can no longer crack everyone at once.
```

> [!NOTE]
> Parts 1 and 2 will look **exactly** the same on your computer, because the same text always gives the same hash, on every computer in the world. The salts and hashes in Part 3 will be **different** every time you run it, because the salt is random. That is the whole point!

---

## 🔎 What Did We Just See?

### Part 1: The properties, proven

- `"hello"` twice → the **same** hash (same input, same output).
- `"hello"` vs `"Hello"` vs `"hello!"` → **completely different** hashes, even though only one character changed.
- A long sentence → still **64 characters**. Fixed length.

### Part 2: Why a plain hash is NOT enough 😟

The hacker **cannot reverse** the hash. But they do not need to! They have lists of **millions of common passwords**. They simply hash each guess and compare. Our tiny list found `"sunshine"` in 5 tries.

Worse:

- One strong gaming graphics card can calculate **billions** of SHA-256 hashes **per second**. Guessing millions of passwords takes **less than a second**.
- If 100 users chose `"sunshine"`, they all have the **same hash**. One guess cracks **all 100 accounts** at once.

### Part 3: Salt to the rescue (partly)

A **salt** is a **random value**, different for **every user**, that is added to the password **before** hashing:

```text
hash( salt + password )
```

- Two users with the same password now get **different** hashes, because their salts are different.
- The hacker can no longer crack everyone at once. They must attack **each user separately**.
- The salt is **not secret**. It is saved right next to the hash in the database, because we need it again at login.

Like salt in cooking: a little extra ingredient that changes the whole taste.

### The last problem: speed

Salt makes the hacker attack users one by one. But SHA-256 is still **too fast**: billions of guesses per second, per user, is still a lot of guesses.

The solution is a hash that is **slow on purpose**. Instead of hashing **once**, we hash **600,000 times** in a row. Here are rough numbers for a hacker with one strong graphics card:

| | Your user logging in | Hacker: guesses per second | Hacker: 1 billion guesses |
|---|---|---|---|
| Plain SHA-256 | Instant | About 20,000,000,000 | **Less than 1 second** |
| Slow hash (600,000 rounds) | About a quarter of a second (users don't notice) | About 15,000 | **Almost a full day, for just ONE user** |

For a database with 10,000 users, the slow hash turns **about 8 minutes** of guessing into **about 20 years**. A tiny delay for real users becomes an **enormous** delay for hackers. 🐢

There are special **password hashing** methods made exactly for this: **PBKDF2**, **bcrypt**, and **Argon2**. They use **salt + slowness** together. .NET has **PBKDF2** built in, and you will use it in the next lesson.

---

## 🔎 Line by Line: The `Sha256` Method

```cs
string Sha256(string text)
{
    byte[] textBytes = Encoding.UTF8.GetBytes(text);
    byte[] hashBytes = SHA256.HashData(textBytes);
    return Convert.ToHexString(hashBytes);
}
```

| Line | What it does |
|---|---|
| `Encoding.UTF8.GetBytes(text)` | Computers store everything as **bytes** (small numbers from 0 to 255). This turns the text into bytes |
| `SHA256.HashData(textBytes)` | Runs the SHA-256 hash function. The result is 32 bytes |
| `Convert.ToHexString(hashBytes)` | Turns the bytes into readable text using `0-9` and `A-F` (called **hexadecimal**). 32 bytes become 64 characters |

```cs
byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
```

Makes **16 random bytes** for the salt. `RandomNumberGenerator` is a **secure** random generator, made for security work. (Never use simple random numbers for security.)

These tools come from the two `using` lines at the top: `System.Security.Cryptography` (hashing and secure random numbers) and `System.Text` (`Encoding`).

---

## 🧠 Hashing Is Not Encryption

People often mix these up:

| | Encryption | Hashing |
|---|---|---|
| Can you get the original back? | **Yes**, with the secret key | **No**, never |
| Used for | Secret messages, files you need to read later | **Passwords**, checking that files were not changed |

Passwords should be **hashed**, never encrypted. If they were encrypted, anyone who steals the key could decrypt **all** of them.

---

## 🏆 The Rules of Password Safety

1. **Never** save real passwords. Save a password **hash**.
2. **Never** use plain SHA-256 (or MD5, or SHA-1) for passwords. They are too fast.
3. **Always** use a **salt**: random and different for every user.
4. **Always** use a **slow** password hash: PBKDF2, bcrypt, or Argon2.
5. **Never** invent your own security method. Use the well-tested tools that already exist.

---

## 🏋️ Practice

**Exercise 1.** Add `ShowHash("sunshine");` to Part 1. Is its hash the same as the "Stolen hash" in Part 2? Why?

<details>
<summary>Show answer</summary>

Yes, it is exactly the same: `A941A4C4FD0C01CDDEF61B8BE963BF4C1E2B0811C037CE3F1835FDDF6EF6C223`. The same input always gives the same hash. That is exactly why a hacker's guess can be checked.

</details>

**Exercise 2.** Add `"sunshine123"` and `"letmein"` to the `commonPasswords` list, and change the stolen hash to `Sha256("letmein")`. Run it. Does the hacker find it?

<details>
<summary>Show answer</summary>

```cs
string stolenHash = Sha256("letmein");
string[] commonPasswords = { "123456", "password", "qwerty", "iloveyou", "sunshine", "admin123", "sunshine123", "letmein" };
```

Yes. The loop tries every guess and finds `"letmein"` on the last try. Any password that appears on a common-password list is cracked almost instantly.

</details>

**Exercise 3.** In your own words, answer: **(a)** Why is it OK to save the salt in the database? **(b)** Why do we want password hashing to be slow?

<details>
<summary>Show answer</summary>

**(a)** The salt does not need to be secret. Its job is to make every user's hash **different**, so a hacker cannot crack many users with one guess. We need the salt again at login to recompute the hash, so we must save it.

**(b)** A slow hash adds only a fraction of a second for a real user logging in once, but it makes a hacker's millions or billions of guesses take far too long to be worth it.

</details>

---

## 📌 Recap

- **Never** save real passwords. Save a **hash** (a one-way fingerprint).
- Hashes are **one-way**, give the **same output for the same input**, change **completely** with any tiny change, and have a **fixed length**.
- Hackers crack plain hashes by **guessing** common passwords very fast.
- **Salt** = a random value per user. Same password, different hashes. The salt is saved with the hash.
- Password hashing must be **slow on purpose**: PBKDF2, bcrypt, or Argon2.
- Hashing is **not** encryption. Hashes cannot be reversed.

---

[⬅️ Lesson 16](16-uploading-csv-data.md) | [🏠 Course Home](../README.md) | [Next: Lesson 18 (Safe Password Hashing) ➡️](18-password-hashing.md)
