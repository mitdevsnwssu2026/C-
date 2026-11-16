# Lesson 16: Uploading Many Records From a CSV File

**Part 3: Databases** · About 90 minutes

## What You Will Learn

You will build an **upload tool** that:

- Reads a **CSV file** (like one saved from Excel)
- **Checks** every row, and reports the problems with their line numbers
- **Uploads** all the good rows into the database at once
- **Skips** students who are already saved, instead of crashing

## The Idea

The registrar sends you an Excel file with **500 new students**. Typing them one by one into the Lesson 15 program would take all day, and you would surely make typing mistakes.

Real systems have an **upload** feature instead:

```text
 students.csv  ──►  1. Read every line
                    2. Check every row, and report the problems
                    3. Ask: "Upload the good rows?"
                    4. Save them all at once  ──►  the database
```

Three new ideas make this work.

**New idea 1: Never trust a file.** Files made by people **always** have mistakes: a missing name, letters where a number should be, the same student twice. A good upload tool **checks every row** and tells the user which lines are wrong.

**New idea 2: Transactions (all or nothing).** A **transaction** groups many database changes into **one**. Either **all** of them are saved, or **none** of them are.

Think of sending money with GCash. The money must leave your account **and** arrive in your friend's account. If only half of that happened, someone would lose money. A transaction makes sure it is **all or nothing**. As a bonus, saving 500 rows in one transaction is also much **faster**.

**New idea 3: `INSERT OR IGNORE`.** This SQL means: *"If this row breaks a rule (like a duplicate student number), **skip it quietly** instead of crashing."*

---

## Step by Step

### Step 1: Create a new project named `Lesson16`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson16`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the SQLite package

1. In the **Solution Explorer**, right-click the project line **Lesson16**.
2. Click **Manage NuGet Packages...**
3. Click **Browse**. In the search box, type `Microsoft.Data.Sqlite`.
4. Click the result named exactly **Microsoft.Data.Sqlite**. Then click **Install**.
5. If **Preview Changes** appears, click **Apply**. If **License Acceptance** appears, click **I Accept**.
6. Close the NuGet tab.

### Step 3: Bring in two files from Lesson 15

You already wrote `Student.cs` and `StudentDatabase.cs`. Do not type them again. **Reuse them!**

**1.** In the **Solution Explorer**, right-click the project line **Lesson16**.

**2.** Move to **Add**, then click **Existing Item...**

✅ **You will see:** a window for choosing files.

**3.** Click the **address bar** at the top of that window. Type `C:\CSharpCourse\Lesson15` and press **Enter**.

**4.** Click **Student.cs** once. Then hold the **Ctrl** key and click **StudentDatabase.cs**. Both files are now highlighted.

**5.** Click **Add**.

✅ **You will see:** `Student.cs` and `StudentDatabase.cs` appear in the Solution Explorer, under **Lesson16**. Visual Studio made **copies** of them for this project.

> This project has its **own new database**, so it starts with **no students**. Your Lesson 15 data is safe in the Lesson 15 project.

### Step 4: Add one new method to `StudentDatabase.cs`

**1.** In the Solution Explorer, double-click **StudentDatabase.cs**.

**2.** Scroll down and find this line, near the bottom:

```cs
    // Helper: turns the rows returned by a SELECT into a List of Student objects
```

**3.** Click at the very start of that line, and press **Enter** twice to make some empty space above it.

**4.** In that empty space, paste this new method:

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
<summary>Not sure where it goes? Show the complete <code>StudentDatabase.cs</code></summary>

You can replace the whole file with this (**Ctrl + A**, **Delete**, then paste):

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

### Step 5: Create the CSV file

**1.** In the Solution Explorer, right-click the project line **Lesson16**.

**2.** Move to **Add**, then click **New Item...**

**3.** In the **Name** box, type `students.csv`

> If the window shows a big list of file types, click **Text File** in the list first. Then type the name.

**4.** Click **Add**.

✅ **You will see:** an empty tab named **students.csv**.

**5.** Type (or paste) these lines into it, exactly. Some lines have mistakes **on purpose**, so you can see the checks work.

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

The first line is the **header**. It names the columns. Here are the mistakes hidden in the file:

- **Line 5:** the full name is missing (`,,`)
- **Line 6:** the year level is `abc`, not a number
- **Line 8:** Maria Santos (`2026-0101`) appears a **second time**
- **Line 9:** there are only 3 values. The year level is missing

### Step 6: Tell Visual Studio to put the CSV file next to your program

Remember Lesson 11? Your program runs inside the **bin → Debug → net10.0** folder. The CSV file must be there too. This setting makes Visual Studio copy it there for you, every time you run.

**1.** In the Solution Explorer, click **students.csv** once, so it is highlighted.

**2.** Look at the **Properties** panel. It is just below the Solution Explorer.

> No Properties panel? Press the **F4** key.

**3.** In the Properties panel, find the line **Copy to Output Directory**.

**4.** Click the box to the right of it (it says **Do not copy**), and choose **Copy if newer**.

### Step 7: Put the main program in `Program.cs`

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Put in this code:

## The Code

**File: `Program.cs`**

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

### Step 8: Run it

1. Press **Ctrl + F5**.
2. When it asks for the file name, just press **Enter**.
3. When it asks to upload, type `Y` and press **Enter**.

## The Output

✅ **You will see:**

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

- **3 bad rows** were reported with their **line numbers**. They were **not** saved.
- **5 rows** passed the checks. One of them was the second Maria Santos. So **4** were added, and **1** was skipped.

**Run it again** and answer `Y`. This time it says `0 student(s) added` and `5 ... skipped`, because they are all saved already. No crash. No duplicates. 🎉

---

## What the Code Means

### Stopping the program early

```cs
if (!File.Exists(fileName))
{
    Console.WriteLine($"File not found: {fileName}");
    ...
    return;
}
```

In the main program (not inside a method), `return;` **ends the whole program**. There is no point in continuing if the file does not exist.

### Skipping the header

```cs
for (int i = 1; i < lines.Length; i++)
```

The loop starts at **`i = 1`**, not 0. Line 0 is the header, and the header is not a student.

```cs
int lineNumber = i + 1;
```

Indexes start at 0, but people count lines from 1. Adding 1 makes the problem report match what the user sees in the file.

### Checking each row

```cs
if (parts.Length != 4)
{
    problems.Add($"Line {lineNumber}: needs 4 values but has {parts.Length}.");
    continue;
}
```

If a check fails, the program writes down the problem, and `continue` **skips to the next line** (Lesson 06). Only rows that pass **every** check reach the end of the loop and are added to `validStudents`.

### Two lists

```cs
List<Student> validStudents = new List<Student>();
List<string> problems = new List<string>();
```

Good rows go into `validStudents`. Problem messages go into `problems`. Nothing is saved to the database during the checking.

### Asking before saving

```cs
string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

if (answer != "Y")
```

An upload changes the database. So the program shows the report and **asks first**. `.ToUpper()` lets the user type `y` or `Y`.

### The transaction (in `ImportStudents`)

```cs
using var connection = OpenConnection();
using var transaction = connection.BeginTransaction();
...
transaction.Commit();
```

1. `BeginTransaction()` starts the "all or nothing" group.
2. The loop inserts each student.
3. `Commit()` saves everything **for real**.

If the program crashes **before** `Commit()`, the transaction is cancelled automatically, and nothing is saved.

```cs
int rowsChanged = command.ExecuteNonQuery();
addedCount = addedCount + rowsChanged;
```

With `INSERT OR IGNORE`, `rowsChanged` is **1** if the student was added, and **0** if the student was skipped. Adding them all up gives the number of students that were really added.

---

## Real Life: Making the CSV in Excel

At work, the data usually comes in Excel. To save an Excel sheet as a CSV file:

1. Make sure row 1 has the headers: `StudentNumber`, `FullName`, `Course`, `YearLevel`.
2. Click **File**, then **Save As**.
3. In **Save as type**, choose **CSV UTF-8 (Comma delimited) (\*.csv)**.
4. Save it somewhere easy to find, like your Desktop.
5. Run the upload tool. When it asks for the file name, type the **full path** of the file, like `C:\Users\Juan\Desktop\students.csv`.

> [!WARNING]
> Our simple program cuts each line at **every comma**. A name with a comma inside (like `Dela Cruz, Juan`) would break that row. Write names without commas: `Juan Dela Cruz`.

---

## If Something Goes Wrong

**`File not found: students.csv`**
Step 6 was skipped. Click `students.csv` in the Solution Explorer, and set **Copy to Output Directory** to **Copy if newer** in the Properties panel. Then run again.

**Every row says `needs 4 values`.**
Your file may use **semicolons** (`;`) instead of commas. Open the file and check. Replace the semicolons with commas.

**The first student is reported as a problem, or is missing.**
The program always skips line 1. Make sure the first line of the file is the header: `StudentNumber,FullName,Course,YearLevel`

**Error List: `CS0117  'StudentDatabase' does not contain a definition for 'ImportStudents'`**
The new method is missing, or it was pasted **outside** the class. It must be **inside** the class's `{ }`. Use the complete file from Step 4.

**I changed `students.csv`, but the program still shows the old data.**
Make sure **Copy to Output Directory** is set to **Copy if newer** (Step 6), then run again.

---

## Exercise

**Exercise 1.** Fix the 3 problem lines in `students.csv`: give line 5 a name, give line 6 a real year level, and give line 9 a 4th value. Run the upload again. How many students are added now?

<details>
<summary>Show answer</summary>

For example, change those lines to:

```text
2026-0104,Rico Dizon,BSIT,1
2026-0105,Mark Villanueva,BSIS,2
2026-0107,Carlo Mendoza,BSCS,1
```

Now all 8 rows are valid. The 4 students already saved (plus the second Maria Santos row) are skipped, and the **3** fixed ones are added. You see `Done! 3 student(s) added.` and `5 student(s) skipped...`

</details>

**Exercise 2.** Add a new check: the course must be `BSIT`, `BSCS`, or `BSIS`. For anything else, report `Line X: unknown course`. (Hint: make a `List<string>` of allowed courses, and use `.Contains`.)

<details>
<summary>Show answer</summary>

Before the `for` loop, add:

```cs
List<string> allowedCourses = new List<string>();
allowedCourses.Add("BSIT");
allowedCourses.Add("BSCS");
allowedCourses.Add("BSIS");
```

Inside the loop, right after the "required" check, add:

```cs
if (!allowedCourses.Contains(course.ToUpper()))
{
    problems.Add($"Line {lineNumber}: unknown course '{course}'.");
    continue;
}
```

</details>

## ⭐ Bonus Challenge

**Build the opposite tool: an export.** Change `Program.cs` so that, after the upload, it also saves **every student in the database** into a new file named `export.csv`, with a header line. Then find the file (project → **bin** → **Debug** → **net10.0**) and open it in Excel or Notepad.

Hint: build a `List<string>` of lines, then save them all with `File.WriteAllLines("export.csv", lines);`

<details>
<summary>Show answer</summary>

Add this at the very end of `Program.cs`:

```cs
// ----- Step 6: Export everything to a new CSV file -----
List<string> exportLines = new List<string>();
exportLines.Add("StudentNumber,FullName,Course,YearLevel");

foreach (Student s in StudentDatabase.GetAllStudents())
{
    exportLines.Add($"{s.StudentNumber},{s.FullName},{s.Course},{s.YearLevel}");
}

File.WriteAllLines("export.csv", exportLines);
Console.WriteLine();
Console.WriteLine($"Exported {exportLines.Count - 1} student(s) to export.csv");
```

`exportLines.Count - 1`, because the header line is not a student.

</details>

---

## Remember

- An **upload** tool reads a file, **checks every row**, reports problems with **line numbers**, and saves the good rows.
- Start the loop at index 1 to skip the header. Show `i + 1` as the line number.
- `continue` skips a bad row. Keep good rows and problem messages in two lists.
- A **transaction** (`BeginTransaction()` ... `Commit()`) is **all or nothing**.
- `INSERT OR IGNORE` skips rows that break a rule.
- To reuse files: right-click the project → **Add** → **Existing Item...**
- A data file needs **Copy to Output Directory** = **Copy if newer**.

---

[⬅️ Lesson 15](15-crud-app.md) | [🏠 Course Home](../README.md) | [Next: Lesson 17 (What Is Hashing?) ➡️](17-what-is-hashing.md)
