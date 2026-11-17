# Lesson 18: What Is a Database? (and Your First SQL)

**Part 3: Databases** · About 90 minutes

## What You Will Learn

- What **databases**, **tables**, **rows**, and **columns** are
- The **SQL** commands to create, add, read, change, and delete data
- How to add a **package** (an extra toolbox) to your project
- How to practice SQL in a **SQL Playground** program

## The Idea

A **database** is a program that stores information in a **very organized** way, and finds it **very fast**. Think of a super-smart digital filing cabinet.

Your school's enrollment system, GCash, Facebook, and online shops all use databases.

A database keeps its data in **tables**. A table looks like an Excel sheet:

**Table: `Students`**

| Id | FullName | Course | YearLevel |
|---|---|---|---|
| 1 | Juan Dela Cruz | BSIT | 1 |
| 2 | Maria Santos | BSCS | 2 |
| 3 | Ana Reyes | BSIT | 3 |

- A **table** holds one kind of thing. (`Students`)
- A **column** is one kind of information. (`FullName`, `Course`)
- A **row** is one item in the table. (Maria's information)
- The **primary key** is a column that is **different for every row**. It identifies the row. (`Id`)

> This should feel familiar. A **table** is like a **class**. Its **columns** are like **properties**. Each **row** is like an **object**.

**SQL** (say "S-Q-L") is the **language for talking to a database**. It reads almost like English.

**SQLite** is the database we will use. Many databases need a big, separate program to be installed. SQLite does not: the **whole database is one file** on your computer. It is used inside every Android phone and iPhone, and it is perfect for learning.

---

## The 6 SQL Commands

You do not need to memorize these now. You will practice each one in a few minutes.

**1. `CREATE TABLE`** makes a new table.

```sql
CREATE TABLE Students (Id INTEGER PRIMARY KEY AUTOINCREMENT, FullName TEXT NOT NULL, Course TEXT NOT NULL, YearLevel INTEGER NOT NULL);
```

- `INTEGER` means whole numbers. `TEXT` means text.
- `PRIMARY KEY AUTOINCREMENT` means: this is the primary key, and the database **numbers it by itself** (1, 2, 3...).
- `NOT NULL` means: this column is **required**. It cannot be left empty.

**2. `INSERT INTO`** adds a row.

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Juan Dela Cruz', 'BSIT', 1);
```

> In SQL, text uses **single quotes**: `'Juan Dela Cruz'`. (In C#, text uses double quotes.)

**3. `SELECT`** reads rows.

```sql
SELECT * FROM Students;
```

The `*` means "all columns". You can also choose columns, choose rows with `WHERE`, and sort with `ORDER BY`:

```sql
SELECT FullName, Course FROM Students WHERE Course = 'BSIT';
```

```sql
SELECT * FROM Students ORDER BY FullName;
```

**4. `UPDATE`** changes rows.

```sql
UPDATE Students SET YearLevel = 2 WHERE Id = 1;
```

**5. `DELETE`** removes rows.

```sql
DELETE FROM Students WHERE Id = 3;
```

**6. `COUNT(*)`** counts rows.

```sql
SELECT COUNT(*) FROM Students;
```

> [!CAUTION]
> **Always use `WHERE` with `UPDATE` and `DELETE`.**
> `DELETE FROM Students;` without `WHERE` deletes **every** student. There is no undo button.

---

## Step by Step

You will build a small program where you can **type SQL commands and see the results right away**.

### Step 1: Create a new project named `Lesson18`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson18`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

C# cannot talk to SQLite by itself. You need to add a **package**: an extra toolbox made by someone else (here, by Microsoft). Packages come from a place called **NuGet** (say "new get"). It is like an app store for code.

**You need an internet connection for this step.**

**1.** In the **Solution Explorer**, **right-click** the project line **Lesson18**.

**2.** Click **Manage NuGet Packages...**

✅ **You will see:** a new tab opens, named **NuGet: Lesson18**.

**3.** Near the top-left of that tab, click the word **Browse**.

**4.** Click the search box and type: `Microsoft.Data.Sqlite`

**5.** In the results, click the one named exactly **Microsoft.Data.Sqlite**.

> Several results have longer names, like `Microsoft.Data.Sqlite.Core`. Pick the one with **nothing after** `Sqlite`.

**6.** On the right side, click the **Install** button.

**7.** Small windows may appear:

- If a window named **Preview Changes** appears, click **Apply**.
- If a window named **License Acceptance** appears, click **I Accept**.

✅ **You will see:** after a few seconds, the **Install** button changes to **Uninstall**. That means the package is installed.

**8.** Close the NuGet tab: click the small **X** on the tab named **NuGet: Lesson18**. You are back at `Program.cs`.

> [!NOTE]
> You add the package **once per project**. Every project that uses a database (Lessons 18 to 22, 25, and 26) needs these same steps.

### Step 3: Paste the code

This program is a **tool** for practicing SQL. You do **not** need to understand its code yet. By the end of Lesson 21, you will understand every line.

**This time, you may copy and paste.**

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Copy the code below, and paste it into the editor (**Ctrl + V**).

## The Code

```csharp
// Lesson 18: SQL PLAYGROUND
// A small tool for practicing SQL commands.
// You do NOT need to understand this code yet.
// By the end of Lesson 21 you will understand every line.

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

Press **Ctrl + F5**.

✅ **You will see:**

```text
========== SQL PLAYGROUND ==========
Type one SQL command and press Enter.
Type EXIT to quit.

SQL>
```

The program is waiting for a SQL command.

### Step 5: Type these SQL commands

Type each command **on one line**, exactly as shown, and press **Enter**. Check that you see the same result.

> [!TIP]
> To save typing, you can copy a command from this page and paste it into the black window: click the black window, then **right-click** (or press **Ctrl + V**).

**1. Create the table.**

```sql
CREATE TABLE Students (Id INTEGER PRIMARY KEY AUTOINCREMENT, FullName TEXT NOT NULL, Course TEXT NOT NULL, YearLevel INTEGER NOT NULL);
```

```text
OK. Command finished.
```

**2. Add three students** (one command at a time).

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Juan Dela Cruz', 'BSIT', 1);
```

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Maria Santos', 'BSCS', 2);
```

```sql
INSERT INTO Students (FullName, Course, YearLevel) VALUES ('Ana Reyes', 'BSIT', 3);
```

After each one:

```text
OK. Command finished.
Rows changed: 1
```

**3. See everyone.**

```sql
SELECT * FROM Students;
```

## The Output

```text
Id                FullName          Course            YearLevel
------------------------------------------------------------------------
1                 Juan Dela Cruz    BSIT              1
2                 Maria Santos      BSCS              2
3                 Ana Reyes         BSIT              3
(3 row(s) found)
```

Look at the `Id` column. You never typed `1`, `2`, `3`. The database numbered them by itself.

**4. Show only the BSIT students, and only two columns.**

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

**5. Move Juan to 2nd year.**

```sql
UPDATE Students SET YearLevel = 2 WHERE Id = 1;
```

```text
OK. Command finished.
Rows changed: 1
```

**6. Delete Ana.**

```sql
DELETE FROM Students WHERE Id = 3;
```

```text
OK. Command finished.
Rows changed: 1
```

**7. Check the result.**

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

**8. Make a mistake on purpose.**

```sql
SELECT * FROM Student;
```

```text
SQL error: SQLite Error 1: 'no such table: Student'.
```

The table is named `Students`, with an **s**. A database tells you what is wrong, just like Visual Studio does.

**9. Quit, and come back.**

Type `EXIT`. Close the black window. Then press **Ctrl + F5** again and type:

```sql
SELECT * FROM Students;
```

**Juan and Maria are still there!** The database saved them in a file named `playground.db`. 🎉

---

## If Something Goes Wrong

**Error List: `CS0234  The type or namespace name 'Data' does not exist in the namespace 'Microsoft'`**
The SQLite package is not installed in this project. Do Step 2 again.

**`SQL error: SQLite Error 1: 'table Students already exists'.`**
This is good news! It means the table from last time was saved. Skip the `CREATE TABLE` command and continue with the next one.

**`SQL error: ... syntax error`**
There is a typing mistake in your SQL. Check the spelling, the commas, and the single quotes `' '`. Then type the command again.

**`SQL error: ... 'no such table: ...'`**
The table name is spelled wrong, or you have not created the table yet.

---

## Exercise

Use the SQL Playground. Write the SQL commands yourself.

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

**Exercise 2.** Show all students, sorted by name from A to Z.

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

It deletes **every** row in the `Students` table, because there is no `WHERE`. Always check your `UPDATE` and `DELETE` commands twice before pressing Enter.

</details>

## ⭐ Bonus Challenge

Make a **second table** by yourself. Create a table named `Subjects` with three columns:

- `Code` → text, and it is the primary key
- `Title` → text, required
- `Units` → whole number, required

Then add two subjects, and show only the subjects with **3 or more** units.

<details>
<summary>Show answer</summary>

```sql
CREATE TABLE Subjects (Code TEXT PRIMARY KEY, Title TEXT NOT NULL, Units INTEGER NOT NULL);
```

```sql
INSERT INTO Subjects (Code, Title, Units) VALUES ('IT101', 'Computing Basics', 3);
```

```sql
INSERT INTO Subjects (Code, Title, Units) VALUES ('PE1', 'Fitness', 2);
```

```sql
SELECT * FROM Subjects WHERE Units >= 3;
```

Only `IT101` is shown. Here the primary key is the `Code`, which you type yourself, so there is no `AUTOINCREMENT`.

</details>

---

## Remember

- A **database** keeps data in **tables**, made of **columns** and **rows**.
- The **primary key** (`Id`) identifies each row.
- **SQL**: `CREATE TABLE`, `INSERT INTO`, `SELECT`, `UPDATE`, `DELETE`, `COUNT(*)`
- `WHERE` chooses **which** rows. Always use it with `UPDATE` and `DELETE`.
- In SQL, text goes in **single quotes**: `'BSIT'`
- To add SQLite: right-click the project → **Manage NuGet Packages...** → **Browse** → `Microsoft.Data.Sqlite` → **Install**.

---

[⬅️ Lesson 17](17-saving-to-files.md) | [🏠 Course Home](../README.md) | [Next: Lesson 19 (Connecting C# to a Database) ➡️](19-connecting-to-a-database.md)
