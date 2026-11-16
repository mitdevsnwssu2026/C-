# Lesson 13: Connecting C# to a Database

| | |
|---|---|
| **Part** | 3: Databases |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 12](12-what-is-a-database.md) |

## 🎯 Goal

By the end of this lesson, you will:

- **Connect** your own C# program to a SQLite database
- Create a table from C# code
- Run SQL commands from C# and get the results
- Know the three ways to run SQL: `ExecuteNonQuery`, `ExecuteScalar`, `ExecuteReader`

---

## 🧠 The Big Idea

In Lesson 12, you typed SQL by hand. Real programs send SQL **automatically**, from their own code.

Talking to a database from C# always follows the **same four steps**. Think of making a phone call:

| Step | Phone call | C# |
|---|---|---|
| 1 | Know the phone number | Write the **connection string**: where the database is |
| 2 | Call and wait for "Hello?" | **Open** the connection |
| 3 | Say what you need | Create a **command** with your SQL and **run** it |
| 4 | Hang up | **Close** the connection (C# does this for you with `using`) |

---

## 🛠️ Let's Build It

### Step 1: Create the project and add SQLite

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson13
cd Lesson13
dotnet add package Microsoft.Data.Sqlite
code .
```

### Step 2: Write the code

Replace everything in `Program.cs` with:

```csharp
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
```

### Step 3: Run it

```cmd
dotnet run
```

✅ **You should see:**

```text
Connected to school.db!
The Students table is ready.
Students saved in the database: 0
```

Run it **a second time**. You should see the **same** result, with no errors. (You will see why below.)

### Step 4: Find your database file

```cmd
dir
```

You will see a new file: **`school.db`**. That is your database! It was created automatically the first time your program connected.

---

## 🔎 Line by Line

### Using the package

```cs
using Microsoft.Data.Sqlite;
```

This line goes at the **very top**. It says: *"In this file, I want to use the tools from the SQLite package."* Without it, C# does not know what `SqliteConnection` is.

### Step 1: The connection string

```cs
string connectionString = "Data Source=school.db";
```

A **connection string** tells C# **where** the database is. `Data Source=school.db` means *"the file `school.db`, in the project folder."* If the file does not exist yet, SQLite creates it.

### Step 2: Open the connection

```cs
using var connection = new SqliteConnection(connectionString);
connection.Open();
```

- `new SqliteConnection(...)` prepares a connection to the database.
- `connection.Open()` actually connects.
- `using var` means: *"When the program is finished with this, close it automatically."* It is like a door that closes by itself after you walk through. You will use `using var` for every connection and command.

### Step 3: Create and run a command

```cs
using var createCommand = connection.CreateCommand();
createCommand.CommandText = @"
    CREATE TABLE IF NOT EXISTS Students (
        ...
    );";
createCommand.ExecuteNonQuery();
```

- `connection.CreateCommand()` makes a new, empty **command**.
- `.CommandText` is the SQL you want to run.
- `.ExecuteNonQuery()` runs it.

**What is `@"..."`?** Normal text in C# must be on **one line**. Putting `@` before the quote lets the text continue over **many lines**, which makes long SQL much easier to read.

### The table design

```sql
CREATE TABLE IF NOT EXISTS Students (
    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentNumber TEXT    NOT NULL UNIQUE,
    FullName      TEXT    NOT NULL,
    Course        TEXT    NOT NULL,
    YearLevel     INTEGER NOT NULL
);
```

| Part | Meaning |
|---|---|
| `IF NOT EXISTS` | Only create the table if it is **not there yet**. This is why running the program twice causes no error |
| `NOT NULL` | This column is **required** |
| `UNIQUE` | **No two rows** can have the same value. Two students can never share a student number |

> [!TIP]
> `NOT NULL` and `UNIQUE` are **rules** that the database enforces for you. Remember the problems with files in Lesson 11? The database now protects your data from blank values and duplicates.

### Asking for one value

```cs
countCommand.CommandText = "SELECT COUNT(*) FROM Students;";
int count = Convert.ToInt32(countCommand.ExecuteScalar());
```

- `.ExecuteScalar()` runs SQL that gives back **one single value** (here, a count).
- `Convert.ToInt32(...)` turns that value into a C# `int`.

---

## 🧠 The Three Ways to Run SQL

| Method | Use it for | What it gives back | Example SQL |
|---|---|---|---|
| `ExecuteNonQuery()` | Changing things | The **number of rows changed** | `CREATE`, `INSERT`, `UPDATE`, `DELETE` |
| `ExecuteScalar()` | Asking for **one** value | That one value | `SELECT COUNT(*) ...` |
| `ExecuteReader()` | Reading **many** rows | A **reader** that goes through the rows one at a time | `SELECT * FROM ...` |

You used the first two in this lesson. You will use `ExecuteReader()` in Lesson 15.

---

## 🔍 Optional: See Inside Your Database

Want to **see** your tables like an Excel sheet? You can install a free tool called **DB Browser for SQLite**.

1. Go to **https://sqlitebrowser.org/dl/** and download the Windows installer.
2. Install it (click **Next** on every page).
3. Open **DB Browser for SQLite**, click **Open Database**, and choose `C:\CSharpCourse\Lesson13\school.db`.
4. Click the **Browse Data** tab to see the rows of a table.

> [!WARNING]
> Click **Close Database** in DB Browser **before** you run your C# program again. If both programs use the database at the same time, you may see the error `database is locked`.

This tool is **optional**. Your C# programs will always show you the data, too.

---

## ⚠️ Common Mistakes

### Mistake 1: Forgetting to add the package

```text
error CS0234: The type or namespace name 'Data' does not exist in the namespace 'Microsoft' (are you missing an assembly reference?)
```

**Fix:** In the Command Prompt, inside your project folder, run:

```cmd
dotnet add package Microsoft.Data.Sqlite
```

### Mistake 2: Forgetting `using Microsoft.Data.Sqlite;`

```text
error CS0246: The type or namespace name 'SqliteConnection' could not be found
```

**Fix:** Add `using Microsoft.Data.Sqlite;` as the **first line** of the file.

### Mistake 3: Forgetting `connection.Open()`

```text
Unhandled exception. System.InvalidOperationException: ExecuteNonQuery can only be called when the connection is open.
```

**Fix:** Call `connection.Open();` right after creating the connection.

### Mistake 4: A typo in the SQL

C# cannot check your SQL. Mistakes in SQL only show up **when the program runs**:

```text
Unhandled exception. Microsoft.Data.Sqlite.SqliteException (0x80004005): SQLite Error 1: 'near "TABEL": syntax error'.
```

**Fix:** Read the message. `near "TABEL"` tells you exactly which word is wrong (it should be `TABLE`).

---

## 🏋️ Practice

**Exercise 1.** Add code to create a second table called `Courses` with these columns: `Code` (TEXT, primary key) and `Title` (TEXT, required). Print `The Courses table is ready.`

<details>
<summary>Show answer</summary>

Add this at the end of `Program.cs`:

```cs
using var coursesCommand = connection.CreateCommand();
coursesCommand.CommandText = @"
    CREATE TABLE IF NOT EXISTS Courses (
        Code  TEXT PRIMARY KEY,
        Title TEXT NOT NULL
    );";
coursesCommand.ExecuteNonQuery();
Console.WriteLine("The Courses table is ready.");
```

</details>

**Exercise 2.** Insert one course into the `Courses` table using `ExecuteNonQuery()`, and print how many rows were changed. Use `INSERT OR IGNORE` so that running the program twice does not cause an error.

<details>
<summary>Show answer</summary>

```cs
using var insertCommand = connection.CreateCommand();
insertCommand.CommandText = @"
    INSERT OR IGNORE INTO Courses (Code, Title)
    VALUES ('BSIT', 'Bachelor of Science in Information Technology');";
int rowsChanged = insertCommand.ExecuteNonQuery();
Console.WriteLine($"Rows changed: {rowsChanged}");
```

The first run shows `Rows changed: 1`. The second run shows `Rows changed: 0`, because `BSIT` already exists. `OR IGNORE` means *"if this would break a rule (like a duplicate primary key), skip it quietly."*

</details>

**Exercise 3.** After counting the students, show `The database is empty.` if the count is 0, or `The database has students.` otherwise.

<details>
<summary>Show answer</summary>

```cs
if (count == 0)
{
    Console.WriteLine("The database is empty.");
}
else
{
    Console.WriteLine("The database has students.");
}
```

</details>

---

## 📌 Recap

- `using Microsoft.Data.Sqlite;` at the top lets you use SQLite.
- The four steps: **connection string** → **open** → **command** → **run** (and `using var` closes things for you).
- `@"..."` lets text continue over many lines, which is great for SQL.
- `CREATE TABLE IF NOT EXISTS` is safe to run many times.
- `NOT NULL` = required. `UNIQUE` = no duplicates.
- `ExecuteNonQuery()` for changes, `ExecuteScalar()` for one value, `ExecuteReader()` for many rows.

---

[⬅️ Lesson 12](12-what-is-a-database.md) | [🏠 Course Home](../README.md) | [Next: Lesson 14 (Inserting Data) ➡️](14-inserting-data.md)
