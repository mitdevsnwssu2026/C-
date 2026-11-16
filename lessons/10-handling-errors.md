# Lesson 10: Handling Errors (try and catch)

| | |
|---|---|
| **Part** | 2: Intermediate |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 09](09-classes-and-objects.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Know the two kinds of errors: **build errors** and **crashes**
- Stop crashes with `try` and `catch`
- Know when to use `TryParse` and when to use `try` / `catch`
- Clean up user input with `.Trim()`

---

## 🧠 The Big Idea

There are two kinds of errors in programming.

| | Build error | Crash (exception) |
|---|---|---|
| **When** | **Before** the program starts | **While** the program is running |
| **Cause** | A mistake in how the code is **written**: a missing `;`, a typo | Something **unexpected happens**: bad input, a missing file, no database |
| **What you see** | `error CS1002: ; expected` | `Unhandled exception. System.FormatException: ...` |
| **How to fix** | Fix the code | **Catch** the problem with `try` / `catch` |

A crash is called an **exception** in C#. It means *"something exceptional (unexpected) happened, and I don't know what to do."* If nobody **catches** the exception, the program stops.

### `try` and `catch`

```cs
try
{
    // Code that MIGHT crash
}
catch (Exception)
{
    // What to do IF it crashes
}
```

Think of a trapeze artist with a **safety net**:

- `try` = *"Try this trick."*
- `catch` = *"If you fall, the net catches you."*

If nothing goes wrong inside `try`, the `catch` block is skipped. If something goes wrong, C# **jumps** straight to `catch`, and the program keeps running.

---

## 🛠️ Let's Build It: A Crash-Proof Divider

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson10
cd Lesson10
code .
```

### Step 2: Write the code

```csharp
// Lesson 10: Catching errors so the program does not crash

Console.Write("Enter a whole number to divide 100 by: ");
string input = Console.ReadLine() ?? "";

try
{
    int number = int.Parse(input);
    int result = 100 / number;
    Console.WriteLine($"100 / {number} = {result}");
}
catch (FormatException)
{
    Console.WriteLine("Oops! That was not a whole number.");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Oops! You cannot divide by zero.");
}
catch (Exception ex)
{
    Console.WriteLine($"Oops! Something else went wrong: {ex.Message}");
}

Console.WriteLine("The program is still running. Goodbye!");
```

### Step 3: Run it four times

Try each of these answers: `4`, `abc`, `0`, and `99999999999`.

✅ **You should see:**

```text
Enter a whole number to divide 100 by: 4
100 / 4 = 25
The program is still running. Goodbye!
```

```text
Enter a whole number to divide 100 by: abc
Oops! That was not a whole number.
The program is still running. Goodbye!
```

```text
Enter a whole number to divide 100 by: 0
Oops! You cannot divide by zero.
The program is still running. Goodbye!
```

```text
Enter a whole number to divide 100 by: 99999999999
Oops! Something else went wrong: Value was either too large or too small for an Int32.
The program is still running. Goodbye!
```

In every case, the last line appears. **The program never crashed.** 🎉

---

## 🔎 Line by Line

```cs
try
{
    int number = int.Parse(input);
    int result = 100 / number;
    Console.WriteLine($"100 / {number} = {result}");
}
```

Two things here can fail: `int.Parse` (if the input is not a whole number) and `100 / number` (if the number is 0). If either one fails, C# **skips the rest** of the `try` block and jumps to the matching `catch`.

```cs
catch (FormatException)
{
    Console.WriteLine("Oops! That was not a whole number.");
}
```

Each kind of problem has its own **exception type**. `FormatException` means "the text was not in the right format".

```cs
catch (DivideByZeroException)
```

This one only catches division by zero.

```cs
catch (Exception ex)
{
    Console.WriteLine($"Oops! Something else went wrong: {ex.Message}");
}
```

`Exception` (without a first word) catches **every** kind of problem. We put it **last**, as the final safety net. The `ex` gives us the exception's details, and `ex.Message` is its explanation in words.

> [!IMPORTANT]
> C# checks the `catch` blocks **from top to bottom** and uses the **first one that matches**. Always put the general `catch (Exception ex)` **last**.

---

## 🧠 `TryParse` or `try` / `catch`?

Both prevent crashes. Here is a simple rule:

| Situation | Use | Why |
|---|---|---|
| Checking what the **user typed** | `TryParse` | Bad typing is normal and expected. Just ask again |
| Things **outside your program**: files, databases, the internet | `try` / `catch` | You cannot check everything in advance. The file might be deleted, the disk might be full... |

You will use `try` / `catch` again when working with files and databases.

---

## 🧠 Cleaning Input with `.Trim()`

Users often type extra spaces by accident, like `"  Juan  "`. `.Trim()` removes the spaces at the **start** and **end** of text:

```cs
string name = (Console.ReadLine() ?? "").Trim();
```

Read this from the inside out:

1. `Console.ReadLine() ?? ""` reads the text (or uses `""` if nothing comes back).
2. The parentheses `( )` around it make sure that happens first.
3. `.Trim()` then removes the extra spaces.

Now, to check if the user typed **nothing** (or only spaces):

```cs
if (name == "")
{
    Console.WriteLine("Name is required.");
}
```

You will use this pattern for every required field from now on.

---

## ⚠️ Common Mistakes

### Mistake 1: Putting the general `catch` first

```cs
catch (Exception ex) { ... }
catch (FormatException) { ... }
```

```text
error CS0160: A previous catch clause already catches all exceptions of this or of a super type ('Exception')
```

**Fix:** Put the specific `catch` blocks first and `catch (Exception ex)` last.

### Mistake 2: Using a variable from `try` outside of it

```cs
try
{
    int number = int.Parse(input);
}
catch (FormatException) { ... }

Console.WriteLine(number);   // error!
```

Variables created inside `{ }` only exist inside those braces. **Fix:** Do all the work that needs `number` inside the `try` block.

### Mistake 3: Catching everything and hiding the problem

```cs
catch (Exception)
{
    // nothing here
}
```

The program will not crash, but nobody will know something went wrong. **Always** show a message in your `catch` block.

---

## 🏋️ Practice

**Exercise 1.** Ask the user for their age using `int.Parse`, inside a `try` / `catch`. If it fails, show `Please type your age using digits, like 18.`

<details>
<summary>Show answer</summary>

```csharp
Console.Write("How old are you? ");
string input = Console.ReadLine() ?? "";

try
{
    int age = int.Parse(input);
    Console.WriteLine($"You are {age} years old.");
}
catch (FormatException)
{
    Console.WriteLine("Please type your age using digits, like 18.");
}
```

</details>

**Exercise 2.** Make a list of 3 names. Ask the user for an index number and show the name at that index. Catch the error if the index does not exist. (Hint: the exception type is `ArgumentOutOfRangeException`.)

<details>
<summary>Show answer</summary>

```csharp
List<string> names = new List<string>();
names.Add("Ana");
names.Add("Ben");
names.Add("Carla");

Console.Write("Which index (0, 1, or 2)? ");

try
{
    int index = int.Parse(Console.ReadLine() ?? "");
    Console.WriteLine($"The name is {names[index]}.");
}
catch (FormatException)
{
    Console.WriteLine("Please type a whole number.");
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine($"There is no name at that index. Use 0 to {names.Count - 1}.");
}
```

</details>

**Exercise 3.** Ask for the user's full name. Use `.Trim()`. If they typed nothing, keep asking until they do. (Hint: use `while (true)` and `break`.)

<details>
<summary>Show answer</summary>

```csharp
string fullName;

while (true)
{
    Console.Write("Full name: ");
    fullName = (Console.ReadLine() ?? "").Trim();

    if (fullName != "")
    {
        break;
    }

    Console.WriteLine("Full name is required. Please type something.");
}

Console.WriteLine($"Hello, {fullName}!");
```

`string fullName;` creates the box **before** the loop, without a value yet. That way we can still use `fullName` **after** the loop ends.

</details>

---

## 📌 Recap

- **Build errors** happen before the program runs. **Exceptions** (crashes) happen while it runs.
- `try { ... } catch (SomeException) { ... }` catches crashes so the program keeps running.
- Put specific `catch` blocks first and `catch (Exception ex)` last. Use `ex.Message` for details.
- Use `TryParse` for user input. Use `try` / `catch` for files, databases, and other outside things.
- `(Console.ReadLine() ?? "").Trim()` reads input and removes extra spaces.

---

[⬅️ Lesson 09](09-classes-and-objects.md) | [🏠 Course Home](../README.md) | [Next: Lesson 11 (Saving to Files) ➡️](11-saving-to-files.md)
