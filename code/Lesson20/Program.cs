// Lesson 20: Saving (inserting) a student into the database

using Microsoft.Data.Sqlite;

const string ConnectionString = "Data Source=school.db";

// ----- Main program -----
CreateStudentsTable();

Console.WriteLine("=== ADD A NEW STUDENT ===");
string studentNumber = ReadRequired("Student number (example: 2026-0001): ");

if (StudentNumberExists(studentNumber))
{
    Console.WriteLine($"Student number {studentNumber} is already saved. Nothing was added.");
}
else
{
    string fullName = ReadRequired("Full name: ");
    string course = ReadRequired("Course (example: BSIT): ");
    int yearLevel = ReadYearLevel();

    long newId = InsertStudent(studentNumber, fullName, course, yearLevel);
    Console.WriteLine($"Saved! {fullName} was given database ID {newId}.");
}

Console.WriteLine($"Total students in the database: {CountStudents()}");


// ----- Database methods -----

void CreateStudentsTable()
{
    using var connection = new SqliteConnection(ConnectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Students (
            Id            INTEGER PRIMARY KEY AUTOINCREMENT,
            StudentNumber TEXT    NOT NULL UNIQUE,
            FullName      TEXT    NOT NULL,
            Course        TEXT    NOT NULL,
            YearLevel     INTEGER NOT NULL
        );";
    command.ExecuteNonQuery();
}

bool StudentNumberExists(string studentNumber)
{
    using var connection = new SqliteConnection(ConnectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT COUNT(*) FROM Students WHERE StudentNumber = $studentNumber;";
    command.Parameters.AddWithValue("$studentNumber", studentNumber);

    int count = Convert.ToInt32(command.ExecuteScalar());
    return count > 0;
}

long InsertStudent(string studentNumber, string fullName, string course, int yearLevel)
{
    using var connection = new SqliteConnection(ConnectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = @"
        INSERT INTO Students (StudentNumber, FullName, Course, YearLevel)
        VALUES ($studentNumber, $fullName, $course, $yearLevel);

        SELECT last_insert_rowid();";

    command.Parameters.AddWithValue("$studentNumber", studentNumber);
    command.Parameters.AddWithValue("$fullName", fullName);
    command.Parameters.AddWithValue("$course", course);
    command.Parameters.AddWithValue("$yearLevel", yearLevel);

    long newId = Convert.ToInt64(command.ExecuteScalar());
    return newId;
}

int CountStudents()
{
    using var connection = new SqliteConnection(ConnectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT COUNT(*) FROM Students;";
    return Convert.ToInt32(command.ExecuteScalar());
}


// ----- Input methods -----

string ReadRequired(string prompt)
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

int ReadYearLevel()
{
    while (true)
    {
        Console.Write("Year level (1-5): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int yearLevel) && yearLevel >= 1 && yearLevel <= 5)
        {
            return yearLevel;
        }

        Console.WriteLine("Please type a whole number from 1 to 5.");
    }
}
