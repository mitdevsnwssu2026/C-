# Lesson 18: Basic Data Entry (A Complete Entry Form)

**Part 2: Intermediate** · About 2 hours (you can split it into two sittings) · **Project lesson**

## What You Will Learn

You will build a complete **data entry form**: a program for typing records into a system. It will:

- Ask for each piece of information, **one field at a time**
- **Check every answer**, and ask again when an answer is not valid
- Let the user **review** the record before it is saved
- **Refuse duplicates** (the same student number twice)
- Show all the records in a neat **table**, and keep them in a **file**

This lesson has no new C# words. It combines **everything from Lessons 01 to 17** into one real program.

## The Idea

**Data entry** means typing information into a computer system. An encoder at the registrar's office does it all day: one student after another.

People make typing mistakes. So a good data entry program is **patient and careful**:

```text
1. Ask for one field       ──►  "Age (15-80): "
2. Check the answer        ──►  Not a number? Out of range? Ask again.
3. Repeat for every field
4. Show the whole record   ──►  "Please check the record:"
5. Ask before saving       ──►  "Save this record? (Y/N)"
6. Ask for the next record ──►  "Enter another record? (Y/N)"
```

**The golden rule of data entry: never let bad data in.** It is much easier to refuse a wrong answer now than to find and fix it later.

Here is what you already know, and where it is used:

| From | Used for |
|---|---|
| Lesson 04: String Manipulation | `.Trim()`, `.ToUpper()`, `.Contains()` to clean and check answers |
| Lessons 06–08: Operators and Conditionals | Checking that answers are valid |
| Lessons 09–10: Loops | Asking again, and entering many records |
| Lesson 12: Lists | Keeping all the records |
| Lesson 13: Functions | One small function for each kind of field |
| Lesson 14: Classes | One `StudentEntry` object for each record |
| Lesson 15: Enums | The list of courses |
| Lesson 17: Files | Keeping the records after the program closes |

---

## Step by Step

### Step 1: Create a new project named `Lesson18`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson18`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add the file `StudentEntry.cs`

This file describes **one record**.

1. In the **Solution Explorer**, right-click the project line **Lesson18**.
2. Move to **Add**, then click **Class...**
3. In the **Name** box, type `StudentEntry.cs`. Click **Add**.
4. In the new **StudentEntry.cs** tab, press **Ctrl + A**, then **Delete**.
5. Type this code:

## The Code

**File: `StudentEntry.cs`**

```csharp
// StudentEntry.cs - one record typed in by the user

public class StudentEntry
{
    public string StudentNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public int Age { get; set; }
    public Course Course { get; set; }
    public int YearLevel { get; set; }
    public string Email { get; set; } = "";
}

// The courses a student can choose from
public enum Course
{
    BSIT,
    BSCS,
    BSIS
}
```

> A small enum can live in the same file as the class that uses it. (Here the enum has the word `public`, because it sits next to a `public` class.)

### Step 3: Type the main program

This is the longest program so far. **Do not rush.** Type it in three sittings if you like: first the main program, then the "read one field" functions, then the "records" functions.

1. In the Solution Explorer, double-click **Program.cs**.
2. Press **Ctrl + A**, then **Delete**.
3. Type this code:

**File: `Program.cs`**

```csharp
// Lesson 18: Basic Data Entry - a student entry form

List<StudentEntry> entries = new List<StudentEntry>();
bool addMore = true;

Console.WriteLine("===== STUDENT DATA ENTRY =====");
Console.WriteLine("Type the information for each student.");
Console.WriteLine("If an answer is not valid, the form asks again.");

while (addMore)
{
    Console.WriteLine();
    Console.WriteLine($"--- Record {entries.Count + 1} ---");

    // 1. Fill in the form, one field at a time
    StudentEntry entry = new StudentEntry();
    entry.StudentNumber = ReadStudentNumber(entries);
    entry.FullName = ReadRequired("Full name: ");
    entry.Age = ReadNumber("Age (15-80): ", 15, 80);
    entry.Course = ReadCourse();
    entry.YearLevel = ReadNumber("Year level (1-5): ", 1, 5);
    entry.Email = ReadEmail();

    // 2. Let the user check the record
    Console.WriteLine();
    Console.WriteLine("Please check the record:");
    ShowEntry(entry);

    // 3. Save it only if the user says yes
    if (ReadYesNo("Save this record? (Y/N): "))
    {
        entries.Add(entry);
        Console.WriteLine("Record saved.");
    }
    else
    {
        Console.WriteLine("Record cancelled.");
    }

    addMore = ReadYesNo("Enter another record? (Y/N): ");
}

// 4. Show everything that was entered
Console.WriteLine();
Console.WriteLine($"===== ALL RECORDS ({entries.Count}) =====");
ShowTable(entries);

// 5. Keep the records in a file
SaveToFile(entries);


// ----- Functions that read ONE field -----

// Keeps asking until the user types something
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

// Keeps asking until the user types a whole number from min to max
int ReadNumber(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int number) && number >= min && number <= max)
        {
            return number;
        }

        Console.WriteLine($"Please type a whole number from {min} to {max}.");
    }
}

// Keeps asking until the student number is one that was not entered yet
string ReadStudentNumber(List<StudentEntry> list)
{
    while (true)
    {
        string studentNumber = ReadRequired("Student number: ");

        if (!NumberIsUsed(list, studentNumber))
        {
            return studentNumber;
        }

        Console.WriteLine("That student number was already entered. Please type a different one.");
    }
}

// Lets the user pick a course from a short menu
Course ReadCourse()
{
    while (true)
    {
        Console.WriteLine("Courses:  1 - BSIT   2 - BSCS   3 - BSIS");
        Console.Write("Choose a course (1-3): ");
        string choice = (Console.ReadLine() ?? "").Trim();

        if (choice == "1")
        {
            return Course.BSIT;
        }

        if (choice == "2")
        {
            return Course.BSCS;
        }

        if (choice == "3")
        {
            return Course.BSIS;
        }

        Console.WriteLine("Please type 1, 2, or 3.");
    }
}

// Keeps asking until the email looks like an email
string ReadEmail()
{
    while (true)
    {
        string email = ReadRequired("Email: ").ToLower();

        if (email.Contains("@") && email.Contains(".") && !email.Contains(" "))
        {
            return email;
        }

        Console.WriteLine("Please type a valid email, like juan@example.com");
    }
}

// Keeps asking until the user types Y or N. Gives back true for Y.
bool ReadYesNo(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (answer == "Y")
        {
            return true;
        }

        if (answer == "N")
        {
            return false;
        }

        Console.WriteLine("Please type Y or N.");
    }
}


// ----- Functions that work with the records -----

bool NumberIsUsed(List<StudentEntry> list, string studentNumber)
{
    foreach (StudentEntry item in list)
    {
        if (item.StudentNumber == studentNumber)
        {
            return true;
        }
    }

    return false;
}

void ShowEntry(StudentEntry item)
{
    Console.WriteLine($"  Student number: {item.StudentNumber}");
    Console.WriteLine($"  Full name:      {item.FullName}");
    Console.WriteLine($"  Age:            {item.Age}");
    Console.WriteLine($"  Course:         {item.Course}");
    Console.WriteLine($"  Year level:     {item.YearLevel}");
    Console.WriteLine($"  Email:          {item.Email}");
}

void ShowTable(List<StudentEntry> list)
{
    if (list.Count == 0)
    {
        Console.WriteLine("No records were saved.");
        return;
    }

    Console.WriteLine($"{"NUMBER",-12}{"FULL NAME",-22}{"AGE",-5}{"COURSE",-8}{"YEAR",-6}{"EMAIL"}");
    Console.WriteLine(new string('-', 72));

    foreach (StudentEntry item in list)
    {
        Console.WriteLine($"{item.StudentNumber,-12}{item.FullName,-22}{item.Age,-5}{item.Course,-8}{item.YearLevel,-6}{item.Email}");
    }
}

void SaveToFile(List<StudentEntry> list)
{
    if (list.Count == 0)
    {
        return;
    }

    foreach (StudentEntry item in list)
    {
        string line = $"{item.StudentNumber},{item.FullName},{item.Age},{item.Course},{item.YearLevel},{item.Email}";
        File.AppendAllText("entries.csv", line + Environment.NewLine);
    }

    Console.WriteLine();
    Console.WriteLine($"{list.Count} record(s) were added to the file entries.csv");
}
```

### Step 4: Run it

Press **Ctrl + F5**. Enter two students. **Make mistakes on purpose**, to see how the form reacts:

- Type letters for the age
- Type `9` for the course
- Type an email with no `@`
- For the second student, type the **same** student number as the first

## The Output

✅ **You will see something like this:**

```text
===== STUDENT DATA ENTRY =====
Type the information for each student.
If an answer is not valid, the form asks again.

--- Record 1 ---
Student number: 2026-0001
Full name: Juan Dela Cruz
Age (15-80): abc
Please type a whole number from 15 to 80.
Age (15-80): 18
Courses:  1 - BSIT   2 - BSCS   3 - BSIS
Choose a course (1-3): 9
Please type 1, 2, or 3.
Courses:  1 - BSIT   2 - BSCS   3 - BSIS
Choose a course (1-3): 1
Year level (1-5): 1
Email: Juan.DC@Example.com

Please check the record:
  Student number: 2026-0001
  Full name:      Juan Dela Cruz
  Age:            18
  Course:         BSIT
  Year level:     1
  Email:          juan.dc@example.com
Save this record? (Y/N): Y
Record saved.
Enter another record? (Y/N): Y

--- Record 2 ---
Student number: 2026-0001
That student number was already entered. Please type a different one.
Student number: 2026-0002
Full name: Maria Santos
Age (15-80): 19
Courses:  1 - BSIT   2 - BSCS   3 - BSIS
Choose a course (1-3): 2
Year level (1-5): 2
Email: maria
Please type a valid email, like juan@example.com
Email: maria@example.com

Please check the record:
  Student number: 2026-0002
  Full name:      Maria Santos
  Age:            19
  Course:         BSCS
  Year level:     2
  Email:          maria@example.com
Save this record? (Y/N): Y
Record saved.
Enter another record? (Y/N): N

===== ALL RECORDS (2) =====
NUMBER      FULL NAME             AGE  COURSE  YEAR  EMAIL
------------------------------------------------------------------------
2026-0001   Juan Dela Cruz        18   BSIT    1     juan.dc@example.com
2026-0002   Maria Santos          19   BSCS    2     maria@example.com

2 record(s) were added to the file entries.csv
```

No wrong answer got in. 🎉

**Find the file:** right-click the project → **Open Folder in File Explorer** → **bin** → **Debug** → **net10.0**. Open `entries.csv` with Notepad (or Excel) to see your records.

---

## What the Code Means

### The main program is short

```cs
StudentEntry entry = new StudentEntry();
entry.StudentNumber = ReadStudentNumber(entries);
entry.FullName = ReadRequired("Full name: ");
entry.Age = ReadNumber("Age (15-80): ", 15, 80);
entry.Course = ReadCourse();
entry.YearLevel = ReadNumber("Year level (1-5): ", 1, 5);
entry.Email = ReadEmail();
```

Each field has **one line**. All the hard work (asking again, checking) is hidden inside the functions. Each function **only gives back a valid answer**. So when the main program gets a value, it can trust it.

### One pattern for every field

Look at the six `Read...` functions. They all have the **same shape**:

```text
while (true)
{
    1. Ask
    2. Is the answer valid?  ──► YES: return it (this leaves the function)
    3. NO: show a helpful message, and go around again
}
```

Once you know this shape, you can write a function for **any** kind of field.

### `ReadNumber`: one function, many uses

```cs
int ReadNumber(string prompt, int min, int max)
```

Because the question and the limits are **parameters**, the same function reads the age (`15` to `80`) and the year level (`1` to `5`).

```cs
if (int.TryParse(input, out int number) && number >= min && number <= max)
```

*"If the answer is a whole number, **and** it is at least `min`, **and** it is at most `max`..."*

### `ReadCourse`: text in, enum out

```cs
if (choice == "1")
{
    return Course.BSIT;
}
```

The user types text (`"1"`). The function gives back an **enum** value. From here on, the course can only be one of the three real courses.

### `ReadEmail`: checking text

```cs
string email = ReadRequired("Email: ").ToLower();

if (email.Contains("@") && email.Contains(".") && !email.Contains(" "))
```

Functions can use other functions: `ReadEmail` first calls `ReadRequired`, so an empty answer is already handled. Then it checks three things: the email has an `@`, has a dot, and has **no** spaces.

> This is a simple check. It catches common typing mistakes, but it cannot prove that an email address really exists.

### `ReadYesNo`: a function that gives back `true` or `false`

```cs
if (ReadYesNo("Save this record? (Y/N): "))
```

The function gives back `true` for `Y` and `false` for `N`. So it can go straight into an `if`. Anything else (`maybe`, or just Enter) is asked again.

### `NumberIsUsed`: searching the list

```cs
foreach (StudentEntry item in list)
{
    if (item.StudentNumber == studentNumber)
    {
        return true;
    }
}

return false;
```

Look at every record. If one has the same student number, give back `true` **right away**. If the loop ends without finding one, give back `false`.

### `ShowTable`: leaving early, and neat columns

```cs
if (list.Count == 0)
{
    Console.WriteLine("No records were saved.");
    return;
}
```

In a `void` function, `return;` alone means *"stop here."* There is nothing to show, so the function ends early.

```cs
Console.WriteLine($"{item.StudentNumber,-12}{item.FullName,-22}...");
```

`,-12` after a value means: *"give this value 12 spaces, and put the text on the left."* That makes the columns line up. (`new string('-', 72)` makes a text of 72 dashes.)

### `SaveToFile`

```cs
string line = $"{item.StudentNumber},{item.FullName},{item.Age},{item.Course},{item.YearLevel},{item.Email}";
File.AppendAllText("entries.csv", line + Environment.NewLine);
```

Each record becomes one line of a CSV file (Lesson 17). `AppendAllText` adds to the end, so the records from earlier runs are kept.

---

## If Something Goes Wrong

**Error List: `CS0246  The type or namespace name 'StudentEntry' could not be found`**
Check that `StudentEntry.cs` is in the Solution Explorer, and that you deleted **all** the starter code before typing.

**Error List: `CS0103  The name 'ReadNumber' does not exist in the current context`**
A function is missing, or its name is spelled differently where it is used. Function names must match exactly, including capital letters.

**Error List: `CS0161  ... not all code paths return a value`**
A `Read...` function is missing its `while (true)` loop, or a `return`.

**Error List: `CS8803  Top-level statements must precede namespace and type declarations.`**
The class or the enum was typed into `Program.cs`. They belong in `StudentEntry.cs`.

**The form asks the same question forever, even for a good answer.**
The check in that function is wrong. Look at its `if`: for example, `number >= min && number <= max`, not `||`.

**The table looks broken after I typed a name with a comma.**
The table is fine. But the CSV file uses commas between values, so do not type commas inside an answer.

---

## Exercise

**Exercise 1.** Save every full name in **CAPITAL LETTERS**, so `juan dela cruz` is saved as `JUAN DELA CRUZ`.

<details>
<summary>Show answer</summary>

Change one line in the main program:

```cs
entry.FullName = ReadRequired("Full name: ").ToUpper();
```

</details>

**Exercise 2.** Add a new field: the **contact number**. It must have **exactly 11 characters** and must **start with `09`**. Write a new function `ReadContactNumber()`, add a property to the class, and show it on the "Please check" screen.

```text
Contact number: 12345
Please type an 11-digit number that starts with 09.
Contact number: 09171234567
```

<details>
<summary>Show answer</summary>

In `StudentEntry.cs`, add a property to the class:

```cs
public string ContactNumber { get; set; } = "";
```

In `Program.cs`, add this function (with the other `Read...` functions):

```cs
string ReadContactNumber()
{
    while (true)
    {
        string number = ReadRequired("Contact number: ");

        if (number.Length == 11 && number.StartsWith("09"))
        {
            return number;
        }

        Console.WriteLine("Please type an 11-digit number that starts with 09.");
    }
}
```

In the main program, after the email line, add:

```cs
entry.ContactNumber = ReadContactNumber();
```

In `ShowEntry`, add:

```cs
Console.WriteLine($"  Contact number: {item.ContactNumber}");
```

It is the same shape as every other `Read...` function: ask, check, give back or ask again.

</details>

## ⭐ Bonus Challenge

After the table, show a **summary**: how many students are in each course.

```text
===== SUMMARY =====
BSIT: 1
BSCS: 1
BSIS: 0
```

Hint: write a function `int CountByCourse(List<StudentEntry> list, Course course)` that loops through the list and counts the records with that course. Then call it three times.

<details>
<summary>Show answer</summary>

Add this function at the bottom of `Program.cs`:

```cs
int CountByCourse(List<StudentEntry> list, Course course)
{
    int count = 0;

    foreach (StudentEntry item in list)
    {
        if (item.Course == course)
        {
            count++;
        }
    }

    return count;
}
```

In the main program, after `ShowTable(entries);`, add:

```cs
Console.WriteLine();
Console.WriteLine("===== SUMMARY =====");
Console.WriteLine($"BSIT: {CountByCourse(entries, Course.BSIT)}");
Console.WriteLine($"BSCS: {CountByCourse(entries, Course.BSCS)}");
Console.WriteLine($"BSIS: {CountByCourse(entries, Course.BSIS)}");
```

An enum can be a parameter. One function does the counting for all three courses.

</details>

---

## Remember

- **Data entry** = typing records into a system. **Never let bad data in.**
- A good form: asks **one field at a time**, **checks** each answer, lets the user **review**, and **asks before saving**.
- Every `Read...` function has the same shape: `while (true)` → ask → valid? `return` → if not, message and ask again.
- Use **parameters** to make one function work for many fields: `ReadNumber(prompt, min, max)`.
- Check for **duplicates** before accepting a record.
- Keep each record in an **object**, all the records in a **List**, and save them to a **file**.

**What is next?** A file is fine for a few records. In the next lessons, you will save your data in a real **database**.

---

[⬅️ Lesson 17](17-saving-to-files.md) | [🏠 Course Home](../README.md) | [Next: Lesson 19 (What Is a Database?) ➡️](19-what-is-a-database.md)
