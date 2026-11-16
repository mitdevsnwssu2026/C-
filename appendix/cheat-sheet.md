# 📋 Cheat Sheet

Everything from the course on one page. Print it, and keep it next to your keyboard!

---

## Visual Studio: What to Click

### Start a new lesson

1. Open **Visual Studio**.
2. Click **Create a new project**.
3. Click **Console App** (with **C#**, not ".NET Framework") → **Next**.
4. Type the **Project name** (like `Lesson05`). Location: `C:\CSharpCourse` → **Next**.
5. Click **Create**.

### Everyday actions

| I want to... | Do this |
|---|---|
| **Run** my program | **Ctrl + F5** (or **Debug** → **Start Without Debugging**) |
| Close the black window | Press any key |
| Stop a program that will not stop | Click the **X** on the black window |
| Clear the editor | **Ctrl + A**, then **Delete** |
| Undo my last change | **Ctrl + Z** |
| Save | **Ctrl + S** (Visual Studio also saves when you run) |
| See my mistakes | Look at the **Error List** at the bottom. Double-click a row to jump to it |
| Open an old project | Start window → **Open recent** → click its name |

### Working with files

| I want to... | Do this |
|---|---|
| Add a **new class file** | Right-click the project → **Add** → **Class...** → type the name → **Add** |
| **Copy a file** from another lesson | Right-click the project → **Add** → **Existing Item...** → find the file → **Add** |
| Add a **new data file** (like a `.csv`) | Right-click the project → **Add** → **New Item...** → type the name → **Add** |
| Make a data file available to my program | Click the file → **Properties** panel (**F4**) → **Copy to Output Directory** → **Copy if newer** |
| Add the **SQLite package** | Right-click the project → **Manage NuGet Packages...** → **Browse** → `Microsoft.Data.Sqlite` → **Install** |
| Find files **made by my program** | Right-click the project → **Open Folder in File Explorer** → **bin** → **Debug** → **net10.0** |

### Panels gone missing?

| Panel | How to bring it back |
|---|---|
| Solution Explorer | **View** → **Solution Explorer** |
| Error List | **View** → **Error List** |
| Properties | Click a file, then press **F4** |
| Everything is messed up | **Window** → **Reset Window Layout** |

---

## C# Basics

```cs
// Showing text
Console.WriteLine("Hello!");           // show text, then go to a new line
Console.Write("Name: ");               // show text, stay on the same line

// Variables
string name = "Juan";                  // text
int age = 18;                          // whole number
double grade = 89.5;                   // decimal number
bool isEnrolled = true;                // true or false
Console.WriteLine($"{name} is {age}"); // put variables inside text
Console.WriteLine($"{grade:F2}");      // 2 decimal places

// Reading what the user types
string text = (Console.ReadLine() ?? "").Trim();
int number = int.Parse(text);                        // crashes on letters
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

- **Compare:** `==` `!=` `>` `<` `>=` `<=`
- **Combine:** `&&` (and), `||` (or), `!` (not)

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
names.Add("Ana");                   // add
string first = names[0];            // positions start at 0
int howMany = names.Count;          // how many
bool has = names.Contains("Ana");   // is it in the list?
names.Remove("Ana");                // remove
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

## Catching Crashes

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

## C# and SQLite Together

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

// All or nothing:
// using var transaction = connection.BeginTransaction();
// ... run commands ...
// transaction.Commit();
```

## Passwords

```cs
string savedHash = PasswordHasher.Hash(password);           // when registering
bool correct = PasswordHasher.Verify(typed, savedHash);     // when logging in
```

**The rules:**

- Never save real passwords
- Always use a salt
- Always use a slow hash (PBKDF2, bcrypt, or Argon2)
- Use the same message for a wrong username and a wrong password
- Always use SQL parameters

[🏠 Course Home](../README.md)
