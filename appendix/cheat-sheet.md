# 📋 Cheat Sheet

Everything from the course on one page. Print it, and keep it next to your keyboard!

---

## Visual Studio: What to Click

### Start a new lesson

1. Open **Visual Studio**.
2. Click **Create a new project**.
3. Click **Console App** (with **C#**, not ".NET Framework") → **Next**.
4. Type the **Project name** (like `Lesson07`). Location: `C:\CSharpCourse` → **Next**.
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
| Add a **package** (SQLite, Dapper...) | Right-click the project → **Manage NuGet Packages...** → **Browse** → type its name → **Install** |
| Open the **Package Manager Console** | **Tools** → **NuGet Package Manager** → **Package Manager Console** |
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

## Text (Strings)

```cs
string name = "  Juan Dela Cruz  ";

string full = "Juan" + " " + "Cruz";      // join text
int count = name.Length;                   // count the characters
string clean = name.Trim();                // remove spaces at the start and end
string big = clean.ToUpper();              // CAPITAL letters
string small = clean.ToLower();            // small letters
string dashed = clean.Replace(" ", "-");   // replace every space with a dash
bool hasJuan = clean.Contains("Juan");     // is "Juan" inside? true or false
bool startsJ = clean.StartsWith("J");      // does it start with "J"?
int position = clean.IndexOf("Dela");      // where is it? (positions start at 0)
string firstFour = clean.Substring(0, 4);  // cut out 4 characters, from position 0
```

A tool gives back a **new** text. Keep the result: `name = name.Trim();`

## Operators

| Kind | Operators |
|---|---|
| **Arithmetic** | `+` `-` `*` `/` `%` |
| **Comparison** | `==` `!=` `>` `<` `>=` `<=` |
| **Logical** | `&&` (and), `\|\|` (or), `!` (not) |

**Order when mixed:** parentheses → `!` → `* / %` → `+ -` → `< > <= >=` → `== !=` → `&&` → `||`

```cs
bool passed = average >= 75;                           // comparison
bool canVote = age >= 18 && hasId;                     // logical
bool scholarship = (a + b + c) / 3 >= 85 && absences <= 3;   // mixed
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

A **nested `if`** is an `if` inside another `if`:

```cs
if (age >= 18)
{
    if (hasId)
    {
        Console.WriteLine("You may enter.");
    }
}
```

## Loops

```cs
for (int i = 1; i <= 10; i++) { ... }   // count from 1 to 10
while (condition) { ... }               // repeat while true (checks BEFORE each round)
do { ... } while (condition);           // runs at least once (checks AFTER each round)
foreach (string item in items) { ... }  // every item in an array or list
break;                                  // leave the loop
continue;                               // skip to the next round

// Nested loops: the inner loop runs completely for each round of the outer loop
for (int row = 1; row <= 3; row++)
{
    for (int column = 1; column <= 5; column++)
    {
        Console.Write("* ");
    }

    Console.WriteLine();
}
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

## Arrays

```cs
int[] scores = { 85, 92, 78, 95, 88 };   // an array of whole numbers
double[] prices = new double[3];         // an empty array with 3 slots

int first = scores[0];                   // positions start at 0
int last = scores[scores.Length - 1];    // the last item
scores[2] = 80;                          // change an item
int howMany = scores.Length;             // how many items

for (int i = 0; i < scores.Length; i++) { ... }   // visit every item, with its index
foreach (int score in scores) { ... }             // visit every item

int total = scores.Sum();                // also .Average(), .Max(), .Min()
Array.Sort(scores);                      // lowest to highest
Array.Reverse(scores);                   // turn the order around
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

## Functions (Methods)

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

## Enums

```cs
CupSize size = CupSize.Large;            // use a choice
int number = (int)size;                  // the number behind it: 2

if (size == CupSize.Large) { ... }

switch (size)
{
    case CupSize.Small:
        Console.WriteLine("Small cup");
        break;
    default:
        Console.WriteLine("Bigger cup");
        break;
}

// In Program.cs, enums go at the very bottom of the file
enum CupSize
{
    Small,
    Medium,
    Large
}
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

## ADO.NET (C# and SQLite Together)

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

## Three Ways to Talk to a Database

The same job ("get all the students, sorted by name") in three ways:

```cs
// 1. ADO.NET (Lessons 20-23): you write the SQL, and you fill the objects
using var command = connection.CreateCommand();
command.CommandText = "SELECT Id, FullName FROM Students ORDER BY FullName;";
using var reader = command.ExecuteReader();
while (reader.Read()) { ... reader.GetInt32(0) ... reader.GetString(1) ... }

// 2. Dapper (Lesson 24): you write the SQL, and Dapper fills the objects
List<Student> students = connection.Query<Student>(
    "SELECT Id, FullName FROM Students ORDER BY FullName;").ToList();

// 3. EF Core (Lesson 25): EF Core writes the SQL and fills the objects
List<Student> students = db.Students.OrderBy(s => s.FullName).ToList();
```

## Dapper

```cs
using Dapper;   // at the very top of the file

connection.Execute(sql, student);                           // INSERT, UPDATE, DELETE
int count = connection.ExecuteScalar<int>(sql);             // one value
List<Student> all = connection.Query<Student>(sql).ToList();                      // many rows
Student? one = connection.QueryFirstOrDefault<Student>(sql, new { Id = 5 });      // one row, or null

// Placeholders use @ and the property name:
// "INSERT INTO Students (FullName) VALUES (@FullName);"
```

## EF Core (Database First)

```text
Packages:  Microsoft.EntityFrameworkCore.Sqlite  +  Microsoft.EntityFrameworkCore.Tools
           (the version must start with the same number as your .NET: .NET 10 → 10.x)

Tools → NuGet Package Manager → Package Manager Console, then:

Scaffold-DbContext "Data Source=C:\CSharpCourse\school.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models -Context SchoolContext
```

```cs
using Lesson25.Models;   // at the very top of the file

using var db = new SchoolContext();

List<Student> all = db.Students.OrderBy(s => s.FullName).ToList();           // READ all
List<Student> some = db.Students.Where(s => s.Course == "BSIT").ToList();    // READ some
Student? one = db.Students.FirstOrDefault(s => s.Id == 5);                   // READ one, or null
bool exists = db.Students.Any(s => s.StudentNumber == "2026-0001");          // is there one?
int total = db.Students.Count();                                             // how many?

db.Students.Add(student);        // CREATE
one.YearLevel = 2;               // UPDATE (just change the object)
db.Students.Remove(one);         // DELETE
db.SaveChanges();                // nothing is saved until this line!
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
