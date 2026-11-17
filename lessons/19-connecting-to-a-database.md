# Lesson 19: Connecting C# to a Database

**Part 3: Databases** · About 60 minutes

## What You Will Learn

- How to **connect** your own C# program to a database
- How to create a table from C# code
- How to run SQL from C# and get the result
- The three ways to run SQL from C#

## The Idea

In Lesson 18, you typed SQL by hand. Real programs send SQL **by themselves**, from their code.

Talking to a database always has the **same four steps**. It is like a phone call:

| Step | Phone call | In C# |
|---|---|---|
| 1 | Know the phone number | Write **where the database is** (the *connection string*) |
| 2 | Call, and wait for "Hello?" | **Open** the connection |
| 3 | Say what you need | Make a **command** with your SQL, and **run** it |
| 4 | Hang up | **Close** the connection (C# does this for you) |

---

## Step by Step

### Step 1: Create a new project named `Lesson19`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson19`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

1. In the **Solution Explorer**, right-click the project line **Lesson19**.
2. Click **Manage NuGet Packages...**
3. Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.
4. Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**.
5. If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.
6. Close the NuGet tab (click the **X** on the tab **NuGet: Lesson19**).

> Need more help with these clicks? Look at [Lesson 18](18-what-is-a-database.md), Step 2.

### Step 3: Type the code

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Type the code below.

## The Code

```csharp
// Lesson 19: Connecting C# to a SQLite database

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

### Step 4: Run it

Press **Ctrl + F5**.

## The Output

✅ **You will see:**

```text
Connected to school.db!
The Students table is ready.
Students saved in the database: 0
```

Close the black window. **Run it a second time.** You will see the same result, with no errors.

Your program just created a real database! It is a file named `school.db`.

> [!NOTE]
> **Where is `school.db`?** In the same place as the CSV file from Lesson 17: **your project → bin → Debug → net10.0**. (Right-click the project → **Open Folder in File Explorer** to go there.) You do not need to open it. Your programs will show you what is inside.

---

## What the Code Means

**Using the package:**

```cs
using Microsoft.Data.Sqlite;
```

This line goes at the **very top**. It means: *"In this file, I want to use the SQLite toolbox."*

**Step 1 of the phone call: where is the database?**

```cs
string connectionString = "Data Source=school.db";
```

A **connection string** says where the database is. Here: *"the file named `school.db`."* If the file does not exist yet, SQLite creates it.

**Step 2: open the connection.**

```cs
using var connection = new SqliteConnection(connectionString);
connection.Open();
```

- `new SqliteConnection(...)` gets a connection ready.
- `connection.Open()` connects.
- `using var` means: *"When the program is done with this, close it automatically."* It is like a door that closes by itself behind you. You will write `using var` for every connection and every command.

**Step 3: make a command and run it.**

```cs
using var createCommand = connection.CreateCommand();
createCommand.CommandText = @"
    CREATE TABLE IF NOT EXISTS Students (
        ...
    );";
createCommand.ExecuteNonQuery();
```

- `connection.CreateCommand()` makes a new, empty command.
- `.CommandText` is the SQL you want to run.
- `.ExecuteNonQuery()` runs it.

**What is `@"..."`?** Normal text in C# must fit on **one line**. Putting `@` before the first quote lets the text go on for **many lines**. That makes long SQL much easier to read.

**The table:**

```sql
CREATE TABLE IF NOT EXISTS Students (
    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentNumber TEXT    NOT NULL UNIQUE,
    FullName      TEXT    NOT NULL,
    Course        TEXT    NOT NULL,
    YearLevel     INTEGER NOT NULL
);
```

- `IF NOT EXISTS` → create the table only if it is **not there yet**. That is why running the program twice causes no error.
- `NOT NULL` → the column is **required**.
- `UNIQUE` → **no two rows** can have the same value. Two students can never have the same student number.

> `NOT NULL` and `UNIQUE` are **rules** that the database enforces for you. Remember the problems with files in Lesson 17? The database now protects your data from empty values and duplicates.

**Asking for one value:**

```cs
countCommand.CommandText = "SELECT COUNT(*) FROM Students;";
int count = Convert.ToInt32(countCommand.ExecuteScalar());
```

- `.ExecuteScalar()` runs SQL that gives back **one single value** (here, a count).
- `Convert.ToInt32(...)` turns that value into a C# `int`.

### The three ways to run SQL

| Method | Use it for | It gives back |
|---|---|---|
| `ExecuteNonQuery()` | Changing things: `CREATE`, `INSERT`, `UPDATE`, `DELETE` | How many rows were changed |
| `ExecuteScalar()` | Asking for **one** value: `SELECT COUNT(*)` | That one value |
| `ExecuteReader()` | Reading **many** rows: `SELECT * FROM ...` | A reader that goes through the rows |

You used the first two today. You will use `ExecuteReader()` in Lesson 21.

---

## Optional: Look Inside Your Database

Do you want to **see** your tables like an Excel sheet? There is a free tool called **DB Browser for SQLite**. This part is optional.

1. Go to **https://sqlitebrowser.org/dl/** and download the installer for Windows.
2. Install it (click **Next** on every page).
3. Open **DB Browser for SQLite**, and click **Open Database**.
4. Go to `C:\CSharpCourse\Lesson19\bin\Debug\net10.0` and choose `school.db`.
5. Click the **Browse Data** tab to see the rows of a table.

> [!WARNING]
> In DB Browser, click **Close Database** before you run your C# program again. If both use the database at the same time, you may see the error `database is locked`.

---

## If Something Goes Wrong

**Error List: `CS0234  The type or namespace name 'Data' does not exist in the namespace 'Microsoft'`**
The SQLite package is not installed in **this** project. Do Step 2 again.

**Error List: `CS0246  The type or namespace name 'SqliteConnection' could not be found`**
The first line is missing. Add `using Microsoft.Data.Sqlite;` at the very top of the file.

**The black window shows: `ExecuteNonQuery can only be called when the connection is open.`**
You forgot `connection.Open();`. Add it right after the line that makes the connection.

**The black window shows: `SQLite Error 1: 'near "TABEL": syntax error'.`**
There is a typing mistake **inside your SQL**. Visual Studio cannot check SQL for you, so these mistakes only show up when the program runs. The word after `near` tells you where the mistake is. (Here, `TABEL` should be `TABLE`.)

---

## Exercise

**Exercise 1.** After counting the students, show `The database is empty.` if the count is 0. Otherwise, show `The database has students.`

<details>
<summary>Show answer</summary>

Add this at the end of `Program.cs`:

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

**Exercise 2.** Add code that creates a **second table** named `Courses`, with two columns: `Code` (text, primary key) and `Title` (text, required). Show `The Courses table is ready.`

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

## ⭐ Bonus Challenge

Add **one course** to your new `Courses` table using `ExecuteNonQuery()`, and show how many rows were changed.

There is a catch: if you run the program twice, the second run will try to add the same course again, and crash. Use `INSERT OR IGNORE` instead of `INSERT`. It means *"if this row breaks a rule, like a duplicate primary key, skip it quietly."*

Run the program twice. What number do you see each time?

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

The first run shows `Rows changed: 1`. The second run shows `Rows changed: 0`, because `BSIT` is already there and the new row was skipped.

</details>

---

## Remember

- `using Microsoft.Data.Sqlite;` at the top lets you use SQLite.
- The four steps: **where** (connection string) → **open** → **command** → **run**. `using var` closes things for you.
- `@"..."` lets text go on for many lines. Great for SQL.
- `CREATE TABLE IF NOT EXISTS` is safe to run many times.
- `NOT NULL` = required. `UNIQUE` = no duplicates.
- `ExecuteNonQuery()` for changes. `ExecuteScalar()` for one value. `ExecuteReader()` for many rows.

---

[⬅️ Lesson 18](18-what-is-a-database.md) | [🏠 Course Home](../README.md) | [Next: Lesson 20 (Inserting Data Safely) ➡️](20-inserting-data.md)
