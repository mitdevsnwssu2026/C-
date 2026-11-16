# 👩‍🏫 Instructor Guide

This guide is for teachers using **C# From Zero: Hello World to Secure Accounts** in a class.

---

## Course Summary

| | |
|---|---|
| **Audience** | Learners with **zero** programming or technical experience |
| **Platform** | Windows, Command Prompt, VS Code, .NET 10 (LTS) |
| **Database** | SQLite through `Microsoft.Data.Sqlite` (nothing to install on the computers except .NET) |
| **Total time** | About 27 hours of guided work, plus practice |
| **Final outcome** | A console account system with registration, login, and password change, storing PBKDF2-hashed passwords in a database |

### Learning outcomes

By the end of the course, students can:

1. Create, build, and run C# console programs from the Command Prompt.
2. Use variables, input, operators, decisions, loops, lists, methods, and classes.
3. Handle bad input and runtime errors without crashing.
4. Design a database table, and write `CREATE`, `INSERT`, `SELECT`, `UPDATE`, and `DELETE` statements.
5. Connect C# to a database and perform full CRUD operations using **parameterized queries**.
6. Validate a CSV file and bulk-upload it into a database inside a **transaction**.
7. Explain hashing, salting, and slow password hashing, and why plain-text passwords are never stored.
8. Build a registration and login system that stores only **PBKDF2** password hashes.

---

## Design Decisions (and Why)

These choices were made to keep students from getting stuck or second-guessing.

| Decision | Reason |
|---|---|
| **One consistent workflow** (`dotnet new` → `cd` → `code .` → `dotnet run`) in every lesson | Students never wonder "how do I start this time?" |
| **Top-level statements** (no `class Program` / `static void Main`) | It is the default .NET template. Students write real code from line 1, with no unexplained boilerplate |
| **`Console.ReadLine() ?? ""`** everywhere | Keeps the build free of nullable warnings, which confuse beginners. It is taught as a fixed rule in Lesson 03 |
| Properties written as `= "";` | Same reason: no `CS8618` warnings |
| **Only four basic types** (`string`, `int`, `double`, `bool`), plus `long` when needed | Fewer concepts. `decimal`, `var`, `char` details, etc. are left for later study |
| **SQLite** instead of MySQL or SQL Server | No server to install, configure, or start. The database is a single file next to the program. The C# pattern (connection → command → parameters → execute) is the same for every database |
| **Plain ADO.NET** (`Microsoft.Data.Sqlite`), not Entity Framework | Students see and understand the real SQL. EF Core is suggested as a next step |
| **Parameters from the very first `INSERT`** (Lesson 14) | SQL injection is taught before students can form a bad habit |
| **PBKDF2** (built into .NET), not a NuGet hashing package | No extra dependency, and students see the salt and iteration count explicitly. 600,000 iterations follows the OWASP recommendation for PBKDF2-HMAC-SHA256 |
| Every expected output is **shown exactly** | Students can check their own work immediately |
| Every `Common Mistakes` error message was **captured from the real compiler** | Students recognize the exact text they see on screen |

### Code conventions in the lessons

- Blocks marked ` ```csharp ` are **complete, compilable files**. They were all compiled automatically against .NET 10.
- Blocks marked ` ```cs ` are **fragments** (a few lines to add or change inside a bigger file).
- The [`code/`](code/) folder holds the **finished program for every lesson**. Each was built with zero warnings and run with sample input.

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
| 8 | Lesson 14: Inserting safely | Lesson 15: CRUD app |
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

## Before the First Class

- [ ] Install the **.NET 10 SDK** and **VS Code** (with the **C#** extension) on all lab computers, or ask students to complete [Lesson 00](lessons/00-before-you-start.md) at home.
- [ ] Check that `dotnet --version` works in a **new** Command Prompt window on each computer.
- [ ] Check that the lab computers can reach **nuget.org** (needed for `dotnet add package` in Lessons 12–20). If the network blocks it, prepare the packages in advance.
- [ ] Check that students can create folders in `C:\`. If not, use a folder they can write to (for example `D:\CSharpCourse`), and tell students to replace `C:\CSharpCourse` with it in every lesson.
- [ ] Run [`code/Lesson20`](code/Lesson20/) once yourself, so you know what the final project looks like.

---

## Teaching Tips

1. **Live-code the "Let's Build It" section.** Type slowly, and run the program often. Make a mistake on purpose, and read the error message out loud.
2. **Read errors together.** Teach students to find the **line number** in the message (`Program.cs(7,35)`). This one skill saves more class time than anything else.
3. **Use "predict, then run".** Before running a program, ask: *"What do you think it will show?"*
4. **Pair programming** works well from Lesson 08: one student types, the other reads the lesson and checks. Swap every 15 minutes.
5. **Lessons 01–11: no copy and paste.** Typing builds familiarity with syntax. From Lesson 12, copying is allowed, but ask students to explain lines back to you.
6. **Lesson 14 (SQL injection) and Lesson 17 (hash cracking) are memorable.** Let students run the attack demos themselves. Seeing the "hacker's name" saved harmlessly, or `sunshine` cracked in 5 tries, makes the lesson stick.
7. **Watch for the "two windows" confusion.** Some students type C# into the Command Prompt or commands into VS Code. The table in Lesson 00 ("VS Code = notebook, Command Prompt = play button") helps.

### The most common student problems

| Problem | Where it usually appears | Quick fix |
|---|---|---|
| Running `dotnet run` in the wrong folder | Every lesson | Check the prompt. `cd C:\CSharpCourse\LessonXX` |
| Forgetting to save | Lessons 01–05 | Turn on **File → Auto Save** in Lesson 01 |
| Lowercase `console`, or missing `;` | Lessons 01–04 | Read the error, find the line |
| `int / int` losing decimals | Lesson 04 | Use `double` |
| `=` vs `==` | Lesson 05 | Read `==` as "is equal to" |
| Class file in the wrong folder (inside `obj`) | Lesson 09 | Move it next to `Program.cs` |
| Forgetting `dotnet add package` | Lessons 12–20 | The `CS0234` error means the package is missing |
| Changed `CREATE TABLE` but the old table remains | Lessons 14–20 | `del school.db` (practice data only) |
| DB Browser keeps the database locked | Lessons 13–20 | Click **Close Database** before running the program |

---

## Assessment Ideas

### Quizzes

Each lesson's **Practice** section can be used directly as graded exercises. The answers are in the lessons (hidden under "Show answer"), so for graded work, change the numbers or the scenario. For example, use a library (`Book`) or a store (`Product`) instead of `Student`.

### Mid-course project (after Lesson 11)

**"Personal Expense Tracker"**: a menu program that adds expenses (description, amount), lists them, shows the total, and saves them to a CSV file so they are still there after a restart.

### Database project (after Lesson 16)

**"Library Book Records"**: full CRUD for books (ISBN unique, title, author, year, copies), plus a CSV upload of new books that reports bad rows.

### Final project rubric (Lessons 19–20)

| Criteria | Points |
|---|---|
| **Registration** with username rules and duplicate check | 15 |
| **Password rules**, confirmation, hidden typing (`*`) | 10 |
| **Only PBKDF2 hashes stored** (verify in the database) | 20 |
| **Login** with `Verify`, the same message for a wrong username or password, 3 tries | 15 |
| **Change password** that requires the current password | 10 |
| **All SQL uses parameters** (no string concatenation with user input) | 15 |
| **Code organization** (one job per file), readable names, no crashes on bad input | 10 |
| **Explanation**: the student explains hashing, salt, and SQL injection in their own words | 5 |
| **Total** | **100** |

Bonus points: any of the **Optional Challenges** in Lesson 20 (last login, delete account, admin, lockout).

---

## Running the Finished Programs

Every lesson's finished program is in `code/LessonXX`. To run one:

```cmd
cd code\Lesson20
dotnet run
```

Lessons 12–16 and 19–20 download the SQLite package automatically the first time they run (this needs internet). Database files (`*.db`) are created next to the program on the first run, and are not stored in this repository.

[🏠 Course Home](README.md)
