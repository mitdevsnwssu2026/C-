// Lesson 26: FINAL PROJECT - Account System (Register, Log in, Change password, Log out)

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
