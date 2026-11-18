# Lesson 28: Account System, Part 1 (Registering Accounts)

**Part 4: Security and Accounts** · About 2 hours (you can split it into two sittings)

## What You Will Learn

In Lessons 28 and 29, you build the **final project**: a complete **account system** with hashed passwords.

In this lesson (Part 1), you will build:

- A `Users` table in a database
- A **Register** screen, with rules for usernames and passwords
- **Hidden password typing**: the screen shows `*****` instead of the password
- A screen that proves the database holds **only hashes**, never real passwords

## The Idea

Everything you have learned comes together here:

- The **menu loop** (Lesson 10)
- **Input methods** that never crash (Lesson 13)
- **Classes** (Lesson 14)
- **Database** connection, parameters, and reading rows (Lessons 20 to 22)
- The **`PasswordHasher`** (Lesson 27)

### The plan: six files, one job each

| File | Its job |
|---|---|
| `PasswordHasher.cs` | Hashes and checks passwords (you wrote it in Lesson 27) |
| `User.cs` | Says what a user account **is** |
| `Database.cs` | Opens the database and creates the `Users` table |
| `ConsoleHelper.cs` | Reads what the user types, including **hidden** passwords |
| `AccountService.cs` | The account **rules** and the account **SQL** |
| `Program.cs` | The menu and the screens |

### What happens when someone registers

```text
The user types:  username "Juan_DC", name "Juan Dela Cruz", password "MyPass123"
                   │
                   ▼
1. Clean up:        "Juan_DC" ──► "juan_dc" (small letters)
2. Check username:  4 to 20 characters? Only letters, numbers, and _ ? Not taken yet?
3. Check password:  at least 8 characters? Has a letter and a number? Typed the same twice?
4. Hash it:         "MyPass123" ──► "600000.PCVKUp7z...==.5gsZQvtz...="
5. Save:            INSERT INTO Users (Username, FullName, PasswordHash, CreatedAt) ...
```

The real password exists only for a moment, in memory. It is **never** written to the database.

---

## Step by Step

### Step 1: Create a new project named `Lesson28`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson28`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

1. In the **Solution Explorer**, right-click the project line **Lesson28**.
2. Click **Manage NuGet Packages...**
3. Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.
4. Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**.
5. If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.
6. Close the NuGet tab.

### Step 3: Bring in `PasswordHasher.cs` from Lesson 27

1. In the Solution Explorer, right-click the project line **Lesson28**.
2. Move to **Add**, then click **Existing Item...**
3. Click the address bar at the top of the window. Type `C:\CSharpCourse\Lesson27` and press **Enter**.
4. Click **PasswordHasher.cs**, then click **Add**.

✅ **You will see:** `PasswordHasher.cs` in the Solution Explorer, under **Lesson28**.

### Step 4: Add the file `User.cs`

1. Right-click the project line **Lesson28** → **Add** → **Class...**
2. In the **Name** box, type `User.cs`. Click **Add**.
3. In the new tab, press **Ctrl + A**, then **Delete**.
4. Put in this code:

## The Code

**File: `User.cs`**

```csharp
// User.cs - the blueprint for one user account

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string FullName { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string CreatedAt { get; set; } = "";
}
```

### Step 5: Add the file `Database.cs`

1. Right-click the project line **Lesson28** → **Add** → **Class...**
2. In the **Name** box, type `Database.cs`. Click **Add**.
3. In the new tab, press **Ctrl + A**, then **Delete**.
4. Put in this code:

**File: `Database.cs`**

```csharp
// Database.cs - opens the database and creates the tables

using Microsoft.Data.Sqlite;

public static class Database
{
    private const string ConnectionString = "Data Source=accounts.db";

    public static SqliteConnection OpenConnection()
    {
        SqliteConnection connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    public static void CreateTables()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id           INTEGER PRIMARY KEY AUTOINCREMENT,
                Username     TEXT    NOT NULL UNIQUE,
                FullName     TEXT    NOT NULL,
                PasswordHash TEXT    NOT NULL,
                CreatedAt    TEXT    NOT NULL
            );";
        command.ExecuteNonQuery();
    }
}
```

This time, the database file is named `accounts.db`.

### Step 6: Add the file `ConsoleHelper.cs`

1. Right-click the project line **Lesson28** → **Add** → **Class...**
2. In the **Name** box, type `ConsoleHelper.cs`. Click **Add**.
3. In the new tab, press **Ctrl + A**, then **Delete**.
4. Put in this code:

**File: `ConsoleHelper.cs`**

```csharp
// ConsoleHelper.cs - reusable methods for reading what the user types

public static class ConsoleHelper
{
    // Keeps asking until the user types something (not empty)
    public static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim();

            if (input != "")
            {
                return input;
            }

            Console.WriteLine("This field is required. Please type something.");
        }
    }

    // Reads a password but shows * instead of the real characters
    public static string ReadPassword(string prompt)
    {
        Console.Write(prompt);

        // If the input is not coming from a keyboard (for example, an automated test),
        // just read the whole line normally.
        if (Console.IsInputRedirected)
        {
            return Console.ReadLine() ?? "";
        }

        string password = "";

        while (true)
        {
            // true = do NOT show the key that was pressed
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    // Remove the last character, and erase one * from the screen
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password = password + key.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }
}
```

### Step 7: Add the file `AccountService.cs`

1. Right-click the project line **Lesson28** → **Add** → **Class...**
2. In the **Name** box, type `AccountService.cs`. Click **Add**.
3. In the new tab, press **Ctrl + A**, then **Delete**.
4. Put in this code:

**File: `AccountService.cs`**

```csharp
// AccountService.cs - the rules and database commands for user accounts

public static class AccountService
{
    // Checks the username rules.
    // Gives back an error message, or "" (empty text) if the username is OK.
    public static string ValidateUsername(string username)
    {
        if (username.Length < 4 || username.Length > 20)
        {
            return "Username must be 4 to 20 characters long.";
        }

        foreach (char c in username)
        {
            if (!char.IsLetterOrDigit(c) && c != '_')
            {
                return "Username can only have letters, numbers, and _ (underscore).";
            }
        }

        if (UsernameExists(username))
        {
            return "That username is already taken. Please choose another one.";
        }

        return "";
    }

    // Checks the password rules.
    // Gives back an error message, or "" (empty text) if the password is OK.
    public static string ValidatePassword(string password)
    {
        if (password.Length < 8)
        {
            return "Password must be at least 8 characters long.";
        }

        bool hasLetter = false;
        bool hasNumber = false;

        foreach (char c in password)
        {
            if (char.IsLetter(c))
            {
                hasLetter = true;
            }

            if (char.IsDigit(c))
            {
                hasNumber = true;
            }
        }

        if (!hasLetter || !hasNumber)
        {
            return "Password must have at least one letter and one number.";
        }

        return "";
    }

    public static bool UsernameExists(string username)
    {
        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = $username;";
        command.Parameters.AddWithValue("$username", username);

        int count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    // Creates a new account. The password is HASHED before it is saved.
    public static void Register(string username, string fullName, string password)
    {
        string passwordHash = PasswordHasher.Hash(password);
        string createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (Username, FullName, PasswordHash, CreatedAt)
            VALUES ($username, $fullName, $passwordHash, $createdAt);";
        command.Parameters.AddWithValue("$username", username);
        command.Parameters.AddWithValue("$fullName", fullName);
        command.Parameters.AddWithValue("$passwordHash", passwordHash);
        command.Parameters.AddWithValue("$createdAt", createdAt);
        command.ExecuteNonQuery();
    }

    public static List<User> GetAllUsers()
    {
        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Username, FullName, PasswordHash, CreatedAt
            FROM Users
            ORDER BY Id;";

        List<User> users = new List<User>();

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                FullName = reader.GetString(2),
                PasswordHash = reader.GetString(3),
                CreatedAt = reader.GetString(4)
            });
        }

        return users;
    }
}
```

### Step 8: Put the main program in `Program.cs`

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

**File: `Program.cs`**

```csharp
// Lesson 28: Account System - Part 1 (Registering accounts with hashed passwords)

Database.CreateTables();

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("========= ACCOUNT SYSTEM =========");
    Console.WriteLine("1 - Register a new account");
    Console.WriteLine("2 - View all accounts (for learning only)");
    Console.WriteLine("3 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            RegisterScreen();
            break;
        case "2":
            ViewAllAccountsScreen();
            break;
        case "3":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Please type a number from 1 to 3.");
            break;
    }
}


// ----- Screens -----

void RegisterScreen()
{
    Console.WriteLine("--- REGISTER A NEW ACCOUNT ---");

    string username = AskForUsername();
    string fullName = ConsoleHelper.ReadRequired("Full name: ");
    string password = AskForNewPassword();

    Console.WriteLine("Creating your account...");
    AccountService.Register(username, fullName, password);

    Console.WriteLine($"Account created! You can now log in as \"{username}\".");
}

void ViewAllAccountsScreen()
{
    List<User> users = AccountService.GetAllUsers();

    if (users.Count == 0)
    {
        Console.WriteLine("No accounts yet.");
        return;
    }

    foreach (User user in users)
    {
        Console.WriteLine($"ID {user.Id}: {user.Username} ({user.FullName}) - created {user.CreatedAt}");
        Console.WriteLine($"     Saved in database: {user.PasswordHash}");
    }

    Console.WriteLine();
    Console.WriteLine("Notice: the real passwords are NOT saved anywhere. Only their hashes.");
}


// ----- Input methods -----

string AskForUsername()
{
    while (true)
    {
        string username = ConsoleHelper.ReadRequired("Username: ").ToLower();
        string error = AccountService.ValidateUsername(username);

        if (error == "")
        {
            return username;
        }

        Console.WriteLine(error);
    }
}

string AskForNewPassword()
{
    Console.WriteLine("(Password: at least 8 characters, with at least one letter and one number.)");

    while (true)
    {
        string password = ConsoleHelper.ReadPassword("Choose a password: ");
        string error = AccountService.ValidatePassword(password);

        if (error != "")
        {
            Console.WriteLine(error);
            continue;
        }

        string confirm = ConsoleHelper.ReadPassword("Type the password again: ");

        if (password == confirm)
        {
            return password;
        }

        Console.WriteLine("The two passwords do not match. Please try again.");
    }
}
```

### Step 9: Check your files

Look at the Solution Explorer. Under **Lesson28**, you should see these six files:

```text
Lesson28
   Dependencies
   AccountService.cs
   ConsoleHelper.cs
   Database.cs
   PasswordHasher.cs
   Program.cs
   User.cs
```

### Step 10: Run it

Press **Ctrl + F5**. Choose `1` and register an account. **Break the rules on purpose**, to see the checks work. When you type a password, you will see stars (`*`) instead of letters.

## The Output

✅ **You will see something like this:**

```text
========= ACCOUNT SYSTEM =========
1 - Register a new account
2 - View all accounts (for learning only)
3 - Exit
Choose an option: 1

--- REGISTER A NEW ACCOUNT ---
Username: J
Username must be 4 to 20 characters long.
Username: juan dc
Username can only have letters, numbers, and _ (underscore).
Username: Juan_DC
Full name: Juan Dela Cruz
(Password: at least 8 characters, with at least one letter and one number.)
Choose a password: *****
Password must be at least 8 characters long.
Choose a password: ************
Password must have at least one letter and one number.
Choose a password: *********
Type the password again: *********
The two passwords do not match. Please try again.
Choose a password: *********
Type the password again: *********
Creating your account...
Account created! You can now log in as "juan_dc".
```

Now register a **second** account (for example: username `maria`, full name `Maria Santos`). Then choose option `2`:

```text
Choose an option: 2

ID 1: juan_dc (Juan Dela Cruz) - created 2026-11-16 14:45:42
     Saved in database: 600000.PCVKUp7zyEiV1KvpG7nC5Q==.5gsZQvtz04+xJMYcwZp9FYjj0BGC80zx5YSyMRe1C5o=
ID 2: maria (Maria Santos) - created 2026-11-16 14:45:42
     Saved in database: 600000.DgnfxpaXSpipFSo+9EAokg==.mK5j1a9LR16YU0As85oJZzYIwORs9za/aMvemNjZOvw=

Notice: the real passwords are NOT saved anywhere. Only their hashes.
```

Look at what is saved. There is **no password** anywhere. Even you, the programmer, cannot see which password Juan chose. 🔒 (Your dates, salts, and hashes will be different.)

**Try one more thing:** register again with the username `JUAN_DC`. You will see `That username is already taken.`, because usernames are saved in small letters.

---

## What the Code Means: `ConsoleHelper.cs`

`ReadPassword` shows a `*` for each key the user presses. Nobody looking at the screen can read the password.

```cs
ConsoleKeyInfo key = Console.ReadKey(true);
```

`Console.ReadKey` reads **one key** at a time, not a whole line. The `true` means *"do not show the key on the screen."*

```cs
if (key.Key == ConsoleKey.Enter)
{
    Console.WriteLine();
    break;
}
```

When the user presses **Enter**, the password is finished. Go to a new line and leave the loop.

```cs
if (key.Key == ConsoleKey.Backspace)
{
    if (password.Length > 0)
    {
        password = password.Substring(0, password.Length - 1);
        Console.Write("\b \b");
    }
}
```

When the user presses **Backspace**, remove the last character:

- `password.Substring(0, password.Length - 1)` keeps every character **except the last one**.
- `"\b \b"` erases one `*` from the screen: step back, write a space over the star, step back again.

```cs
else if (!char.IsControl(key.KeyChar))
{
    password = password + key.KeyChar;
    Console.Write("*");
}
```

For a normal key (a letter, a number, a symbol), add the character to the password and show a `*`.

```cs
if (Console.IsInputRedirected)
{
    return Console.ReadLine() ?? "";
}
```

You can skip this part while learning. It is only used when a teacher tests the program automatically, with answers that come from a file instead of a keyboard.

---

## What the Code Means: `AccountService.cs`

### Rules that give back a message

```cs
public static string ValidateUsername(string username)
```

"Validate" means *check that something follows the rules*. This method gives back an **error message** when something is wrong, or `""` (empty text) when everything is OK. The screen then decides what to do:

```cs
string error = AccountService.ValidateUsername(username);

if (error == "")
{
    return username;      // all good
}

Console.WriteLine(error); // show what is wrong, then ask again
```

### Checking every character

```cs
foreach (char c in username)
{
    if (!char.IsLetterOrDigit(c) && c != '_')
    {
        return "Username can only have letters, numbers, and _ (underscore).";
    }
}
```

A `string` is made of **characters**. A single character has the type `char`. `foreach` can go through them one by one. If a character is **not** a letter or number, **and** it is **not** an underscore, the username is refused.

### `Register`: hash first, then save

```cs
string passwordHash = PasswordHasher.Hash(password);
string createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
```

The password is hashed **before** anything goes to the database. `DateTime.Now` is the date and time right now. `.ToString("yyyy-MM-dd HH:mm:ss")` writes it like `2026-11-16 14:45:42`.

Then the `INSERT` saves the **hash**, never the password. And every value goes in as a **parameter** (Lesson 21).

---

## What the Code Means: `Program.cs`

```cs
string username = ConsoleHelper.ReadRequired("Username: ").ToLower();
```

Usernames are saved in **small letters**. That way `Juan_DC`, `JUAN_DC`, and `juan_dc` are all the same account. Nobody can make a look-alike account by changing capital letters.

```cs
string AskForNewPassword()
{
    ...
    if (error != "")
    {
        Console.WriteLine(error);
        continue;
    }

    string confirm = ConsoleHelper.ReadPassword("Type the password again: ");
    ...
}
```

If the password breaks a rule, `continue` goes back to the top of the loop and asks again. If it passes, the user types it **a second time**. That catches typing mistakes, which matters because the user cannot see what they typed.

---

## If Something Goes Wrong

**Error List: `CS0103  The name 'PasswordHasher' does not exist in the current context`**
`PasswordHasher.cs` is not in this project. Do Step 3 again.

**Error List: many errors right after adding a file.**
You probably did not delete **all** of the starter code in the new file. Open the file, press **Ctrl + A**, then **Delete**, and paste the code again.

**The black window shows: `SQLite Error 1: 'no such table: Users'.`**
`Database.CreateTables();` must be the **first line** of code in `Program.cs`. The table has to exist before anything uses it.

**I forgot the password I registered.**
Nobody can see it. Not even you. That is the whole point! While practicing, just register a **new** account with a different username.

---

## Exercise

**Exercise 1.** Change the password rule: passwords must now be at least **10** characters long.

<details>
<summary>Show answer</summary>

In `AccountService.cs`, in `ValidatePassword`, change the number and the message:

```cs
if (password.Length < 10)
{
    return "Password must be at least 10 characters long.";
}
```

Also change the hint in `Program.cs` (in `AskForNewPassword`) so it says `at least 10 characters`.

</details>

**Exercise 2.** Register **two** accounts with **exactly the same password**. Then choose option 2 and compare the two saved values. Are they the same? Why?

<details>
<summary>Show answer</summary>

They are **different**. Each account got its own random **salt**, so the hashes are different, even though the passwords are the same. A hacker who steals the database cannot even tell that the two users share a password.

</details>

## ⭐ Bonus Challenge

Add a new password rule: the password must **not contain the username**. For example, the user `juan_dc` may not use the password `juan_dc2026`.

Hint: `ValidatePassword` must now receive the username too. Use `password.ToLower().Contains(username)`. You will change two files.

<details>
<summary>Show answer</summary>

In `AccountService.cs`, change the first line of the method, and add a check at the start:

```cs
public static string ValidatePassword(string password, string username)
{
    if (password.ToLower().Contains(username))
    {
        return "Password must not contain your username.";
    }

    // ... the rest of the checks stay the same ...
```

In `Program.cs`, give `AskForNewPassword` a `username` parameter, and pass it along:

```cs
string AskForNewPassword(string username)
{
    ...
    string error = AccountService.ValidatePassword(password, username);
    ...
}
```

And in `RegisterScreen`:

```cs
string password = AskForNewPassword(username);
```

</details>

---

## Remember

- A real project is split into files with **one job each**.
- **Validate** usernames and passwords with methods that give back an error message, or `""`.
- Save usernames in **small letters** with `.ToLower()`.
- `Console.ReadKey(true)` reads one key without showing it. Use it to show `*` for passwords.
- Ask for the password **twice**, to catch typing mistakes.
- **Hash first, then save.** The database only ever sees the hash.

---

[⬅️ Lesson 27](27-password-hashing.md) | [🏠 Course Home](../README.md) | [Next: Lesson 29 (Account System: Log In) ➡️](29-account-system-login.md)
