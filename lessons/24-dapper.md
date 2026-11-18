# Lesson 24: Dapper (The Same Database Work, With Less Code)

**Part 3: Database Connections** · About 90 minutes

## What You Will Learn

- What **Dapper** is, and why programmers like it
- How to install a second package
- How to run SQL with Dapper: `Execute`, `Query`, `QueryFirstOrDefault`, and `ExecuteScalar`
- How Dapper turns database rows into objects **for you**
- How to replace one file of a program without touching the others

## The Idea

In Lessons 20 to 23, you talked to the database with **ADO.NET**. It works well, but look at how much you had to type just to read students:

```cs
// ADO.NET (Lesson 22): about 25 lines
using var command = connection.CreateCommand();
command.CommandText = "SELECT Id, StudentNumber, FullName, Course, YearLevel FROM Students;";

List<Student> students = new List<Student>();

using var reader = command.ExecuteReader();
while (reader.Read())
{
    Student student = new Student
    {
        Id = reader.GetInt32(0),
        StudentNumber = reader.GetString(1),
        FullName = reader.GetString(2),
        Course = reader.GetString(3),
        YearLevel = reader.GetInt32(4)
    };
    students.Add(student);
}
```

Making the command, reading each row, and copying each column into a property, one by one... It is the same boring work every time.

**Dapper** is a small, free package that does that boring part for you:

```cs
// Dapper: 1 line
List<Student> students = connection.Query<Student>("SELECT Id, StudentNumber, FullName, Course, YearLevel FROM Students;").ToList();
```

You still write the **SQL**. Dapper does the rest:

- It puts your values into the SQL **as parameters** (so it is still safe from SQL injection).
- It turns each row into an **object**, by matching **column names** with **property names**.

Think of ADO.NET as washing clothes by hand, and Dapper as a washing machine. The same clothes get clean. You just do less of the work.

> Dapper is not a replacement for ADO.NET. It is built **on top of** ADO.NET. You still open the same connection. Dapper just adds faster tools to it.

---

## Step by Step

### Step 1: Create a new project named `Lesson24`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson24`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add TWO packages

This project needs the SQLite package (as before) **and** the Dapper package.

**1.** In the **Solution Explorer**, right-click the project line **Lesson24**.

**2.** Click **Manage NuGet Packages...**

**3.** Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.

**4.** Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**. (If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.)

**5.** Now click the search box again. Delete what is there, and type: `Dapper`

**6.** Click the result named exactly **Dapper**. Then click **Install**. (Click **Apply** if asked.)

✅ **You will see:** click the word **Installed** (next to **Browse**). The list shows both **Dapper** and **Microsoft.Data.Sqlite**.

**7.** Close the NuGet tab.

### Step 3: Bring in `Student.cs` from Lesson 22

1. In the Solution Explorer, right-click the project line **Lesson24**.
2. Move to **Add**, then click **Existing Item...**
3. Click the address bar at the top of the window. Type `C:\CSharpCourse\Lesson22` and press **Enter**.
4. Click **Student.cs**, then click **Add**.

### Step 4: Add the file `StudentDatabase.cs` (the Dapper version)

1. Right-click the project line **Lesson24** → **Add** → **Class...**
2. In the **Name** box, type `StudentDatabase.cs`. Click **Add**.
3. In the new tab, press **Ctrl + A**, then **Delete**.
4. Put in this code:

## The Code

**File: `StudentDatabase.cs`**

```csharp
// StudentDatabase.cs - the Dapper version. Compare it with Lesson 22!

using Dapper;
using Microsoft.Data.Sqlite;

public static class StudentDatabase
{
    private const string ConnectionString = "Data Source=school.db";

    // Opens a connection to school.db and gives it back, ready to use
    private static SqliteConnection OpenConnection()
    {
        SqliteConnection connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    // CREATE: make the table if it does not exist yet
    public static void CreateTable()
    {
        using var connection = OpenConnection();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Students (
                Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentNumber TEXT    NOT NULL UNIQUE,
                FullName      TEXT    NOT NULL,
                Course        TEXT    NOT NULL,
                YearLevel     INTEGER NOT NULL
            );");
    }

    // Is this student number already used?
    public static bool StudentNumberExists(string studentNumber)
    {
        using var connection = OpenConnection();

        int count = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM Students WHERE StudentNumber = @StudentNumber;",
            new { StudentNumber = studentNumber });

        return count > 0;
    }

    // CREATE: save a new student
    public static void AddStudent(Student student)
    {
        using var connection = OpenConnection();

        connection.Execute(@"
            INSERT INTO Students (StudentNumber, FullName, Course, YearLevel)
            VALUES (@StudentNumber, @FullName, @Course, @YearLevel);",
            student);
    }

    // READ: get every student, sorted by name
    public static List<Student> GetAllStudents()
    {
        using var connection = OpenConnection();

        return connection.Query<Student>(@"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            ORDER BY FullName;").ToList();
    }

    // READ: get students whose name contains the keyword
    public static List<Student> SearchByName(string keyword)
    {
        using var connection = OpenConnection();

        return connection.Query<Student>(@"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            WHERE FullName LIKE @Pattern
            ORDER BY FullName;",
            new { Pattern = "%" + keyword + "%" }).ToList();
    }

    // READ: find one student by student number (gives back null if not found)
    public static Student? FindByStudentNumber(string studentNumber)
    {
        using var connection = OpenConnection();

        return connection.QueryFirstOrDefault<Student>(@"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            WHERE StudentNumber = @StudentNumber;",
            new { StudentNumber = studentNumber });
    }

    // UPDATE: save new values for an existing student
    public static bool UpdateStudent(Student student)
    {
        using var connection = OpenConnection();

        int rowsChanged = connection.Execute(@"
            UPDATE Students
            SET FullName = @FullName,
                Course = @Course,
                YearLevel = @YearLevel
            WHERE Id = @Id;",
            student);

        return rowsChanged > 0;
    }

    // DELETE: remove a student
    public static bool DeleteStudent(int id)
    {
        using var connection = OpenConnection();

        int rowsChanged = connection.Execute(
            "DELETE FROM Students WHERE Id = @Id;",
            new { Id = id });

        return rowsChanged > 0;
    }
}
```

### Step 5: Put the main program in `Program.cs`

Here is something nice: `Program.cs` is **the same as in Lesson 22**. Only its first comment line is different.

Why? Because `Program.cs` (the front desk) never talks to the database directly. It only asks `StudentDatabase` (the records office). We changed **how** the records office works inside, but its functions have the **same names**. The front desk does not even notice.

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

<details>
<summary>Show <code>Program.cs</code> (the same as Lesson 22)</summary>

**File: `Program.cs`**

```csharp
// Lesson 24: Student Records Manager, now with Dapper (this file is the same as in Lesson 22)

StudentDatabase.CreateTable();

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("========= STUDENT RECORDS =========");
    Console.WriteLine("1 - Add a student");
    Console.WriteLine("2 - Show all students");
    Console.WriteLine("3 - Search by name");
    Console.WriteLine("4 - Update a student");
    Console.WriteLine("5 - Delete a student");
    Console.WriteLine("6 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddStudentScreen();
            break;
        case "2":
            ShowStudents(StudentDatabase.GetAllStudents());
            break;
        case "3":
            string keyword = ReadRequired("Type part of the name: ");
            ShowStudents(StudentDatabase.SearchByName(keyword));
            break;
        case "4":
            UpdateStudentScreen();
            break;
        case "5":
            DeleteStudentScreen();
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

void AddStudentScreen()
{
    Console.WriteLine("--- ADD A STUDENT ---");
    string studentNumber = ReadRequired("Student number: ");

    if (StudentDatabase.StudentNumberExists(studentNumber))
    {
        Console.WriteLine("That student number is already saved.");
        return;
    }

    Student student = new Student
    {
        StudentNumber = studentNumber,
        FullName = ReadRequired("Full name: "),
        Course = ReadRequired("Course: "),
        YearLevel = ReadYearLevel()
    };

    StudentDatabase.AddStudent(student);
    Console.WriteLine($"Saved! {student.FullName} was added.");
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

void UpdateStudentScreen()
{
    Console.WriteLine("--- UPDATE A STUDENT ---");
    string studentNumber = ReadRequired("Student number to update: ");
    Student? student = StudentDatabase.FindByStudentNumber(studentNumber);

    if (student == null)
    {
        Console.WriteLine("No student has that student number.");
        return;
    }

    Console.WriteLine($"Current: {student.FullName}, {student.Course}, Year {student.YearLevel}");
    Console.WriteLine("Type the new values:");

    student.FullName = ReadRequired("Full name: ");
    student.Course = ReadRequired("Course: ");
    student.YearLevel = ReadYearLevel();

    if (StudentDatabase.UpdateStudent(student))
    {
        Console.WriteLine("Student updated!");
    }
}

void DeleteStudentScreen()
{
    Console.WriteLine("--- DELETE A STUDENT ---");
    string studentNumber = ReadRequired("Student number to delete: ");
    Student? student = StudentDatabase.FindByStudentNumber(studentNumber);

    if (student == null)
    {
        Console.WriteLine("No student has that student number.");
        return;
    }

    Console.Write($"Delete {student.FullName}? Type YES to confirm: ");
    string answer = Console.ReadLine() ?? "";

    if (answer == "YES" && StudentDatabase.DeleteStudent(student.Id))
    {
        Console.WriteLine("Student deleted.");
    }
    else
    {
        Console.WriteLine("Nothing was deleted.");
    }
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

</details>

### Step 6: Run it

Press **Ctrl + F5**. Add at least **three students**, then try every option.

> This project has its **own new database**, so it starts empty. Please add a few students now. **You will need them in the next lesson.**

## The Output

✅ **You will see** the same program as in Lesson 22:

```text
========= STUDENT RECORDS =========
1 - Add a student
2 - Show all students
3 - Search by name
4 - Update a student
5 - Delete a student
6 - Exit
Choose an option: 1

--- ADD A STUDENT ---
Student number: 2026-0001
Full name: Juan Dela Cruz
Course: BSIT
Year level (1-5): 1
Saved! Juan Dela Cruz was added.

...
Choose an option: 2

NUMBER      FULL NAME                COURSE  YEAR
-------------------------------------------------
2026-0003   Ana Reyes                BSIT    3
2026-0001   Juan Dela Cruz           BSIT    1
2026-0002   Maria Santos             BSCS    2
(3 student(s))
```

It looks and works exactly the same. But `StudentDatabase.cs` went from about **160 lines** to about **120 lines**, and every function is much easier to read.

---

## What the Code Means

```cs
using Dapper;
using Microsoft.Data.Sqlite;
```

Two `using` lines now. `using Dapper;` adds Dapper's tools to the connection.

### The four Dapper tools

| Tool | Use it for | It gives back |
|---|---|---|
| `connection.Execute(sql, values)` | `CREATE`, `INSERT`, `UPDATE`, `DELETE` | How many rows were changed |
| `connection.ExecuteScalar<int>(sql, values)` | **One** value, like `COUNT(*)` | That value, already as an `int` |
| `connection.Query<Student>(sql, values)` | **Many** rows | The rows, already as `Student` objects |
| `connection.QueryFirstOrDefault<Student>(sql, values)` | **One** row | One `Student`, or `null` if nothing was found |

Compare them with ADO.NET's three tools from Lesson 20. They match:

| ADO.NET | Dapper |
|---|---|
| `ExecuteNonQuery()` | `Execute(...)` |
| `ExecuteScalar()` | `ExecuteScalar<int>(...)` |
| `ExecuteReader()` + a `while` loop | `Query<Student>(...)` |

### Giving values to the SQL

There are two ways. Both are safe **parameters**, like in Lesson 21.

**Way 1: give a whole object.**

```cs
connection.Execute(@"
    INSERT INTO Students (StudentNumber, FullName, Course, YearLevel)
    VALUES (@StudentNumber, @FullName, @Course, @YearLevel);",
    student);
```

In the SQL, each placeholder starts with **`@`**, followed by the name of a **property**. Dapper looks inside the `student` object, and fills `@FullName` with `student.FullName`, `@Course` with `student.Course`, and so on. No more `AddWithValue` lines!

> [!IMPORTANT]
> With Dapper, placeholders are written with **`@`** and must be spelled **exactly like the property**: `@FullName` for the property `FullName`.

**Way 2: make a small object on the spot.**

```cs
new { StudentNumber = studentNumber }
```

Sometimes you only have a loose variable, not a whole `Student`. `new { ... }` makes a tiny object **without a class**, just to carry the value. Read it as: *"an object with one property, `StudentNumber`, holding the value of my variable `studentNumber`."*

```cs
int count = connection.ExecuteScalar<int>(
    "SELECT COUNT(*) FROM Students WHERE StudentNumber = @StudentNumber;",
    new { StudentNumber = studentNumber });
```

### Turning rows into objects

```cs
return connection.Query<Student>(@"
    SELECT Id, StudentNumber, FullName, Course, YearLevel
    FROM Students
    ORDER BY FullName;").ToList();
```

- `Query<Student>` means: *"run this SQL, and give me the rows as `Student` objects."* The type goes inside `< >`, like in `List<Student>`.
- Dapper matches each **column name** with the **property** of the same name: the column `FullName` goes into the property `FullName`. No more counting columns from 0!
- `.ToList()` turns the result into a `List<Student>`.

> [!TIP]
> This matching by name only works when the names are the same. If a column is named `full_name` and the property is `FullName`, write `SELECT full_name AS FullName` in your SQL.

### One row, or nothing

```cs
return connection.QueryFirstOrDefault<Student>(sql, new { StudentNumber = studentNumber });
```

"First or default" means: *"give me the **first** row as a `Student`. If there is no row, give me the default, which is `null`."* That is exactly what `FindByStudentNumber` needs.

### Did it work?

```cs
int rowsChanged = connection.Execute("DELETE FROM Students WHERE Id = @Id;", new { Id = id });

return rowsChanged > 0;
```

Like `ExecuteNonQuery`, `Execute` gives back how many rows were changed.

### ADO.NET or Dapper?

| | ADO.NET | Dapper |
|---|---|---|
| You write the SQL | Yes | Yes |
| Extra package needed | No | Yes (`Dapper`) |
| Amount of code | More | Less |
| Rows become objects | By hand, column by column | Automatically, by name |
| Parameters | `AddWithValue`, one by one | From an object |
| Good for... | Learning how it really works | Everyday work |

Both are used in real jobs. Knowing ADO.NET first (as you do now) makes Dapper easy to understand.

---

## If Something Goes Wrong

**Error List: `CS0246  The type or namespace name 'Dapper' could not be found`**
The Dapper package is not installed in this project. Do Step 2 again (parts 5 and 6).

**Error List: `CS1061  'SqliteConnection' does not contain a definition for 'Query'`**
The line `using Dapper;` is missing at the top of `StudentDatabase.cs`.

**The black window shows: `Must add values for the following parameters: @FullName`**
A placeholder in the SQL has no matching property. Check the spelling: `@FullName` needs a property named exactly `FullName` in the object you gave to Dapper.

**The list shows students, but a column is empty (or 0).**
A column name and a property name do not match, so Dapper could not fill that property. Check the names in your `SELECT`.

**The black window shows: `SQLite Error 1: 'no such table: Students'.`**
`StudentDatabase.CreateTable();` must be the first line of `Program.cs`.

---

## Exercise

**Exercise 1.** Add a function `CountStudents()` to `StudentDatabase.cs`. It gives back the total number of students, using `ExecuteScalar<int>`. In `Program.cs`, show the total right under the menu title.

<details>
<summary>Show answer</summary>

In `StudentDatabase.cs`, inside the class:

```cs
public static int CountStudents()
{
    using var connection = OpenConnection();

    return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Students;");
}
```

In `Program.cs`, right after the line that shows `========= STUDENT RECORDS =========`, add:

```cs
Console.WriteLine($"Total students: {StudentDatabase.CountStudents()}");
```

This SQL has no placeholders, so no values are needed.

</details>

**Exercise 2.** Add a function `SearchByCourse(string course)` that gives back all the students of one course, sorted by name. Use `Query<Student>` and a placeholder named `@Course`.

<details>
<summary>Show answer</summary>

```cs
public static List<Student> SearchByCourse(string course)
{
    using var connection = OpenConnection();

    return connection.Query<Student>(@"
        SELECT Id, StudentNumber, FullName, Course, YearLevel
        FROM Students
        WHERE Course = @Course
        ORDER BY FullName;",
        new { Course = course }).ToList();
}
```

To try it, add a menu option in `Program.cs`, like in the bonus of Lesson 22:

```cs
case "6":
    string course = ReadRequired("Course: ").ToUpper();
    ShowStudents(StudentDatabase.SearchByCourse(course));
    break;
```

(Then move **Exit** to option 7.)

</details>

## ⭐ Bonus Challenge

Dapper can fill **any** class, not just `Student`. Make a report that shows **how many students are in each course**.

```text
BSCS: 1 student(s)
BSIT: 2 student(s)
```

1. Add a new class file `CourseCount.cs`, with two properties: `Course` (text) and `Total` (whole number).
2. Add a function `GetCourseCounts()` to `StudentDatabase.cs`. Use this SQL:

```sql
SELECT Course, COUNT(*) AS Total FROM Students GROUP BY Course ORDER BY Course;
```

`GROUP BY Course` makes one row for each course. `COUNT(*) AS Total` counts the students in that group, and names the result column `Total`.

<details>
<summary>Show answer</summary>

**File: `CourseCount.cs`**

```csharp
public class CourseCount
{
    public string Course { get; set; } = "";
    public int Total { get; set; }
}
```

In `StudentDatabase.cs`, inside the class:

```cs
public static List<CourseCount> GetCourseCounts()
{
    using var connection = OpenConnection();

    return connection.Query<CourseCount>(@"
        SELECT Course, COUNT(*) AS Total
        FROM Students
        GROUP BY Course
        ORDER BY Course;").ToList();
}
```

In `Program.cs`, add a menu option that shows the report:

```cs
foreach (CourseCount item in StudentDatabase.GetCourseCounts())
{
    Console.WriteLine($"{item.Course}: {item.Total} student(s)");
}
```

The column `Course` fills the property `Course`, and the column `Total` fills the property `Total`. That is why the SQL says `AS Total`.

</details>

---

## Remember

- **Dapper** is a small package on top of ADO.NET. You still write the SQL. Dapper does the boring parts.
- Install **two** packages: `Microsoft.Data.Sqlite` and `Dapper`. Add `using Dapper;` at the top.
- `Execute` → changes. `ExecuteScalar<int>` → one value. `Query<Student>` → many rows. `QueryFirstOrDefault<Student>` → one row or `null`.
- Placeholders use **`@`** and the **property name**: `@FullName`.
- Give Dapper a whole object (`student`), or a small one made on the spot (`new { Id = id }`).
- Dapper matches **column names** with **property names**.
- It is still safe: the values are always sent as **parameters**.

---

[⬅️ Lesson 23](23-adonet-uploading-csv-data.md) | [🏠 Course Home](../README.md) | [Next: Lesson 25 (EF Core Database First) ➡️](25-ef-core-database-first.md)
