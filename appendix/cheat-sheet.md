# 📋 Cheat Sheet

Everything from the course on one page. Print it and keep it next to your keyboard!

---

## Command Prompt

| Command | What it does |
|---|---|
| `cd C:\CSharpCourse` | Go to the course folder |
| `cd FolderName` | Go into a folder |
| `cd ..` | Go up one folder |
| `dir` | List the files here |
| `mkdir Name` | Make a folder |
| `cls` | Clear the screen |
| `copy ..\Lesson15\Student.cs .` | Copy a file from another folder to here |
| `del school.db` | Delete a file (⚠️ careful!) |
| `notepad file.csv` | Open a file in Notepad |
| **↑** / **Tab** / **Ctrl + C** | Previous command / finish a name / stop a program |

## .NET Commands

| Command | What it does |
|---|---|
| `dotnet --version` | Check that .NET is installed |
| `dotnet new console -n Lesson05` | Create a new project |
| `dotnet run` | Build and run the project in this folder |
| `dotnet add package Microsoft.Data.Sqlite` | Add SQLite to this project |
| `code .` | Open this folder in VS Code |

## The Lesson Routine

```cmd
cd C:\CSharpCourse
dotnet new console -n LessonXX
cd LessonXX
code .
dotnet run
```

---

## C# Basics

```cs
// Output
Console.WriteLine("Hello!");           // show text, then a new line
Console.Write("Name: ");               // show text, stay on the same line

// Variables
string name = "Juan";                  // text
int age = 18;                          // whole number
double grade = 89.5;                   // decimal number
bool isEnrolled = true;                // true or false
Console.WriteLine($"{name} is {age}"); // put variables inside text
Console.WriteLine($"{grade:F2}");      // 2 decimal places

// Input
string text = (Console.ReadLine() ?? "").Trim();
int number = int.Parse(text);                        // crashes on bad input
bool ok = int.TryParse(text, out int safeNumber);    // never crashes

// Math
int sum = 5 + 3;      // also - * /
int rest = 7 % 2;     // remainder: 1
sum += 10;            // add 10
sum++;                // add 1
```

## Decisions

```cs
if (grade >= 90)
{
    Console.WriteLine("Excellent");
}
else if (grade >= 75 && isEnrolled)
{
    Console.WriteLine("Passed");
}
else
{
    Console.WriteLine("Failed");
}

switch (choice)
{
    case "1":
        Console.WriteLine("One");
        break;
    default:
        Console.WriteLine("Invalid");
        break;
}
```

| Compare | `==` `!=` `>` `<` `>=` `<=` |
|---|---|
| **Combine** | `&&` (and) `\|\|` (or) `!` (not) |

## Loops

```cs
for (int i = 1; i <= 10; i++) { ... }   // count from 1 to 10
while (condition) { ... }               // repeat while true
foreach (string item in items) { ... }  // every item in a list
break;                                  // leave the loop
continue;                               // skip to the next round
```

## The Menu Loop

```cs
bool running = true;

while (running)
{
    Console.WriteLine("1 - Do something");
    Console.WriteLine("2 - Exit");
    Console.Write("Choose: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            // do something
            break;
        case "2":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
```

## Lists

```cs
List<string> names = new List<string>();
names.Add("Ana");               // add
string first = names[0];        // index starts at 0
int howMany = names.Count;      // count
bool has = names.Contains("Ana");
names.Remove("Ana");
```

## Methods

```cs
double Average(double a, double b)   // gives back a double
{
    return (a + b) / 2;
}

void Greet(string name)              // gives back nothing
{
    Console.WriteLine($"Hi, {name}!");
}
```

## Classes

```cs
public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";

    public void Display()
    {
        Console.WriteLine(FullName);
    }
}

Student s = new Student { Id = 1, FullName = "Juan Dela Cruz" };
s.Display();
```

## Errors

```cs
try
{
    // code that might crash
}
catch (FormatException)
{
    Console.WriteLine("Not a number.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Files

```cs
File.AppendAllText("data.csv", "a,b,c" + Environment.NewLine);
string[] lines = File.ReadAllLines("data.csv");
string[] parts = lines[0].Split(',');
bool exists = File.Exists("data.csv");
```

---

## SQL

```sql
CREATE TABLE IF NOT EXISTS Students (
    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentNumber TEXT    NOT NULL UNIQUE,
    FullName      TEXT    NOT NULL,
    YearLevel     INTEGER NOT NULL
);

INSERT INTO Students (StudentNumber, FullName, YearLevel) VALUES ('2026-0001', 'Juan', 1);
SELECT * FROM Students;
SELECT FullName FROM Students WHERE YearLevel = 1 ORDER BY FullName;
SELECT * FROM Students WHERE FullName LIKE '%san%';
SELECT COUNT(*) FROM Students;
UPDATE Students SET YearLevel = 2 WHERE Id = 1;   -- ALWAYS use WHERE!
DELETE FROM Students WHERE Id = 1;                -- ALWAYS use WHERE!
INSERT OR IGNORE INTO ...                         -- skip rows that break a rule
SELECT last_insert_rowid();                       -- the Id of the row just added
```

## C# + SQLite

```cs
using Microsoft.Data.Sqlite;   // at the very top of the file

using var connection = new SqliteConnection("Data Source=school.db");
connection.Open();

using var command = connection.CreateCommand();
command.CommandText = "SELECT Id, FullName FROM Students WHERE YearLevel = $year;";
command.Parameters.AddWithValue("$year", 1);    // ALWAYS use parameters!

// Changing data:   int rowsChanged = command.ExecuteNonQuery();
// One value:       int count = Convert.ToInt32(command.ExecuteScalar());
// Many rows:
using var reader = command.ExecuteReader();
while (reader.Read())
{
    int id = reader.GetInt32(0);
    string fullName = reader.GetString(1);
}

// Transaction (all or nothing):
// using var transaction = connection.BeginTransaction();
// ... run commands ...
// transaction.Commit();
```

## Passwords

```cs
string savedHash = PasswordHasher.Hash(password);           // when registering
bool correct = PasswordHasher.Verify(typed, savedHash);     // when logging in
```

**The rules:** never save real passwords · always salt · always use a slow hash (PBKDF2, bcrypt, Argon2) · same error message for a wrong username or password · always use SQL parameters.

[🏠 Course Home](../README.md)
