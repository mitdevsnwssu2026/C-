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
| **Total time** | About 28 hours of guided work, plus practice |
| **Final outcome** | A console account system with registration, login, and password change, storing PBKDF2-hashed passwords in a database |

### Learning outcomes

By the end of the course, students can:

1. Create, run, and fix C# console programs in Visual Studio.
2. Use variables, input, operators, decisions, loops, lists, methods, and classes.
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

Design choices that keep beginners from getting lost:

| Choice | Reason |
|---|---|
| **Setup is Lesson 00**, with every installer click written out | Students with no technical background can install the tools alone |
| **One click or one action per numbered step**, with the exact button name in bold | No guessing about what to press next |
| A **"✅ You will see"** check after each important step | Students can confirm they are on track before moving on |
| **Do first, explain after.** The steps come before the explanation of the code | Students get a working result quickly, then learn why it works |
| The same **5-click "create a project" block** at the start of every lesson | The routine becomes automatic through repetition |
| Run with **Ctrl + F5** (Start Without Debugging) | The console window stays open, and crashes are shown as plain text instead of pausing in the debugger |
| **Top-level statements** (no `class Program` / `static void Main`) | It is the default template. Students write real code from line 1 |
| **`Console.ReadLine() ?? ""`** everywhere, and properties written as `= "";` | No nullable warnings to confuse beginners. Taught as a fixed habit in Lesson 03 |
| **Only four basic types** (`string`, `int`, `double`, `bool`), plus `long` when needed | Fewer concepts at the start |
| **SQLite** instead of MySQL or SQL Server | Nothing extra to install or start. The C# pattern (connection → command → parameters → execute) is the same for every database |
| **Plain SQL** through `Microsoft.Data.Sqlite`, not Entity Framework | Students see and understand the real SQL |
| **Parameters from the very first `INSERT`** (Lesson 14) | SQL injection is taught before a bad habit can form |
| **PBKDF2** (built into .NET), 600,000 iterations | No extra dependency. Follows the OWASP recommendation for PBKDF2-HMAC-SHA256 |
| **Exercises plus one Bonus Challenge** in every lesson, with hidden answers | Students practice, then stretch. Fast students always have something to do |

### About the code

- The [`code/`](code/) folder holds the **finished program for every lesson**. Each one was built with zero warnings and run with sample input.
- In the lesson files, blocks marked ` ```csharp ` are **complete programs**. They were all compiled automatically. Blocks marked ` ```cs ` are **fragments** (a few lines to add or change).
- Error messages quoted in the lessons were captured from the real C# compiler and runtime.

> [!IMPORTANT]
> **Please do one walk-through before your first class.** The C# code was tested by building and running it. The Visual Studio click paths (installer, menus, dialogs) were written from Microsoft's documented steps for Visual Studio 2026, but they were **not** tested on a Windows computer. Follow Lesson 00, Lesson 01, Lesson 12 (NuGet), and Lesson 16 (files) once on a lab PC, and adjust any button name that differs in your version of Visual Studio. Adding your own **screenshots** to Lessons 00 and 01 will help beginners a lot.

---

## Before the First Class

- [ ] Install **Visual Studio Community** with the **.NET desktop development** workload on every lab computer, or ask students to do [Lesson 00](lessons/00-before-you-start.md) at home. The download is several gigabytes, so do not leave this for the first class meeting.
- [ ] On each computer, open Visual Studio once and get past the sign-in and theme screens.
- [ ] Check that students can create projects in `C:\CSharpCourse`. If lab computers do not allow that, choose another folder (for example `D:\CSharpCourse`) and tell students to use it as the **Location** in every lesson.
- [ ] Check that the computers can reach **nuget.org**. Lessons 12 to 20 install the `Microsoft.Data.Sqlite` package from there.
- [ ] Open [`code/Lesson20`](code/Lesson20/) yourself (**Open a project or solution** → `Lesson20.csproj`) and run it, so you know what the final project looks like.

---

## Suggested Schedules

### Option A: 10 weeks, 2 sessions per week (about 1.5 hours each)

| Week | Session 1 | Session 2 |
|---|---|---|
| 1 | Lesson 00: Setup | Lesson 01: Hello World |
| 2 | Lesson 02: Variables | Lesson 03: User Input |
| 3 | Lesson 04: Math | Lesson 05: Decisions |
| 4 | Lesson 06: Loops | Lesson 07: Lists · **Quiz 1** (Lessons 01–07) |
| 5 | Lesson 08: Methods | Lesson 09: Classes |
| 6 | Lesson 10: Errors | Lesson 11: Files · **Quiz 2** (Lessons 08–11) |
| 7 | Lesson 12: Databases and SQL | Lesson 13: Connecting |
| 8 | Lesson 14: Inserting safely | Lesson 15: Records manager |
| 9 | Lesson 16: CSV upload · **Quiz 3** (Lessons 12–16) | Lessons 17–18: Hashing |
| 10 | Lesson 19: Register | Lesson 20: Log in · **Final project check** |

### Option B: Intensive (5 full days)

| Day | Lessons |
|---|---|
| 1 | 00 – 05 |
| 2 | 06 – 10 |
| 3 | 11 – 14 |
| 4 | 15 – 18 |
| 5 | 19 – 20, final project check |

---

## Teaching Tips

1. **Project your screen and do the "Step by Step" part live.** Go slowly. Say the name of every button before you click it.
2. **Make a mistake on purpose**, and read the Error List out loud. Teach students to double-click the row to jump to the mistake. This one skill saves more class time than anything else.
3. **Use "guess, then run".** Before running a program, ask: *"What do you think it will show?"*
4. **Lessons 01–11: no copy and paste.** Typing builds familiarity. From Lesson 12, copying is allowed, but ask students to explain lines back to you.
5. **Pair work** is effective from Lesson 08: one student types, the other reads the lesson and checks. Swap every 15 minutes.
6. **Lesson 14 (SQL injection) and Lesson 17 (password guessing) are memorable.** Let students run the attack demos themselves.
7. **Use the Bonus Challenge for fast finishers**, so that the rest of the class can finish the exercises without pressure.

### The most common student problems

| Problem | Where it appears | Quick fix |
|---|---|---|
| Picked **Console App (.NET Framework)** | Lesson 01 onward | Recreate the project with the right template |
| Clicked **Yes** on the "build errors" message | Lesson 01 onward | Teach: always click **No**, then read the Error List |
| An old console window is still running, so the build fails ("being used by another process") | Lesson 03 onward | Close all black windows, then run again |
| Used the solid green ▶ button, and the program "pauses" on a crash | Any lesson | Stop (red square), then use **Ctrl + F5** |
| Lowercase `console`, or a missing `;` | Lessons 01–04 | Read the Error List, double-click the row |
| `int / int` loses the decimals | Lesson 04 | Use `double` |
| `=` instead of `==` | Lesson 05 | Read `==` as "is equal to" |
| Did not delete the starter code in a new class file | Lessons 09, 15, 18, 19 | **Ctrl + A**, **Delete**, paste again |
| Typed code into the wrong tab | Lesson 09 onward | Check the tab name before typing |
| Forgot the NuGet package | Lessons 12–20 | `CS0234` means the package is missing |
| Forgot **Copy to Output Directory** | Lesson 16 | `File not found` → set it to **Copy if newer** |
| Changed `CREATE TABLE`, but the old table remains | Lessons 14–20 | Delete the `.db` file in `bin\Debug\net10.0` |

---

## Assessment Ideas

### Quizzes

Each lesson's **Exercise** and **Bonus Challenge** can be used as graded work. The answers are in the lessons, so for graded work, change the numbers or the scenario. For example, use a library (`Book`) or a store (`Product`) instead of `Student`.

### Mid-course project (after Lesson 11)

**"Personal Expense Tracker"**: a menu program that adds expenses (description and amount), lists them, shows the total, and saves them to a CSV file so that they are still there after a restart.

### Database project (after Lesson 16)

**"Library Book Records"**: full CRUD for books (unique ISBN, title, author, year, copies), plus a CSV upload of new books that reports bad rows.

### Final project rubric (Lessons 19–20)

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

The test checklist in Lesson 20 ("Exercise: Test Your Final Project") can be used directly as your checking sheet. Bonus points: any of the **Bonus Challenges** in Lesson 20.

[🏠 Course Home](README.md)
