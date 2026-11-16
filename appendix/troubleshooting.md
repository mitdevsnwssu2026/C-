# 🛟 Troubleshooting Guide

Something is not working? Find your problem below. They are grouped by **when** they happen.

> [!TIP]
> **Before anything else, check these three things.** They fix most problems:
> 1. Is your Command Prompt in the **right folder**? The prompt should show your project, like `C:\CSharpCourse\Lesson05>`.
> 2. Did you **save** your file? Turn on **File → Auto Save** in VS Code.
> 3. Did you read the **first** error message? Fix that one first, then run again.

---

## 🔧 Setup Problems

### `'dotnet' is not recognized as an internal or external command`

The Command Prompt cannot find .NET.

1. **Close** the Command Prompt and open a **new** one. A window that was open during the installation does not know about .NET yet.
2. Still not working? **Restart your computer**.
3. Still not working? Install the .NET SDK again (see [Lesson 00](../lessons/00-before-you-start.md)). Make sure you downloaded the **SDK**, not the "Runtime".

### `'code' is not recognized as an internal or external command`

The Command Prompt cannot find VS Code.

1. Close the Command Prompt and open a new one.
2. If it still fails, open VS Code from the Start menu, then click **File → Open Folder...** and choose your project folder (for example `C:\CSharpCourse\Lesson05`). This does the same thing as `code .`.
3. To fix `code` for good, install VS Code again and make sure **"Add to PATH"** is checked.

### `dotnet add package` fails, or says it cannot find the package

- Check your **internet connection**. Packages are downloaded from the internet.
- Check the spelling: `Microsoft.Data.Sqlite` (capital M, D, and S; dots between the words).
- Make sure you are **inside** your project folder (where the `.csproj` file is). Type `dir` to check.

---

## 🏃 Problems When You Type `dotnet run`

### `Couldn't find a project to run`

You are in the wrong folder. Go into your project folder:

```cmd
cd C:\CSharpCourse\Lesson05
```

Type `dir`. You should see a file ending in `.csproj`.

### "I changed my code, but the program still does the old thing"

Your file was not saved. In VS Code, an unsaved file shows a **dot (●)** on its tab instead of an **X**. Press **Ctrl + S**, or turn on **File → Auto Save**.

### The program seems frozen

It is probably **waiting for you to type something**. Type an answer and press **Enter**.

If the screen keeps printing the same thing over and over, you have an **infinite loop**. Press **Ctrl + C** to stop it, and check that your loop's condition can become `false` (see [Lesson 06](../lessons/06-loops.md)).

### Yellow `warning` messages

Warnings do **not** stop your program, but you should fix them. The most common one in this course:

```text
warning CS8600: Converting null literal or possible null value to non-nullable type.
```

**Fix:** Add `?? ""` after `Console.ReadLine()`:

```cs
string name = Console.ReadLine() ?? "";
```

---

## ❌ Build Errors (Red Errors)

The message always tells you the **file**, **line**, and **position**. For example, `Program.cs(7,35)` means *line 7, character 35*. In VS Code, the line number is shown on the left side of the editor.

| Error | What it means | How to fix it |
|---|---|---|
| `CS1002: ; expected` | A `;` is missing | Add `;` at the end of the instruction on that line (or the line before) |
| `CS0103: The name 'xyz' does not exist in the current context` | C# does not know that name | Check the spelling and capital letters. Check that the variable was created **before** this line, and inside the same `{ }` |
| `CS1010: Newline in constant` | A text value is missing its closing `"` | Add the missing `"` |
| `CS1513: } expected` | A `{` was opened but never closed | Count your `{` and `}`. Every `{` needs a matching `}` |
| `CS1026: ) expected` | A `(` was opened but never closed | Add the missing `)` |
| `CS0029: Cannot implicitly convert type 'string' to 'int'` | You put text where a number is needed | Use `int.Parse(...)` or `int.TryParse(...)`, or fix the variable's type |
| `CS0128: A local variable ... named 'x' is already defined` | You created the same variable twice | The second time, leave out the type: `x = 5;` not `int x = 5;` |
| `CS0165: Use of unassigned local variable 'x'` | You used a variable before giving it a value | Give it a value when you create it, for example `= new List<string>();` |
| `CS0161: not all code paths return a value` | A method that should give back a value does not always `return` | Make sure every path through the method ends with `return something;` |
| `CS0163: Control cannot fall through from one case label` | A `case` in a `switch` is missing `break;` | Add `break;` at the end of every `case` |
| `CS0246: The type or namespace name 'Student' could not be found` | C# cannot find that class | Check that the class file exists, is in the **same folder** as `Program.cs`, and the class name is spelled the same (with the same capital letters) |
| `CS0234: The type or namespace name 'Data' does not exist in the namespace 'Microsoft'` | The SQLite package is missing | Run `dotnet add package Microsoft.Data.Sqlite` in the project folder |
| `CS1061: 'Student' does not contain a definition for 'Name'` | That property or method does not exist | Check the exact name in the class (for example `FullName`, not `Name`) |
| `CS0117: 'StudentDatabase' does not contain a definition for 'ImportStudents'` | That method is missing from the class | Add the method **inside** the class's `{ }` |

---

## 💥 Crashes While the Program Runs

A crash starts with `Unhandled exception.` The first line tells you **what** went wrong. Look for `line` in the message to see **where**.

| Exception | What it means | How to fix it |
|---|---|---|
| `FormatException: The input string 'abc' was not in a correct format.` | `int.Parse` or `double.Parse` got text that is not a number | Use `TryParse` instead (see [Lesson 05](../lessons/05-decisions.md) and [Lesson 08](../lessons/08-methods.md)) |
| `DivideByZeroException` | You divided a whole number by 0 | Check that the number is not 0 before dividing |
| `ArgumentOutOfRangeException: Index was out of range.` | You used a List index that does not exist | The last index is `Count - 1` |
| `IndexOutOfRangeException: Index was outside the bounds of the array.` | You used an array index that does not exist | The last index is `Length - 1` |
| `FileNotFoundException: Could not find file '...'` | The file does not exist in that folder | Check with `File.Exists(...)` first. Check the file is in your **project folder** |
| `NullReferenceException` | You used something that is `null` (nothing) | Check `if (x == null)` before using it |
| `InvalidOperationException: ... connection is open` | You forgot `connection.Open()` | Call `connection.Open();` right after creating the connection |
| `InvalidOperationException: Must add values for the following parameters: $name` | A SQL placeholder was not filled in | Add `command.Parameters.AddWithValue("$name", value);` and check that the spelling matches **exactly** |

---

## 🗄️ Database Problems

| Error | What it means | How to fix it |
|---|---|---|
| `SQLite Error 1: 'no such table: Students'` | The table does not exist, or its name is misspelled | Check the spelling (`Students` with an s). Make sure `CreateTable()` runs **first** |
| `SQLite Error 1: 'no such column: Email'` | The table does not have that column | If you added a column to `CREATE TABLE`, the old table did not change. While practicing, delete the database file (`del school.db`) and run again |
| `SQLite Error 1: 'near "TABEL": syntax error'` | A typo in your SQL | Look at the word after `near`. That is where the mistake is |
| `SQLite Error 19: 'UNIQUE constraint failed: Students.StudentNumber'` | You tried to save a duplicate | Check if it exists first (see [Lesson 14](../lessons/14-inserting-data.md)), or use `INSERT OR IGNORE` (see [Lesson 16](../lessons/16-uploading-csv-data.md)) |
| `SQLite Error 19: 'NOT NULL constraint failed: ...'` | A required column got no value | Make sure every `NOT NULL` column gets a value in your `INSERT` |
| `SQLite Error 5: 'database is locked'` | Another program is using the database | Close **DB Browser for SQLite** (or click **Close Database** in it), and make sure the program is not running in another Command Prompt window |

### "My data disappeared!"

Each project folder has its **own** database file. `Lesson15\school.db` and `Lesson16\school.db` are **different** databases. Make sure you are running the program in the folder you think you are.

---

## 🔐 Account System Problems (Lessons 19–20)

### The password shows letters instead of `*`

Run the program with `dotnet run` in the **Command Prompt**, not from a VS Code "Output" panel.

### "I can't log in"

- Usernames are saved in small letters. Use option **3 (View all accounts)** to see the exact usernames.
- In Lesson 20, did you copy `accounts.db` from Lesson 19? Type `dir` to check.
- Forgot the password? Nobody can see it, not even you. Register a new account.

---

## 🙋 Still Stuck?

1. Compare your code, line by line, with the finished program in the [`code/`](../code/) folder for that lesson.
2. Copy the **finished** program into your project and run it. If it works, the difference between the two files is your bug.
3. Ask your instructor or a classmate. Show them the **full error message**.

[🏠 Course Home](../README.md)
