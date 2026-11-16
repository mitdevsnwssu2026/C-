# Lesson 20: Account System, Part 2 (Log In, Change Password, Log Out) 🏆 Final Project

| | |
|---|---|
| **Part** | 4: Security and Accounts |
| **Time** | About 2 hours |
| **Before this** | [Lesson 19](19-account-system-register.md) |

## 🎯 Goal

You will finish the **final project**. By the end of this lesson, your account system will be able to:

- **Log in** with a username and a hidden password (3 tries)
- Show a different menu for a **logged-in** user
- Show the user's **profile**
- **Change the password** (only after typing the current one)
- **Log out**

---

## 🧠 The Big Idea

### How login works

```text
User types:  username "juan_dc", password "MyPass123"
                │
                ▼
1. Find the user:     SELECT ... FROM Users WHERE Username = 'juan_dc'
                      Not found? ──► "Invalid username or password."
2. Check the password: PasswordHasher.Verify("MyPass123", savedHash)
                      False? ──► "Invalid username or password."
3. Both OK?           ──► Logged in! ✅
```

### Why the same error message for both mistakes?

Notice that a wrong **username** and a wrong **password** show the **same** message: `Invalid username or password.`

If the message said *"That username does not exist"*, a hacker could use your login screen to discover **which usernames are real**, and then attack only those accounts. A single, vague message gives hackers **nothing** to work with. Banks, Google, and Facebook all do this.

### Remembering who is logged in

```cs
User? currentUser = null;
```

- If `currentUser` is `null`, **nobody** is logged in. Show the **visitor menu** (Register, Log in...).
- If `currentUser` holds a `User`, that person is logged in. Show **their** menu (Profile, Change password, Log out).
- Logging out is as simple as setting `currentUser = null;` again.

---

## 🛠️ Let's Build It

### Step 1: Create the project and add SQLite

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson20
cd Lesson20
dotnet add package Microsoft.Data.Sqlite
```

### Step 2: Copy your files from Lesson 19

```cmd
copy ..\Lesson19\*.cs .
```

The `*` means **"every file"**, so `*.cs` means *every file ending in `.cs`*. You should see the 6 file names and `6 file(s) copied.`

> [!NOTE]
> This also copies Lesson 19's `Program.cs` over the new one. That is fine, because you will replace `Program.cs` in Step 4.

**Bring your accounts too!** Copy the database, so you can log in with the accounts you registered in Lesson 19:

```cmd
copy ..\Lesson19\accounts.db .
```

Now open VS Code:

```cmd
code .
```

### Step 3: Add three methods to `AccountService.cs`

Open `AccountService.cs`. Find the line `public static List<User> GetAllUsers()`. **Just above** that line, add these three methods:

```cs
    // Finds one user by username. Gives back null if there is no such user.
    public static User? FindByUsername(string username)
    {
        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Username, FullName, PasswordHash, CreatedAt
            FROM Users
            WHERE Username = $username;";
        command.Parameters.AddWithValue("$username", username);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            FullName = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            CreatedAt = reader.GetString(4)
        };
    }

    // Checks a username and password.
    // Gives back the User if both are correct, or null if not.
    public static User? Login(string username, string password)
    {
        User? user = FindByUsername(username);

        if (user == null)
        {
            return null; // no such username
        }

        if (!PasswordHasher.Verify(password, user.PasswordHash))
        {
            return null; // wrong password
        }

        return user;
    }

    // Saves a new password (hashed, of course!) for a user.
    public static void ChangePassword(int userId, string newPassword)
    {
        string newHash = PasswordHasher.Hash(newPassword);

        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Users SET PasswordHash = $passwordHash WHERE Id = $id;";
        command.Parameters.AddWithValue("$passwordHash", newHash);
        command.Parameters.AddWithValue("$id", userId);
        command.ExecuteNonQuery();
    }

```

<details>
<summary>Show the complete <code>AccountService.cs</code> after this change</summary>

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

    // Finds one user by username. Gives back null if there is no such user.
    public static User? FindByUsername(string username)
    {
        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Username, FullName, PasswordHash, CreatedAt
            FROM Users
            WHERE Username = $username;";
        command.Parameters.AddWithValue("$username", username);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            FullName = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            CreatedAt = reader.GetString(4)
        };
    }

    // Checks a username and password.
    // Gives back the User if both are correct, or null if not.
    public static User? Login(string username, string password)
    {
        User? user = FindByUsername(username);

        if (user == null)
        {
            return null; // no such username
        }

        if (!PasswordHasher.Verify(password, user.PasswordHash))
        {
            return null; // wrong password
        }

        return user;
    }

    // Saves a new password (hashed, of course!) for a user.
    public static void ChangePassword(int userId, string newPassword)
    {
        string newHash = PasswordHasher.Hash(newPassword);

        using var connection = Database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Users SET PasswordHash = $passwordHash WHERE Id = $id;";
        command.Parameters.AddWithValue("$passwordHash", newHash);
        command.Parameters.AddWithValue("$id", userId);
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

</details>

### Step 4: Replace `Program.cs`

Replace everything in `Program.cs` with:

```csharp
// Lesson 20: FINAL PROJECT - Account System (Register, Log in, Change password, Log out)

Database.CreateTables();

User? currentUser = null; // null means "nobody is logged in"
bool running = true;

while (running)
{
    Console.WriteLine();

    if (currentUser == null)
    {
        // ----- Menu for visitors (nobody is logged in) -----
        Console.WriteLine("========= ACCOUNT SYSTEM =========");
        Console.WriteLine("1 - Register a new account");
        Console.WriteLine("2 - Log in");
        Console.WriteLine("3 - View all accounts (for learning only)");
        Console.WriteLine("4 - Exit");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine() ?? "";
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                RegisterScreen();
                break;
            case "2":
                currentUser = LoginScreen();
                break;
            case "3":
                ViewAllAccountsScreen();
                break;
            case "4":
                running = false;
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid option. Please type a number from 1 to 4.");
                break;
        }
    }
    else
    {
        // ----- Menu for a logged-in user -----
        Console.WriteLine($"========= Logged in as: {currentUser.Username} =========");
        Console.WriteLine("1 - View my profile");
        Console.WriteLine("2 - Change my password");
        Console.WriteLine("3 - Log out");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine() ?? "";
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                ProfileScreen(currentUser);
                break;
            case "2":
                ChangePasswordScreen(currentUser);
                break;
            case "3":
                Console.WriteLine($"Goodbye, {currentUser.FullName}! You are now logged out.");
                currentUser = null;
                break;
            default:
                Console.WriteLine("Invalid option. Please type a number from 1 to 3.");
                break;
        }
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

User? LoginScreen()
{
    Console.WriteLine("--- LOG IN ---");

    for (int attempt = 1; attempt <= 3; attempt++)
    {
        string username = ConsoleHelper.ReadRequired("Username: ").ToLower();
        string password = ConsoleHelper.ReadPassword("Password: ");

        User? user = AccountService.Login(username, password);

        if (user != null)
        {
            Console.WriteLine($"Login successful! Welcome, {user.FullName}.");
            return user;
        }

        // Same message for "wrong username" and "wrong password" (safer!)
        Console.WriteLine($"Invalid username or password. Attempts left: {3 - attempt}");
    }

    Console.WriteLine("Too many failed attempts. Going back to the main menu.");
    return null;
}

void ProfileScreen(User user)
{
    Console.WriteLine("--- MY PROFILE ---");
    Console.WriteLine($"Account ID:   {user.Id}");
    Console.WriteLine($"Username:     {user.Username}");
    Console.WriteLine($"Full name:    {user.FullName}");
    Console.WriteLine($"Member since: {user.CreatedAt}");
}

void ChangePasswordScreen(User user)
{
    Console.WriteLine("--- CHANGE MY PASSWORD ---");
    string currentPassword = ConsoleHelper.ReadPassword("Current password: ");

    // Check the current password first, so nobody else can change it
    if (AccountService.Login(user.Username, currentPassword) == null)
    {
        Console.WriteLine("Wrong current password. Your password was NOT changed.");
        return;
    }

    string newPassword = AskForNewPassword();
    AccountService.ChangePassword(user.Id, newPassword);

    Console.WriteLine("Your password was changed!");
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

### Step 5: Run it

```cmd
dotnet run
```

Log in with an account from Lesson 19 (or register a new one first). Try a wrong password once, too.

✅ **You should see something like this:**

```text
========= ACCOUNT SYSTEM =========
1 - Register a new account
2 - Log in
3 - View all accounts (for learning only)
4 - Exit
Choose an option: 2

--- LOG IN ---
Username: juan_dc
Password: ********
Invalid username or password. Attempts left: 2
Username: juan_dc
Password: *********
Login successful! Welcome, Juan Dela Cruz.

========= Logged in as: juan_dc =========
1 - View my profile
2 - Change my password
3 - Log out
Choose an option: 1

--- MY PROFILE ---
Account ID:   1
Username:     juan_dc
Full name:    Juan Dela Cruz
Member since: 2026-11-16 14:45:42

========= Logged in as: juan_dc =========
1 - View my profile
2 - Change my password
3 - Log out
Choose an option: 2

--- CHANGE MY PASSWORD ---
Current password: *********
(Password: at least 8 characters, with at least one letter and one number.)
Choose a password: **********
Type the password again: **********
Your password was changed!

========= Logged in as: juan_dc =========
1 - View my profile
2 - Change my password
3 - Log out
Choose an option: 3

Goodbye, Juan Dela Cruz! You are now logged out.

========= ACCOUNT SYSTEM =========
1 - Register a new account
2 - Log in
3 - View all accounts (for learning only)
4 - Exit
Choose an option: 4

Goodbye!
```

Now test these on your own:

- [ ] Log in with the **old** password after changing it. It should **fail**.
- [ ] Log in with the **new** password. It should **work**.
- [ ] Type `JUAN_DC` (capital letters) as the username. It should still work.
- [ ] Type a username that does not exist. You should see the **same** message as for a wrong password.
- [ ] Fail **3 times** in a row. You should go back to the main menu.
- [ ] Choose option 3 on the main menu. The saved hash for Juan should be **different** from before, because you changed the password.

**🎉 Congratulations! You have built a real account system with secure, hashed passwords!**

---

## 🔎 Line by Line: `AccountService.cs`

### `FindByUsername`: reading one row

```cs
using var reader = command.ExecuteReader();

if (!reader.Read())
{
    return null;
}
```

We only expect **one** row (usernames are `UNIQUE`), so instead of `while (reader.Read())`, we call `reader.Read()` **once**. If it gives back `false`, there is no such user, so we return `null`.

### `Login`: two checks

```cs
User? user = FindByUsername(username);

if (user == null)
{
    return null; // no such username
}

if (!PasswordHasher.Verify(password, user.PasswordHash))
{
    return null; // wrong password
}

return user;
```

The method returns the same thing (`null`) for both mistakes, so the screen **cannot** tell them apart, and neither can a hacker.

### `ChangePassword`: hash the new one

```cs
string newHash = PasswordHasher.Hash(newPassword);
...
command.CommandText = "UPDATE Users SET PasswordHash = $passwordHash WHERE Id = $id;";
```

The new password gets a **new salt** and a **new hash**. Only that one user's row is updated, thanks to `WHERE Id = $id`.

---

## 🔎 Line by Line: `Program.cs`

### Two menus in one loop

```cs
while (running)
{
    if (currentUser == null)
    {
        // visitor menu
    }
    else
    {
        // logged-in menu
    }
}
```

Each time around the loop, the program checks **who is logged in** and shows the right menu.

### Logging in

```cs
case "2":
    currentUser = LoginScreen();
    break;
```

`LoginScreen` gives back a `User` if the login worked, or `null` if it failed. Either way, we store it in `currentUser`. Next time around the loop, the menu changes automatically.

### `LoginScreen`: 3 tries

```cs
for (int attempt = 1; attempt <= 3; attempt++)
{
    ...
    if (user != null)
    {
        Console.WriteLine($"Login successful! Welcome, {user.FullName}.");
        return user;
    }

    Console.WriteLine($"Invalid username or password. Attempts left: {3 - attempt}");
}

Console.WriteLine("Too many failed attempts. Going back to the main menu.");
return null;
```

This is the **PIN example from Lesson 06**, now protecting real, hashed accounts. A successful login `return`s **immediately**, which also ends the loop. After 3 failures, the loop ends and we return `null`.

### Changing the password safely

```cs
if (AccountService.Login(user.Username, currentPassword) == null)
{
    Console.WriteLine("Wrong current password. Your password was NOT changed.");
    return;
}
```

Why ask for the **current** password, when the user is already logged in? Imagine someone leaves the computer for a minute while still logged in. Another person could sit down and change the password, locking the real owner out. Asking for the current password stops that.

We reuse `AccountService.Login` to check it. Writing a check **once** and reusing it is less work and has fewer bugs.

### Logging out

```cs
case "3":
    Console.WriteLine($"Goodbye, {currentUser.FullName}! You are now logged out.");
    currentUser = null;
    break;
```

Set `currentUser` back to `null`. The next time around the loop, the visitor menu appears.

---

## ⚠️ Common Mistakes

### Mistake 1: `'AccountService' does not contain a definition for 'Login'`

The new methods are missing, or were pasted **outside** the class's `{ }`. **Fix:** Compare your file with the complete `AccountService.cs` in Step 3.

### Mistake 2: "I can't log in with my Lesson 19 account"

- Did you copy `accounts.db` from Lesson 19? Run `dir` and look for it.
- Usernames are in small letters. Check option 3 for the exact usernames.
- If you forgot the password, register a new account.

### Mistake 3: Comparing passwords directly

```cs
if (password == user.PasswordHash)   // ❌ never true!
```

`user.PasswordHash` is a hash, not a password. **Fix:** Always use `PasswordHasher.Verify(password, user.PasswordHash)`.

---

## 🏆 Final Project Checklist

Your project is complete when **all** of these are true:

- [ ] A new account can be registered
- [ ] Usernames must be 4 to 20 characters long, using only letters, numbers, and `_`
- [ ] Duplicate usernames are refused (even with different capital letters)
- [ ] Passwords must be at least 8 characters, with a letter and a number
- [ ] Passwords are typed twice, and show `*` on the screen
- [ ] The database contains **only hashes**, never real passwords
- [ ] Log in works with the correct password and fails with a wrong one
- [ ] Wrong usernames and wrong passwords show the **same** message
- [ ] After 3 failed tries, the user goes back to the main menu
- [ ] A logged-in user can view their profile
- [ ] Changing the password requires the current password
- [ ] After changing it, the old password no longer works
- [ ] Log out returns to the visitor menu
- [ ] **All SQL uses parameters.** No user input is joined into SQL text

---

## 🚀 Optional Challenges

Finished early? Try these. There are hints, but no full answers. Figuring it out is the challenge!

> [!TIP]
> Some challenges add **new columns** to the `Users` table. Remember from Lesson 14: `CREATE TABLE IF NOT EXISTS` does not change an existing table. While practicing, delete the old database (`del accounts.db`) so it is created again with your new columns. You will need to register your accounts again.

**Challenge 1: Last login time.** Add a `LastLoginAt` column (TEXT). When a login succeeds, `UPDATE` it with the current date and time. Show it on the profile screen.
*Hint:* `LastLoginAt TEXT` (no `NOT NULL`, since new accounts have not logged in yet). To read a column that might be empty, use `reader.IsDBNull(5) ? "Never" : reader.GetString(5)`.

**Challenge 2: Delete my account.** Add option `4 - Delete my account` to the logged-in menu. Ask for the current password **and** for the user to type `DELETE` to confirm. Then delete the row and log the user out.

**Challenge 3: An admin.** Make option 3 ("View all accounts") work **only** when the logged-in user's username is `admin`. Move it to the logged-in menu.

**Challenge 4: Account lockout (advanced).** Add `FailedAttempts INTEGER NOT NULL DEFAULT 0` and `LockedUntil TEXT` columns. After 5 wrong passwords in a row, lock the account for 5 minutes, even if the program is restarted. Reset `FailedAttempts` to 0 after a successful login.
*Hint:* save times with `DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")`, and read them back with `DateTime.Parse(text)`. Compare with `DateTime.Now < lockedUntil`.

---

## 🎓 What's Next?

You started with `Hello, World!` and finished with a secure account system backed by a database. That is a real achievement. 👏

Here is where you can go from here:

| Topic | What it is |
|---|---|
| **Git and GitHub** | Save every version of your code and share it online |
| **ASP.NET Core** | Build **websites** with C#. Your `AccountService` and `PasswordHasher` ideas work there too |
| **Entity Framework Core** | A tool that writes most of the SQL for you |
| **SQL Server or MySQL** | Bigger database servers used by companies. The same ideas apply: connection, command, parameters |
| **ASP.NET Core Identity** | A ready-made, professional account system with password hashing, lockout, and more |

> [!IMPORTANT]
> In real projects, use well-tested, ready-made tools like **ASP.NET Core Identity** for accounts whenever you can. You built this system to **understand how it works inside**. That understanding will make you a much better programmer, whatever tools you use.

---

## 📌 Recap

- **Login** = find the user by username, then `Verify` the password against the saved hash.
- Use **one** error message for a wrong username and a wrong password.
- `User? currentUser` remembers who is logged in. `null` means nobody.
- Show different menus depending on whether someone is logged in.
- Ask for the **current** password before allowing a password change.
- A new password gets a **new salt** and a **new hash**.

---

[⬅️ Lesson 19](19-account-system-register.md) | [🏠 Course Home](../README.md)
