# 👩‍🏫 Instructor Guide

This guide is for teachers using **C# From Zero: Hello World to Secure Accounts** in a class.

---

## Course Summary

| | |
|---|---|
| **Audience** | Learners with **zero** programming or technical experience |
| **Tools** | Windows 10 or 11, **Visual Studio Community** (the free edition) with the **.NET desktop development** workload |
| **Language** | C# on .NET 10 (LTS) |
| **Database** | SQLite, through the `Microsoft.Data.Sqlite` NuGet package (no database server to install) |
| **Size** | 27 lessons (00 to 26), about 35 hours of guided work, plus practice |
| **Final outcome** | A console account system with registration, login, and password change, storing PBKDF2-hashed passwords in a database |

### Topic coverage

| Area | Topics | Lessons |
|---|---|---|
| **C# Syntax** | HelloWorld, Variables, User Input, String Manipulation | 01–04 |
| **Operators** | Arithmetic, Comparison, Logical, Mixed | 05–06 |
| **Conditionals** | Simple If, If Else, Nested If, Switch, Conditional Challenge | 07–08 |
| **Loops** | For Loop, While Loop, Do-While Loop, Nested Loops, Loop Control | 09–10 |
| **Arrays** | Array Basics, Array Access, Array Iteration, Array Operations, Array Challenge, Lists | 11–12 |
| **Functions** | Basic Functions, Functions With Parameters, return values | 13 |
| **Classes** | Simple Classes, Class Methods | 14 |
| **Enums** | Enums | 15 |
| **More tools** | Handling Errors, Saving to Files | 16–17 |
| **Database connections** | SQL, connecting, inserting (uploading) data, CRUD, CSV upload | 18–22 |
| **Hashed accounts** | Hashing, salt, PBKDF2, register, login | 23–26 |

The **Topic Checklist** in the [README](README.md#-topic-checklist) links every topic to its lesson and part.

### Learning outcomes

By the end of the course, students can:

1. Create, run, and fix C# console programs in Visual Studio.
2. Use variables, strings, operators, conditionals, loops, arrays, lists, functions, classes, and enums.
3. Handle bad input and runtime errors without crashing.
4. Design a database table, and write `CREATE`, `INSERT`, `SELECT`, `UPDATE`, and `DELETE` statements.
5. Connect C# to a database and perform full CRUD operations using **parameterized queries**.
6. Validate a CSV file and bulk-upload it into a database inside a **transaction**.
7. Explain hashing, salting, and slow password hashing, and why plain-text passwords are never stored.
8. Build a registration and login system that stores only **PBKDF2** password hashes.

---

## How the Lessons Are Built

Every lesson has the same parts, in the same order:

**What You Will Learn → The Idea → Step by Step → The Code → The Output → What the Code Means → If Something Goes Wrong → Exercise → ⭐ Bonus Challenge → Remember**

Lessons that cover several topics (for example Lesson 11: Arrays) are split into **Parts**, one per topic. Each part has its own short program and its own output.

Design choices that keep beginners from getting lost:

| Choice | Reason |
|---|---|
| **Setup is Lesson 00**, with every installer click written out | Students with no technical background can install the tools alone |
| **One click or one action per numbered step**, with the exact button name in bold | No guessing about what to press next |
| A **"✅ You will see"** check after each important step | Students can confirm they are on track before moving on |
| **Do first, explain after.** The steps come before the explanation of the code | Students get a working result quickly, then learn why it works |
| The same **5-click "create a project" block** at the start of every lesson | The routine becomes automatic through repetition |
| **Operators before conditionals.** Lesson 06 teaches comparisons as questions that give `True` or `False`, before `if` appears in Lesson 07 | One new idea at a time |
| Run with **Ctrl + F5** (Start Without Debugging) | The console window stays open, and crashes are shown as plain text instead of pausing in the debugger |
| **Top-level statements** (no `class Program` / `static void Main`) | It is the default template. Students write real code from line 1 |
| **`Console.ReadLine() ?? ""`** everywhere, and properties written as `= "";` | No nullable warnings to confuse beginners. Taught as a fixed habit in Lesson 03 |
| **"Function" and "method"** are introduced as the same thing (Lesson 13) | Students recognize both words, in the syllabus and in Visual Studio |
| **SQLite** instead of MySQL or SQL Server | Nothing extra to install or start. The C# pattern (connection → command → parameters → execute) is the same for every database |
| **Plain SQL** through `Microsoft.Data.Sqlite`, not Entity Framework | Students see and understand the real SQL |
| **Parameters from the very first `INSERT`** (Lesson 20) | SQL injection is taught before a bad habit can form |
| **PBKDF2** (built into .NET), 600,000 iterations | No extra dependency. Follows the OWASP recommendation for PBKDF2-HMAC-SHA256 |
| **Exercises plus one Bonus Challenge** in every lesson, with hidden answers | Students practice, then stretch. Fast students always have something to do |
| Two named **challenges**: the Conditional Challenge (Lesson 08) and the Array Challenge (Lesson 11) | A bigger task that combines a whole unit |

### About the code

- The [`code/`](code/) folder holds the **finished program for every lesson**. Each one was built with zero warnings and run with sample input.
- In the lesson files, blocks marked ` ```csharp ` are **complete programs**. They were all compiled automatically. Blocks marked ` ```cs ` are **fragments** (a few lines to add or change).
- Error messages quoted in the lessons were captured from the real C# compiler and runtime.

> [!IMPORTANT]
> **Please do one walk-through before your first class.** The C# code was tested by building and running it. The Visual Studio click paths (installer, menus, dialogs) were written from Microsoft's documented steps for Visual Studio 2026, but they were **not** tested on a Windows computer. Follow Lesson 00, Lesson 01, Lesson 14 (adding a class file), Lesson 18 (NuGet), and Lesson 22 (files) once on a lab PC, and adjust any button name that differs in your version of Visual Studio. Adding your own **screenshots** to Lessons 00 and 01 will help beginners a lot.

---

## Before the First Class

- [ ] Install **Visual Studio Community** with the **.NET desktop development** workload on every lab computer, or ask students to do [Lesson 00](lessons/00-before-you-start.md) at home. The download is several gigabytes, so do not leave this for the first class meeting.
- [ ] On each computer, open Visual Studio once and get past the sign-in and theme screens.
- [ ] Check that students can create projects in `C:\CSharpCourse`. If lab computers do not allow that, choose another folder (for example `D:\CSharpCourse`) and tell students to use it as the **Location** in every lesson.
- [ ] Check that the computers can reach **nuget.org**. Lessons 18 to 22, 25, and 26 install the `Microsoft.Data.Sqlite` package from there.
- [ ] Open [`code/Lesson26`](code/Lesson26/) yourself (**Open a project or solution** → `Lesson26.csproj`) and run it, so you know what the final project looks like.

---

## Suggested Schedules

### Option A: 13 weeks, 2 sessions per week (about 1.5 hours each)

| Week | Session 1 | Session 2 |
|---|---|---|
| 1 | Lesson 00: Setup | Lesson 01: Hello World |
| 2 | Lesson 02: Variables | Lesson 03: User Input |
| 3 | Lesson 04: String Manipulation | Lesson 05: Arithmetic Operators |
| 4 | Lesson 06: Comparison, Logical, Mixed Operators | Lesson 07: If, Else, Else If |
| 5 | Lesson 08: Nested If, Switch, Conditional Challenge | Lesson 09: for, while, do-while · **Quiz 1** (Lessons 01–08) |
| 6 | Lesson 10: Nested Loops, Loop Control | Lesson 11: Arrays, Array Challenge |
| 7 | Lesson 12: Lists · **Quiz 2** (Lessons 09–12) | Lesson 13: Functions |
| 8 | Lesson 14: Classes | Lesson 15: Enums |
| 9 | Lesson 16: Errors | Lesson 17: Files · **Quiz 3** (Lessons 13–17) |
| 10 | Lesson 18: Databases and SQL | Lesson 19: Connecting |
| 11 | Lesson 20: Inserting safely | Lesson 21: Records manager |
| 12 | Lesson 22: CSV upload · **Quiz 4** (Lessons 18–22) | Lessons 23–24: Hashing |
| 13 | Lesson 25: Register | Lesson 26: Log in · **Final project check** |

### Option B: Intensive (7 full days)

| Day | Lessons | Unit |
|---|---|---|
| 1 | 00 – 04 | Setup, C# syntax |
| 2 | 05 – 08 | Operators, conditionals |
| 3 | 09 – 12 | Loops, arrays, lists |
| 4 | 13 – 17 | Functions, classes, enums, errors, files |
| 5 | 18 – 20 | Databases: SQL, connecting, inserting |
| 6 | 21 – 24 | CRUD, CSV upload, hashing |
| 7 | 25 – 26 | Final project, and final project check |

---

## Teaching Tips

1. **Project your screen and do the "Step by Step" part live.** Go slowly. Say the name of every button before you click it.
2. **Make a mistake on purpose**, and read the Error List out loud. Teach students to double-click the row to jump to the mistake. This one skill saves more class time than anything else.
3. **Use "guess, then run".** Before running a program, ask: *"What do you think it will show?"* Lesson 06 (operators) is built for this.
4. **Lessons 01–17: no copy and paste.** Typing builds familiarity. From Lesson 18, copying is allowed, but ask students to explain lines back to you.
5. **Pair work** is effective from Lesson 13: one student types, the other reads the lesson and checks. Swap every 15 minutes.
6. **Let students attempt the two Challenges alone first** (Lesson 08 and Lesson 11). Show the answer only after a real attempt.
7. **Lesson 20 (SQL injection) and Lesson 23 (password guessing) are memorable.** Let students run the attack demos themselves.
8. **Use the Bonus Challenge for fast finishers**, so that the rest of the class can finish the exercises without pressure.

### The most common student problems

| Problem | Where it appears | Quick fix |
|---|---|---|
| Picked **Console App (.NET Framework)** | Lesson 01 onward | Recreate the project with the right template |
| Clicked **Yes** on the "build errors" message | Lesson 01 onward | Teach: always click **No**, then read the Error List |
| An old console window is still running, so the build fails ("being used by another process") | Lesson 03 onward | Close all black windows, then run again |
| Used the solid green ▶ button, and the program "pauses" on a crash | Any lesson | Stop (red square), then use **Ctrl + F5** |
| Lowercase `console`, or a missing `;` | Lessons 01–05 | Read the Error List, double-click the row |
| Used a string tool but did not keep the result (`name.ToUpper();`) | Lesson 04 | `name = name.ToUpper();` |
| `int / int` loses the decimals | Lesson 05 | Use `double` |
| `=` instead of `==` | Lessons 06–07 | Read `==` as "is equal to" |
| A missing `}` in a nested `if` or nested loop | Lessons 08, 10 | **Edit → Advanced → Format Document**, then check the indentation |
| An infinite loop | Lessons 09–10 | Close the console window, then check what changes the condition |
| Index out of range (`i <= Length`) | Lessons 11–12 | Use `i < Length`. Indexes start at 0 |
| Did not delete the starter code in a new class file | Lessons 14, 21, 24, 25 | **Ctrl + A**, **Delete**, paste again |
| Typed code into the wrong tab | Lesson 14 onward | Check the tab name before typing |
| Put code below an enum in `Program.cs` | Lesson 15 | Enums go at the very bottom |
| Forgot the NuGet package | Lessons 18–26 | `CS0234` means the package is missing |
| Forgot **Copy to Output Directory** | Lesson 22 | `File not found` → set it to **Copy if newer** |
| Changed `CREATE TABLE`, but the old table remains | Lessons 20–26 | Delete the `.db` file in `bin\Debug\net10.0` |

---

## Assessment Ideas

### Quizzes

Each lesson's **Exercise** and **Bonus Challenge** can be used as graded work. The answers are in the lessons, so for graded work, change the numbers or the scenario. For example, use a library (`Book`) or a store (`Product`) instead of `Student`.

### Unit challenges

The **Conditional Challenge** (Lesson 08) and the **Array Challenge** (Lesson 11) work well as graded hands-on tests. Give students the task and the sample output, and hide the answer.

### Mid-course project (after Lesson 17)

**"Personal Expense Tracker"**: a menu program that adds expenses (description, amount, and a category that is an **enum**), lists them, shows the total, and saves them to a CSV file so that they are still there after a restart.

### Database project (after Lesson 22)

**"Library Book Records"**: full CRUD for books (unique ISBN, title, author, year, copies), plus a CSV upload of new books that reports bad rows.

### Final project rubric (Lessons 25–26)

| Criteria | Points |
|---|---|
| **Registration** with username rules and a duplicate check | 15 |
| **Password rules**, typed twice, hidden typing (`*`) | 10 |
| **Only PBKDF2 hashes are stored** (check the database) | 20 |
| **Login** with `Verify`, the same message for a wrong username or password, 3 tries | 15 |
| **Change password** requires the current password | 10 |
| **All SQL uses parameters** (no joining user input into SQL text) | 15 |
| **Code organization** (one job per file), clear names, no crashes on bad input | 10 |
| **Explanation**: the student explains hashing, salt, and SQL injection in their own words | 5 |
| **Total** | **100** |

The test checklist in Lesson 26 ("Exercise: Test Your Final Project") can be used directly as your checking sheet. Bonus points: any of the **Bonus Challenges** in Lesson 26.

[🏠 Course Home](README.md)
