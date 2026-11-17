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
