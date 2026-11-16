# Lesson 14: Saving User Input to the Database (Safely!)

| | |
|---|---|
| **Part** | 3: Databases |
| **Time** | About 90 minutes |
| **Before this** | [Lesson 13](13-connecting-to-a-database.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Save what the user types into the database with `INSERT`
- Use **parameters** to keep your database safe from **SQL injection** attacks
- Check for duplicates **before** saving
- Get the new row's `Id` back from the database

---

## 🧠 The Big Idea: Never Glue User Input Into SQL

You might think you can build SQL by joining text together, like this:

```cs
// ❌ DANGEROUS! NEVER DO THIS!
command.CommandText = "INSERT INTO Students (FullName) VALUES ('" + fullName + "');";
```

This looks fine when the user types `Juan Dela Cruz`. The SQL becomes:

```sql
INSERT INTO Students (FullName) VALUES ('Juan Dela Cruz');
```

But what if a hacker types this as their "name"?

```text
x'); DROP TABLE Students;--
```

Now the SQL becomes:

```sql
INSERT INTO Students (FullName) VALUES ('x'); DROP TABLE Students;--');
```

The database sees **two** commands: insert `x`, then **delete the entire Students table**! 😱

This attack is called **SQL injection**. The hacker **injected** their own SQL into yours. It is one of the most common ways real websites get hacked.

### The fix: parameters

A **parameter** is a **placeholder** in your SQL. You fill it in **separately**:

```cs
// ✅ SAFE: always do it this way
command.CommandText = "INSERT INTO Students (FullName) VALUES ($fullName);";
command.Parameters.AddWithValue("$fullName", fullName);
```

| Piece | Meaning |
|---|---|
| `$fullName` (inside the SQL) | A **placeholder**: *"a value goes here"* |
| `command.Parameters.AddWithValue("$fullName", fullName)` | *"Fill the `$fullName` placeholder with the value in the C# variable `fullName`."* |

With parameters, the database treats the user's input as **plain data, never as SQL commands**. Even if a hacker types `x'); DROP TABLE Students;--`, it is saved as a weird-looking name, and nothing bad happens.

> [!CAUTION]
> **The #1 database rule of this course:**
> Whenever a value comes from the user (or from a file, or from anywhere outside your code), put it in the SQL **as a parameter**. Never join it into the SQL text with `+` or `$"..."`.

Think of it like a bank deposit slip. You write the amount **in the box** for the amount. You do not write new instructions for the bank teller in that box. Even if you did, the teller would just read it as a (strange) amount.

---

## 🛠️ Let's Build It: Add a Student

### Step 1: Create the project and add SQLite

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson14
cd Lesson14
dotnet add package Microsoft.Data.Sqlite
code .
```

### Step 2: Write the code

This program is longer, so it is organized into **methods** (Lesson 08). Read the **main program** first. It reads like a story. Then read each method.

> [!NOTE]
> From this lesson on, you may copy and paste the code. **Read every line** and make sure you understand it using the explanations below.

```csharp
// Lesson 14: Saving (inserting) a student into the database

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
```

### Step 3: Run it three times

**Run 1: Add a student.** Try leaving the course empty and typing a wrong year level, to see the checks work.

✅ **You should see:**

```text
=== ADD A NEW STUDENT ===
Student number (example: 2026-0001): 2026-0001
Full name: Juan Dela Cruz
Course (example: BSIT):
This field is required. Please type something.
Course (example: BSIT): BSIT
Year level (1-5): abc
Please type a whole number from 1 to 5.
Year level (1-5): 9
Please type a whole number from 1 to 5.
Year level (1-5): 1
Saved! Juan Dela Cruz was given database ID 1.
Total students in the database: 1
```

**Run 2: Try the same student number again.**

```text
=== ADD A NEW STUDENT ===
Student number (example: 2026-0001): 2026-0001
Student number 2026-0001 is already saved. Nothing was added.
Total students in the database: 1
```

**Run 3: Try the hacker's trick.** For the full name, type `x'); DROP TABLE Students;--`

```text
=== ADD A NEW STUDENT ===
Student number (example: 2026-0001): 2026-0002
Full name: x'); DROP TABLE Students;--
Course (example: BSIT): BSCS
Year level (1-5): 2
Saved! x'); DROP TABLE Students;-- was given database ID 2.
Total students in the database: 2
```

The table was **not** deleted. The total is 2. The "attack" was saved as a plain (silly) name. **Parameters protected your database.** 🛡️

---

## 🔎 Line by Line

### The main program

```cs
const string ConnectionString = "Data Source=school.db";
```

`const` means **constant**: a value that **never changes**. Every method uses this same connection string, so we write it once, at the top.

```cs
if (StudentNumberExists(studentNumber))
{
    Console.WriteLine(...already saved...);
}
else
{
    ...ask for the rest and save...
}
```

We check for a duplicate **right after** the student number is typed, so the user does not waste time typing everything else.

### `StudentNumberExists`: checking before saving

```cs
command.CommandText = "SELECT COUNT(*) FROM Students WHERE StudentNumber = $studentNumber;";
command.Parameters.AddWithValue("$studentNumber", studentNumber);

int count = Convert.ToInt32(command.ExecuteScalar());
return count > 0;
```

Count how many students already have this number. If more than 0, it exists. Notice the **parameter**, even here, because `studentNumber` came from the user.

> [!NOTE]
> The table also has the `UNIQUE` rule, so the database itself would refuse a duplicate. But a friendly message is much better than a crash. **Check first**, and let `UNIQUE` be your safety net.

### `InsertStudent`: saving and getting the new Id

```cs
command.CommandText = @"
    INSERT INTO Students (StudentNumber, FullName, Course, YearLevel)
    VALUES ($studentNumber, $fullName, $course, $yearLevel);

    SELECT last_insert_rowid();";
```

This runs **two** SQL commands, one after the other:

1. `INSERT` the new student, with four placeholders.
2. `SELECT last_insert_rowid();` asks SQLite: *"What `Id` did you just give that new row?"*

```cs
command.Parameters.AddWithValue("$studentNumber", studentNumber);
command.Parameters.AddWithValue("$fullName", fullName);
command.Parameters.AddWithValue("$course", course);
command.Parameters.AddWithValue("$yearLevel", yearLevel);
```

Fill in all four placeholders. The names must match **exactly**, including the `$`.

```cs
long newId = Convert.ToInt64(command.ExecuteScalar());
```

`ExecuteScalar` gives back the one value from the `SELECT`: the new `Id`. SQLite Ids can be very large numbers, so we use **`long`**, which is like `int` but for bigger whole numbers.

### Each method opens its own connection

Every database method follows the same 4-step pattern from Lesson 13:

```cs
using var connection = new SqliteConnection(ConnectionString);
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = "...";
```

Open the connection, do one job, and let `using` close it. This is simple and safe.

### `ReadRequired` and `ReadYearLevel`

These are the **crash-proof input** methods from Lessons 08 and 10. `ReadRequired` keeps asking until the user types something. `ReadYearLevel` keeps asking until the user types a whole number from 1 to 5:

```cs
if (int.TryParse(input, out int yearLevel) && yearLevel >= 1 && yearLevel <= 5)
```

*"If the input is a whole number **and** it is at least 1 **and** at most 5..."*

---

## ⚠️ Common Mistakes

### Mistake 1: A parameter name that does not match

```cs
command.CommandText = "... VALUES ($fullName);";
command.Parameters.AddWithValue("$fullname", fullName);   // small n!
```

```text
Unhandled exception. System.InvalidOperationException: Must add values for the following parameters: $fullName
```

**Fix:** The name in the SQL and the name in `AddWithValue` must be **exactly the same**.

### Mistake 2: Putting quotes around a parameter

```cs
VALUES ('$fullName')   // ❌ wrong
VALUES ($fullName)     // ✅ right
```

With quotes, SQLite saves the literal text `$fullName` instead of the value. Parameters **never** need quotes.

### Mistake 3: `UNIQUE constraint failed`

```text
Unhandled exception. Microsoft.Data.Sqlite.SqliteException (0x80004005): SQLite Error 19: 'UNIQUE constraint failed: Students.StudentNumber'.
```

You tried to save a duplicate student number without checking first. **Fix:** Check with a method like `StudentNumberExists` before inserting.

### Mistake 4: You changed the `CREATE TABLE`, but nothing changed

`CREATE TABLE IF NOT EXISTS` does **nothing** if the table already exists, even if you changed the columns in your code.

**Fix (while learning):** Delete the old database file, and the program will create a fresh one with your new columns:

```cmd
del school.db
```

⚠️ This deletes **all** the data in that database. Only do this while practicing.

---

## 🏋️ Practice

**Exercise 1.** Make the program ask `Add another student? (Y/N)` at the end, and repeat if the user types `Y`.

<details>
<summary>Show answer</summary>

Wrap the main program (from `Console.WriteLine("=== ADD A NEW STUDENT ===");` to the total line) in a loop:

```cs
CreateStudentsTable();

bool addMore = true;

while (addMore)
{
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

    Console.Write("Add another student? (Y/N): ");
    string answer = (Console.ReadLine() ?? "").Trim().ToUpper();
    addMore = answer == "Y";
}

Console.WriteLine($"Total students in the database: {CountStudents()}");
```

</details>

**Exercise 2.** Turn every course into capital letters before saving, so `bsit`, `Bsit`, and `BSIT` are all saved as `BSIT`.

<details>
<summary>Show answer</summary>

Change the course line in the main program:

```cs
string course = ReadRequired("Course (example: BSIT): ").ToUpper();
```

</details>

**Exercise 3.** In your own words, explain to a classmate why we use `$fullName` and `AddWithValue` instead of joining the name into the SQL with `+`.

<details>
<summary>Show answer</summary>

If we join user input into SQL with `+`, a user can type SQL code as their "input", and the database will **run** it. That attack is called SQL injection. With parameters, the database always treats the input as **data only**, never as commands, so the attack cannot work.

</details>

---

## 📌 Recap

- **SQL injection**: a hacker types SQL into an input box to attack your database.
- **Parameters** stop it: write `$name` in the SQL, then `command.Parameters.AddWithValue("$name", value);`
- **Always** use parameters for values that come from outside your code.
- Check for duplicates with `SELECT COUNT(*) ... WHERE ...` **before** inserting.
- `SELECT last_insert_rowid();` gives back the `Id` of the row you just added.
- `const` creates a value that never changes. `long` is a bigger `int`.

---

[⬅️ Lesson 13](13-connecting-to-a-database.md) | [🏠 Course Home](../README.md) | [Next: Lesson 15 (Student Records Manager) ➡️](15-crud-app.md)
