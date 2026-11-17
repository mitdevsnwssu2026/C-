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
