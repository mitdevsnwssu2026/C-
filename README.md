# C# From Zero: Hello World to Secure Accounts

A step-by-step C# course for people who have **never programmed before**.

You start by installing **Visual Studio** and showing `Hello, World!` on the screen. You finish by building a real **account system** that saves users in a **database**, with safely **hashed passwords**.

Every lesson tells you **which button to click**, **what to type**, and **what you should see**.

## 👉 Start Here

**New to all of this? Begin with [Lesson 00: Setup (Install Visual Studio)](lessons/00-before-you-start.md).**

Then do the lessons in order. Each lesson uses what you learned in the one before it.

---

## The Lessons

### Getting Started

| # | Lesson | What you will do | Time |
|---|---|---|---|
| 00 | [Setup: Install Visual Studio](lessons/00-before-you-start.md) | Download, install, and open Visual Studio | 1 hr |

### Part 1: Beginner

| # | Lesson | What you will do | Time |
|---|---|---|---|
| 01 | [Hello World](lessons/01-hello-world.md) | Create, run, and change your first program | 45 min |
| 02 | [Variables](lessons/02-variables.md) | Store text, numbers, and true/false values | 60 min |
| 03 | [User Input](lessons/03-user-input.md) | Ask questions and read the answers | 60 min |
| 04 | [Math and Operators](lessons/04-math-and-operators.md) | Calculate, and build a grade average calculator | 60 min |
| 05 | [Making Decisions](lessons/05-decisions.md) | Use `if`, `else`, and `switch` | 75 min |
| 06 | [Loops](lessons/06-loops.md) | Repeat code, and build a menu that comes back | 75 min |
| 07 | [Arrays and Lists](lessons/07-lists.md) | Keep many values in one place | 60 min |

### Part 2: Intermediate

| # | Lesson | What you will do | Time |
|---|---|---|---|
| 08 | [Methods](lessons/08-methods.md) | Make reusable blocks of code | 75 min |
| 09 | [Classes and Objects](lessons/09-classes-and-objects.md) | Make your own types, like `Student` | 90 min |
| 10 | [Handling Errors](lessons/10-handling-errors.md) | Stop crashes with `try` and `catch` | 60 min |
| 11 | [Saving to Files](lessons/11-saving-to-files.md) | Keep data after the program closes | 60 min |

### Part 3: Databases

| # | Lesson | What you will do | Time |
|---|---|---|---|
| 12 | [What Is a Database?](lessons/12-what-is-a-database.md) | Learn tables and SQL in a practice tool | 90 min |
| 13 | [Connecting to a Database](lessons/13-connecting-to-a-database.md) | Connect C# to a database and create a table | 60 min |
| 14 | [Inserting Data Safely](lessons/14-inserting-data.md) | Save what the user types, and block hackers | 90 min |
| 15 | [Student Records Manager](lessons/15-crud-app.md) | Build a full add / show / update / delete app | 2 hrs |
| 16 | [Uploading a CSV File](lessons/16-uploading-csv-data.md) | Check and upload many records at once | 90 min |

### Part 4: Security and Accounts

| # | Lesson | What you will do | Time |
|---|---|---|---|
| 17 | [What Is Hashing?](lessons/17-what-is-hashing.md) | Learn why real passwords are never saved | 75 min |
| 18 | [Safe Password Hashing](lessons/18-password-hashing.md) | Hash and check passwords the safe way | 75 min |
| 19 | [Account System: Register](lessons/19-account-system-register.md) | Create accounts with hidden password typing | 2 hrs |
| 20 | [Account System: Log In 🏆](lessons/20-account-system-login.md) | Log in, change password, log out (final project) | 2 hrs |

### Help

| | |
|---|---|
| [🛟 Troubleshooting](appendix/troubleshooting.md) | Every common problem, and how to fix it |
| [📋 Cheat Sheet](appendix/cheat-sheet.md) | The clicks, the C#, and the SQL on one page |
| [📖 Glossary](appendix/glossary.md) | Every new word, in plain language |
| [👩‍🏫 Instructor Guide](INSTRUCTOR-GUIDE.md) | Schedule, teaching tips, and grading |

---

## How Every Lesson Works

Every lesson has the **same parts**, in the **same order**:

1. **What You Will Learn** → the goal, in a few lines
2. **The Idea** → the new idea, explained with everyday examples
3. **Step by Step** → numbered steps. One click or one action per step
4. **The Code** → the complete code to type
5. **The Output** → exactly what your screen should show
6. **What the Code Means** → the code explained, piece by piece
7. **If Something Goes Wrong** → common problems and their fixes
8. **Exercise** → practice tasks. The answers are hidden: try first, then click **Show answer**
9. **⭐ Bonus Challenge** → one harder task, for when you want more
10. **Remember** → the key points, in a short list

Look for this mark in every lesson:

✅ **You will see:** → this is what should appear on your screen. If your screen shows something different, stop and check the step again.

---

## What You Need

- A computer with **Windows 10** or **Windows 11**
- An **internet connection**
- About **10 GB** of free space
- About **28 hours** in total, spread over several weeks

Everything in this course is **free**.

---

## 5 Rules for Success

1. **Type the code yourself** in Lessons 01 to 11. From Lesson 12, you may copy and paste, but **read every line**.
2. **Run your program often.** After every small change, run it and check.
3. **Mistakes are normal.** Every programmer makes them every day. Visual Studio tells you where the mistake is.
4. **Capital letters matter.** `Console` is not the same as `console`.
5. **Do the exercises** before you look at the answers.

---

## What Is in This Repository

```text
README.md              ← You are here
lessons/               ← The 21 lessons (00 to 20)
appendix/              ← Troubleshooting, cheat sheet, glossary
code/                  ← The finished, working program for every lesson
INSTRUCTOR-GUIDE.md    ← For teachers
```

**Stuck on a lesson?** Compare your code with the finished program in the [`code/`](code/) folder. Every program there was built and tested.

To open a finished program: start Visual Studio, click **Open a project or solution**, go to the lesson's folder inside `code`, and choose the file that ends in `.csproj`.
