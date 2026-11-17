// Lesson 25: Account System - Part 1 (Registering accounts with hashed passwords)

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
