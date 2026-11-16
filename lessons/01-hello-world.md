# Lesson 01: Hello World (Your First Program)

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 45 minutes |
| **Before this** | [Lesson 00](00-before-you-start.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Create your first C# program from the Command Prompt
- Run it and see it print **Hello, World!**
- Change the program to print your own messages

---

## 🧠 The Big Idea

"Hello, World!" is the first program almost every programmer writes. It does only one thing: it shows a message on the screen.

It is small, but it proves that **everything works**: your tools are installed, and you know how to write, save, and run a program. After this lesson, you are officially a programmer! 🎉

---

## 🛠️ Let's Build It

### Step 1: Go to your course folder

Open the **Command Prompt** (Windows key → type `cmd` → Enter). Then type:

```cmd
cd C:\CSharpCourse
```

Your prompt should show `C:\CSharpCourse>`.

### Step 2: Create a new project

A **project** is a folder that holds everything for one program. Type:

```cmd
dotnet new console -n Lesson01
```

Here is what each part of that command means:

| Part | Meaning |
|---|---|
| `dotnet` | Use the .NET tool |
| `new` | Make something new |
| `console` | Make a **console app**: a program that runs inside the Command Prompt window |
| `-n Lesson01` | **N**ame it `Lesson01` |

✅ **You should see something like this:**

```text
The template "Console App" was created successfully.

Processing post-creation actions...
Restoring C:\CSharpCourse\Lesson01\Lesson01.csproj:
  Determining projects to restore...
  Restored C:\CSharpCourse\Lesson01\Lesson01.csproj (in 91 ms).
Restore succeeded.
```

The important words are **"created successfully"** and **"Restore succeeded"**. (The number of milliseconds will be different on your computer. That is fine.)

### Step 3: Go into the project folder

```cmd
cd Lesson01
```

Your prompt should now show `C:\CSharpCourse\Lesson01>`.

Let's see what .NET made for you. Type:

```cmd
dir
```

You will see these items:

| Item | What it is | Should you edit it? |
|---|---|---|
| `Program.cs` | **Your code.** This is the recipe. | ✅ Yes, this is where you work |
| `Lesson01.csproj` | Project settings | ❌ No |
| `obj` | A folder .NET uses behind the scenes | ❌ No |

### Step 4: Run the program

```cmd
dotnet run
```

✅ **You should see:**

```text
Hello, World!
```

**Congratulations! You just ran your first C# program!** 🎉

> [!NOTE]
> The first time you run a project, it can take a few seconds. .NET is **building** your program: turning your C# code into something the computer can run. After that it is faster.

### Step 5: Open the project in VS Code

Type this in the Command Prompt (notice the space and the dot at the end):

```cmd
code .
```

The dot `.` means **"this folder"**. VS Code opens your `Lesson01` folder.

- If VS Code asks **"Do you trust the authors of the files in this folder?"**, click **"Yes, I trust the authors"**. (You are the author!)
- On the left side, you will see your files. Click **`Program.cs`** to open it.

### Step 6: Turn on Auto Save (do this once)

In VS Code, click the **File** menu at the top, then click **Auto Save** so it has a check mark.

Now VS Code saves your work automatically. You will never run an old version of your code by accident.

### Step 7: Read your first code

`Program.cs` contains these two lines:

```cs
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

**Line 1** starts with `//`. This is a **comment**: a note for humans. The computer ignores everything after `//` on that line.

**Line 2** is an **instruction** (programmers also call it a **statement**). Let's break it into pieces:

| Piece | Meaning |
|---|---|
| `Console` | The Command Prompt window (the "console") |
| `.` | "Use something that belongs to it." `Console.WriteLine` means *the WriteLine that belongs to Console* |
| `WriteLine` | Write a line of text, then move to the next line |
| `(` and `)` | Hold the thing you give to `WriteLine` |
| `"Hello, World!"` | The text to show. **Text always goes inside double quotes `" "`** |
| `;` | The end of the instruction. It is like the period at the end of a sentence |

### Step 8: Change the program

In VS Code, **delete everything** in `Program.cs`, then type this (use your own name):

```csharp
// My first C# program
Console.WriteLine("Hello, World!");
Console.WriteLine("My name is Juan Dela Cruz.");
Console.WriteLine("I am learning C#!");
```

Now go back to the **Command Prompt** and run it again:

```cmd
dotnet run
```

✅ **You should see:**

```text
Hello, World!
My name is Juan Dela Cruz.
I am learning C#!
```

The computer runs your instructions **from top to bottom**, one line at a time. That is why the three lines appear in the same order you wrote them.

---

## 🔎 `Write` vs. `WriteLine`

There are two ways to show text:

| Instruction | What happens after the text |
|---|---|
| `Console.WriteLine("Hi");` | Moves to a **new line** |
| `Console.Write("Hi");` | **Stays** on the same line |

Try it. Replace your code with this and run it:

```csharp
Console.Write("Hello, ");
Console.Write("World!");
Console.WriteLine();
Console.WriteLine("Done.");
```

✅ **You should see:**

```text
Hello, World!
Done.
```

`Console.WriteLine();` with nothing inside the parentheses prints an **empty line** (it just moves to the next line).

---

## 🔁 The Lesson Routine

You will repeat these steps at the start of **every** lesson. Only the lesson number changes.

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson02
cd Lesson02
code .
```

Then: write your code in VS Code → type `dotnet run` in the Command Prompt → check the result.

---

## ⚠️ Common Mistakes

When there is a mistake, `dotnet run` shows an **error** instead of running your program. **Read the error.** It tells you the file, the **line number**, and the **position** on that line.

For example, `Program.cs(1,35)` means: *file Program.cs, line 1, character 35*.

### Mistake 1: Forgetting the semicolon `;`

```cs
Console.WriteLine("Hello, World!")
```

```text
Program.cs(1,35): error CS1002: ; expected
```

**Fix:** Add `;` at the end of the line.

### Mistake 2: Using a small letter `c` in `Console`

```cs
console.WriteLine("Hello, World!");
```

```text
Program.cs(1,1): error CS0103: The name 'console' does not exist in the current context
```

**Fix:** Write `Console` with a capital **C**. C# cares about capital letters.

### Mistake 3: Forgetting the closing quote `"`

```cs
Console.WriteLine("Hello, World!);
```

```text
Program.cs(1,19): error CS1010: Newline in constant
```

**Fix:** Text needs a quote at the start **and** at the end: `"Hello, World!"`.

> [!TIP]
> One small mistake can cause **several** error messages. Always fix the **first** error in the list, then run again.

### Mistake 4: Running from the wrong folder

```text
Couldn't find a project to run. Ensure a project exists in C:\CSharpCourse, or pass the path to the project using --project.
```

**Fix:** You are in the wrong folder. Go into your project folder first:

```cmd
cd C:\CSharpCourse\Lesson01
```

### Mistake 5: "I changed my code but the old message still shows"

**Fix:** Your file was not saved. Turn on **File → Auto Save** in VS Code (Step 6), or press **Ctrl + S** to save.

---

## 🏋️ Practice

Do these in your `Lesson01` project. Replace the code in `Program.cs` for each one.

**Exercise 1.** Print your full name, your course, and your school on three separate lines.

<details>
<summary>Show answer</summary>

```csharp
Console.WriteLine("Juan Dela Cruz");
Console.WriteLine("BS Information Technology");
Console.WriteLine("Northwest Samar State University");
```

</details>

**Exercise 2.** Print this small tree exactly as you see it:

```text
    *
   ***
  *****
 *******
    |
```

<details>
<summary>Show answer</summary>

```csharp
Console.WriteLine("    *");
Console.WriteLine("   ***");
Console.WriteLine("  *****");
Console.WriteLine(" *******");
Console.WriteLine("    |");
```

Spaces inside the quotes are printed too. That is how you move the stars to the right.

</details>

**Exercise 3.** Use `Console.Write` **three times** to print `C# is fun!` on **one** line. (Hint: `"C# "`, `"is "`, `"fun!"`.)

<details>
<summary>Show answer</summary>

```csharp
Console.Write("C# ");
Console.Write("is ");
Console.Write("fun!");
Console.WriteLine();
```

</details>

---

## 📌 Recap

- `dotnet new console -n Name` creates a new project.
- `dotnet run` builds and runs the project in the current folder.
- `code .` opens the current folder in VS Code.
- `Console.WriteLine("text");` shows text and moves to a new line. `Console.Write` stays on the same line.
- Text goes inside `" "`. Every instruction ends with `;`.
- `//` starts a comment, which is a note the computer ignores.
- Errors tell you the **line number**. Fix the **first** error first.

---

[⬅️ Lesson 00](00-before-you-start.md) | [🏠 Course Home](../README.md) | [Next: Lesson 02 (Variables) ➡️](02-variables.md)
