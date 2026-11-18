# Lesson 26: What Is Hashing? (Why We Never Save Real Passwords)

**Part 4: Security and Accounts** · About 75 minutes

## What You Will Learn

- Why saving real passwords in a database is **dangerous**
- What a **hash** is
- How hackers **guess** passwords (you will see it with your own eyes)
- What **salt** is, and why password hashing must be **slow**

This lesson is about **understanding**. In the next lesson, you will write the real, safe code.

## The Idea

### The danger

Imagine your school's system saves passwords like this:

| Username | Password |
|---|---|
| juan_dc | `sunshine123` |
| maria | `Maria2008!` |

One day, a hacker steals a copy of the database. (This happens to big companies every year.) Now the hacker knows **everyone's password**. And many people use the **same password everywhere**. So the hacker can now try those passwords on Facebook, email, and online banking too.

> [!CAUTION]
> **Never save real passwords.** Not in a database, not in a file, not anywhere. Not even "just for now." This is one of the most important rules in programming.

### The solution: save a hash instead

A **hash** is like a **fingerprint** of some text. A **hash function** turns any text into a jumble of letters and numbers:

```text
"sunshine"  ──► hash function ──►  A941A4C4FD0C01CDDEF61B8BE963BF4C1E2B0811C037CE3F1835FDDF6EF6C223
```

Think of a **blender**. Put in a mango, and you get a mango shake. But you can **never** turn the shake back into the mango.

A hash has four important features:

1. **One-way.** You cannot turn a hash back into the original text.
2. **Same text, same hash.** `"sunshine"` always gives the same hash.
3. **A tiny change gives a totally different hash.** `"sunshine"` and `"Sunshine"` look nothing alike.
4. **Always the same length.** One letter or a whole book: the hash is the same length.

### How login works with hashes

```text
REGISTER:  password "sunshine"   ──► hash ──►  save  A941A4C4...  in the database

LOG IN:    user types "sunshine" ──► hash ──►  A941A4C4...
           Same as the saved hash?  YES ──► Correct password! ✅

           user types "sunshin3" ──► hash ──►  4C5514A1...
           Same as the saved hash?  NO  ──► Wrong password! ❌
```

The system **never** needs to know the real password. It only compares fingerprints.

---

## Step by Step

### Step 1: Create a new project named `Lesson26`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson26`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

> No package is needed this time. The hashing tools are already part of C#.

### Step 2: Put in the code

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Put in the code below.

## The Code

```csharp
// Lesson 26: What is hashing? (A learning demo)
// IMPORTANT: Plain SHA-256 is NOT safe for passwords.
// This lesson shows WHY. Lesson 27 shows the safe way.

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

Press **Ctrl + F5**.

## The Output

✅ **You will see:**

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
> Parts 1 and 2 look **exactly** the same on your computer, because the same text gives the same hash on every computer in the world. The salts and hashes in Part 3 are **different** every time you run it, because a salt is random. That is the whole point!

---

## What the Output Means

### Part 1: The four features, proven

- `"hello"` two times → the **same** hash.
- `"hello"`, `"Hello"`, and `"hello!"` → **completely different** hashes, even though only one character changed.
- A long sentence → still **64 characters**.

### Part 2: Why a plain hash is NOT enough 😟

The hacker **cannot reverse** the hash. But they do not need to! They have lists of **millions of common passwords**. They hash each guess and compare. Our tiny list found `"sunshine"` in 5 tries.

It gets worse:

- One strong gaming graphics card can compute **billions** of these hashes **per second**. Trying millions of passwords takes **less than a second**.
- If 100 users chose `"sunshine"`, they all have the **same hash**. One guess opens **all 100 accounts**.

### Part 3: Salt helps

A **salt** is a **random value**, different for **every user**, that is added to the password **before** hashing:

```text
hash( salt + password )
```

- Two users with the same password now get **different** hashes.
- The hacker can no longer crack everyone at once. They must attack **each user separately**.
- The salt is **not a secret**. It is saved right next to the hash, because it is needed again at login.

Like salt in cooking: a little extra ingredient that changes the whole taste.

### The last problem: speed

Salt forces the hacker to attack users one by one. But this kind of hash (called **SHA-256**) is still **too fast**.

The fix is a hash that is **slow on purpose**. Instead of hashing **once**, we hash **600,000 times** in a row. Here are rough numbers for a hacker with one strong graphics card:

| | Your user logging in | Hacker: guesses per second | Hacker: 1 billion guesses |
|---|---|---|---|
| Plain SHA-256 | Instant | About 20,000,000,000 | **Less than 1 second** |
| Slow hash (600,000 rounds) | About a quarter of a second | About 15,000 | **Almost a full day, for just ONE user** |

For a database with 10,000 users, the slow hash turns **about 8 minutes** of guessing into **about 20 years**. A tiny wait for real users becomes a **huge** wait for hackers. 🐢

There are special **password hashing** methods made for exactly this: **PBKDF2**, **bcrypt**, and **Argon2**. They use **salt and slowness** together. C# has **PBKDF2** built in. You will use it in the next lesson.

---

## What the Code Means

```cs
string Sha256(string text)
{
    byte[] textBytes = Encoding.UTF8.GetBytes(text);
    byte[] hashBytes = SHA256.HashData(textBytes);
    return Convert.ToHexString(hashBytes);
}
```

Computers store everything as **bytes**: small numbers from 0 to 255.

1. `Encoding.UTF8.GetBytes(text)` turns the text into bytes.
2. `SHA256.HashData(textBytes)` runs the hash function. The result is 32 bytes.
3. `Convert.ToHexString(hashBytes)` turns those bytes into text you can read, using `0-9` and `A-F`. 32 bytes become 64 characters.

```cs
byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
```

This makes **16 random bytes** for the salt. `RandomNumberGenerator` is a **secure** random maker, built for security work.

These tools come from the two `using` lines at the top of the file.

### Hashing is not encryption

People often mix these up.

- **Encryption** can be undone, with a secret key. It is for secret messages you need to read later.
- **Hashing** can **never** be undone. It is for passwords.

Passwords should be **hashed**, never encrypted. If they were encrypted, anyone who stole the key could unlock **all** of them.

### The 5 rules of password safety

1. **Never** save real passwords. Save a **hash**.
2. **Never** use plain SHA-256 for passwords. It is too fast.
3. **Always** use a **salt**: random, and different for every user.
4. **Always** use a **slow** password hash: PBKDF2, bcrypt, or Argon2.
5. **Never** invent your own security method. Use the well-tested tools that already exist.

---

## If Something Goes Wrong

**Error List: `CS0103  The name 'SHA256' does not exist in the current context`**
The `using` lines are missing. The file must start with these two lines (after the comments):

```cs
using System.Security.Cryptography;
using System.Text;
```

**My Part 3 looks different from the lesson.**
That is correct! Salts are random, so Part 3 is different on every run.

**My Part 1 or Part 2 looks different from the lesson.**
Then there is a typing difference in a text. Even one different letter, space, or capital changes the whole hash. Compare your texts with the lesson's code.

---

## Exercise

**Exercise 1.** In Part 1, add the line `ShowHash("sunshine");`. Run it. Is the hash the same as the "Stolen hash" in Part 2? Why?

<details>
<summary>Show answer</summary>

Yes, it is exactly the same: `A941A4C4FD0C01CDDEF61B8BE963BF4C1E2B0811C037CE3F1835FDDF6EF6C223`. The same text always gives the same hash. That is exactly why a hacker's guess can be checked.

</details>

**Exercise 2.** Add `"sunshine123"` and `"letmein"` to the `commonPasswords` list. Change the stolen hash to `Sha256("letmein")`. Run it. Does the hacker find the password?

<details>
<summary>Show answer</summary>

```cs
string stolenHash = Sha256("letmein");
string[] commonPasswords = { "123456", "password", "qwerty", "iloveyou", "sunshine", "admin123", "sunshine123", "letmein" };
```

Yes. The loop tries every guess and finds `"letmein"` on the last try. Any password that is on a common-password list is cracked almost instantly.

</details>

**Exercise 3.** Answer in your own words: **(a)** Why is it OK to save the salt in the database? **(b)** Why do we want password hashing to be slow?

<details>
<summary>Show answer</summary>

**(a)** The salt does not need to be secret. Its job is to make every user's hash **different**, so one guess cannot open many accounts. The salt is needed again at login, so it must be saved.

**(b)** A slow hash adds only a fraction of a second for a real user who logs in once. But it makes a hacker's millions of guesses take far too long.

</details>

## ⭐ Bonus Challenge

**Does salt alone stop a hacker?** Find out. A hacker has stolen one user's **salt** and **salted hash**. Add code to the program that guesses the password, using the same `commonPasswords` list.

Add this just **above** the line `// ----- Methods -----`, then write the loop yourself:

```cs
// ----- Bonus: guessing a SALTED hash -----
Console.WriteLine();
Console.WriteLine("===== BONUS: GUESSING A SALTED HASH =====");

string stolenSalt = "A1B2C3D4";
string stolenSaltedHash = Sha256(stolenSalt + "iloveyou");
```

Hint: the hacker knows the salt, so they can add it in front of every guess.

<details>
<summary>Show answer</summary>

```cs
foreach (string guess in commonPasswords)
{
    if (Sha256(stolenSalt + guess) == stolenSaltedHash)
    {
        Console.WriteLine($"MATCH! The password is \"{guess}\".");
        break;
    }
}
```

It shows `MATCH! The password is "iloveyou".`

**The lesson:** salt alone does **not** stop a hacker from guessing one user's password. It only stops them from cracking **everyone at once**. That is why we also need the hash to be **slow**. Salt and slowness work as a team.

</details>

---

## Remember

- **Never** save real passwords. Save a **hash**: a one-way fingerprint.
- Same text, same hash. A tiny change, a totally different hash. A hash cannot be reversed.
- Hackers crack plain hashes by **guessing** common passwords, very fast.
- **Salt** = a random value for each user. Same password, different hashes.
- Password hashing must be **slow on purpose**: PBKDF2, bcrypt, or Argon2.
- Hashing is **not** encryption.

---

[⬅️ Lesson 25](25-ef-core-database-first.md) | [🏠 Course Home](../README.md) | [Next: Lesson 27 (Safe Password Hashing) ➡️](27-password-hashing.md)
