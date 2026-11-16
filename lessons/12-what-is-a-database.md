# Lesson 12: What Is a Database? (and Your First SQL)

| | |
|---|---|
| **Part** | 3: Databases |
| **Time** | About 90 minutes |
| **Before this** | [Lesson 11](11-saving-to-files.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Understand **databases**, **tables**, **rows**, and **columns**
- Know the **SQL** commands to create, add, read, change, and delete data
- Add a **package** (an extra toolbox) to a project
- Practice SQL using a **SQL Playground** program

---

## 🧠 The Big Idea

A **database** is a program that stores information in a **very organized** way and lets you find it **very fast**. Think of it as a super-smart digital filing cabinet.

Your school's enrollment system, GCash, Facebook, and online shops all use databases.

### Tables, rows, and columns

A database keeps data in **tables**. A table looks like an Excel sheet:

**Table: `Students`**

| Id | FullName | Course | YearLevel |
|---|---|---|---|
| 1 | Juan Dela Cruz | BSIT | 1 |
| 2 | Maria Santos | BSCS | 2 |
| 3 | Ana Reyes | BSIT | 3 |

| Word | Meaning | In the example |
|---|---|---|
| **Table** | A collection of one kind of thing | `Students` |
| **Column** | One kind of information. Every row has it | `FullName`, `Course` |
| **Row** (or **record**) | One item in the table | Maria Santos's information |
| **Primary key** | A column whose value is **different for every row**. It identifies the row | `Id` |

> [!TIP]
> This should feel familiar! A **table** is like a **class** (`Student`), its **columns** are like **properties**, and each **row** is like an **object**.

### Column types

Each column holds one type of value, just like C# variables:

| Database type | Holds | Like C#'s... |
|---|---|---|
| `INTEGER` | Whole numbers | `int` |
| `REAL` | Decimal numbers | `double` |
| `TEXT` | Text | `string` |

### What is SQL?

**SQL** (say "S-Q-L" or "sequel") stands for **Structured Query Language**. It is the language you use to **talk to a database**. SQL reads almost like English.

### What is SQLite?

Many databases (like MySQL or SQL Server) need a separate **server** program installed and running. **SQLite** is different: the **whole database is one file** on your computer, like `school.db`. Nothing extra to install, and nothing to start.

SQLite is used inside every Android phone, iPhone, and web browser. It is the perfect database for learning.

---

## 🧠 The SQL Commands You Need

Here are the six SQL commands you will use in this course. You do not need to memorize them now. You will practice each one below.

### 1. `CREATE TABLE`: make a new table

```sql
CREATE TABLE Students (Id INTEGER PRIMARY KEY AUTOINCREMENT, FullName TEXT NOT NULL, Course TEXT NOT NULL, YearLevel INTEGER NOT NULL);
```

| Part | Meaning |
|---|---|
| `Students` | The table's name |
| `Id INTEGER PRIMARY KEY AUTOINCREMENT` | A column `Id` that holds whole numbers. It is the **primary key**, and the database **numbers it automatically** (1, 2, 3...) |
| `FullName TEXT NOT NULL` | A text column. `NOT NULL` means it is **required** (it cannot be empty) |

### 2. `INSERT INTO`: add a row

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Juan Dela Cruz', 'BSIT', 1);
```

*"Insert into Students, in these columns, these values."* We do not give an `Id`. The database adds it for us.

> [!IMPORTANT]
> In SQL, text values use **single quotes**: `'Juan Dela Cruz'`. (In C#, text uses double quotes.)

### 3. `SELECT`: read rows

```sql
SELECT * FROM Students;
```

*"Select everything (`*` means all columns) from Students."*

```sql
SELECT FullName, Course FROM Students WHERE Course = 'BSIT';
```

*"Select only the FullName and Course columns, but only the rows **where** the course is BSIT."*

```sql
SELECT * FROM Students ORDER BY FullName;
```

*"...sorted by FullName, from A to Z."*

### 4. `UPDATE`: change rows

```sql
UPDATE Students SET YearLevel = 2 WHERE Id = 1;
```

*"In Students, set YearLevel to 2, but only where Id is 1."*

### 5. `DELETE`: remove rows

```sql
DELETE FROM Students WHERE Id = 3;
```

*"Delete from Students the row where Id is 3."*

> [!CAUTION]
> **Always use `WHERE` with `UPDATE` and `DELETE`!**
> `DELETE FROM Students;` without `WHERE` deletes **every** student. `UPDATE Students SET YearLevel = 2;` without `WHERE` changes **every** student. There is no "undo" button.

### 6. `COUNT(*)`: count rows

```sql
SELECT COUNT(*) FROM Students;
```

*"How many rows are in Students?"*

---

## 🛠️ Let's Build It: The SQL Playground

You will build a small program where you can **type SQL commands and see the results immediately**.

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson12
cd Lesson12
```

### Step 2: Add the SQLite package

C# does not know how to talk to SQLite by itself. You need to add a **package**: an extra toolbox made by someone else (in this case, by Microsoft). Packages are downloaded from **NuGet** (say "new get"), which works like an app store for code.

Type this command. **You need an internet connection.**

```cmd
dotnet add package Microsoft.Data.Sqlite
```

✅ **At the end, you should see a line like this:**

```text
info : PackageReference for package 'Microsoft.Data.Sqlite' version '11.0.0' added to file 'C:\CSharpCourse\Lesson12\Lesson12.csproj'.
```

Your version number may be different. That is fine. The important part is **"added to file"**.

> [!NOTE]
> You only add a package **once per project**. Every new project that uses a database (Lessons 13, 14, 15, 16, 19, 20) needs this same command.

### Step 3: Open VS Code and write the code

```cmd
code .
```

Replace everything in `Program.cs` with the code below.

> [!NOTE]
> **You may copy and paste this one.** You do **not** need to understand this code yet. Just use it as a tool to practice SQL. By the end of Lesson 15, you will understand every line, so come back and read it then!

```csharp
// Lesson 12: SQL PLAYGROUND
// A small tool for practicing SQL commands.
// You do NOT need to understand this code yet.
// By the end of Lesson 15 you will understand every line.

using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data Source=playground.db");
connection.Open();

Console.WriteLine("========== SQL PLAYGROUND ==========");
Console.WriteLine("Type one SQL command and press Enter.");
Console.WriteLine("Type EXIT to quit.");

while (true)
{
    Console.WriteLine();
    Console.Write("SQL> ");
    string sql = (Console.ReadLine() ?? "").Trim();

    if (sql.ToUpper() == "EXIT")
    {
        break;
    }

    if (sql == "")
    {
        continue;
    }

    try
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        using var reader = command.ExecuteReader();

        // Commands like CREATE, INSERT, UPDATE and DELETE do not return a table
        if (reader.FieldCount == 0)
        {
            Console.WriteLine("OK. Command finished.");
            if (reader.RecordsAffected > 0)
            {
                Console.WriteLine($"Rows changed: {reader.RecordsAffected}");
            }
            continue;
        }

        // SELECT returns a table, so print the column names first...
        for (int i = 0; i < reader.FieldCount; i++)
        {
            Console.Write(reader.GetName(i).PadRight(18));
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', 18 * reader.FieldCount));

        // ...then print every row
        int rowCount = 0;
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string value = reader.GetValue(i).ToString() ?? "";
                Console.Write(value.PadRight(18));
            }
            Console.WriteLine();
            rowCount++;
        }
        Console.WriteLine($"({rowCount} row(s) found)");
    }
    catch (SqliteException ex)
    {
        Console.WriteLine($"SQL error: {ex.Message}");
    }
}

Console.WriteLine("Goodbye!");
```

### Step 4: Run it

```cmd
dotnet run
```

✅ **You should see:**

```text
========== SQL PLAYGROUND ==========
Type one SQL command and press Enter.
Type EXIT to quit.

SQL>
```

The program is waiting for a SQL command. A new file called `playground.db` was just created in your project folder. **That file is your database.**

---

## 🏋️ Guided Practice: Type These Commands

Type each command below **exactly**, on **one line**, then press **Enter**. Check that you see the same result.

### 1. Create the table

```sql
CREATE TABLE Students (Id INTEGER PRIMARY KEY AUTOINCREMENT, FullName TEXT NOT NULL, Course TEXT NOT NULL, YearLevel INTEGER NOT NULL);
```

```text
OK. Command finished.
```

### 2. Add three students (one command at a time)

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Juan Dela Cruz', 'BSIT', 1);
```

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Maria Santos', 'BSCS', 2);
```

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Ana Reyes', 'BSIT', 3);
```

After each one, you should see:

```text
OK. Command finished.
Rows changed: 1
```

### 3. See everyone

```sql
SELECT * FROM Students;
```

```text
Id                FullName          Course            YearLevel
------------------------------------------------------------------------
1                 Juan Dela Cruz    BSIT              1
2                 Maria Santos      BSCS              2
3                 Ana Reyes         BSIT              3
(3 row(s) found)
```

Notice the `Id` column: you never typed `1`, `2`, `3`. The database numbered them automatically.

### 4. Only BSIT students, only two columns

```sql
SELECT FullName, Course FROM Students WHERE Course = 'BSIT';
```

```text
FullName          Course
------------------------------------
Juan Dela Cruz    BSIT
Ana Reyes         BSIT
(2 row(s) found)
```

### 5. Move Juan to 2nd year

```sql
UPDATE Students SET YearLevel = 2 WHERE Id = 1;
```

```text
OK. Command finished.
Rows changed: 1
```

### 6. Delete Ana

```sql
DELETE FROM Students WHERE Id = 3;
```

```text
OK. Command finished.
Rows changed: 1
```

### 7. Check the result

```sql
SELECT * FROM Students;
```

```text
Id                FullName          Course            YearLevel
------------------------------------------------------------------------
1                 Juan Dela Cruz    BSIT              2
2                 Maria Santos      BSCS              2
(2 row(s) found)
```

Juan is now in year 2, and Ana is gone.

### 8. Make a mistake on purpose

```sql
SELECT * FROM Student;
```

```text
SQL error: SQLite Error 1: 'no such table: Student'.
```

The table is called `Students` (with an **s**). Database errors tell you what went wrong, just like C# errors.

### 9. Exit, then come back

Type `EXIT` to quit. Then run `dotnet run` again and type:

```sql
SELECT * FROM Students;
```

**Juan and Maria are still there!** The database saved them in `playground.db`. 🎉

> [!NOTE]
> If you type the `CREATE TABLE` command again, you will see `SQL error: SQLite Error 1: 'table Students already exists'.` That is **good news**. It means the table from last time was saved. Just continue with the next command.

---

## 🏋️ Practice

Use the SQL Playground for these. Write your own SQL commands.

**Exercise 1.** Add two more students of your choice.

<details>
<summary>Show answer</summary>

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Mark Villanueva', 'BSIS', 1);
```

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Liza Bautista', 'BSIT', 4);
```

</details>

**Exercise 2.** Show all students sorted by name from A to Z.

<details>
<summary>Show answer</summary>

```sql
SELECT * FROM Students ORDER BY FullName;
```

</details>

**Exercise 3.** Count how many students are in BSIT.

<details>
<summary>Show answer</summary>

```sql
SELECT COUNT(*) FROM Students WHERE Course = 'BSIT';
```

</details>

**Exercise 4.** Change Maria Santos's course to `BSIT`. Use her `Id` in the `WHERE`.

<details>
<summary>Show answer</summary>

```sql
UPDATE Students SET Course = 'BSIT' WHERE Id = 2;
```

</details>

**Exercise 5.** What would `DELETE FROM Students;` do? (**Do not run it!** Just answer.)

<details>
<summary>Show answer</summary>

It deletes **every** row in the `Students` table, because there is no `WHERE`. Always double-check `UPDATE` and `DELETE` commands before pressing Enter.

</details>

---

## 📌 Recap

- A **database** stores data in **tables** made of **columns** (kinds of information) and **rows** (records).
- The **primary key** (`Id`) identifies each row. `AUTOINCREMENT` numbers it for you.
- **SQL** commands: `CREATE TABLE`, `INSERT INTO`, `SELECT`, `UPDATE`, `DELETE`, `COUNT(*)`.
- `WHERE` chooses **which** rows. Always use it with `UPDATE` and `DELETE`.
- In SQL, text goes in **single quotes**: `'BSIT'`.
- **SQLite** keeps the whole database in **one file**.
- `dotnet add package Microsoft.Data.Sqlite` adds SQLite support to a project.

---

[⬅️ Lesson 11](11-saving-to-files.md) | [🏠 Course Home](../README.md) | [Next: Lesson 13 (Connecting C# to a Database) ➡️](13-connecting-to-a-database.md)
