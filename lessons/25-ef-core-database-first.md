# Lesson 25: EF Core Database First

**Part 3: Database Connections** · About 2 hours

## What You Will Learn

- What **Entity Framework Core** (EF Core) is
- What **Database First** means
- How to let Visual Studio **write your classes for you**, from a database that already exists
- How to add, read, change, and delete records **without writing any SQL**

## The Idea

You now know two ways to talk to a database:

- **ADO.NET** (Lessons 20–23): you write the SQL, and you copy every column into an object by hand.
- **Dapper** (Lesson 24): you write the SQL, and Dapper fills the objects.

**Entity Framework Core** (people say **EF Core**) goes one step further: **it writes the SQL for you.** You just work with objects and lists, as if the database were a normal `List<Student>`:

```cs
db.Students.Add(student);     // EF Core writes the INSERT
db.SaveChanges();             // ...and sends it to the database
```

Think of ordering food:

- **ADO.NET** is cooking the meal yourself.
- **Dapper** is cooking with a helper who chops the vegetables.
- **EF Core** is ordering from a waiter. You say what you want, and the kitchen does the rest.

### What does "Database First" mean?

There are two ways to start with EF Core:

| Way | You begin with... | EF Core makes... |
|---|---|---|
| **Database First** | A **database** that already exists | The C# **classes**, by reading the database |
| Code First | C# classes that you write | The database, from your classes |

In this lesson you use **Database First**. This is very common at work: the company's database is already there (maybe for years), and your new program must fit it.

Your database already exists too: it is the `school.db` you filled in Lesson 24. EF Core will **read** it and **write the `Student` class for you**. Programmers call this **scaffolding**.

```text
 school.db                       EF Core reads it,                 Your project
┌───────────────────┐            and writes...                    ┌─────────────────────────┐
│ table: Students   │   ───────────────────────────────────►      │ Models/Student.cs       │
│  Id               │            "Scaffold-DbContext"             │ Models/SchoolContext.cs │
│  StudentNumber    │                                             └─────────────────────────┘
│  FullName ...     │
└───────────────────┘
```

---

## Step by Step

This lesson has more setup than usual. Go slowly, and tick off each step.

### Step 1: Put the database in an easy place

EF Core needs to know exactly where the database file is. Let's copy your Lesson 24 database to the course folder.

**1.** Press the **Windows key + E**. A File Explorer window opens.

**2.** Click the **address bar** at the top. Type `C:\CSharpCourse\Lesson24\bin\Debug` and press **Enter**.

**3.** Double-click the folder whose name starts with **net** (for example **net10.0**).

**4.** Click the file **school.db** once (it may show as just **school**). Press **Ctrl + C** to copy it.

**5.** Click the address bar again. Type `C:\CSharpCourse` and press **Enter**.

**6.** Press **Ctrl + V** to paste.

✅ **You will see:** the file `school.db` is now in `C:\CSharpCourse`, next to your lesson folders.

> **No `school.db` in Lesson 24?** Open your Lesson 24 project, run it, and add three students. Then do this step again.

### Step 2: Create a new project named `Lesson25`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson25`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Look at **Framework**, and **remember its first number** (for example, **10** in ".NET 10.0"). You need it in the next step.
6. Click **Create**.

### Step 3: Add two EF Core packages (choose the right version!)

> [!IMPORTANT]
> **The version of EF Core must match your .NET version.** If your project uses **.NET 10**, install EF Core version **10**.something. The newest version in the list may be for a newer .NET, and it will not install.

**1.** In the **Solution Explorer**, right-click the project line **Lesson25**.

**2.** Click **Manage NuGet Packages...**

**3.** Click **Browse**. In the search box, type `Microsoft.EntityFrameworkCore.Sqlite`

**4.** Click the result named exactly **Microsoft.EntityFrameworkCore.Sqlite**.

**5.** On the right side, find the **Version** box. Click it to open the list. Choose the **highest** version that starts with **your .NET number**. (For .NET 10, pick the highest one that starts with `10.`, like `10.0.15`.)

**6.** Click **Install**. (If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.)

**7.** Now search for the second package. In the search box, type `Microsoft.EntityFrameworkCore.Tools`

**8.** Click the result named exactly **Microsoft.EntityFrameworkCore.Tools**.

**9.** In the **Version** box, choose **the same version** as in part 5. Click **Install**. (Click **Apply** and **I Accept** if asked.)

**10.** Close the NuGet tab.

- The first package lets EF Core talk to **SQLite**.
- The second package adds the **tools** that read a database and write the classes.

### Step 4: Open the Package Manager Console

This is a small window inside Visual Studio where you can type commands.

**1.** On the menu bar, click **Tools**.

**2.** Move to **NuGet Package Manager**.

**3.** Click **Package Manager Console**.

✅ **You will see:** a panel opens at the bottom of Visual Studio. It ends with `PM>` and a blinking cursor.

### Step 5: Let EF Core write the classes

**1.** Click in the Package Manager Console, after `PM>`.

**2.** Type this command **on one line**, exactly. (You may copy it from here and paste it with **Ctrl + V**.)

```text
Scaffold-DbContext "Data Source=C:\CSharpCourse\school.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models -Context SchoolContext
```

**3.** Press **Enter**, and wait a few seconds.

✅ **You will see:**

```text
Build started...
Build succeeded.
To protect potentially sensitive information in your connection string, you should move it out of source code. ...
PM>
```

The long yellow message is **only a reminder**. It is not an error.

**4.** Look at the **Solution Explorer**. There is a new folder named **Models**. Click the small arrow next to it.

✅ **You will see:** two new files that you did **not** write:

```text
Lesson25
   Dependencies
   Models
      SchoolContext.cs
      Student.cs
   Program.cs
```

**What the command means:**

| Part | Meaning |
|---|---|
| `Scaffold-DbContext` | "Read a database, and write the classes for it" |
| `"Data Source=C:\CSharpCourse\school.db"` | Where the database is (the connection string) |
| `Microsoft.EntityFrameworkCore.Sqlite` | The kind of database: SQLite |
| `-OutputDir Models` | Put the new files in a folder named `Models` |
| `-Context SchoolContext` | Name the main class `SchoolContext` |

### Step 6: Look at what EF Core wrote

**1.** In the Solution Explorer, double-click **Student.cs** (inside **Models**).

## The Code

**File: `Models/Student.cs`** (written by EF Core)

```csharp
using System;
using System.Collections.Generic;

namespace Lesson25.Models;

public partial class Student
{
    public int Id { get; set; }

    public string StudentNumber { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Course { get; set; } = null!;

    public int YearLevel { get; set; }
}
```

Does it look familiar? It is almost the same `Student` class that **you** wrote by hand in Lesson 22! EF Core looked at the columns of the `Students` table, and made one property for each column.

**2.** Now double-click **SchoolContext.cs**.

**File: `Models/SchoolContext.cs`** (written by EF Core)

```csharp
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lesson25.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=C:\\CSharpCourse\\school.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.StudentNumber, "IX_Students_StudentNumber").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```

Your file has **one extra line** that starts with `#warning`. It is the same reminder you saw in Step 5.

**3.** Find the line that starts with **`#warning`**. Click on it, and delete that **whole line**. (This removes a yellow warning from the Error List. Everything else stays.)

You do not need to understand every line of this file. The two important lines are explained below.

### Step 7: Write the main program

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

**File: `Program.cs`**

```csharp
// Lesson 25: EF Core Database First - working with students without writing SQL

using Lesson25.Models;

using var db = new SchoolContext();

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("===== STUDENT RECORDS (EF CORE) =====");
    Console.WriteLine("1 - Show all students");
    Console.WriteLine("2 - Add a student");
    Console.WriteLine("3 - Search by name");
    Console.WriteLine("4 - Change a student's year level");
    Console.WriteLine("5 - Delete a student");
    Console.WriteLine("6 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            ShowAllScreen();
            break;
        case "2":
            AddScreen();
            break;
        case "3":
            SearchScreen();
            break;
        case "4":
            ChangeYearScreen();
            break;
        case "5":
            DeleteScreen();
            break;
        case "6":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Please type a number from 1 to 6.");
            break;
    }
}


// ----- Screens -----

void ShowAllScreen()
{
    // READ: get every student, sorted by name
    List<Student> students = db.Students
        .OrderBy(s => s.FullName)
        .ToList();

    ShowStudents(students);
}

void AddScreen()
{
    Console.WriteLine("--- ADD A STUDENT ---");
    string studentNumber = ReadRequired("Student number: ");

    // Is this student number already used?
    bool exists = db.Students.Any(s => s.StudentNumber == studentNumber);

    if (exists)
    {
        Console.WriteLine("That student number is already saved.");
        return;
    }

    Student student = new Student
    {
        StudentNumber = studentNumber,
        FullName = ReadRequired("Full name: "),
        Course = ReadRequired("Course: ").ToUpper(),
        YearLevel = ReadYearLevel()
    };

    // CREATE: add the object, then save
    db.Students.Add(student);
    db.SaveChanges();

    Console.WriteLine($"Saved! {student.FullName} was given database ID {student.Id}.");
}

void SearchScreen()
{
    string keyword = ReadRequired("Type part of the name: ").ToLower();

    // READ: get only the students whose name contains the keyword
    List<Student> students = db.Students
        .Where(s => s.FullName.ToLower().Contains(keyword))
        .OrderBy(s => s.FullName)
        .ToList();

    ShowStudents(students);
}

void ChangeYearScreen()
{
    Console.WriteLine("--- CHANGE YEAR LEVEL ---");
    string studentNumber = ReadRequired("Student number: ");

    // READ: find one student (gives back null if not found)
    Student? student = db.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);

    if (student == null)
    {
        Console.WriteLine("No student has that student number.");
        return;
    }

    Console.WriteLine($"{student.FullName} is in year {student.YearLevel} now.");

    // UPDATE: change the object, then save
    student.YearLevel = ReadYearLevel();
    db.SaveChanges();

    Console.WriteLine("Year level updated!");
}

void DeleteScreen()
{
    Console.WriteLine("--- DELETE A STUDENT ---");
    string studentNumber = ReadRequired("Student number: ");

    Student? student = db.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);

    if (student == null)
    {
        Console.WriteLine("No student has that student number.");
        return;
    }

    Console.Write($"Delete {student.FullName}? Type YES to confirm: ");
    string answer = Console.ReadLine() ?? "";

    if (answer == "YES")
    {
        // DELETE: remove the object, then save
        db.Students.Remove(student);
        db.SaveChanges();
        Console.WriteLine("Student deleted.");
    }
    else
    {
        Console.WriteLine("Nothing was deleted.");
    }
}

void ShowStudents(List<Student> students)
{
    if (students.Count == 0)
    {
        Console.WriteLine("No students found.");
        return;
    }

    Console.WriteLine($"{"NUMBER",-12}{"FULL NAME",-25}{"COURSE",-8}{"YEAR"}");
    Console.WriteLine(new string('-', 49));

    foreach (Student s in students)
    {
        Console.WriteLine($"{s.StudentNumber,-12}{s.FullName,-25}{s.Course,-8}{s.YearLevel}");
    }

    Console.WriteLine($"({students.Count} student(s))");
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

### Step 8: Run it

Press **Ctrl + F5**. Choose `1` first.

## The Output

✅ **You will see** the students you added in **Lesson 24**:

```text
===== STUDENT RECORDS (EF CORE) =====
1 - Show all students
2 - Add a student
3 - Search by name
4 - Change a student's year level
5 - Delete a student
6 - Exit
Choose an option: 1

NUMBER      FULL NAME                COURSE  YEAR
-------------------------------------------------
2026-0003   Ana Reyes                BSIT    3
2026-0001   Juan Dela Cruz           BSIT    1
2026-0002   Maria Santos             BSCS    2
(3 student(s))
```

Now add a student, and change a year level:

```text
Choose an option: 2

--- ADD A STUDENT ---
Student number: 2026-0004
Full name: Mark Villanueva
Course: bsis
Year level (1-5): 1
Saved! Mark Villanueva was given database ID 4.

...
Choose an option: 4

--- CHANGE YEAR LEVEL ---
Student number: 2026-0001
Juan Dela Cruz is in year 1 now.
Year level (1-5): 2
Year level updated!
```

Try the search and the delete, too. (Your ID numbers may be different.)

**Look at `Program.cs` again. There is not one line of SQL in it.** 🎉

---

## What the Code Means

### The two important lines in `SchoolContext.cs`

```cs
public virtual DbSet<Student> Students { get; set; }
```

`SchoolContext` is your **door to the database**. Its property `Students` stands for the whole `Students` **table**. A `DbSet<Student>` works a lot like a `List<Student>`, but it lives in the database.

```cs
=> optionsBuilder.UseSqlite("Data Source=C:\\CSharpCourse\\school.db");
```

This is the **connection string**: where the database is. (Inside normal C# text, a backslash is written twice: `\\`.)

### Using the classes

```cs
using Lesson25.Models;
```

EF Core put the classes in a **namespace** named `Lesson25.Models`. A namespace is like a folder name for classes. This line means: *"in this file, I want to use the classes from that folder."*

```cs
using var db = new SchoolContext();
```

Open the door to the database. `using var` closes it automatically at the end, like a connection in Lesson 20.

### READ: getting records

```cs
List<Student> students = db.Students
    .OrderBy(s => s.FullName)
    .ToList();
```

Read it from top to bottom: *"From the Students table, order them by full name, and give me a list."*

EF Core turns this into `SELECT ... FROM Students ORDER BY FullName` for you.

**What is `s => s.FullName`?** It is a tiny function with no name. Read the arrow `=>` as **"goes to"**, or simply: *"for each student `s`, use `s.FullName`."* The letter `s` is just a short name that you choose.

```cs
.Where(s => s.FullName.ToLower().Contains(keyword))
```

*"...but only the students **where** the full name contains the keyword."* This becomes the `WHERE` part of the SQL.

```cs
Student? student = db.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);
```

*"Give me the **first** student whose number is this one. If there is none, give me `null`."*

```cs
bool exists = db.Students.Any(s => s.StudentNumber == studentNumber);
```

*"Is there **any** student with this number?"* The answer is `true` or `false`.

| You write | Meaning | Like this SQL |
|---|---|---|
| `.ToList()` | Get the records as a list | `SELECT ...` |
| `.Where(s => ...)` | Only the records that match | `WHERE ...` |
| `.OrderBy(s => ...)` | Sort the records | `ORDER BY ...` |
| `.FirstOrDefault(s => ...)` | One record, or `null` | `SELECT ... LIMIT 1` |
| `.Any(s => ...)` | Is there at least one? | `SELECT COUNT(*) ...` |
| `.Count()` | How many? | `SELECT COUNT(*) ...` |

### CREATE: adding a record

```cs
db.Students.Add(student);
db.SaveChanges();
```

`Add` puts the new object in the table, like adding to a list. But nothing is sent to the database yet! **`SaveChanges()`** sends it.

After saving, EF Core even fills in `student.Id` with the number the database gave it.

### UPDATE: changing a record

```cs
student.YearLevel = ReadYearLevel();
db.SaveChanges();
```

Just **change the property**, and save. EF Core remembers which objects it gave you, notices what changed, and writes the `UPDATE` for you.

### DELETE: removing a record

```cs
db.Students.Remove(student);
db.SaveChanges();
```

> [!IMPORTANT]
> **Nothing changes in the database until you call `db.SaveChanges()`.** Forgetting it is the number one EF Core mistake.

### Is it safe from SQL injection?

Yes. EF Core always sends your values to the database as **parameters**. You never glue text into SQL, because you never write SQL at all.

---

## Three Ways to Do the Same Thing

Here is the same job, "get all the students, sorted by name", in the three ways you now know:

**ADO.NET (Lesson 22):**

```cs
using var command = connection.CreateCommand();
command.CommandText = "SELECT Id, StudentNumber, FullName, Course, YearLevel FROM Students ORDER BY FullName;";

List<Student> students = new List<Student>();

using var reader = command.ExecuteReader();
while (reader.Read())
{
    students.Add(new Student
    {
        Id = reader.GetInt32(0),
        StudentNumber = reader.GetString(1),
        FullName = reader.GetString(2),
        Course = reader.GetString(3),
        YearLevel = reader.GetInt32(4)
    });
}
```

**Dapper (Lesson 24):**

```cs
List<Student> students = connection.Query<Student>(
    "SELECT Id, StudentNumber, FullName, Course, YearLevel FROM Students ORDER BY FullName;").ToList();
```

**EF Core (this lesson):**

```cs
List<Student> students = db.Students.OrderBy(s => s.FullName).ToList();
```

| | ADO.NET | Dapper | EF Core |
|---|---|---|---|
| Who writes the SQL? | You | You | EF Core |
| Who fills the objects? | You | Dapper | EF Core |
| Who writes the classes? | You | You | EF Core (Database First) |
| Amount of code | Most | Less | Least |
| You control every detail | Yes | Yes | Less |
| Good for... | Learning, full control | Fast work with your own SQL | Big projects with many tables |

**Which one is best?** None. All three are used in real jobs, sometimes in the same project. Now you can read and write all three.

---

## If Something Goes Wrong

**Installing a package fails with `NU1202 ... is not compatible with net10.0`.**
You picked a version of EF Core that is for a newer .NET. In the **Version** box, choose a version that starts with the same number as your .NET (Step 3, part 5).

**`Scaffold-DbContext : The term 'Scaffold-DbContext' is not recognized...`**
The package **Microsoft.EntityFrameworkCore.Tools** is not installed. Do Step 3 again (parts 7 to 9). Then close the Package Manager Console and open it again.

**`Build failed.`**
There is a mistake in your code, and EF Core cannot continue. Look at the **Error List**, fix the mistake, and run the command again. (Run the command **before** you change `Program.cs`, as in the steps.)

**The command finished, but there is no `Student.cs` in the Models folder.**
EF Core did not find the database, so it found no tables. Check that `school.db` really is in `C:\CSharpCourse` (Step 1), and that the path in the command is spelled correctly.

**Error List: `CS0246  The type or namespace name 'Student' could not be found`**
The line `using Lesson25.Models;` is missing at the top of `Program.cs`. If your project has a different name, use that name: `using YourProjectName.Models;`

**The black window shows: `SQLite Error 1: 'no such table: Students'.`**
The program is looking at an empty database. Check the path in `SchoolContext.cs`, and check that `C:\CSharpCourse\school.db` is the file you copied.

**I added a student, but it is gone when I show the list again.**
You forgot `db.SaveChanges();` after `db.Students.Add(...)`.

**A yellow warning about a "connection string" is in the Error List.**
Delete the line that starts with `#warning` in `SchoolContext.cs` (Step 6, part 3).

---

## Exercise

**Exercise 1.** At the bottom of the student table, also show the **total number of students in the database**. Use `db.Students.Count()`.

<details>
<summary>Show answer</summary>

At the end of `ShowAllScreen`, add:

```cs
int total = db.Students.Count();
Console.WriteLine($"Total students in the database: {total}");
```

EF Core turns `.Count()` into `SELECT COUNT(*) FROM Students`.

</details>

**Exercise 2.** Add a menu option **Show students of one course**. Ask for the course, and show only the students of that course, sorted by name. (Move **Exit** to option 7.)

<details>
<summary>Show answer</summary>

Add this screen with the other screens:

```cs
void CourseScreen()
{
    string course = ReadRequired("Course: ").ToUpper();

    List<Student> students = db.Students
        .Where(s => s.Course == course)
        .OrderBy(s => s.FullName)
        .ToList();

    ShowStudents(students);
}
```

Add the menu line `Console.WriteLine("6 - Show students of one course");`, change Exit to `7 - Exit`, and change the cases:

```cs
case "6":
    CourseScreen();
    break;
case "7":
    running = false;
    Console.WriteLine("Goodbye!");
    break;
```

</details>

## ⭐ Bonus Challenge

Add a menu option that shows the students sorted by **year level, from highest to lowest**. Students in the same year must be sorted by **name**.

Hints:

- `.OrderByDescending(s => ...)` sorts from highest to lowest.
- `.ThenBy(s => ...)` adds a second sort, for records that are equal in the first one.

<details>
<summary>Show answer</summary>

```cs
void ByYearScreen()
{
    List<Student> students = db.Students
        .OrderByDescending(s => s.YearLevel)
        .ThenBy(s => s.FullName)
        .ToList();

    ShowStudents(students);
}
```

Then add a menu line and a `case` for it, like in Exercise 2.

EF Core turns this into `ORDER BY YearLevel DESC, FullName`.

</details>

---

## Remember

- **EF Core** writes the SQL for you. You work with objects and lists.
- **Database First**: the database exists first. EF Core reads it and writes the classes (**scaffolding**).
- EF Core's version must match your .NET version (.NET 10 → EF Core 10.x).
- The command: `Scaffold-DbContext "Data Source=..." Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models -Context SchoolContext`
- `db.Students` is the table. Use `.Where()`, `.OrderBy()`, `.FirstOrDefault()`, `.Any()`, `.Count()`, `.ToList()`.
- `s => s.FullName` means "for each student `s`, use its `FullName`".
- **Add**, change, or **Remove**, and then always call **`db.SaveChanges()`**.
- ADO.NET, Dapper, and EF Core are three ways to do the same work. All three are worth knowing.

---

[⬅️ Lesson 24](24-dapper.md) | [🏠 Course Home](../README.md) | [Next: Lesson 26 (What Is Hashing?) ➡️](26-what-is-hashing.md)
