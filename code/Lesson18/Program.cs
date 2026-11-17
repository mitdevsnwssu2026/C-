// Lesson 18: SQL PLAYGROUND
// A small tool for practicing SQL commands.
// You do NOT need to understand this code yet.
// By the end of Lesson 21 you will understand every line.

using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data Source=playground.db");
connection.Open();

Console.WriteLine("========== SQL PLAYGROUND ==========");
Console.WriteLine("Type one SQL command and press Enter.");
Console.WriteLine("Type EXIT to quit.");

while (true)
{
    Console.WriteLine();
    Console.Write("SQL> ");
    string sql = (Console.ReadLine() ?? "").Trim();

    if (sql.ToUpper() == "EXIT")
    {
        break;
    }

    if (sql == "")
    {
        continue;
    }

    try
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        using var reader = command.ExecuteReader();

        // Commands like CREATE, INSERT, UPDATE and DELETE do not return a table
        if (reader.FieldCount == 0)
        {
            Console.WriteLine("OK. Command finished.");
            if (reader.RecordsAffected > 0)
            {
                Console.WriteLine($"Rows changed: {reader.RecordsAffected}");
            }
            continue;
        }

        // SELECT returns a table, so print the column names first...
        for (int i = 0; i < reader.FieldCount; i++)
        {
            Console.Write(reader.GetName(i).PadRight(18));
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', 18 * reader.FieldCount));

        // ...then print every row
        int rowCount = 0;
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string value = reader.GetValue(i).ToString() ?? "";
                Console.Write(value.PadRight(18));
            }
            Console.WriteLine();
            rowCount++;
        }
        Console.WriteLine($"({rowCount} row(s) found)");
    }
    catch (SqliteException ex)
    {
        Console.WriteLine($"SQL error: {ex.Message}");
    }
}

Console.WriteLine("Goodbye!");
