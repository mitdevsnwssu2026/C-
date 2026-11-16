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
