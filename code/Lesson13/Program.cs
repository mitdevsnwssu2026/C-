// Lesson 13: Connecting C# to a SQLite database

using Microsoft.Data.Sqlite;

// 1. Tell C# where the database file is
string connectionString = "Data Source=school.db";

// 2. Open the connection
using var connection = new SqliteConnection(connectionString);
connection.Open();
Console.WriteLine("Connected to school.db!");

// 3. Create the Students table (only if it does not exist yet)
using var createCommand = connection.CreateCommand();
createCommand.CommandText = @"
    CREATE TABLE IF NOT EXISTS Students (
        Id            INTEGER PRIMARY KEY AUTOINCREMENT,
        StudentNumber TEXT    NOT NULL UNIQUE,
        FullName      TEXT    NOT NULL,
        Course        TEXT    NOT NULL,
        YearLevel     INTEGER NOT NULL
    );";
createCommand.ExecuteNonQuery();
Console.WriteLine("The Students table is ready.");

// 4. Ask the database how many students are saved
using var countCommand = connection.CreateCommand();
countCommand.CommandText = "SELECT COUNT(*) FROM Students;";
int count = Convert.ToInt32(countCommand.ExecuteScalar());
Console.WriteLine($"Students saved in the database: {count}");
