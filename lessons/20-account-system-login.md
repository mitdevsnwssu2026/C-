# Lesson 20: Account System, Part 2 (Log In, Change Password, Log Out) 🏆

**Part 4: Security and Accounts** · About 2 hours · **This is the final project!**

## What You Will Learn

You will finish the account system. At the end of this lesson, it can:

- **Log in** with a username and a hidden password (3 tries)
- Show a different menu for a user who is **logged in**
- Show the user's **profile**
- **Change the password** (only after typing the current one)
- **Log out**

## The Idea

### How login works

```text
The user types:  username "juan_dc", password "MyPass123"
                   │
                   ▼
1. Find the user:       SELECT ... FROM Users WHERE Username = 'juan_dc'
                        Not found? ──► "Invalid username or password."
2. Check the password:  PasswordHasher.Verify("MyPass123", savedHash)
                        False?     ──► "Invalid username or password."
3. Both OK?             ──► Logged in! ✅
```

### Why the same message for both mistakes?

A wrong **username** and a wrong **password** show the **same** message: `Invalid username or password.`

Imagine the message said *"That username does not exist."* Then a hacker could use your login screen to find out **which usernames are real**, and attack only those. One vague message gives a hacker **nothing**. Banks, Google, and Facebook all do this.

### Remembering who is logged in

```cs
User? currentUser = null;
```

- When `currentUser` is `null`, **nobody** is logged in. Show the **visitor menu** (Register, Log in...).
- When `currentUser` holds a `User`, that person is logged in. Show **their** menu (Profile, Change password, Log out).
- To log out, just set `currentUser = null;` again.

---

## Step by Step

### Step 1: Create a new project named `Lesson20`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson20`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

1. In the **Solution Explorer**, right-click the project line **Lesson20**.
2. Click **Manage NuGet Packages...**
3. Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.
4. Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**.
5. If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.
6. Close the NuGet tab.

### Step 3: Bring in five files from Lesson 19

**1.** In the Solution Explorer, right-click the project line **Lesson20**.

**2.** Move to **Add**, then click **Existing Item...**

**3.** Click the address bar at the top of the window. Type `C:\CSharpCourse\Lesson19` and press **Enter**.

**4.** Click **AccountService.cs** once. Then hold the **Ctrl** key, and click each of these: **ConsoleHelper.cs**, **Database.cs**, **PasswordHasher.cs**, **User.cs**.

> You should have **5 files** highlighted. Do **not** select `Program.cs`.

**5.** Click **Add**.

✅ **You will see:** the five files appear in the Solution Explorer, under **Lesson20**.

### Step 4: Add three new methods to `AccountService.cs`

**1.** In the Solution Explorer, double-click **AccountService.cs**.

**2.** Scroll down and find this line:

```cs
    public static List<User> GetAllUsers()
```

**3.** Click at the very start of that line, and press **Enter** twice to make empty space above it.

**4.** In that empty space, paste these three methods:

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
<summary>Not sure where they go? Show the complete <code>AccountService.cs</code></summary>

You can replace the whole file with this (**Ctrl + A**, **Delete**, then paste):

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

### Step 5: Put the main program in `Program.cs`

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

## The Code

**File: `Program.cs`**

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

### Step 6: Run it

Press **Ctrl + F5**.

> This project has its **own new database**, so the accounts from Lesson 19 are not here. **Register a new account first** (option 1). Then log in with it (option 2).

## The Output

✅ **You will see something like this** (after registering `juan_dc`):

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

**🎉 Congratulations! You have built a real account system with safe, hashed passwords!**

---

## What the Code Means: `AccountService.cs`

### `FindByUsername`: reading one row

```cs
using var reader = command.ExecuteReader();

if (!reader.Read())
{
    return null;
}
```

Only **one** row can match, because usernames are `UNIQUE`. So instead of `while (reader.Read())`, the code calls `reader.Read()` **once**. If it gives `false`, there is no such user, and the method gives back `null`.

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

The method gives back the same thing (`null`) for both mistakes. The screen **cannot** tell them apart. Neither can a hacker.

### `ChangePassword`: hash the new one

```cs
string newHash = PasswordHasher.Hash(newPassword);
...
command.CommandText = "UPDATE Users SET PasswordHash = $passwordHash WHERE Id = $id;";
```

The new password gets a **new salt** and a **new hash**. Only that one user's row is changed, thanks to `WHERE Id = $id`.

---

## What the Code Means: `Program.cs`

### Two menus in one loop

```cs
while (running)
{
    if (currentUser == null)
    {
        // the visitor menu
    }
    else
    {
        // the menu for a logged-in user
    }
}
```

Each time around the loop, the program checks **who is logged in**, and shows the right menu.

### Logging in

```cs
case "2":
    currentUser = LoginScreen();
    break;
```

`LoginScreen` gives back a `User` when the login worked, or `null` when it failed. Either way, the result goes into `currentUser`. The next time around the loop, the menu changes by itself.

### `LoginScreen`: three tries

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

This is the **PIN program from Lesson 06**, now protecting real, hashed accounts. A good login does `return user;` right away, which also ends the loop. After 3 failures, the loop ends, and the method gives back `null`.

### Changing the password safely

```cs
if (AccountService.Login(user.Username, currentPassword) == null)
{
    Console.WriteLine("Wrong current password. Your password was NOT changed.");
    return;
}
```

Why ask for the **current** password, when the user is already logged in? Imagine someone leaves the computer for a minute, still logged in. Another person could sit down and change the password, and lock the real owner out. Asking for the current password stops that.

### Logging out

```cs
case "3":
    Console.WriteLine($"Goodbye, {currentUser.FullName}! You are now logged out.");
    currentUser = null;
    break;
```

Set `currentUser` back to `null`. The next time around the loop, the visitor menu appears.

---

## If Something Goes Wrong

**Error List: `CS0117  'AccountService' does not contain a definition for 'Login'`**
The three new methods are missing, or they were pasted **outside** the class. Use the complete `AccountService.cs` from Step 4.

**Error List: `CS0103  The name 'Database' does not exist in the current context`** (or `ConsoleHelper`, `PasswordHasher`, `User`)
A file from Lesson 19 is missing. Do Step 3 again, and check that all five files are in the Solution Explorer.

**I can't log in.**
- This project has a new, empty database. Did you **register** an account in this project first?
- Usernames are saved in small letters. Choose option 3 to see the exact usernames.
- Forgot the password? Register a new account.

**I compared the password with `==`, and it never matches.**

```cs
if (password == user.PasswordHash)   // ❌ never true!
```

`user.PasswordHash` is a hash, not a password. Always use `PasswordHasher.Verify(password, user.PasswordHash)`.

---

## Exercise: Test Your Final Project

A real programmer **tests** their work. Go through this list. Tick each box only when you have seen it work with your own eyes.

**Registering**

- [ ] A new account can be registered
- [ ] A username shorter than 4 characters is refused
- [ ] A username with a space in it is refused
- [ ] A username that is already taken is refused, even with different capital letters
- [ ] A password shorter than 8 characters is refused
- [ ] A password with no number is refused
- [ ] The password shows as `*` while typing
- [ ] Two different passwords in "Choose" and "Type again" are refused

**Logging in**

- [ ] The correct password logs you in
- [ ] A wrong password is refused
- [ ] A username that does not exist shows the **same** message as a wrong password
- [ ] Typing the username in CAPITAL letters still works
- [ ] After 3 failed tries, you go back to the main menu

**While logged in**

- [ ] "View my profile" shows your information
- [ ] "Change my password" refuses a wrong current password
- [ ] After changing the password, the **old** password no longer works
- [ ] After changing the password, the **new** password works
- [ ] "Log out" brings back the visitor menu

**Safety**

- [ ] Option 3 shows **only hashes**. No real password is anywhere
- [ ] After a password change, the saved hash for that user is **different**

## ⭐ Bonus Challenges

Finished early? Try these. They have hints, but no full answers. Working it out yourself **is** the challenge!

> [!TIP]
> Some challenges add **new columns** to the `Users` table. Remember Lesson 14: `CREATE TABLE IF NOT EXISTS` does not change a table that already exists. While practicing, delete the old database (project → **bin** → **Debug** → **net10.0** → delete `accounts.db`). The program will create it again with your new columns. Then register your accounts again.

**Bonus 1: Last login time.** Add a `LastLoginAt` column (TEXT). When a login works, `UPDATE` it with the current date and time. Show it on the profile screen.
*Hint:* write `LastLoginAt TEXT` with no `NOT NULL`, because a new account has not logged in yet. To read a column that may be empty: `reader.IsDBNull(5) ? "Never" : reader.GetString(5)`

**Bonus 2: Delete my account.** Add `4 - Delete my account` to the logged-in menu. Ask for the current password, **and** ask the user to type `DELETE` to confirm. Then delete the row, and log the user out.

**Bonus 3: An admin.** Make "View all accounts" work **only** when the logged-in user's username is `admin`. Move it to the logged-in menu.

**Bonus 4: Lock the account (advanced).** Add two columns: `FailedAttempts INTEGER NOT NULL DEFAULT 0` and `LockedUntil TEXT`. After 5 wrong passwords in a row, lock the account for 5 minutes, even if the program is closed and opened again. Set `FailedAttempts` back to 0 after a good login.
*Hint:* save a time with `DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")`. Read it back with `DateTime.Parse(text)`. Compare with `DateTime.Now < lockedUntil`.

---

## What's Next?

You started with `Hello, World!`. You finished with a safe account system that saves to a database. That is a real achievement. 👏

Here is where you can go from here:

- **Git and GitHub** → save every version of your code, and share it online
- **ASP.NET Core** → build **websites** with C#. Your `AccountService` and `PasswordHasher` ideas work there too
- **Entity Framework Core** → a tool that writes most of the SQL for you
- **SQL Server or MySQL** → bigger databases used by companies. The same ideas apply: connection, command, parameters
- **ASP.NET Core Identity** → a ready-made, professional account system

> [!IMPORTANT]
> In real projects, use well-tested, ready-made tools like **ASP.NET Core Identity** for accounts whenever you can. You built this system to **understand how it works inside**. That understanding makes you a much better programmer, whatever tools you use.

---

## Remember

- **Login** = find the user by username, then `Verify` the password against the saved hash.
- Use **one** message for a wrong username and a wrong password.
- `User? currentUser` remembers who is logged in. `null` means nobody.
- Ask for the **current** password before allowing a password change.
- A new password gets a **new salt** and a **new hash**.
- **Test** your program before you say it is finished.

---

[⬅️ Lesson 19](19-account-system-register.md) | [🏠 Course Home](../README.md)
