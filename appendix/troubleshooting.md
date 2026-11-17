# 🛟 Troubleshooting Guide

Something is not working? Find your problem below.

> [!TIP]
> **Check these three things first.** They fix most problems.
> 1. Did you click **No** on the "build errors" message, and read the **Error List**?
> 2. Are you typing in the right **tab** (the right file)?
> 3. Is an old **black window** still open? Close it.

---

## Installing and Opening Visual Studio

**I installed "Visual Studio Code" (blue icon). The steps don't match.**
That is a different program. Install **Visual Studio Community** (purple icon). See [Lesson 00](../lessons/00-before-you-start.md).

**The installation stopped or failed.**
Check your internet connection, then open the downloaded installer file again. It continues where it stopped.

**I did not click ".NET desktop development" during installation.**
Click **Start**, type `Visual Studio Installer`, and open it. Click **Modify**, click the **.NET desktop development** box, then click **Modify** at the bottom-right.

**Visual Studio asks me to sign in.**
You do not need an account. Look for the word **Skip**, and click it.

---

## Creating a Project

**I can't find "Console App" in the list.**
Type `console` in the search box at the top. Set the first small menu under it to **C#**. If the list is still empty, the **.NET desktop development** workload is missing. See the answer above.

**The code Visual Studio wrote is long, with `namespace`, `class Program`, and `static void Main`.**
You chose **Console App (.NET Framework)**, or the box **Do not use top-level statements** was checked. Close Visual Studio and create the project again. Choose **Console App** (with the **C#** label, and **without** ".NET Framework"), and leave that box **empty**.

**It says a project with this name already exists.**
You already created this lesson's project. Either open it (start window → **Open recent**), or use a new name, like `Lesson07b`.

**Where are my projects saved?**
In `C:\CSharpCourse`. Each lesson has its own folder there.

**How do I open an old project?**
Open Visual Studio. On the left side of the start window, under **Open recent**, click the project's name.

---

## The Visual Studio Window

**I can't see the Solution Explorer.**
Click **View** on the menu bar, then **Solution Explorer**.

**I can't see the Error List.**
Click **View** on the menu bar, then **Error List**.

**I can't see the Properties panel.**
Click a file in the Solution Explorer, then press **F4**.

**I closed the tab with my code.**
In the Solution Explorer, double-click the file (for example `Program.cs`). It opens again. Nothing is lost.

**The panels are all in strange places.**
Click **Window** on the menu bar, then **Reset Window Layout**, then **Yes**.

---

## Running a Program

**A message asks: "There were build errors. Would you like to continue and run the last successful build?"**
Click **No**. There is a mistake in your code. Read the **Error List** at the bottom, double-click the first row to jump to the mistake, fix it, and run again.

**An error says a file "is being used by another process", or "Could not copy ... apphost.exe".**
An old black window is still open, with your program still running. Close **every** black window, then run again.

**Nothing happens after I run the program.**
The program is probably **waiting for you to type**. Click the black window, type an answer, and press **Enter**.

**The black window keeps printing the same thing forever.**
That is an **infinite loop**. Close the black window with the **X** at its top-right corner. Then fix your loop (see [Lesson 09](../lessons/09-loops.md)).

**Ctrl + F5 does nothing.**
On some laptops, you must also hold the **Fn** key: **Ctrl + Fn + F5**. Or use the menu: **Debug** → **Start Without Debugging**.

**The program pauses, and a box says "Exception Unhandled".**
You started the program with the solid green ▶ button (or **F5**), which runs it in *debugging* mode. Click the red square **■** (Stop) on the toolbar. Then run with **Ctrl + F5** instead.

**The black window closes immediately.**
You started with the solid green ▶ button. Use **Ctrl + F5** (**Debug** → **Start Without Debugging**). Then the window stays open until you press a key.

---

## Mistakes in the Code (the Error List)

Each row in the Error List has a **Code**, a **Description**, and a **Line** number. **Double-click a row** to jump to the mistake. Always fix the **first** row first.

| Code | The description says... | What it means | How to fix it |
|---|---|---|---|
| `CS1002` | ; expected | A `;` is missing | Type `;` at the end of that line (or the line above it) |
| `CS0103` | The name '...' does not exist in the current context | C# does not know that name | Check the spelling and the capital letters. Was the variable made **before** this line, inside the same `{ }`? |
| `CS1010` | Newline in constant | A text is missing its closing `"` | Add the missing `"` |
| `CS1513` | } expected | A `{` was opened but never closed | Every `{` needs a matching `}` |
| `CS1026` | ) expected | A `(` was opened but never closed | Add the missing `)` |
| `CS0029` | Cannot implicitly convert type 'string' to 'int' | Text was used where a number is needed | Use `int.Parse(...)` or `int.TryParse(...)` |
| `CS0128` | A local variable ... named '...' is already defined | The same variable was made twice | The second time, leave out the type: `x = 5;` |
| `CS0165` | Use of unassigned local variable | A variable was used before it got a value | Give it a value first, for example `= new List<string>();` |
| `CS0161` | not all code paths return a value | A method does not always `return` | Every path through the method must end with `return something;` |
| `CS0163` | Control cannot fall through from one case label | A `case` is missing `break;` | Add `break;` at the end of every `case` |
| `CS0246` | The type or namespace name '...' could not be found | C# cannot find that class | Is the class file in the Solution Explorer? Is the name spelled the same? Did you delete the starter code in the file? |
| `CS0234` | 'Data' does not exist in the namespace 'Microsoft' | The SQLite package is missing | Install it: right-click the project → **Manage NuGet Packages...** (see [Lesson 18](../lessons/18-what-is-a-database.md)) |
| `CS1061` | '...' does not contain a definition for '...' | That property or method does not exist | Check the exact name in the class |
| `CS0117` | '...' does not contain a definition for '...' | That method is missing from the class | Add the method **inside** the class's `{ }` |
| `CS0160` | A previous catch clause already catches all exceptions | `catch (Exception)` is not last | Move `catch (Exception ex)` to the end |
| `CS8803` | Top-level statements must precede namespace and type declarations | There is normal code **below** an enum (or a class) in `Program.cs` | Move the enum to the **very bottom** of the file (see [Lesson 15](../lessons/15-enums.md)) |
| `CS0136` | A local or parameter named '...' cannot be declared in this scope | Two nested loops use the same counter name | Give them different names, like `row` and `column` |
| `CS0139` | No enclosing loop out of which to break or continue | `break` or `continue` is outside a loop | Check your `{ }` |
| `CS1955` | Non-invocable member 'string.Length' cannot be used like a method | You wrote `.Length()` | Remove the `( )`: it is just `.Length` |

**No error, but the result is wrong?** Two common causes:

- **A text tool whose result was not kept.** `name.ToUpper();` alone changes nothing. Write `name = name.ToUpper();` (see [Lesson 04](../lessons/04-string-manipulation.md)).
- **The wrong order of operators.** Add parentheses around the part that should happen first (see [Lesson 06](../lessons/06-comparison-and-logical-operators.md)).

**Green wavy lines** are **warnings**. The program still runs, but fix them. The most common one:

```text
CS8600  Converting null literal or possible null value to non-nullable type.
```

**Fix:** add `?? ""` after `Console.ReadLine()`:

```cs
string name = Console.ReadLine() ?? "";
```

---

## Crashes While the Program Runs

A crash shows `Unhandled exception.` in the black window. The first line says **what** went wrong.

| The black window says... | What it means | How to fix it |
|---|---|---|
| `FormatException: The input string '...' was not in a correct format.` | `int.Parse` got text that is not a number | Use `TryParse` (see [Lesson 07](../lessons/07-if-and-else.md) and [Lesson 13](../lessons/13-methods.md)) |
| `DivideByZeroException` | A whole number was divided by 0 | Check that the number is not 0 first |
| `ArgumentOutOfRangeException: Index was out of range.` | A List index that does not exist | The last index is `Count - 1` |
| `IndexOutOfRangeException` | An array index that does not exist | The last index is `Length - 1` |
| `FileNotFoundException: Could not find file '...'` | The file is not where the program looks | Check with `File.Exists(...)` first. For a data file in your project, set **Copy to Output Directory** to **Copy if newer** (see [Lesson 22](../lessons/22-uploading-csv-data.md)) |
| `NullReferenceException` | Something that is `null` (nothing) was used | Check `if (x == null)` first |
| `ExecuteNonQuery can only be called when the connection is open.` | `connection.Open()` is missing | Add `connection.Open();` |
| `Must add values for the following parameters: $name` | A SQL placeholder was not filled in | Add `command.Parameters.AddWithValue("$name", value);` with **exactly** the same spelling |

---

## Files and Folders

**Where is the file my program created (a `.csv`, `.txt`, or `.db` file)?**
Right-click the project in the Solution Explorer → **Open Folder in File Explorer**. Then open **bin** → **Debug** → **net10.0**.

**My program can't find a file that I added to the project.**
Click the file in the Solution Explorer. In the **Properties** panel (press **F4**), set **Copy to Output Directory** to **Copy if newer**.

**I copied a file with "Add → Existing Item", but changes in the old lesson don't appear.**
That is correct. Visual Studio made a **copy**. The two files are now separate.

---

## Database Problems

| The black window says... | What it means | How to fix it |
|---|---|---|
| `SQLite Error 1: 'no such table: Students'` | The table does not exist, or its name is misspelled | Check the spelling. Make sure the method that creates the table runs **first** |
| `SQLite Error 1: 'no such column: Email'` | The table does not have that column | You changed `CREATE TABLE`, but the old table is still there. Delete the database file (see below) |
| `SQLite Error 1: 'near "...": syntax error'` | A typing mistake in your SQL | Look at the word after `near` |
| `SQLite Error 19: 'UNIQUE constraint failed: ...'` | A duplicate was saved | Check first if it exists (see [Lesson 20](../lessons/20-inserting-data.md)) |
| `SQLite Error 19: 'NOT NULL constraint failed: ...'` | A required column got no value | Give every `NOT NULL` column a value in your `INSERT` |
| `SQLite Error 5: 'database is locked'` | Another program is using the database | In **DB Browser for SQLite**, click **Close Database**. Close other black windows |

**How do I delete a database and start fresh?**
Right-click the project → **Open Folder in File Explorer** → **bin** → **Debug** → **net10.0**. Right-click the `.db` file and click **Delete**. ⚠️ This deletes all the data in it.

**My data from another lesson is not here.**
Each project has its **own** database file. `Lesson21` and `Lesson22` do not share data.

---

## The Account System (Lessons 25 and 26)

**I can't log in.**
- In Lesson 26, register an account **in that project** first. It has its own new database.
- Usernames are saved in small letters. Use **View all accounts** to see them.
- Forgot the password? Nobody can see it. Register a new account.

**Installing the NuGet package fails.**
Check your internet connection. In the search box, the name must be exactly `Microsoft.Data.Sqlite`.

---

## Still Stuck?

1. Compare your code, line by line, with the finished program in the [`code/`](../code/) folder for that lesson.
2. Copy the finished code into your file, and run it. If it works, the difference between the two is your mistake.
3. Ask your instructor or a classmate. Show them the **Error List** or the full message in the black window.

[🏠 Course Home](../README.md)
