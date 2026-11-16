# Lesson 15: Student Records Manager (Create, Read, Update, Delete)

**Part 3: Databases** · About 2 hours (you can split it into two sittings)

## What You Will Learn

You will build a complete **Student Records Manager**. It can:

- **Add** students
- **Show** all students in a neat table
- **Search** students by name
- **Update** a student's information
- **Delete** a student

You will also learn to **read many rows** from a database, and to split a program into **several files**.

## The Idea: CRUD

Almost every business system in the world does four things with data. Programmers call them **CRUD**:

| Letter | Action | SQL | Example |
|---|---|---|---|
| **C** | **C**reate | `INSERT` | Enroll a new student |
| **R** | **R**ead | `SELECT` | Show the class list |
| **U** | **U**pdate | `UPDATE` | Fix a misspelled name |
| **D** | **D**elete | `DELETE` | Remove a student who left |

An enrollment system, a library system, an inventory system, a payroll system: at the center, they are all CRUD. **After this lesson, you can build the center of any of them.**

### One job per file

This program is bigger, so it is split into **three files**. Each file has **one job**:

| File | Its job | Like a... |
|---|---|---|
| `Student.cs` | Says what a student **is** | Blank form |
| `StudentDatabase.cs` | **All** the SQL. It talks to the database | Records office |
| `Program.cs` | The menu and the screens. It talks to the **user** | Front desk |

The front desk (`Program.cs`) never writes SQL. It just asks the records office (`StudentDatabase`): *"Please add this student,"* or *"Please give me all the students."*

---

## Step by Step

### Step 1: Create a new project named `Lesson15`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson15`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

1. In the **Solution Explorer**, right-click the project line **Lesson15**.
2. Click **Manage NuGet Packages...**
3. Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.
4. Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**.
5. If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.
6. Close the NuGet tab (click the **X** on the tab **NuGet: Lesson15**).

### Step 3: Add the file `Student.cs`

1. In the **Solution Explorer**, right-click the project line **Lesson15**.
2. Move to **Add**, then click **Class...**
3. In the **Name** box, type `Student.cs`. Click **Add**.
4. In the new **Student.cs** tab, press **Ctrl + A**, then **Delete**.
5. Put in this code:

## The Code

**File: `Student.cs`**

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

This is the `Student` class from Lesson 09, with one new property: `Id`, for the database's primary key.

### Step 4: Add the file `StudentDatabase.cs`

1. In the **Solution Explorer**, right-click the project line **Lesson15**.
2. Move to **Add**, then click **Class...**
3. In the **Name** box, type `StudentDatabase.cs`. Click **Add**.
4. In the new **StudentDatabase.cs** tab, press **Ctrl + A**, then **Delete**.
5. Put in this code:

**File: `StudentDatabase.cs`**

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

### Step 5: Put the main program in `Program.cs`

1. In the **Solution Explorer**, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

**File: `Program.cs`**

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

### Step 6: Check your files

Look at the **Solution Explorer**. You should see these three files under **Lesson15**:

```text
Lesson15
   Dependencies
   Program.cs
   Student.cs
   StudentDatabase.cs
```

### Step 7: Run it

Press **Ctrl + F5**. Try every option!

## The Output

✅ Here is an example. (`...` means the menu is shown again.)

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

**Exit the program and run it again.** Choose option 2. Everything you did is still there, because it was saved in the database.

---

## What the Code Means: `StudentDatabase.cs`

### Reading many rows with `ExecuteReader`

`ExecuteScalar` gives back **one** value. To read **many rows**, use `ExecuteReader`. It gives you a **reader** that moves through the rows **one at a time**, like moving your finger down a list:

```cs
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

- `reader.Read()` moves to the **next row**. It gives `true` if there is a row, and `false` when there are no more.
- `while (reader.Read())` repeats for **every** row.
- `reader.GetInt32(0)` gets the value in **column 0** of this row, as an `int`.
- `reader.GetString(2)` gets the value in **column 2**, as a `string`.

The column numbers follow the order in your `SELECT`, **starting from 0**:

```text
SELECT Id, StudentNumber, FullName, Course, YearLevel FROM Students
       0   1              2         3       4
```

So for **each row**, the code makes one `Student` object, fills it in, and adds it to a list.

### `static`: no object needed

```cs
public static class StudentDatabase
```

In Lesson 09, you had to make an object (`new Student()`) before using it. A **`static`** class is different: you use it **directly by its name**, with no `new`.

```cs
StudentDatabase.CreateTable();
```

That makes sense here. There is only **one** records office. Every method inside a `static` class must be `static` too.

### `public` and `private`

- `public` → **any** file can use it. `Program.cs` can call it.
- `private` → **only this class** can use it. It is a helper for the class itself.

`OpenConnection` and `ReadStudents` are `private`. The front desk does not need to know how the records office opens its filing cabinet.

### `OpenConnection`: write it once

```cs
private static SqliteConnection OpenConnection()
{
    SqliteConnection connection = new SqliteConnection(ConnectionString);
    connection.Open();
    return connection;
}
```

In Lesson 14, every method repeated the same two lines to open a connection. Now they are written **once**. Every method just says `using var connection = OpenConnection();`

### `SearchByName`: finding part of a name with `LIKE`

```cs
WHERE FullName LIKE $pattern
```

```cs
command.Parameters.AddWithValue("$pattern", "%" + keyword + "%");
```

`LIKE` searches for a **pattern**. `%` means *"any text here (or nothing)"*. So `%santos%` matches every name that **contains** "santos". In SQLite, `LIKE` ignores capital letters.

Notice: the `%` signs are added to the **value**, and the value still goes in as a **parameter**. Always!

### `FindByStudentNumber`: `Student?` and `null`

```cs
public static Student? FindByStudentNumber(string studentNumber)
```

The `?` after `Student` means: *"this method gives back a Student, **or nothing**."* In C#, "nothing" is called **`null`**.

```cs
if (results.Count == 0)
{
    return null;     // not found
}

return results[0];   // found
```

### `UpdateStudent` and `DeleteStudent`: did it work?

```cs
int rowsChanged = command.ExecuteNonQuery();
return rowsChanged > 0;
```

`ExecuteNonQuery` gives back **how many rows were changed**. More than 0 means it worked.

Both use `WHERE Id = $id`. The `Id` is the primary key, so it is the safest way to change **exactly one** row.

---

## What the Code Means: `Program.cs`

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

In a `void` method, `return;` means *"stop this method now."* The rest of the screen is skipped, and the menu comes back.

### Checking for `null`

```cs
Student? student = StudentDatabase.FindByStudentNumber(studentNumber);

if (student == null)
{
    Console.WriteLine("No student has that student number.");
    return;
}
```

**Always** check for `null` before you use the result.

### Neat columns

```cs
Console.WriteLine($"{s.StudentNumber,-12}{s.FullName,-25}{s.Course,-8}{s.YearLevel}");
```

`,-12` after a value means: *"give this value 12 spaces, and put the text on the left."* That makes the columns line up like a table.

### A safe delete

```cs
Console.Write($"Delete {student.FullName}? Type YES to confirm: ");
string answer = Console.ReadLine() ?? "";

if (answer == "YES" && StudentDatabase.DeleteStudent(student.Id))
```

A delete cannot be undone. So the program asks the user to **confirm** first, and deletes only if they type exactly `YES`.

> [!TIP]
> Now look again at the **SQL Playground** code in [Lesson 12](12-what-is-a-database.md). You will recognize `using var`, `CreateCommand`, `ExecuteReader`, `reader.Read()`, and `try` / `catch`. You can understand all of it now!

---

## If Something Goes Wrong

**Names and student numbers are mixed up on the screen.**
A column number is wrong, like `FullName = reader.GetString(1)`. Count the columns in your `SELECT`, starting from **0**.

**Error List: `CS0029  Cannot implicitly convert type 'string' to 'int'`**
You used the wrong `Get` for a column, like `YearLevel = reader.GetString(4)`. Use `GetInt32` for `INTEGER` columns, and `GetString` for `TEXT` columns.

**Error List: `CS0103  The name 'StudentDatabase' does not exist in the current context`**
Check that the file `StudentDatabase.cs` is in the Solution Explorer, and that its code starts with `public static class StudentDatabase`. Check that you deleted **all** the starter code before pasting.

**Error List: `CS0234 ... 'Data' does not exist in the namespace 'Microsoft'`**
The SQLite package is missing. Do Step 2 again.

**A green wavy line, and a warning about a "possibly null reference".**
You used a `Student?` without checking for `null` first. Add the `if (student == null)` check.

---

## Exercise

**Exercise 1.** Let the user type `yes` in any way (`yes`, `Yes`, `YES`) to confirm a delete.

<details>
<summary>Show answer</summary>

In `DeleteStudentScreen`, change one line:

```cs
string answer = (Console.ReadLine() ?? "").Trim().ToUpper();
```

Now `yes` becomes `YES` before it is compared.

</details>

**Exercise 2.** At the bottom of the student table, also show **how many BSIT students** are in the list. (Hint: loop through the list, and count the ones where `s.Course == "BSIT"`.)

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

## ⭐ Bonus Challenge

Add a new menu option: **Search by course**. This needs changes in **two** files:

1. A new method `SearchByCourse(string course)` in `StudentDatabase.cs`
2. A new menu line and a new `case` in `Program.cs` (move **Exit** to option 7)

Hint: `SearchByCourse` looks almost like `SearchByName`, but it uses `WHERE Course = $course`.

<details>
<summary>Show answer</summary>

In `StudentDatabase.cs`, inside the class, next to `SearchByName`:

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

Also change the `default` message to say `1 to 7`.

</details>

---

## Remember

- **CRUD** = Create (`INSERT`), Read (`SELECT`), Update (`UPDATE`), Delete (`DELETE`).
- `ExecuteReader()` + `while (reader.Read())` + `reader.GetInt32(0)` / `reader.GetString(1)` reads many rows.
- Split a big program into files with **one job each**.
- A `static` class is used directly by its name. `private` things are only for the class itself.
- `Student?` means "a Student, or `null`". Always check for `null`.
- `LIKE '%text%'` finds rows that **contain** the text.
- Always **confirm** before deleting.

---

[⬅️ Lesson 14](14-inserting-data.md) | [🏠 Course Home](../README.md) | [Next: Lesson 16 (Uploading a CSV File) ➡️](16-uploading-csv-data.md)
