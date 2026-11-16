# Lesson 01: Hello World (Your First Program)

**Part 1: Beginner** · About 45 minutes

> **Before you start:** Visual Studio must be installed. If it is not, do [Lesson 00: Setup](00-before-you-start.md) first.

## What You Will Learn

- How to create a new project in Visual Studio (click by click)
- How to run a program and see the result in the black console window
- How to change the program so it shows your own message

## The Idea

"Hello, World!" is the first program almost every programmer writes. It does one small thing: it shows a message on the screen.

It is small, but it proves that everything works. After this lesson, you are a programmer. 🎉

---

## Step by Step

Follow each step in order. Do exactly one thing per step.

### Step 1: Open Visual Studio

**1.** Click the purple **Visual Studio** icon on your taskbar.

(No icon there? Click the **Start** button, type `Visual Studio`, and click **Visual Studio** with the purple icon.)

✅ **You will see:** the start window. On the right side, under **Get started**, there are four buttons.

### Step 2: Click "Create a new project"

A **project** is a folder that holds everything for one program. Each lesson gets its own project.

**1.** Click the last button on the right: **Create a new project**.

✅ **You will see:** a window with the title **Create a new project**. It shows a long list of project types.

### Step 3: Choose "Console App"

A **Console App** is a program that shows text in the black console window. That is the kind of program we want.

**1.** Click the **search box** at the top of the window. Type: `console`

**2.** Just below the search box, click the first small menu (it says **All languages**). Choose **C#**.

**3.** In the list, click **Console App**.

> [!IMPORTANT]
> There are two choices with almost the same name. Pick the right one:
>
> - ✅ **Console App** (the small labels under it include **C#**, **Windows**, and **Console**)
> - ❌ **Console App (.NET Framework)** ← do **not** pick this one

**4.** Click the **Next** button at the bottom-right corner.

✅ **You will see:** a window with the title **Configure your new project**.

### Step 4: Name your project

**1.** Click the **Project name** box. Delete what is in it, and type: `Lesson01`

**2.** Click the **Location** box. Delete what is in it, and type: `C:\CSharpCourse`

> This is the folder where all your lessons will be saved. Visual Studio creates the folder for you.

**3.** Click the small box next to **Place solution and project in the same directory** so that it has a **check mark**.

**4.** Click **Next**.

✅ **You will see:** a window with the title **Additional information**.

### Step 5: Finish creating the project

**1.** Look at **Framework**. Do not change it. (It says something like **.NET 10.0 (Long Term Support)**.)

**2.** Look at the box **Do not use top-level statements**. It must be **empty** (no check mark). If it has a check mark, click it to remove it.

**3.** Click **Create**.

Wait a few seconds.

✅ **You will see:** the main Visual Studio window, with a little bit of code already in the middle.

### Step 6: Get to know the window

This is where you will work in every lesson. You only need to know **four** places:

```text
┌──────────────────────────────────────────────────────────────────────┐
│  File   Edit   View   Git   Project   Build   Debug   Test   ...     │ ← 1. MENU BAR
├─────────────────────────────────────────────┬────────────────────────┤
│  Program.cs                                 │  Solution Explorer     │
│                                             │                        │
│   1   // See https://aka.ms/new-console-... │   Solution 'Lesson01'  │
│   2   Console.WriteLine("Hello, World!");   │     Lesson01           │
│                                             │       Dependencies     │
│                                             │       Program.cs       │
│              2. THE EDITOR                  │                        │
│          (you type your code here)          │  3. SOLUTION EXPLORER  │
│                                             │     (your files)       │
├─────────────────────────────────────────────┴────────────────────────┤
│  Error List                                                          │ ← 4. ERROR LIST
│  (your mistakes are listed here)                                     │
└──────────────────────────────────────────────────────────────────────┘
```

1. **Menu bar** (top): the words **File**, **Edit**, **View**, **Debug**, and so on. You click these to give Visual Studio commands.
2. **The editor** (big area in the middle): where you type your code. The tab at the top says **Program.cs**. That is the name of the file that holds your code.
3. **Solution Explorer** (right side): the list of files in your project.
4. **Error List** (bottom): if you make a mistake, Visual Studio lists it here.

> If you do not see the **Error List** at the bottom, do not worry. It appears when there is a mistake. You can also open it yourself: click **View** on the menu bar, then click **Error List**.

### Step 7: Run the program

Visual Studio already wrote a tiny program for you. Let's run it.

**1.** On the menu bar, click **Debug**.

**2.** In the menu that opens, click **Start Without Debugging**.

✅ **You will see:** a **black window** appears. This is the **console**. It shows:

```text
Hello, World!

C:\CSharpCourse\Lesson01\bin\Debug\net10.0\Lesson01.exe (process 12345) exited with code 0 (0x0).
Press any key to close this window . . .
```

**You just ran your first C# program!** 🎉

- The **first line**, `Hello, World!`, is what **your program** showed.
- The **last two lines** are added by Visual Studio. They mean: *"The program has finished. Press a key to close this window."* You will see them after every program. (The number after `process` is different every time. That is normal.)

**3.** Press any key on your keyboard (for example **Enter**) to close the black window.

> [!TIP]
> **A faster way to run:** hold **Ctrl** and press **F5**. On some laptops, you must hold **Ctrl** and **Fn**, then press **F5**. From now on, this course says: *"Run the program (**Ctrl + F5**)."*

### Step 8: Change the program

Now make it say something of your own.

**1.** Click anywhere inside the **editor** (on the code).

**2.** Hold **Ctrl** and press **A**. All the code becomes highlighted (selected).

**3.** Press the **Delete** key. The editor is now empty.

**4.** Type the code below, exactly as you see it. Use your own name in the second line.

> Type it yourself. Do not copy and paste. Typing is how your fingers and your brain learn.

## The Code

```csharp
// My first C# program
Console.WriteLine("Hello, World!");
Console.WriteLine("My name is Juan Dela Cruz.");
Console.WriteLine("I am learning C#!");
```

**5.** Run the program (**Ctrl + F5**, or **Debug** → **Start Without Debugging**).

## The Output

✅ **You will see** this in the black window (plus the two lines from Visual Studio at the end):

```text
Hello, World!
My name is Juan Dela Cruz.
I am learning C#!
```

**6.** Press any key to close the black window.

> From now on, the output boxes in this course show only **your program's** text. They leave out the two lines that Visual Studio adds at the end.

---

## What the Code Means

**Line 1** starts with `//`.

```cs
// My first C# program
```

This is a **comment**: a note for people. The computer skips everything after `//` on that line. Visual Studio shows comments in green.

**Lines 2, 3, and 4** are **instructions**. Each one shows one line of text.

```cs
Console.WriteLine("Hello, World!");
```

Read it piece by piece:

- `Console` means the black console window.
- `.` (the dot) means "use something that belongs to it".
- `WriteLine` means "write a line of text, then go to the next line".
- `( )` holds the thing you want to write.
- `"Hello, World!"` is the text. **Text always goes inside double quotes** `" "`.
- `;` ends the instruction. It is like the period at the end of a sentence.

So the whole line says: *"Console, write this line of text: Hello, World!"*

**The computer runs your instructions from top to bottom**, one line at a time. That is why the three messages appear in the same order you typed them.

### `Write` or `WriteLine`?

- `Console.WriteLine("Hi");` shows the text, then moves to a **new line**.
- `Console.Write("Hi");` shows the text and **stays** on the same line.

Try it. Replace your code with this (Ctrl + A, Delete, then type) and run it:

```csharp
Console.Write("Hello, ");
Console.Write("World!");
Console.WriteLine();
Console.WriteLine("Done.");
```

✅ **You will see:**

```text
Hello, World!
Done.
```

`Console.WriteLine();` with nothing inside the `( )` just moves to the next line.

---

## Closing and Opening Your Project

**To close:** click **File** on the menu bar, then click **Exit**. Visual Studio saves your work.

**To open it again later:** open Visual Studio. On the left side of the start window, under **Open recent**, click **Lesson01**.

---

## If Something Goes Wrong

### A message asks: "There were build errors. Would you like to continue and run the last successful build?"

This means there is a **mistake in your code**, so Visual Studio could not run it.

**1.** Click **No**. (Always click **No** on this message.)

**2.** Look at the **Error List** at the bottom of the window. Each row is one mistake. The row tells you **what** is wrong and on which **Line**.

**3.** **Double-click** the row. Visual Studio jumps to the place of the mistake. The mistake also has a **red wavy line** under it in the editor.

**4.** Fix it, then run again.

Here are the three most common mistakes in this lesson:

**Mistake 1: You forgot the `;` at the end.**

```cs
Console.WriteLine("Hello, World!")
```

The Error List says: `CS1002  ; expected`

**Fix:** type `;` at the end of the line.

**Mistake 2: You typed `console` with a small `c`.**

```cs
console.WriteLine("Hello, World!");
```

The Error List says: `CS0103  The name 'console' does not exist in the current context`

**Fix:** type `Console` with a capital **C**. In C#, capital letters and small letters are different.

**Mistake 3: You forgot the closing quote `"`.**

```cs
Console.WriteLine("Hello, World!);
```

The Error List says: `CS1010  Newline in constant`

**Fix:** text needs a quote at the start **and** at the end: `"Hello, World!"`

> [!TIP]
> One small mistake can cause several rows in the Error List. Always fix the **first** row, then run again. The other rows often disappear.

### The code that Visual Studio wrote looks long, with words like `namespace`, `class Program`, and `static void Main`

You picked the wrong project type, or the box **Do not use top-level statements** had a check mark.

**Fix:** close Visual Studio, and start again from Step 1. In Step 3, choose **Console App** (not ".NET Framework"). In Step 5, make sure the box is **empty**.

### I can't see the Solution Explorer or the Error List

Click **View** on the menu bar, then click **Solution Explorer** or **Error List**.

### I clicked the solid green ▶ button at the top instead

That button also runs your program, but in a special mode called *debugging* that we do not use in this course. It is not a problem. Close the black window, and next time use **Debug** → **Start Without Debugging** (**Ctrl + F5**).

---

## Exercise

Do these in your `Lesson01` project. For each one, clear the editor (**Ctrl + A**, then **Delete**), type your code, and run it (**Ctrl + F5**).

**Exercise 1.** Show your full name, your course, and your school on three separate lines.

<details>
<summary>Show answer</summary>

```csharp
Console.WriteLine("Juan Dela Cruz");
Console.WriteLine("BS Information Technology");
Console.WriteLine("Northwest Samar State University");
```

</details>

**Exercise 2.** Show this small tree, exactly as you see it:

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

Spaces inside the quotes are shown too. That is how the stars move to the right.

</details>

**Exercise 3.** Use `Console.Write` **three times** to show `C# is fun!` on **one** line.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("C# ");
Console.Write("is ");
Console.Write("fun!");
Console.WriteLine();
```

</details>

## ⭐ Bonus Challenge

Make a **student ID card** like this one, with your own name and course. The right edge must line up.

```text
+------------------------+
| STUDENT ID             |
| Name:   Juan Dela Cruz |
| Course: BSIT           |
+------------------------+
```

Hint: every line must have the same number of characters. Add spaces before the last `|` until the edges line up.

<details>
<summary>Show answer</summary>

```csharp
Console.WriteLine("+------------------------+");
Console.WriteLine("| STUDENT ID             |");
Console.WriteLine("| Name:   Juan Dela Cruz |");
Console.WriteLine("| Course: BSIT           |");
Console.WriteLine("+------------------------+");
```

Your name is probably longer or shorter than this one. Add or remove `-` characters and spaces until every line is the same length.

</details>

---

## Remember

- To start a lesson: open Visual Studio → **Create a new project** → **Console App** (C#) → name it → **Create**.
- To run: **Debug** → **Start Without Debugging**, or **Ctrl + F5**.
- `Console.WriteLine("text");` shows text and goes to a new line. `Console.Write` stays on the same line.
- Text goes inside `" "`. Every instruction ends with `;`.
- `//` starts a comment. The computer skips it.
- If there is a mistake: click **No**, read the **Error List**, double-click the row, fix it, run again.

---

[⬅️ Lesson 00](00-before-you-start.md) | [🏠 Course Home](../README.md) | [Next: Lesson 02 (Variables) ➡️](02-variables.md)
