# Lesson 16: Uploading Many Records From a CSV File

| | |
|---|---|
| **Part** | 3: Databases |
| **Time** | About 90 minutes |
| **Before this** | [Lesson 15](15-crud-app.md) |

## 🎯 Goal

By the end of this lesson, you will build an **upload tool** that:

- Reads a **CSV file** (like one exported from Excel)
- **Checks** every row and reports the problems, with line numbers
- **Uploads** all the good rows into the database at once, using a **transaction**
- **Skips** students who are already saved, instead of crashing

---

## 🧠 The Big Idea

The registrar sends you an Excel file with **500 new students**. Typing them one by one into the Lesson 15 program would take all day, and you would surely make typing mistakes.

Instead, real systems have an **upload** (or **import**) feature:

```text
 students.csv  ──►  1. Read every line
                    2. Check every row (report problems)
                    3. Ask: "Upload the good rows?"
                    4. Save them all at once  ──►  school.db
```

### New idea 1: Never trust a file

Files made by people **always** have mistakes: a missing name, letters where a number should be, the same student twice. A good upload tool **checks every row** and tells the user exactly which lines have problems, so they can fix them.

### New idea 2: Transactions (all or nothing)

A **transaction** groups many database changes together, so that they are treated as **one** change:

- If everything works, `Commit()` saves **all** the changes.
- If anything crashes in the middle, **none** of the changes are saved. The database goes back to how it was.

Think of sending money with a mobile wallet: the money must leave your account **and** arrive in your friend's account. If only half of that happened, someone would lose money. A transaction makes sure it is **all or nothing**.

There is a bonus: saving 500 rows **inside one transaction** is much **faster** than saving them one at a time.

### New idea 3: `INSERT OR IGNORE`

```sql
INSERT OR IGNORE INTO Students (...) VALUES (...);
```

`OR IGNORE` means: *"If this row would break a rule (like the `UNIQUE` student number), **skip it quietly** instead of crashing."* Then `ExecuteNonQuery()` gives back **0** (no rows changed), so we know it was skipped.

---

## 🛠️ Let's Build It

### Step 1: Create the project and add SQLite

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson16
cd Lesson16
dotnet add package Microsoft.Data.Sqlite
```

### Step 2: Copy the files from Lesson 15

You already wrote `Student.cs` and `StudentDatabase.cs`. Reuse them! In the Command Prompt (inside `Lesson16`), type:

```cmd
copy ..\Lesson15\Student.cs .
```

```cmd
copy ..\Lesson15\StudentDatabase.cs .
```

After each command, you should see `1 file(s) copied.`

| Part | Meaning |
|---|---|
| `copy` | Copy a file |
| `..\Lesson15\Student.cs` | The file to copy. `..` means "go up one folder", so this is `C:\CSharpCourse\Lesson15\Student.cs` |
| `.` | Copy it **here** (the current folder) |

> [!NOTE]
> This project gets its **own new** `school.db` file, so it starts with **no** students. Your Lesson 15 data is safe in the `Lesson15` folder.

Now open VS Code:

```cmd
code .
```

### Step 3: Add the `ImportStudents` method to `StudentDatabase.cs`

Open `StudentDatabase.cs`. Find the line that says `// Helper: turns the rows returned by a SELECT into a List of Student objects`. **Just above** that line, add this new method:

```cs
    // IMPORT: save many students at once, inside one transaction.
    // Gives back how many students were actually added.
    // Students whose number is already saved are skipped (not added twice).
    public static int ImportStudents(List<Student> students)
    {
        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction();

        int addedCount = 0;

        foreach (Student student in students)
        {
            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT OR IGNORE INTO Students (StudentNumber, FullName, Course, YearLevel)
                VALUES ($studentNumber, $fullName, $course, $yearLevel);";
            command.Parameters.AddWithValue("$studentNumber", student.StudentNumber);
            command.Parameters.AddWithValue("$fullName", student.FullName);
            command.Parameters.AddWithValue("$course", student.Course);
            command.Parameters.AddWithValue("$yearLevel", student.YearLevel);

            int rowsChanged = command.ExecuteNonQuery();
            addedCount = addedCount + rowsChanged;
        }

        transaction.Commit();
        return addedCount;
    }

```

<details>
<summary>Show the complete <code>StudentDatabase.cs</code> after this change</summary>

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

    // IMPORT: save many students at once, inside one transaction.
    // Gives back how many students were actually added.
    // Students whose number is already saved are skipped (not added twice).
    public static int ImportStudents(List<Student> students)
    {
        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction();

        int addedCount = 0;

        foreach (Student student in students)
        {
            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT OR IGNORE INTO Students (StudentNumber, FullName, Course, YearLevel)
                VALUES ($studentNumber, $fullName, $course, $yearLevel);";
            command.Parameters.AddWithValue("$studentNumber", student.StudentNumber);
            command.Parameters.AddWithValue("$fullName", student.FullName);
            command.Parameters.AddWithValue("$course", student.Course);
            command.Parameters.AddWithValue("$yearLevel", student.YearLevel);

            int rowsChanged = command.ExecuteNonQuery();
            addedCount = addedCount + rowsChanged;
        }

        transaction.Commit();
        return addedCount;
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

</details>

### Step 4: Create the CSV file

In VS Code, create a new file named **`students.csv`** (in the same folder as `Program.cs`) and type these lines **exactly**. Some lines have mistakes **on purpose**, so you can see the checks work.

```text
StudentNumber,FullName,Course,YearLevel
2026-0101,Maria Santos,BSIT,1
2026-0102,Jose Garcia,BSCS,2
2026-0103,Ana Reyes,BSIT,3
2026-0104,,BSIT,1
2026-0105,Mark Villanueva,BSIS,abc
2026-0106,Liza Bautista,BSIT,4
2026-0101,Maria Santos,BSIT,1
2026-0107,Carlo Mendoza,BSCS
```

The first line is the **header**: it names the columns. Here are the mistakes hidden in this file:

| Line | Problem |
|---|---|
| 5 | The full name is missing (`,,`) |
| 6 | The year level is `abc`, not a number |
| 8 | Maria Santos (`2026-0101`) appears **twice** |
| 9 | Only 3 values. The year level is missing |

### Step 5: Write `Program.cs`

Replace everything in `Program.cs` with:

```csharp
// Lesson 16: Uploading many students from a CSV file into the database

StudentDatabase.CreateTable();

Console.WriteLine("===== IMPORT STUDENTS FROM A CSV FILE =====");
Console.Write("File name (just press Enter to use students.csv): ");
string fileName = (Console.ReadLine() ?? "").Trim();

if (fileName == "")
{
    fileName = "students.csv";
}

if (!File.Exists(fileName))
{
    Console.WriteLine($"File not found: {fileName}");
    Console.WriteLine("Make sure the file is inside your project folder.");
    return;
}

// ----- Step 1: Read and check every line -----
string[] lines = File.ReadAllLines(fileName);
List<Student> validStudents = new List<Student>();
List<string> problems = new List<string>();
int rowsChecked = 0;

// i starts at 1 to skip line 0, which is the header row
for (int i = 1; i < lines.Length; i++)
{
    int lineNumber = i + 1; // people count lines starting from 1
    string line = lines[i].Trim();

    if (line == "")
    {
        continue; // skip empty lines
    }

    rowsChecked++;
    string[] parts = line.Split(',');

    if (parts.Length != 4)
    {
        problems.Add($"Line {lineNumber}: needs 4 values but has {parts.Length}.");
        continue;
    }

    string studentNumber = parts[0].Trim();
    string fullName = parts[1].Trim();
    string course = parts[2].Trim();
    string yearText = parts[3].Trim();

    if (studentNumber == "" || fullName == "" || course == "")
    {
        problems.Add($"Line {lineNumber}: student number, full name, and course are required.");
        continue;
    }

    bool isNumber = int.TryParse(yearText, out int yearLevel);

    if (!isNumber || yearLevel < 1 || yearLevel > 5)
    {
        problems.Add($"Line {lineNumber}: year level must be a number from 1 to 5.");
        continue;
    }

    validStudents.Add(new Student
    {
        StudentNumber = studentNumber,
        FullName = fullName,
        Course = course,
        YearLevel = yearLevel
    });
}

// ----- Step 2: Show a report before saving -----
Console.WriteLine();
Console.WriteLine($"Rows checked:       {rowsChecked}");
Console.WriteLine($"Valid rows:         {validStudents.Count}");
Console.WriteLine($"Rows with problems: {problems.Count}");

foreach (string problem in problems)
{
    Console.WriteLine($"  - {problem}");
}

if (validStudents.Count == 0)
{
    Console.WriteLine("There is nothing to import.");
    return;
}

// ----- Step 3: Ask before saving -----
Console.WriteLine();
Console.Write($"Upload the {validStudents.Count} valid row(s) to the database? (Y/N): ");
string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

if (answer != "Y")
{
    Console.WriteLine("Upload cancelled. Nothing was saved.");
    return;
}

// ----- Step 4: Save everything in one transaction -----
int addedCount = StudentDatabase.ImportStudents(validStudents);
int skippedCount = validStudents.Count - addedCount;

Console.WriteLine($"Done! {addedCount} student(s) added.");

if (skippedCount > 0)
{
    Console.WriteLine($"{skippedCount} student(s) skipped because the student number was already saved.");
}

// ----- Step 5: Show what is now in the database -----
Console.WriteLine();
Console.WriteLine("===== STUDENTS IN THE DATABASE =====");

foreach (Student s in StudentDatabase.GetAllStudents())
{
    Console.WriteLine($"{s.StudentNumber,-12}{s.FullName,-25}{s.Course,-8}{s.YearLevel}");
}
```

### Step 6: Run it

```cmd
dotnet run
```

When asked for the file name, just press **Enter**. When asked to upload, type **`Y`**.

✅ **You should see:**

```text
===== IMPORT STUDENTS FROM A CSV FILE =====
File name (just press Enter to use students.csv):

Rows checked:       8
Valid rows:         5
Rows with problems: 3
  - Line 5: student number, full name, and course are required.
  - Line 6: year level must be a number from 1 to 5.
  - Line 9: needs 4 values but has 3.

Upload the 5 valid row(s) to the database? (Y/N): Y
Done! 4 student(s) added.
1 student(s) skipped because the student number was already saved.

===== STUDENTS IN THE DATABASE =====
2026-0103   Ana Reyes                BSIT    3
2026-0102   Jose Garcia              BSCS    2
2026-0106   Liza Bautista            BSIT    4
2026-0101   Maria Santos             BSIT    1
```

Look closely:

- **3 bad rows** were reported with their **line numbers**, and were **not** saved.
- **5 rows** passed the checks. But one was the duplicate Maria Santos, so only **4** were added and **1** was skipped.

**Run it again** and answer `Y`. This time, `0 student(s) added` and `5 ... skipped`, because they are all saved already. No crash, no duplicates. 🎉

---

## 🔎 Line by Line

### Stopping the program early

```cs
if (!File.Exists(fileName))
{
    Console.WriteLine($"File not found: {fileName}");
    ...
    return;
}
```

In the main program (not inside a method), `return;` **ends the whole program**. There is no point continuing if the file does not exist.

### Skipping the header

```cs
for (int i = 1; i < lines.Length; i++)
```

The loop starts at **`i = 1`**, not 0. Line 0 is the header (`StudentNumber,FullName,...`), which is not a student.

```cs
int lineNumber = i + 1;
```

Array indexes start at 0, but people (and Notepad) count lines starting from 1. We add 1 so the problem report matches what the user sees in the file.

### Checking each row: `continue`

```cs
if (parts.Length != 4)
{
    problems.Add($"Line {lineNumber}: needs 4 values but has {parts.Length}.");
    continue;
}
```

If a check fails, we write down the problem and use `continue` (Lesson 06) to **skip to the next line**. Only rows that pass **every** check reach the end of the loop and are added to `validStudents`.

### Two lists

```cs
List<Student> validStudents = new List<Student>();
List<string> problems = new List<string>();
```

Good rows go into `validStudents`. Problem messages go into `problems`. Nothing is saved to the database while checking.

### Asking before saving

```cs
string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

if (answer != "Y")
```

Uploading changes the database, so we show the report and **ask first**. `.ToUpper()` lets the user type `y` or `Y`.

### The transaction (in `ImportStudents`)

```cs
using var connection = OpenConnection();
using var transaction = connection.BeginTransaction();
...
transaction.Commit();
```

1. `BeginTransaction()` starts the "all or nothing" group. Every command on this connection is now part of it.
2. The loop inserts each student.
3. `Commit()` saves everything **for real**.

If the program crashes **before** `Commit()`, the `using` automatically **cancels** the transaction (this is called a **rollback**), and nothing is saved.

```cs
int rowsChanged = command.ExecuteNonQuery();
addedCount = addedCount + rowsChanged;
```

With `INSERT OR IGNORE`, `rowsChanged` is **1** if the student was added and **0** if skipped. Adding them all up gives the number of students actually added.

---

## 🧠 Real Life: Making the CSV in Excel

In real work, you will get the data in Excel. To turn an Excel sheet into a CSV file:

1. Make sure row 1 has the headers: `StudentNumber`, `FullName`, `Course`, `YearLevel`.
2. Click **File** → **Save As**.
3. In **"Save as type"**, choose **CSV UTF-8 (Comma delimited) (\*.csv)**.
4. Save it in your project folder (`C:\CSharpCourse\Lesson16`).
5. If Excel asks about features that may be lost, click **Yes** (keep the CSV format).

> [!WARNING]
> Our simple program splits lines at **every comma**. A name that contains a comma (like `Dela Cruz, Juan`) would break that row. Write names without commas, like `Juan Dela Cruz`.

---

## ⚠️ Common Mistakes

### Mistake 1: `File not found: students.csv`

The file must be in the **project folder**, the same folder as `Program.cs`. In the Command Prompt, type `dir` and check that `students.csv` is in the list.

### Mistake 2: Every row says `needs 4 values`

Your file may use **semicolons** (`;`) instead of commas. This happens with Excel on some computers. **Fix:** Open the file in Notepad and check. Save it again from Excel as **CSV UTF-8 (Comma delimited)**.

### Mistake 3: The first student is reported as a problem

You may have forgotten the **header line**. The program always skips line 1. **Fix:** Make sure the first line of the file is `StudentNumber,FullName,Course,YearLevel`.

### Mistake 4: `error CS0117: 'StudentDatabase' does not contain a definition for 'ImportStudents'`

The new method is missing, or was pasted **outside** the class. **Fix:** It must be **inside** the class's `{ }`, just above the `ReadStudents` helper. Compare with the complete file in Step 3.

---

## 🏋️ Practice

**Exercise 1.** Fix the 3 problem lines in `students.csv` (give them a name, a number year level, and a 4th value). Run the upload again. How many students are added now?

<details>
<summary>Show answer</summary>

For example, change the lines to:

```text
2026-0104,Rico Dizon,BSIT,1
2026-0105,Mark Villanueva,BSIS,2
2026-0107,Carlo Mendoza,BSCS,1
```

Now all 8 rows are valid. The 4 students already saved (plus the duplicate Maria Santos row) are skipped, and the **3** fixed ones are added: `Done! 3 student(s) added.` and `5 student(s) skipped...`

</details>

**Exercise 2.** Add a check: the course must be `BSIT`, `BSCS`, or `BSIS`. Otherwise, report `Line X: unknown course.` (Hint: make a `List<string>` of allowed courses and use `.Contains`.)

<details>
<summary>Show answer</summary>

Before the `for` loop, add:

```cs
List<string> allowedCourses = new List<string>();
allowedCourses.Add("BSIT");
allowedCourses.Add("BSCS");
allowedCourses.Add("BSIS");
```

Inside the loop, after the "required" check, add:

```cs
if (!allowedCourses.Contains(course.ToUpper()))
{
    problems.Add($"Line {lineNumber}: unknown course '{course}'.");
    continue;
}
```

</details>

**Exercise 3.** **Export.** Write the opposite tool: a program (in a new project, `Lesson16Export`, with the same `Student.cs` and `StudentDatabase.cs`) that saves every student in the database into `export.csv`, with a header line. (Hint: build a `List<string>` of lines, then `File.WriteAllLines("export.csv", lines);`)

<details>
<summary>Show answer</summary>

Copy `Student.cs`, `StudentDatabase.cs`, and `school.db` from `Lesson16` into the new project. Add the SQLite package. Then `Program.cs`:

```cs
List<string> lines = new List<string>();
lines.Add("StudentNumber,FullName,Course,YearLevel");

foreach (Student s in StudentDatabase.GetAllStudents())
{
    lines.Add($"{s.StudentNumber},{s.FullName},{s.Course},{s.YearLevel}");
}

File.WriteAllLines("export.csv", lines);
Console.WriteLine($"Exported {lines.Count - 1} student(s) to export.csv");
```

`lines.Count - 1` because the header line is not a student. You can open `export.csv` in Excel!

</details>

---

## 📌 Recap

- An **upload/import** tool reads a file, **checks every row**, reports problems with **line numbers**, and saves the good rows.
- Skip the header by starting the loop at index 1. Show `i + 1` as the line number.
- `continue` skips a bad row. Keep good rows and problem messages in separate lists.
- A **transaction** (`BeginTransaction()` ... `Commit()`) is **all or nothing**, and much faster for many rows.
- `INSERT OR IGNORE` skips rows that break a rule. `ExecuteNonQuery()` returns 0 for a skipped row.
- In Excel, use **Save As → CSV UTF-8 (Comma delimited)**.

---

[⬅️ Lesson 15](15-crud-app.md) | [🏠 Course Home](../README.md) | [Next: Lesson 17 (What Is Hashing?) ➡️](17-what-is-hashing.md)
