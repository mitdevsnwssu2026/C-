# Lesson 10: Handling Errors (try and catch)

**Part 2: Intermediate** · About 60 minutes

## What You Will Learn

- The two kinds of errors: **mistakes in the code** and **crashes**
- How to stop crashes with `try` and `catch`
- When to use `TryParse` and when to use `try` / `catch`
- How to clean up what the user types with `.Trim()`

## The Idea

There are two kinds of errors.

**1. Mistakes in the code.** A missing `;`, a typing mistake. Visual Studio finds these **before** the program runs and lists them in the **Error List**. You fix them by fixing the code.

**2. Crashes.** The code is written correctly, but something unexpected happens **while the program is running**: the user types letters instead of a number, or a file is missing. The black window shows `Unhandled exception...` and the program stops.

In C#, a crash is called an **exception**.

You can **catch** an exception, so the program keeps running:

```cs
try
{
    // code that MIGHT crash
}
catch (Exception)
{
    // what to do IF it crashes
}
```

Think of a trapeze artist with a **safety net**:

- `try` → *"Try this trick."*
- `catch` → *"If you fall, the net catches you."*

If nothing goes wrong inside `try`, the `catch` part is skipped. If something goes wrong, C# **jumps** to `catch`, and the program keeps going.

---

## Step by Step

### Step 1: Create a new project named `Lesson10`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson10`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Type the code

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Type the code below.

## The Code

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

Press **Ctrl + F5** four times. Use these four answers: `4`, `abc`, `0`, and `99999999999`.

## The Output

✅ **You will see:**

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

Every time, the last line appears. **The program never crashed.** 🎉

---

## What the Code Means

```cs
try
{
    int number = int.Parse(input);
    int result = 100 / number;
    Console.WriteLine($"100 / {number} = {result}");
}
```

Two things here can crash: `int.Parse` (if the input is not a whole number) and `100 / number` (if the number is 0). If one of them crashes, C# **skips the rest** of the `try` part and jumps to the matching `catch`.

```cs
catch (FormatException)
{
    Console.WriteLine("Oops! That was not a whole number.");
}
```

Each kind of problem has its own name. `FormatException` means "the text was not in the right format."

```cs
catch (DivideByZeroException)
```

This one catches only division by zero.

```cs
catch (Exception ex)
{
    Console.WriteLine($"Oops! Something else went wrong: {ex.Message}");
}
```

Plain `Exception` catches **every** kind of problem. It goes **last**, as the final safety net. `ex` holds the details, and `ex.Message` is the explanation in words.

> [!IMPORTANT]
> C# checks the `catch` parts **from top to bottom** and uses the **first one that matches**. Always put `catch (Exception ex)` **last**.

### `TryParse` or `try` / `catch`?

Both stop crashes. Here is a simple rule:

- **Checking what the user typed?** Use **`TryParse`**. Wrong typing is normal. Just ask again.
- **Working with things outside your program** (files, databases)? Use **`try` / `catch`**. You cannot check everything in advance.

### Cleaning input with `.Trim()`

Users often type extra spaces by accident, like `"  Juan  "`. `.Trim()` removes the spaces at the **start** and the **end**:

```cs
string name = (Console.ReadLine() ?? "").Trim();
```

Read it from the inside out: read the text first (inside the parentheses), then trim it.

Now you can check if the user typed **nothing**:

```cs
if (name == "")
{
    Console.WriteLine("Name is required.");
}
```

You will use this for every required answer from now on.

---

## If Something Goes Wrong

**Error List: `CS0160  A previous catch clause already catches all exceptions of this or of a super type ('Exception')`**
You put `catch (Exception ex)` first. Move it to the **end**, after the other `catch` parts.

**Error List: `CS0103  The name 'number' does not exist in the current context`**
You used `number` **after** the `try { }` part. A variable made inside `{ }` lives only inside those braces. Do the work that needs `number` **inside** the `try` part.

**My program does not crash, but it also says nothing when something goes wrong.**
Your `catch` part is empty. Always show a message inside `catch`, so the user knows what happened.

---

## Exercise

**Exercise 1.** Ask the user for their age using `int.Parse`, inside `try` / `catch`. If it fails, show `Please type your age using digits, like 18.`

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

**Exercise 2.** Make a list of 3 names. Ask the user for an index number, and show the name at that index. Catch the crash if the index does not exist. (Hint: that problem is called `ArgumentOutOfRangeException`.)

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

## ⭐ Bonus Challenge

Ask for the user's full name. Remove extra spaces with `.Trim()`. If they typed nothing (or only spaces), keep asking until they type something. Then say hello.

```text
Full name:
Full name is required. Please type something.
Full name: Juan Dela Cruz
Hello, Juan Dela Cruz!
```

Hint: use `while (true)` and `break`.

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

`string fullName;` makes the box **before** the loop, with nothing in it yet. That way, `fullName` can still be used **after** the loop.

</details>

---

## Remember

- **Mistakes in the code** are found before the program runs. **Exceptions** (crashes) happen while it runs.
- `try { ... } catch (SomeException) { ... }` catches a crash, so the program keeps running.
- Put `catch (Exception ex)` last. Use `ex.Message` for the details.
- `TryParse` for what the user types. `try` / `catch` for files and databases.
- `(Console.ReadLine() ?? "").Trim()` reads the input and removes extra spaces.

---

[⬅️ Lesson 09](09-classes-and-objects.md) | [🏠 Course Home](../README.md) | [Next: Lesson 11 (Saving to Files) ➡️](11-saving-to-files.md)
