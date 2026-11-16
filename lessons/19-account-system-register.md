# Lesson 19: Account System, Part 1 (Registering Accounts)

| | |
|---|---|
| **Part** | 4: Security and Accounts |
| **Time** | About 2 hours (you can split it into two sessions) |
| **Before this** | [Lesson 18](18-password-hashing.md) |

## 🎯 Goal

In Lessons 19 and 20, you will build the **final project**: a complete **account system** with hashed passwords.

In this lesson (Part 1), you will build:

- A `Users` table in a SQLite database
- A **Register** screen with username and password **rules**
- **Hidden password typing**: the screen shows `*****` instead of the password
- A screen that proves the database holds **only hashes**, never real passwords

---

## 🧠 The Big Idea

Here is everything you have learned, coming together:

| Lesson | What you will use from it |
|---|---|
| 06 | The menu loop |
| 08 | Crash-proof input methods |
| 09 | Classes (`User`) |
| 13–15 | Database connection, parameters, reading rows, one job per file |
| 17–18 | `PasswordHasher` |

### The plan: six files, one job each

| File | Its job |
|---|---|
| `User.cs` | Describes what a user account **is** |
| `Database.cs` | Opens the database and creates the `Users` table |
| `PasswordHasher.cs` | Hashes and verifies passwords (from Lesson 18) |
| `ConsoleHelper.cs` | Reads input from the keyboard, including **hidden** passwords |
| `AccountService.cs` | The account **rules** and the account **SQL** |
| `Program.cs` | The menu and the screens |

### What happens when someone registers

```text
User types:  username "Juan_DC", name "Juan Dela Cruz", password "MyPass123"
                │
                ▼
1. Clean up:      "Juan_DC" ──► "juan_dc" (small letters)
2. Check rules:   4 to 20 characters? Only letters, numbers, _ ? Not taken?
3. Check password: at least 8 characters? Has a letter and a number? Typed the same twice?
4. Hash it:       "MyPass123" ──► "600000.PCVKUp7z...==.5gsZQvtz...="
5. Save:          INSERT INTO Users (Username, FullName, PasswordHash, CreatedAt) ...
```

The real password exists only **in memory**, for a moment. It is **never** written to the database.

---

## 🛠️ Let's Build It

### Step 1: Create the project and add SQLite

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson19
cd Lesson19
dotnet add package Microsoft.Data.Sqlite
```

### Step 2: Copy `PasswordHasher.cs` from Lesson 18

```cmd
copy ..\Lesson18\PasswordHasher.cs .
```

You should see `1 file(s) copied.` Now open VS Code:

```cmd
code .
```

### Step 3: Create `User.cs`

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

### Step 4: Create `Database.cs`

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

This time, the database file is named **`accounts.db`**.

### Step 5: Create `ConsoleHelper.cs`

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

### Step 6: Create `AccountService.cs`

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

### Step 7: Write `Program.cs`

Replace everything in `Program.cs` with:

```csharp
// Lesson 19: Account System - Part 1 (Registering accounts with hashed passwords)

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

### Step 8: Check your files

```text
LESSON19
├── AccountService.cs
├── ConsoleHelper.cs
├── Database.cs
├── Lesson19.csproj
├── PasswordHasher.cs
├── Program.cs
└── User.cs
```

### Step 9: Run it

```cmd
dotnet run
```

Register an account. **Break the rules on purpose** to see the checks work. When you type a password, you will see stars (`*`) instead of letters.

✅ **You should see something like this:**

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

Register a **second** account (for example, username `maria`, full name `Maria Santos`). Then choose **option 2**:

```text
Choose an option: 2

ID 1: juan_dc (Juan Dela Cruz) - created 2026-11-16 14:45:42
     Saved in database: 600000.PCVKUp7zyEiV1KvpG7nC5Q==.5gsZQvtz04+xJMYcwZp9FYjj0BGC80zx5YSyMRe1C5o=
ID 2: maria (Maria Santos) - created 2026-11-16 14:45:42
     Saved in database: 600000.DgnfxpaXSpipFSo+9EAokg==.mK5j1a9LR16YU0As85oJZzYIwORs9za/aMvemNjZOvw=

Notice: the real passwords are NOT saved anywhere. Only their hashes.
```

Look at what is saved. There is **no password** anywhere. Even you, the programmer, cannot see what password Juan chose. 🔒 (Your dates, salts, and hashes will be different.)

**Try one more thing:** register again with the username `JUAN_DC`. You will see `That username is already taken.` because usernames are saved in small letters.

---

## 🔎 Line by Line: `ConsoleHelper.ReadPassword`

This method shows `*` for each key the user presses, so nobody looking at the screen can read the password.

```cs
ConsoleKeyInfo key = Console.ReadKey(true);
```

`Console.ReadKey` reads **one key** at a time, instead of a whole line. The `true` means *"do not show the key on the screen."*

```cs
if (key.Key == ConsoleKey.Enter)
{
    Console.WriteLine();
    break;
}
```

When the user presses **Enter**, the password is finished. Move to a new line and leave the loop.

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
- `"\b \b"` erases one `*` from the screen: move back one space (`\b`), write a space over the star, and move back again.

```cs
else if (!char.IsControl(key.KeyChar))
{
    password = password + key.KeyChar;
    Console.Write("*");
}
```

For any normal key (a letter, a number, a symbol), add the character to the password and show a `*`. `char.IsControl` filters out special keys like **Tab** and the arrow keys.

```cs
if (Console.IsInputRedirected)
{
    return Console.ReadLine() ?? "";
}
```

You can ignore this part while learning. When a program's input comes from a **file** instead of a person typing (for example, when teachers test programs automatically), `ReadKey` cannot work, so we read the whole line instead.

---

## 🔎 Line by Line: `AccountService.cs`

### Rules that give back an error message

```cs
public static string ValidateUsername(string username)
```

This method returns an **error message** if something is wrong, or `""` (empty text) if everything is OK. The screen then decides what to do:

```cs
string error = AccountService.ValidateUsername(username);

if (error == "")
{
    return username;      // all good
}

Console.WriteLine(error); // show what is wrong, then ask again
```

Keeping the **rules** in `AccountService` and the **asking** in `Program.cs` means the rules can be reused anywhere. For example, a future website version of this system could use the same `ValidateUsername`.

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

A `string` is made of **characters** (`char`), and `foreach` can go through them one by one. If a character is **not** a letter or digit **and not** an underscore, the username is rejected.

### `Register`: hash first, then save

```cs
string passwordHash = PasswordHasher.Hash(password);
string createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
```

The password is hashed **before** anything touches the database. `DateTime.Now` is the current date and time. `.ToString("yyyy-MM-dd HH:mm:ss")` writes it like `2026-11-16 14:24:20`.

Then the `INSERT` saves the **hash**, never the password. And, of course, every value goes in as a **parameter** (Lesson 14).

---

## 🔎 Line by Line: `Program.cs`

```cs
string username = ConsoleHelper.ReadRequired("Username: ").ToLower();
```

Usernames are saved in **small letters**. That way, `Juan_DC`, `JUAN_DC`, and `juan_dc` are all the same account, and nobody can create a look-alike account by changing capital letters.

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

If the password breaks a rule, `continue` goes back to the top of the loop and asks again. If it passes, the user types it **again** to confirm. This catches typing mistakes, which is important because the user cannot see what they typed.

---

## ⚠️ Common Mistakes

### Mistake 1: `The name 'PasswordHasher' does not exist`

You forgot to copy `PasswordHasher.cs` into this project. **Fix:** `copy ..\Lesson18\PasswordHasher.cs .`

### Mistake 2: `no such table: Users`

`Database.CreateTables()` must be the **first** line of `Program.cs`, so the table exists before anything else uses it.

### Mistake 3: The password shows letters, not stars

This happens if you run the program in a place where `ReadKey` does not work, like some editor "output" panels. **Fix:** Always run with `dotnet run` in the **Command Prompt**.

### Mistake 4: "I forgot the password I registered"

Nobody can see it, not even you. That is the point! While practicing, just register a **new** account with a different username. (In a real system, there would be a "reset password" feature that sets a **new** password.)

---

## 🏋️ Practice

**Exercise 1.** Change the password rule so that passwords must be at least **10** characters.

<details>
<summary>Show answer</summary>

In `AccountService.ValidatePassword`, change the number and the message:

```cs
if (password.Length < 10)
{
    return "Password must be at least 10 characters long.";
}
```

Also update the hint in `Program.cs` (`AskForNewPassword`) so it says `at least 10 characters`.

</details>

**Exercise 2.** Add a rule: the password must **not contain the username**. For example, user `juan_dc` cannot use `juan_dc2026`. (Hint: change `ValidatePassword` to also take the username, and use `password.ToLower().Contains(username)`.)

<details>
<summary>Show answer</summary>

In `AccountService.cs`, change the method's first line and add a check at the start:

```cs
public static string ValidatePassword(string password, string username)
{
    if (password.ToLower().Contains(username))
    {
        return "Password must not contain your username.";
    }

    // ... the rest of the checks stay the same ...
```

In `Program.cs`, give `AskForNewPassword` a `username` parameter and pass it along:

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

**Exercise 3.** Install **DB Browser for SQLite** (see [Lesson 13](13-connecting-to-a-database.md#-optional-see-inside-your-database)) and open `accounts.db`. Look at the `Users` table in **Browse Data**. Can you find any real password? (Remember to **Close Database** before running your program again.)

<details>
<summary>Show answer</summary>

No. The `PasswordHash` column only contains values like `600000.xxxx==.yyyy=`. There is no way to read the real passwords from them. This is exactly what a hacker would see if they stole the database, and it does not help them log in.

</details>

---

## 📌 Recap

- A real project is split into files with **one job each**: model, database, hashing, input, rules, and screens.
- **Validate** usernames and passwords with methods that return an error message or `""`.
- Save usernames in **small letters** with `.ToLower()`.
- `Console.ReadKey(true)` reads one key without showing it. Use it to show `*` for passwords.
- Ask for the password **twice** to catch typing mistakes.
- **Hash first, then save.** The database only ever sees the hash.

---

[⬅️ Lesson 18](18-password-hashing.md) | [🏠 Course Home](../README.md) | [Next: Lesson 20 (Account System: Log In) ➡️](20-account-system-login.md)
