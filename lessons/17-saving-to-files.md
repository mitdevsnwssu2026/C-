# Lesson 17: Saving Data to a File

**Part 2: Intermediate** · About 60 minutes

## What You Will Learn

- Why information **disappears** when a program closes
- How to save text to a file, and read it back
- What a **CSV** file is
- Why real systems use a **database** instead of plain files

## The Idea

Open your Lesson 14 student list. Add a student. Exit. Run it again.

**The student you added is gone!** 😱

Why? Variables live in the computer's **memory**. Memory is like a **whiteboard**: fast and easy to write on, but it is **wiped clean** when the program ends.

To keep information, you must save it somewhere permanent, like a **notebook**. On a computer, the simplest notebook is a **file**.

### CSV files

**CSV** means **Comma-Separated Values**. Each line is one record. The values are separated by commas:

```text
2026-0001,Juan Dela Cruz,BSIT
2026-0002,Maria Santos,BSCS
```

A CSV file is plain text, so Notepad can open it. Excel can open it too, and shows it as a table. That is why CSV is a very popular way to share data.

---

## Step by Step

### Step 1: Create a new project named `Lesson17`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson17`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Type the code

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Type the code below.

## The Code

```csharp
// Lesson 17: Saving students to a CSV file so they are not forgotten

string filePath = "students.csv";

Console.WriteLine("=== SAVE A STUDENT ===");

Console.Write("Student number: ");
string studentNumber = (Console.ReadLine() ?? "").Trim();

Console.Write("Full name: ");
string fullName = (Console.ReadLine() ?? "").Trim();

Console.Write("Course: ");
string course = (Console.ReadLine() ?? "").Trim();

// Join the values with commas to make one line of text
string line = $"{studentNumber},{fullName},{course}";

// Add the line to the end of the file (the file is created if it does not exist)
File.AppendAllText(filePath, line + Environment.NewLine);
Console.WriteLine("Saved to students.csv!");

// Read every line back from the file
Console.WriteLine();
Console.WriteLine("=== EVERYONE IN THE FILE ===");

string[] lines = File.ReadAllLines(filePath);

foreach (string savedLine in lines)
{
    string[] parts = savedLine.Split(',');
    Console.WriteLine($"Number: {parts[0]} | Name: {parts[1]} | Course: {parts[2]}");
}

Console.WriteLine($"Total saved: {lines.Length}");
```

### Step 3: Run it two times

**First time.** Press **Ctrl + F5** and add a student.

## The Output

✅ **You will see:**

```text
=== SAVE A STUDENT ===
Student number: 2026-0001
Full name: Juan Dela Cruz
Course: BSIT
Saved to students.csv!

=== EVERYONE IN THE FILE ===
Number: 2026-0001 | Name: Juan Dela Cruz | Course: BSIT
Total saved: 1
```

Close the black window.

**Second time.** Press **Ctrl + F5** again and add a different student.

```text
=== SAVE A STUDENT ===
Student number: 2026-0002
Full name: Maria Santos
Course: BSCS
Saved to students.csv!

=== EVERYONE IN THE FILE ===
Number: 2026-0001 | Name: Juan Dela Cruz | Course: BSIT
Number: 2026-0002 | Name: Maria Santos | Course: BSCS
Total saved: 2
```

**Juan is still there!** The information stayed, even after the program closed. 🎉

### Step 4: Find the file and open it

Where is `students.csv`? Visual Studio keeps your running program, and the files it makes, in a folder deep inside your project. Let's go there.

**1.** In the **Solution Explorer**, **right-click** the project line **Lesson17**.

**2.** Click **Open Folder in File Explorer** (near the bottom of the menu).

✅ **You will see:** a Windows folder window opens. It shows your project's files.

**3.** Double-click the folder **bin**.

**4.** Double-click the folder **Debug**.

**5.** Double-click the folder whose name starts with **net** (for example **net10.0**).

✅ **You will see:** several files. One of them is **students.csv** (it may show as just **students**).

**6.** **Right-click** `students.csv`, move to **Open with**, and click **Notepad**.

✅ **You will see:**

```text
2026-0001,Juan Dela Cruz,BSIT
2026-0002,Maria Santos,BSCS
```

That is your saved data. Close Notepad and the folder window.

> [!NOTE]
> Remember this folder: **your project → bin → Debug → net10.0**. Every file your programs create (including databases, later) is saved there.

---

## What the Code Means

```cs
string line = $"{studentNumber},{fullName},{course}";
File.AppendAllText(filePath, line + Environment.NewLine);
```

Join the three values with commas into one line. Then add that line to the **end** of the file. If the file does not exist yet, C# creates it.

`Environment.NewLine` is like pressing **Enter** in the file. It makes the next text start on a new line.

```cs
string[] lines = File.ReadAllLines(filePath);
```

Read **every** line of the file. Each line becomes one item in an array.

```cs
foreach (string savedLine in lines)
{
    string[] parts = savedLine.Split(',');
    Console.WriteLine($"Number: {parts[0]} | Name: {parts[1]} | Course: {parts[2]}");
}
```

For each line, `.Split(',')` **cuts** the text at every comma and gives back the pieces:

- `parts[0]` is the student number
- `parts[1]` is the full name
- `parts[2]` is the course

Notice that `','` uses **single quotes**. Single quotes are for **one character**. Double quotes are for text.

### The file tools

- `File.AppendAllText("data.txt", "Hello")` → add text to the end of a file
- `File.WriteAllText("data.txt", "Hello")` → replace everything in the file
- `File.ReadAllLines("data.txt")` → read all the lines
- `File.Exists("data.txt")` → does the file exist? Gives `true` or `false`

### Why not use files for everything?

Files work. But imagine your school's enrollment system with **10,000 students** in one CSV file:

- **Searching is slow.** To find one student, the program must read every line.
- **There are no rules.** Nothing stops two students from having the same student number, or an empty name.
- **Changing data is hard.** To fix one typing mistake, the whole file must be written again.
- **Many users at once is a problem.** If two people save at the same moment, data can be lost.
- **Commas break it.** A name like `Dela Cruz, Juan` has a comma, so `.Split(',')` cuts it in the wrong place.

A **database** solves all of these. You will learn to use one starting in Lesson 19. First, in the next lesson, you will put together everything you know in one complete project: a **data entry form**.

---

## If Something Goes Wrong

**I can't find `students.csv`.**
Follow Step 4 again, slowly: right-click the project → **Open Folder in File Explorer** → **bin** → **Debug** → **net10.0**.

**The output looks wrong after I typed a name with a comma.**
A comma inside a value breaks the line into too many pieces. For now, do **not** type commas inside the answers.

**The black window shows a crash:**

```text
Unhandled exception. System.IO.FileNotFoundException: Could not find file '...'
```

The program tried to read a file that does not exist yet. Check first with `if (File.Exists(filePath))`.

---

## Exercise

**Exercise 1.** **A tiny diary.** Ask the user to type one sentence. Add it to `diary.txt` with today's date in front, like `2026-11-16: I learned about files.` Then show the whole diary.

Hint: `DateTime.Now.ToString("yyyy-MM-dd")` gives today's date as text.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("What did you do today? ");
string entry = (Console.ReadLine() ?? "").Trim();

string today = DateTime.Now.ToString("yyyy-MM-dd");
File.AppendAllText("diary.txt", $"{today}: {entry}" + Environment.NewLine);

Console.WriteLine();
Console.WriteLine("=== MY DIARY ===");

foreach (string line in File.ReadAllLines("diary.txt"))
{
    Console.WriteLine(line);
}
```

</details>

**Exercise 2.** Change the lesson's program so it **refuses to save** if any of the three answers is empty. Show `All fields are required. Nothing was saved.` (Hint: use `||`.)

<details>
<summary>Show answer</summary>

Replace the three lines that build and save the line with this:

```cs
if (studentNumber == "" || fullName == "" || course == "")
{
    Console.WriteLine("All fields are required. Nothing was saved.");
}
else
{
    string line = $"{studentNumber},{fullName},{course}";
    File.AppendAllText(filePath, line + Environment.NewLine);
    Console.WriteLine("Saved to students.csv!");
}
```

</details>

## ⭐ Bonus Challenge

Write a program that reads `students.csv` and counts how many students are in `BSIT`. If the file does not exist, show `No students saved yet.` instead of crashing.

(Write it in your `Lesson17` project, so it reads the file you already made.)

<details>
<summary>Show answer</summary>

```csharp
string filePath = "students.csv";

if (!File.Exists(filePath))
{
    Console.WriteLine("No students saved yet.");
}
else
{
    int bsitCount = 0;

    foreach (string line in File.ReadAllLines(filePath))
    {
        string[] parts = line.Split(',');

        if (parts[2] == "BSIT")
        {
            bsitCount++;
        }
    }

    Console.WriteLine($"BSIT students: {bsitCount}");
}
```

</details>

---

## Remember

- Variables are wiped when the program ends. **Files** keep information.
- `File.AppendAllText`, `File.ReadAllLines`, `File.Exists`
- Files made by your program are in **your project → bin → Debug → net10.0**.
- **CSV** = one record per line, with commas between the values. `.Split(',')` cuts a line into pieces.
- Files have limits. A **database** fixes them. You will start using one in Lesson 19.

---

[⬅️ Lesson 16](16-handling-errors.md) | [🏠 Course Home](../README.md) | [Next: Lesson 18 (Basic Data Entry) ➡️](18-basic-data-entry.md)
