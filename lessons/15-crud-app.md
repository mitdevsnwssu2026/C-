# Lesson 15: Student Records Manager (Create, Read, Update, Delete)

| | |
|---|---|
| **Part** | 3: Databases |
| **Time** | About 2 hours (you can split it into two sessions) |
| **Before this** | [Lesson 14](14-inserting-data.md) |

## 🎯 Goal

By the end of this lesson, you will build a complete **Student Records Manager** that can:

- **Add** students
- **Show** all students in a neat table
- **Search** students by name
- **Update** a student's information
- **Delete** a student

You will also learn to **read many rows** with `ExecuteReader`, and to organize a program into **several files**.

---

## 🧠 The Big Idea: CRUD

Almost every business system in the world does four basic things with data. Programmers call them **CRUD**:

| Letter | Action | SQL | Example |
|---|---|---|---|
| **C** | **Create** | `INSERT` | Enroll a new student |
| **R** | **Read** | `SELECT` | Show the class list |
| **U** | **Update** | `UPDATE` | Fix a misspelled name |
| **D** | **Delete** | `DELETE` | Remove a student who withdrew |

An enrollment system, a library system, an inventory system, a payroll system: they are all CRUD at their core. **After this lesson, you can build the core of any of them.**

### Organizing the program: one job per file

This program is bigger, so we split it into **three files**. Each file has **one job**:

| File | Its job | Like a... |
|---|---|---|
| `Student.cs` | Describes what a student **is** | The blank form |
| `StudentDatabase.cs` | **All** the SQL. Talks to the database | The records office |
| `Program.cs` | The menu and screens. Talks to the **user** | The front desk |

The front desk (`Program.cs`) never writes SQL. It just asks the records office (`StudentDatabase`) for what it needs: *"Please add this student,"* or *"Please give me all students."*

---

## 🧠 New Idea: Reading Many Rows with `ExecuteReader`

`ExecuteScalar` gives back **one** value. To read **many rows**, use `ExecuteReader`. It gives you a **reader** that moves through the results **one row at a time**, like reading a list with your finger:

```cs
using var reader = command.ExecuteReader();

while (reader.Read())
{
    int id = reader.GetInt32(0);
    string fullName = reader.GetString(2);
}
```

| Code | Meaning |
|---|---|
| `reader.Read()` | Move your finger to the **next row**. Gives back `true` if there is a row, or `false` when there are no more rows |
| `while (reader.Read())` | Repeat for **every** row |
| `reader.GetInt32(0)` | Get the value in **column 0** of this row as an `int` |
| `reader.GetString(2)` | Get the value in **column 2** of this row as a `string` |

The column numbers follow the order in your `SELECT`, **starting from 0**:

```sql
SELECT Id, StudentNumber, FullName, Course, YearLevel FROM Students
--     0   1              2         3       4
```

---

## 🛠️ Let's Build It

### Step 1: Create the project and add SQLite

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson15
cd Lesson15
dotnet add package Microsoft.Data.Sqlite
code .
```

### Step 2: Create `Student.cs`

In VS Code, create a new file named **`Student.cs`** (Explorer panel → **New File** icon → type the name → Enter), and write:

```csharp
// Student.cs - the blueprint for one student record

public class Student
{
    public int Id { get; set; }
    public string StudentNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Course { get; set; } = "";
    public int YearLevel { get; set; }
}
```

This is like the `Student` class from Lesson 09, with a new `Id` property for the database's primary key.

### Step 3: Create `StudentDatabase.cs`

Create another new file named **`StudentDatabase.cs`** and write:

```csharp
// StudentDatabase.cs - every database command for students lives here

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

    // Is this student number already used?
    public static bool StudentNumberExists(string studentNumber)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Students WHERE StudentNumber = $studentNumber;";
        command.Parameters.AddWithValue("$studentNumber", studentNumber);

        int count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    // CREATE: save a new student
    public static void AddStudent(Student student)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Students (StudentNumber, FullName, Course, YearLevel)
            VALUES ($studentNumber, $fullName, $course, $yearLevel);";
        command.Parameters.AddWithValue("$studentNumber", student.StudentNumber);
        command.Parameters.AddWithValue("$fullName", student.FullName);
        command.Parameters.AddWithValue("$course", student.Course);
        command.Parameters.AddWithValue("$yearLevel", student.YearLevel);
        command.ExecuteNonQuery();
    }

    // READ: get every student, sorted by name
    public static List<Student> GetAllStudents()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            ORDER BY FullName;";

        return ReadStudents(command);
    }

    // READ: get students whose name contains the keyword
    public static List<Student> SearchByName(string keyword)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            WHERE FullName LIKE $pattern
            ORDER BY FullName;";
        command.Parameters.AddWithValue("$pattern", "%" + keyword + "%");

        return ReadStudents(command);
    }

    // READ: find one student by student number (gives back null if not found)
    public static Student? FindByStudentNumber(string studentNumber)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, StudentNumber, FullName, Course, YearLevel
            FROM Students
            WHERE StudentNumber = $studentNumber;";
        command.Parameters.AddWithValue("$studentNumber", studentNumber);

        List<Student> results = ReadStudents(command);

        if (results.Count == 0)
        {
            return null;
        }

        return results[0];
    }

    // UPDATE: save new values for an existing student
    public static bool UpdateStudent(Student student)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Students
            SET FullName = $fullName,
                Course = $course,
                YearLevel = $yearLevel
            WHERE Id = $id;";
        command.Parameters.AddWithValue("$fullName", student.FullName);
        command.Parameters.AddWithValue("$course", student.Course);
        command.Parameters.AddWithValue("$yearLevel", student.YearLevel);
        command.Parameters.AddWithValue("$id", student.Id);

        int rowsChanged = command.ExecuteNonQuery();
        return rowsChanged > 0;
    }

    // DELETE: remove a student
    public static bool DeleteStudent(int id)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Students WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);

        int rowsChanged = command.ExecuteNonQuery();
        return rowsChanged > 0;
    }

    // Helper: turns the rows returned by a SELECT into a List of Student objects
    private static List<Student> ReadStudents(SqliteCommand command)
    {
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

        return students;
    }
}
```

### Step 4: Write `Program.cs`

Replace everything in `Program.cs` with:

```csharp
// Lesson 15: Student Records Manager (Create, Read, Update, Delete)

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

### Step 5: Check your files

Your Explorer panel in VS Code should show these files (plus the `bin` and `obj` folders):

```text
LESSON15
├── Lesson15.csproj
├── Program.cs
├── Student.cs
└── StudentDatabase.cs
```

### Step 6: Run it

```cmd
dotnet run
```

Try every option! Here is an example session (some menus are shortened with `...`):

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

...
Choose an option: 3

Type part of the name: santos
NUMBER      FULL NAME                COURSE  YEAR
-------------------------------------------------
2026-0002   Maria Santos             BSCS    2
(1 student(s))

...
Choose an option: 4

--- UPDATE A STUDENT ---
Student number to update: 2026-0001
Current: Juan Dela Cruz, BSIT, Year 1
Type the new values:
Full name: Juan Dela Cruz
Course: BSIT
Year level (1-5): 2
Student updated!

...
Choose an option: 5

--- DELETE A STUDENT ---
Student number to delete: 2026-0003
Delete Ana Reyes? Type YES to confirm: YES
Student deleted.
```

**Exit the program and run it again.** Choose option 2. All your changes are still there, because they were saved in `school.db`.

---

## 🔎 Line by Line: `StudentDatabase.cs`

### `static`: no object needed

```cs
public static class StudentDatabase
```

In Lesson 09, you had to create an object (`new Student()`) before using it. A **`static`** class is different: you use it **directly by its name**, without `new`. That makes sense here, because there is only **one** records office.

```cs
StudentDatabase.CreateTable();                         // ✅ use it directly
List<Student> all = StudentDatabase.GetAllStudents();  // ✅
```

Every method inside a `static` class must also be `static`.

### `public` and `private`

| Word | Who can use it |
|---|---|
| `public` | **Any** file. `Program.cs` can call it |
| `private` | **Only this class**. It is a helper for the class's own use |

`OpenConnection` and `ReadStudents` are `private`, because `Program.cs` never needs to call them directly. The front desk does not need to know how the records office opens its filing cabinet.

### `OpenConnection`: no more repeating

```cs
private static SqliteConnection OpenConnection()
{
    SqliteConnection connection = new SqliteConnection(ConnectionString);
    connection.Open();
    return connection;
}
```

In Lesson 14, every method repeated the same two lines to open a connection. Now we write them **once**, and every method just calls `using var connection = OpenConnection();`.

### `ReadStudents`: turning rows into objects

```cs
while (reader.Read())
{
    Student student = new Student
    {
        Id = reader.GetInt32(0),
        StudentNumber = reader.GetString(1),
        ...
    };
    students.Add(student);
}
```

For **each row**, create a `Student` object, fill it with the values from the columns, and add it to a list. Three methods (`GetAllStudents`, `SearchByName`, `FindByStudentNumber`) need to do this, so it is written **once** here.

### `SearchByName`: finding part of a name with `LIKE`

```cs
command.CommandText = @"... WHERE FullName LIKE $pattern ...";
command.Parameters.AddWithValue("$pattern", "%" + keyword + "%");
```

`LIKE` searches for a **pattern**. `%` means *"any text (or nothing) here"*. So `%santos%` matches any name that **contains** "santos" anywhere: `Maria Santos`, `Santos Reyes`, and so on. In SQLite, `LIKE` also ignores capital letters.

Notice that the `%` signs are added to the **value**, and the value is still passed as a **parameter**. Always!

### `FindByStudentNumber`: `Student?` and `null`

```cs
public static Student? FindByStudentNumber(string studentNumber)
```

The `?` after `Student` means *"this method gives back a Student, **or nothing**."* In C#, "nothing" is called **`null`**.

```cs
if (results.Count == 0)
{
    return null;     // not found
}

return results[0];   // found: give back the first (and only) result
```

### `UpdateStudent` and `DeleteStudent`: did it work?

```cs
int rowsChanged = command.ExecuteNonQuery();
return rowsChanged > 0;
```

`ExecuteNonQuery` gives back **how many rows were changed**. If it is more than 0, the update or delete worked, so we return `true`.

Both use `WHERE Id = $id`. The `Id` is the **primary key**, so it is the safest way to make sure we change **exactly one** row.

---

## 🔎 Line by Line: `Program.cs`

### Screens

Each menu option has its own method, called a **screen**: `AddStudentScreen()`, `UpdateStudentScreen()`, `DeleteStudentScreen()`. This keeps the `switch` short and easy to read.

### Leaving a method early with `return;`

```cs
if (StudentDatabase.StudentNumberExists(studentNumber))
{
    Console.WriteLine("That student number is already saved.");
    return;
}
```

In a `void` method, `return;` means *"stop this method now and go back."* The rest of the screen is skipped, and the menu appears again.

### Checking for `null`

```cs
Student? student = StudentDatabase.FindByStudentNumber(studentNumber);

if (student == null)
{
    Console.WriteLine("No student has that student number.");
    return;
}
```

**Always** check for `null` before using the result. After this check, C# knows `student` is a real object.

### Neat columns: `{value,-12}`

```cs
Console.WriteLine($"{s.StudentNumber,-12}{s.FullName,-25}{s.Course,-8}{s.YearLevel}");
```

`,-12` after a value means *"use 12 spaces for this value, and put the text on the **left**."* Extra space is filled with blanks. This makes the columns line up like a table.

### A safe delete

```cs
Console.Write($"Delete {student.FullName}? Type YES to confirm: ");
string answer = Console.ReadLine() ?? "";

if (answer == "YES" && StudentDatabase.DeleteStudent(student.Id))
```

Deleting cannot be undone, so we ask the user to **confirm** first. The student is deleted only if they type exactly `YES`.

> [!TIP]
> Now go back to the **SQL Playground** code in [Lesson 12](12-what-is-a-database.md). You will recognize `using var`, `CreateCommand`, `ExecuteReader`, `reader.Read()`, and `try` / `catch (SqliteException ex)`. You can now understand the whole thing!

---

## ⚠️ Common Mistakes

### Mistake 1: The wrong column number in the reader

```cs
FullName = reader.GetString(1),   // column 1 is StudentNumber!
```

The program runs, but names and numbers are mixed up. **Fix:** Count the columns in your `SELECT`, starting from **0**.

### Mistake 2: The wrong `Get` method for a column's type

```cs
YearLevel = reader.GetString(4),
```

```text
error CS0029: Cannot implicitly convert type 'string' to 'int'
```

**Fix:** Use `GetInt32` for `INTEGER` columns and `GetString` for `TEXT` columns.

### Mistake 3: Using an object without checking for `null`

If you skip the `if (student == null)` check, VS Code shows a warning, and the program can crash with `NullReferenceException` when the student is not found. **Fix:** Always check for `null` first.

### Mistake 4: `The name 'StudentDatabase' does not exist`

**Fix:** Make sure the file is named `StudentDatabase.cs`, is in the **same folder** as `Program.cs`, and the class inside is `public static class StudentDatabase`.

---

## 🏋️ Practice

**Exercise 1.** In `ShowStudents`, also show the **total number of BSIT students** at the bottom. (Hint: loop through the list and count where `s.Course == "BSIT"`.)

<details>
<summary>Show answer</summary>

At the end of `ShowStudents`, add:

```cs
int bsitCount = 0;

foreach (Student s in students)
{
    if (s.Course == "BSIT")
    {
        bsitCount++;
    }
}

Console.WriteLine($"BSIT students in this list: {bsitCount}");
```

</details>

**Exercise 2.** Let the user type `yes` in any capitalization (`yes`, `Yes`, `YES`) to confirm a delete.

<details>
<summary>Show answer</summary>

In `DeleteStudentScreen`, change one line:

```cs
string answer = (Console.ReadLine() ?? "").Trim().ToUpper();
```

Now `yes` becomes `YES` before the comparison.

</details>

**Exercise 3.** Add menu option **"Search by course"**. Add a new method `SearchByCourse(string course)` to `StudentDatabase.cs`, and a new `case` in `Program.cs`. Move **Exit** to option 7.

<details>
<summary>Show answer</summary>

In `StudentDatabase.cs` (inside the class, next to `SearchByName`):

```cs
public static List<Student> SearchByCourse(string course)
{
    using var connection = OpenConnection();
    using var command = connection.CreateCommand();
    command.CommandText = @"
        SELECT Id, StudentNumber, FullName, Course, YearLevel
        FROM Students
        WHERE Course = $course
        ORDER BY FullName;";
    command.Parameters.AddWithValue("$course", course);

    return ReadStudents(command);
}
```

In `Program.cs`, add the menu line `Console.WriteLine("6 - Search by course");`, change Exit to `7 - Exit`, and change the cases:

```cs
case "6":
    string course = ReadRequired("Course: ").ToUpper();
    ShowStudents(StudentDatabase.SearchByCourse(course));
    break;
case "7":
    running = false;
    Console.WriteLine("Goodbye!");
    break;
```

Also update the `default` message to say `1 to 7`.

</details>

---

## 📌 Recap

- **CRUD** = Create (`INSERT`), Read (`SELECT`), Update (`UPDATE`), Delete (`DELETE`).
- `ExecuteReader()` + `while (reader.Read())` + `reader.GetInt32(0)` / `reader.GetString(1)` reads many rows.
- Split big programs into files with **one job each**: the model (`Student`), the database code (`StudentDatabase`), and the screens (`Program`).
- A `static` class is used directly by its name. `private` members are for the class's own use only.
- `Student?` means "a Student or `null`". Always check for `null` before using the result.
- `LIKE '%text%'` finds rows that **contain** the text.
- Always **confirm** before deleting.

---

[⬅️ Lesson 14](14-inserting-data.md) | [🏠 Course Home](../README.md) | [Next: Lesson 16 (Uploading a CSV File) ➡️](16-uploading-csv-data.md)
