# Lesson 11: Saving Data to a File

| | |
|---|---|
| **Part** | 2: Intermediate |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 10](10-handling-errors.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Understand why data **disappears** when a program closes
- Save text to a file and read it back
- Use the **CSV** format to store records
- Understand why real systems use a **database** instead of plain files

---

## 🧠 The Big Idea

Run your Lesson 09 student list again. Add a student. Exit. Run it again.

**The student you added is gone!** 😱

That is because variables live in the computer's **memory** (RAM). Memory is like a **whiteboard**: fast and easy to write on, but it is **erased** when the program ends.

To keep information, you must save it somewhere **permanent**, like a **notebook**. On a computer, the simplest notebook is a **file**.

| | Memory (variables) | File |
|---|---|---|
| Like a... | Whiteboard | Notebook |
| Speed | Very fast | Slower |
| When the program closes | **Erased** | **Kept** |

### Reading and writing files in C#

| You want to... | Write |
|---|---|
| Add a line to the end of a file (creates the file if needed) | `File.AppendAllText("data.txt", "Hello" + Environment.NewLine);` |
| Replace everything in a file | `File.WriteAllText("data.txt", "Hello");` |
| Read all lines (gives an array of strings) | `string[] lines = File.ReadAllLines("data.txt");` |
| Check if a file exists | `File.Exists("data.txt")` (gives `true` or `false`) |

`Environment.NewLine` is the "Enter key" for files. It makes the next text start on a new line.

> [!NOTE]
> **Where is the file saved?** In the folder where you typed `dotnet run`, which is your project folder (for example `C:\CSharpCourse\Lesson11`).

### The CSV format

**CSV** means **Comma-Separated Values**. Each line is one record, and each value is separated by a comma:

```text
2026-0001,Juan Dela Cruz,BSIT
2026-0002,Maria Santos,BSCS
```

CSV files are plain text, so Notepad can open them. **Microsoft Excel** can open them too, and shows them as a table. That makes CSV a very popular way to share data.

### Splitting a line into pieces

`.Split(',')` cuts text at every comma and gives back an **array**:

```cs
string line = "2026-0001,Juan Dela Cruz,BSIT";
string[] parts = line.Split(',');

// parts[0] is "2026-0001"
// parts[1] is "Juan Dela Cruz"
// parts[2] is "BSIT"
```

Notice: `','` uses **single quotes** because it is a single **character**, not text.

---

## 🛠️ Let's Build It: A Student File Saver

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson11
cd Lesson11
code .
```

### Step 2: Write the code

```csharp
// Lesson 11: Saving students to a CSV file so they are not forgotten

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

### Step 3: Run it twice

**First run:**

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

**Second run** (with a different student):

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

**Juan is still there!** The data survived after the program closed. 🎉

### Step 4: Look at the file

In the Command Prompt, type:

```cmd
notepad students.csv
```

Notepad opens and shows:

```text
2026-0001,Juan Dela Cruz,BSIT
2026-0002,Maria Santos,BSCS
```

Close Notepad when you are done.

---

## 🔎 Line by Line

```cs
string line = $"{studentNumber},{fullName},{course}";
File.AppendAllText(filePath, line + Environment.NewLine);
```

Join the three values with commas into one line, then add it to the **end** of the file. If `students.csv` does not exist yet, C# creates it.

```cs
string[] lines = File.ReadAllLines(filePath);
```

Read **every** line in the file. Each line becomes one item in the array.

```cs
foreach (string savedLine in lines)
{
    string[] parts = savedLine.Split(',');
    Console.WriteLine($"Number: {parts[0]} | Name: {parts[1]} | Course: {parts[2]}");
}
```

For each line, split it at the commas and show each part.

---

## 🧠 Why Not Just Use Files Forever?

Files work, but imagine your school's real enrollment system with **10,000 students** stored in a CSV file:

| Problem | Example |
|---|---|
| **Searching is slow** | To find one student, the program must read every line |
| **No rules** | Nothing stops two students from having the same student number, or a blank name |
| **Changing data is hard** | To fix one typo, you must rewrite the entire file |
| **Many users at once** | If two people save at the same time, data can get lost or mixed up |
| **Commas break it** | A name like `Dela Cruz, Juan` has a comma, so `.Split(',')` cuts it in the wrong place |

**Databases** solve all of these problems. Starting next lesson, you will learn how to use one. This is where your programs start to look like **real systems**.

---

## ⚠️ Common Mistakes

### Mistake 1: "I can't find my file"

The file is in your **project folder**. In the Command Prompt, make sure you are in `C:\CSharpCourse\Lesson11`, then type `dir` to see it.

### Mistake 2: Typing a comma inside a value

If you type `Dela Cruz, Juan` as the name, the line becomes `2026-0003,Dela Cruz, Juan,BSIT`, which has **4** parts instead of 3. The display will be wrong. For now, **do not type commas** inside values.

### Mistake 3: `FileNotFoundException`

```text
Unhandled exception. System.IO.FileNotFoundException: Could not find file 'C:\CSharpCourse\Lesson11\students.csv'.
```

This happens if you read a file before it exists. **Fix:** Check first with `if (File.Exists(filePath))`.

---

## 🏋️ Practice

**Exercise 1.** **A tiny diary.** Ask the user to type one sentence. Add it to `diary.txt` with today's date in front, like `2026-11-16: I learned about files.` Then show the whole diary. (Hint: `DateTime.Now.ToString("yyyy-MM-dd")` gives today's date as text.)

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

**Exercise 2.** Write a program that reads `students.csv` and counts how many students are in `BSIT`. If the file does not exist, show `No students saved yet.`

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

**Exercise 3.** Make the Lesson 11 program refuse to save if any of the three values is empty. Show `All fields are required. Nothing was saved.` (Hint: use `||`.)

<details>
<summary>Show answer</summary>

Wrap the saving part in an `if`:

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

---

## 📌 Recap

- Variables are erased when the program ends. **Files** keep data permanently.
- `File.AppendAllText`, `File.WriteAllText`, `File.ReadAllLines`, `File.Exists`.
- Files are saved in your **project folder**.
- **CSV** = one record per line, values separated by commas. Use `.Split(',')` to read them.
- Files have limits. **Databases** fix them, and that is what you will learn next.

---

[⬅️ Lesson 10](10-handling-errors.md) | [🏠 Course Home](../README.md) | [Next: Lesson 12 (What Is a Database?) ➡️](12-what-is-a-database.md)
