# Lesson 14: Saving User Input to the Database (Safely!)

**Part 3: Databases** · About 90 minutes

## What You Will Learn

- How to save what the user types into the database
- What a **SQL injection** attack is, and how **parameters** stop it
- How to check for duplicates **before** saving
- How to get the new row's `Id` back from the database

## The Idea: Never Glue User Input Into SQL

You might think you can build SQL by joining pieces of text, like this:

```cs
// ❌ DANGEROUS! NEVER DO THIS!
command.CommandText = "INSERT INTO Students (FullName) VALUES ('" + fullName + "');";
```

When the user types `Juan Dela Cruz`, the SQL becomes:

```sql
INSERT INTO Students (FullName) VALUES ('Juan Dela Cruz');
```

That looks fine. But what if a hacker types this as their "name"?

```text
x'); DROP TABLE Students;--
```

Now the SQL becomes:

```sql
INSERT INTO Students (FullName) VALUES ('x'); DROP TABLE Students;--');
```

The database sees **two** commands: add `x`, and then **delete the whole Students table**! 😱

This attack is called **SQL injection**. The hacker *injects* their own SQL into yours. It is one of the most common ways that real websites get hacked.

### The fix: parameters

A **parameter** is a **placeholder** in your SQL. You fill it in **separately**:

```cs
// ✅ SAFE: always do it this way
command.CommandText = "INSERT INTO Students (FullName) VALUES ($fullName);";
command.Parameters.AddWithValue("$fullName", fullName);
```

- `$fullName` inside the SQL is a placeholder. It means *"a value goes here."*
- `command.Parameters.AddWithValue("$fullName", fullName)` means *"fill the placeholder `$fullName` with what is in the C# variable `fullName`."*

With parameters, the database treats what the user typed as **plain data, never as commands**. If a hacker types `x'); DROP TABLE Students;--`, it is simply saved as a strange-looking name. Nothing bad happens.

Think of a bank deposit slip. You write the amount **in the box** for the amount. Even if you wrote instructions for the teller in that box, the teller would just read it as a (strange) amount.

> [!CAUTION]
> **The number 1 database rule of this course:**
> When a value comes from the user, put it in the SQL **as a parameter**. Never join it into the SQL text with `+` or `$"..."`.

---

## Step by Step

### Step 1: Create a new project named `Lesson14`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson14`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

1. In the **Solution Explorer**, right-click the project line **Lesson14**.
2. Click **Manage NuGet Packages...**
3. Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.
4. Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**.
5. If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.
6. Close the NuGet tab (click the **X** on the tab **NuGet: Lesson14**).

### Step 3: Put in the code

This program is longer. From this lesson on, **you may copy and paste** the code. But **read every line**, and use the explanations below to understand it.

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Copy the code below and paste it (**Ctrl + V**).

The code has three parts: the **main program** at the top, then the **database methods**, then the **input methods**. Read the main program first. It reads like a story.

## The Code

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

### Step 4: Run it three times

**Run 1: add a student.** Press **Ctrl + F5**. On purpose, leave the course empty once, and type a wrong year level, to see the checks work.

## The Output

✅ **You will see:**

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

**Run 2: try the same student number again.**

```text
=== ADD A NEW STUDENT ===
Student number (example: 2026-0001): 2026-0001
Student number 2026-0001 is already saved. Nothing was added.
Total students in the database: 1
```

**Run 3: try the hacker's trick.** For the full name, type: `x'); DROP TABLE Students;--`

```text
=== ADD A NEW STUDENT ===
Student number (example: 2026-0001): 2026-0002
Full name: x'); DROP TABLE Students;--
Course (example: BSIT): BSCS
Year level (1-5): 2
Saved! x'); DROP TABLE Students;-- was given database ID 2.
Total students in the database: 2
```

The table was **not** deleted. The total is 2. The "attack" was saved as a plain, silly name. **Parameters protected your database.** 🛡️

---

## What the Code Means

### The main program

```cs
const string ConnectionString = "Data Source=school.db";
```

`const` means **constant**: a value that **never changes**. Every method uses the same connection string, so it is written once, at the top.

```cs
if (StudentNumberExists(studentNumber))
{
    ...already saved...
}
else
{
    ...ask for the rest, and save...
}
```

The program checks for a duplicate **right after** the student number is typed. That way, the user does not waste time typing everything else.

### `StudentNumberExists`: check before saving

```cs
command.CommandText = "SELECT COUNT(*) FROM Students WHERE StudentNumber = $studentNumber;";
command.Parameters.AddWithValue("$studentNumber", studentNumber);

int count = Convert.ToInt32(command.ExecuteScalar());
return count > 0;
```

Count how many students already have this number. If the count is more than 0, the number exists. Notice the **parameter**, even here, because `studentNumber` came from the user.

> The table also has the `UNIQUE` rule, so the database would refuse a duplicate anyway. But a friendly message is much better than a crash. **Check first**, and let `UNIQUE` be your safety net.

### `InsertStudent`: save, and get the new Id

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

`ExecuteScalar` gives back the one value from the `SELECT`: the new `Id`. An `Id` can become a very big number, so we use **`long`**. A `long` is like an `int`, but for bigger whole numbers.

### Every database method has the same shape

```cs
using var connection = new SqliteConnection(ConnectionString);
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = "...";
```

Open a connection, do one job, and let `using` close it. Simple and safe.

### `ReadRequired` and `ReadYearLevel`

These are the **crash-proof input** methods from Lessons 08 and 10. `ReadRequired` keeps asking until the user types something. `ReadYearLevel` keeps asking until the user types a whole number from 1 to 5:

```cs
if (int.TryParse(input, out int yearLevel) && yearLevel >= 1 && yearLevel <= 5)
```

*"If the input is a whole number, **and** it is at least 1, **and** it is at most 5..."*

---

## If Something Goes Wrong

**The black window shows: `Must add values for the following parameters: $fullName`**
A placeholder was not filled in. The name in the SQL and the name in `AddWithValue` must be **exactly** the same. `$fullName` and `$fullname` are different!

**The database saved the text `$fullName` instead of the real name.**
You put quotes around the placeholder: `VALUES ('$fullName')`. Remove the quotes: `VALUES ($fullName)`. Placeholders never need quotes.

**The black window shows: `SQLite Error 19: 'UNIQUE constraint failed: Students.StudentNumber'.`**
The program tried to save a student number that is already saved. Check first, with a method like `StudentNumberExists`.

**I changed the `CREATE TABLE` in my code, but the table did not change.**
`CREATE TABLE IF NOT EXISTS` does **nothing** when the table already exists. While you are practicing, you can delete the old database file. The program will make a fresh one:

1. Right-click the project → **Open Folder in File Explorer**.
2. Open **bin** → **Debug** → **net10.0**.
3. Right-click `school.db` and click **Delete**.

⚠️ This deletes **all** the data in that database. Only do it with practice data.

---

## Exercise

**Exercise 1.** Change every course to capital letters before saving, so `bsit`, `Bsit`, and `BSIT` are all saved as `BSIT`. (Hint: `.ToUpper()`)

<details>
<summary>Show answer</summary>

Change the course line in the main program:

```cs
string course = ReadRequired("Course (example: BSIT): ").ToUpper();
```

</details>

**Exercise 2.** Explain to a classmate, in your own words: why do we use `$fullName` and `AddWithValue`, instead of joining the name into the SQL with `+`?

<details>
<summary>Show answer</summary>

If we join what the user typed into the SQL with `+`, a user can type SQL code as their "answer", and the database will **run** it. That attack is called SQL injection. With parameters, the database always treats the answer as **data only**, never as commands. So the attack cannot work.

</details>

## ⭐ Bonus Challenge

Right now, the program adds one student and stops. Make it ask `Add another student? (Y/N):` at the end. If the user types `Y`, it asks for another student. If not, it shows the total and ends.

Hint: put the main program inside a `while` loop, with a `bool addMore = true;` before it.

<details>
<summary>Show answer</summary>

Replace the main program (everything from `CreateStudentsTable();` down to the "Total students" line) with this:

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

`addMore = answer == "Y";` puts `true` in the box if the answer is `Y`, and `false` if it is anything else.

</details>

---

## Remember

- **SQL injection**: a hacker types SQL into an input box to attack the database.
- **Parameters** stop it: write `$name` in the SQL, then `command.Parameters.AddWithValue("$name", value);`
- **Always** use parameters for values that come from the user.
- Check for duplicates with `SELECT COUNT(*) ... WHERE ...` **before** inserting.
- `SELECT last_insert_rowid();` gives back the `Id` of the row you just added.
- `const` is a value that never changes. `long` is a bigger `int`.

---

[⬅️ Lesson 13](13-connecting-to-a-database.md) | [🏠 Course Home](../README.md) | [Next: Lesson 15 (Student Records Manager) ➡️](15-crud-app.md)
