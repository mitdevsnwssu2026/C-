# 👩‍🏫 Instructor Guide

This guide is for teachers using **C# From Zero: Hello World to Secure Accounts** in a class.

---

## Course Summary

| | |
|---|---|
| **Audience** | Learners with **zero** programming or technical experience |
| **Tools** | Windows 10 or 11, **Visual Studio Community** (the free edition) with the **.NET desktop development** workload |
| **Language** | C# on .NET 10 (LTS) |
| **Database** | SQLite (no database server to install), used three ways: ADO.NET, Dapper, and EF Core Database First |
| **Size** | 30 lessons (00 to 29), about 40 hours of guided work, plus practice |
| **Assessment** | 30 quizzes, one per lesson, with answer keys (in the password-protected instructor package) |
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
| **Data entry** | Basic Data Entry (a complete entry form that checks every field) | 18 |
| **Database connections** | SQL; **ADO.NET** (connecting, data entry with parameters, CRUD, CSV upload with a transaction); **Dapper**; **EF Core Database First** | 19–25 |
| **Hashed accounts** | Hashing, salt, PBKDF2, register, login | 26–29 |

The **Topic Checklist** in the [README](README.md#-topic-checklist) links every topic to its lesson and part.

### Learning outcomes

By the end of the course, students can:

1. Create, run, and fix C# console programs in Visual Studio.
2. Use variables, strings, operators, conditionals, loops, arrays, lists, functions, classes, and enums.
3. Build a data entry form that refuses bad input, and handle runtime errors without crashing.
4. Design a database table, and write `CREATE`, `INSERT`, `SELECT`, `UPDATE`, and `DELETE` statements.
5. Perform full CRUD operations with **ADO.NET** and **parameterized queries**, then do the same work with **Dapper** and with **EF Core Database First**.
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
| **Do first, explain after** | Students get a working result quickly, then learn why it works |
| The same **5-click "create a project" block** at the start of every lesson | The routine becomes automatic through repetition |
| **Operators before conditionals** (Lesson 06 before Lesson 07) | One new idea at a time |
| **Basic Data Entry (Lesson 18)** before databases | Students combine everything they know in one project, and learn "never let bad data in" before any SQL |
| **ADO.NET first, then Dapper, then EF Core** (Lessons 20–25) | Students see the real SQL first. Then each tool removes more of the work, and they understand what it does for them |
| **Dapper and EF Core reuse the same Students database** | Students compare three tools on the same data, and the Lesson 24 database becomes the Lesson 25 "existing database" |
| Run with **Ctrl + F5** (Start Without Debugging) | The console window stays open, and crashes are shown as plain text instead of pausing in the debugger |
| **Top-level statements** (no `class Program` / `static void Main`) | It is the default template. Students write real code from line 1 |
| **`Console.ReadLine() ?? ""`** everywhere, and properties written as `= "";` | No nullable warnings to confuse beginners |
| **SQLite** instead of MySQL or SQL Server | Nothing extra to install or start. Lesson 20 shows that the same ADO.NET pattern works with SQL Server and MySQL |
| **Parameters from the very first `INSERT`** (Lesson 21) | SQL injection is taught before a bad habit can form |
| **PBKDF2** (built into .NET), 600,000 iterations | No extra dependency. Follows the OWASP recommendation for PBKDF2-HMAC-SHA256 |
| **Exercises plus one Bonus Challenge** in every lesson, with hidden answers | Students practice, then stretch |

### Version note for EF Core (Lesson 25)

EF Core's version must match the .NET version: .NET 10 needs EF Core **10.x**. The newest EF Core in the NuGet list may be for a newer .NET and will refuse to install. Lesson 25 tells students to pick the highest version that starts with their .NET number. The lesson was tested with EF Core 10.0.15 and Dapper 2.1.89.

### About the tests

- Every lesson's finished program was built with zero warnings and run with sample input.
- In the lesson files, blocks marked ` ```csharp ` are **complete programs**. All of them were compiled automatically. Blocks marked ` ```cs ` are **fragments** (a few lines to add or change).
- Error messages quoted in the lessons were captured from the real C# compiler and runtime.

> [!IMPORTANT]
> **Please do one walk-through before your first class.** The C# code was tested by building and running it. The Visual Studio click paths (installer, menus, dialogs, the NuGet window, and the Package Manager Console) were written from Microsoft's documented steps for Visual Studio 2026, but they were **not** tested on a Windows computer. Follow Lessons 00, 01, 14 (adding a class file), 19 (NuGet), 23 (data files), and 25 (scaffolding) once on a lab PC, and adjust any button name that differs in your version of Visual Studio.

---

## 🔒 The Instructor Package (Quizzes and Finished Programs)

The folder [`instructor-only/`](instructor-only/) contains one file, `instructor-materials.7z`, locked with a password. **Students cannot open it without the password.** Ask the course owner for it, and do not share it with students.

Inside the package:

| Folder | Contents |
|---|---|
| `quizzes/` | 30 quizzes (Quiz 00 to Quiz 29), one per lesson, as Word files you can print or edit |
| `answer-keys/` | The answer key for every quiz, with scoring notes and sample solutions |
| `quiz-sources/` | The same quizzes and keys as Markdown files, for editing |
| `code/` | The finished, tested program for every lesson (`Lesson01` to `Lesson29`) |

**To open it:** install the free program **7-Zip** (https://www.7-zip.org). Right-click `instructor-materials.7z` → **7-Zip** → **Extract Here**, then type the password. (The Windows built-in "Extract All" cannot open password-protected 7z files.)

### How the quizzes work

Each quiz is about **20 points** (Quiz 00 is 10) and takes about **30 minutes**. Each one checks **its own lesson**, with a small part that looks back at earlier lessons. The format is deliberately **different from the lesson exercises**:

| Part | Format | Points |
|---|---|---|
| A, B | Multiple choice, true or false, matching, "name the part", put in order | 8 |
| C | **What is the output?** Read a short program, and write what it shows | 4 |
| D | Find and fix the bug, complete the code, or write one line | 2 |
| E | **Looking back:** two quick questions about earlier lessons | 2 |
| F | **Hands-on:** a small program to write in Visual Studio | 4 |

Part F needs a computer. For a paper-only quiz, skip Part F and grade out of 16.

The quizzes assume that the student has **done the lesson**, so they are a little more challenging than the exercises, but every question uses only what has been taught so far. Every "What is the output?" answer in the keys was checked by running the program, and every sample solution was built.

---

## Before the First Class

- [ ] Install **Visual Studio Community** with the **.NET desktop development** workload on every lab computer, or ask students to do [Lesson 00](lessons/00-before-you-start.md) at home. The download is several gigabytes.
- [ ] On each computer, open Visual Studio once and get past the sign-in and theme screens.
- [ ] Check that students can create projects in `C:\CSharpCourse`. If not, choose another folder (for example `D:\CSharpCourse`) and tell students to use it as the **Location** in every lesson. (In Lesson 25, the path in the `Scaffold-DbContext` command must change too.)
- [ ] Check that the computers can reach **nuget.org**. Lessons 19 to 25, 28, and 29 install packages from there.
- [ ] Install **7-Zip** on your own computer, open the instructor package, and print the quizzes you need.
- [ ] Open `code/Lesson29` from the package (**Open a project or solution** → `Lesson29.csproj`) and run it, so you know what the final project looks like.

---

## Suggested Schedules

### Option A: 15 weeks, 2 sessions per week (about 1.5 hours each)

| Week | Session 1 | Session 2 |
|---|---|---|
| 1 | Lesson 00: Setup | Lesson 01: Hello World |
| 2 | Lesson 02: Variables | Lesson 03: User Input |
| 3 | Lesson 04: String Manipulation | Lesson 05: Arithmetic Operators |
| 4 | Lesson 06: Comparison, Logical, Mixed | Lesson 07: If, Else, Else If |
| 5 | Lesson 08: Nested If, Switch, Challenge | Lesson 09: for, while, do-while |
| 6 | Lesson 10: Nested Loops, Loop Control | Lesson 11: Arrays, Array Challenge |
| 7 | Lesson 12: Lists | Lesson 13: Functions |
| 8 | Lesson 14: Classes | Lesson 15: Enums |
| 9 | Lesson 16: Errors | Lesson 17: Files |
| 10 | Lesson 18: Basic Data Entry (project) | Lesson 19: Databases and SQL |
| 11 | Lesson 20: ADO.NET Connecting | Lesson 21: ADO.NET Data Entry |
| 12 | Lesson 22: ADO.NET CRUD | Lesson 23: ADO.NET CSV Upload |
| 13 | Lesson 24: Dapper | Lesson 25: EF Core Database First |
| 14 | Lesson 26: Hashing | Lesson 27: Password Hashing |
| 15 | Lesson 28: Register | Lesson 29: Log In · **Final project check** |

Give each lesson's quiz at the start of the **next** session (about 30 minutes), or as homework.

### Option B: Intensive (8 full days)

| Day | Lessons | Unit |
|---|---|---|
| 1 | 00 – 04 | Setup, C# syntax |
| 2 | 05 – 08 | Operators, conditionals |
| 3 | 09 – 12 | Loops, arrays, lists |
| 4 | 13 – 17 | Functions, classes, enums, errors, files |
| 5 | 18 – 20 | Data entry, SQL, ADO.NET |
| 6 | 21 – 23 | ADO.NET data entry, CRUD, CSV upload |
| 7 | 24 – 27 | Dapper, EF Core, hashing |
| 8 | 28 – 29 | Final project, and final project check |

---

## Teaching Tips

1. **Project your screen and do the "Step by Step" part live.** Say the name of every button before you click it.
2. **Make a mistake on purpose**, and read the Error List out loud. Teach students to double-click the row to jump to the mistake.
3. **Use "guess, then run".** Before running a program, ask: *"What do you think it will show?"* The quizzes' Part C uses the same skill.
4. **Lessons 01–18: no copy and paste.** From Lesson 19, copying is allowed, but ask students to explain lines back to you.
5. **Let students attempt the two Challenges alone first** (Lessons 08 and 11), and treat Lesson 18 as a mini project.
6. **Lessons 22, 24, and 25 are a set.** After Lesson 25, put the three versions side by side (the comparison table in Lesson 25) and discuss when each tool fits.
7. **Lesson 21 (SQL injection) and Lesson 26 (password guessing) are memorable.** Let students run the attack demos themselves.

### The most common student problems

| Problem | Where it appears | Quick fix |
|---|---|---|
| Picked **Console App (.NET Framework)** | Lesson 01 onward | Recreate the project with the right template |
| Clicked **Yes** on the "build errors" message | Lesson 01 onward | Always click **No**, then read the Error List |
| An old console window is still running ("being used by another process") | Lesson 03 onward | Close all black windows, then run again |
| Used a string tool but did not keep the result (`name.ToUpper();`) | Lesson 04 | `name = name.ToUpper();` |
| `int / int` loses the decimals | Lesson 05 | Use `double` |
| `=` instead of `==` | Lessons 06–07 | Read `==` as "is equal to" |
| Infinite loop | Lessons 09–10 | Close the console window, then check what changes the condition |
| Index out of range (`i <= Length`) | Lessons 11–12 | Use `i < Length`. Indexes start at 0 |
| Did not delete the starter code in a new class file | Lessons 14, 18, 22, 24, 27, 28 | **Ctrl + A**, **Delete**, paste again |
| Put code below an enum in `Program.cs` | Lesson 15 | Enums go at the very bottom |
| Forgot the NuGet package | Lessons 19–25, 28–29 | `CS0234` or `CS0246` means a package is missing |
| Forgot **Copy to Output Directory** | Lesson 23 | `File not found` → set it to **Copy if newer** |
| Dapper placeholder does not match the property (`@fullname` vs `FullName`) | Lesson 24 | Spell it exactly like the property |
| Installed an EF Core version for a newer .NET (`NU1202`) | Lesson 25 | Pick a version that starts with the .NET number |
| Forgot `db.SaveChanges()` | Lesson 25 | Nothing is saved without it |
| Changed `CREATE TABLE`, but the old table remains | Lessons 21–29 | Delete the `.db` file in `bin\Debug\net10.0` |

---

## Assessment Ideas

- **Quizzes:** one per lesson, in the instructor package.
- **Unit challenges:** the Conditional Challenge (Lesson 08) and the Array Challenge (Lesson 11) work well as graded hands-on tests.
- **Mid-course project (after Lesson 18):** "Personal Expense Tracker": an entry form for expenses (description, amount, and a category that is an **enum**) that checks every field, shows a table, and saves to a CSV file.
- **Database project (after Lesson 25):** "Library Book Records": full CRUD for books with ADO.NET, then a second version of the same project with Dapper or EF Core.

### Final project rubric (Lessons 28–29)

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

The test checklist in Lesson 29 ("Exercise: Test Your Final Project") can be used directly as your checking sheet.

[🏠 Course Home](README.md)
