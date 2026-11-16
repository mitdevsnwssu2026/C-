# C# From Zero: Hello World to Secure Accounts

A step-by-step C# course for people with **zero programming experience**.

You will start by printing `Hello, World!` in the Command Prompt. You will finish by building a real **account system** that saves users in a **database** with **securely hashed passwords**.

No experience needed. Every lesson tells you **exactly** what to type, **exactly** what you should see, and **exactly** what each line means.

---

## 🗺️ The Learning Path

```text
 PART 0           PART 1              PART 2               PART 3              PART 4
 Setup     ──►    Beginner     ──►    Intermediate  ──►    Databases    ──►    Security & Accounts
 (Lesson 00)      (Lessons 01–07)     (Lessons 08–11)      (Lessons 12–16)     (Lessons 17–20)

 Install the      Write, run, and     Organize code and    Save, read, and     Hash passwords and
 tools            make decisions      save to files        upload data         build a login system
```

### Part 0: Getting Started

| # | Lesson | You will learn to... | Time |
|---|---|---|---|
| 00 | [Before You Start](lessons/00-before-you-start.md) | Install .NET and VS Code, and use the Command Prompt | 45 min |

### Part 1: Beginner

| # | Lesson | You will learn to... | Time |
|---|---|---|---|
| 01 | [Hello World](lessons/01-hello-world.md) | Create, run, and change your first program | 45 min |
| 02 | [Variables](lessons/02-variables.md) | Store text, numbers, and true/false values | 60 min |
| 03 | [User Input](lessons/03-user-input.md) | Ask questions and read the answers | 60 min |
| 04 | [Math and Operators](lessons/04-math-and-operators.md) | Calculate, and build a grade average calculator | 60 min |
| 05 | [Making Decisions](lessons/05-decisions.md) | Use `if`, `else`, and `switch`, and check input safely | 75 min |
| 06 | [Loops](lessons/06-loops.md) | Repeat code, and build a menu that comes back | 75 min |
| 07 | [Arrays and Lists](lessons/07-lists.md) | Keep many values in one place | 60 min |

### Part 2: Intermediate

| # | Lesson | You will learn to... | Time |
|---|---|---|---|
| 08 | [Methods](lessons/08-methods.md) | Make reusable blocks of code, and write crash-proof input | 75 min |
| 09 | [Classes and Objects](lessons/09-classes-and-objects.md) | Make your own types, like `Student` | 90 min |
| 10 | [Handling Errors](lessons/10-handling-errors.md) | Stop crashes with `try` and `catch` | 60 min |
| 11 | [Saving to Files](lessons/11-saving-to-files.md) | Keep data after the program closes, and use CSV files | 60 min |

### Part 3: Databases

| # | Lesson | You will learn to... | Time |
|---|---|---|---|
| 12 | [What Is a Database?](lessons/12-what-is-a-database.md) | Understand tables, and practice SQL in a playground | 90 min |
| 13 | [Connecting to a Database](lessons/13-connecting-to-a-database.md) | Connect C# to SQLite and create tables | 60 min |
| 14 | [Inserting Data Safely](lessons/14-inserting-data.md) | Save user input, and block SQL injection with parameters | 90 min |
| 15 | [Student Records Manager](lessons/15-crud-app.md) | Build a full Create / Read / Update / Delete app | 2 hrs |
| 16 | [Uploading a CSV File](lessons/16-uploading-csv-data.md) | Check and upload many records at once with a transaction | 90 min |

### Part 4: Security and Accounts

| # | Lesson | You will learn to... | Time |
|---|---|---|---|
| 17 | [What Is Hashing?](lessons/17-what-is-hashing.md) | Understand hashes and salt, and why passwords are never saved | 75 min |
| 18 | [Safe Password Hashing](lessons/18-password-hashing.md) | Hash and verify passwords with PBKDF2 | 75 min |
| 19 | [Account System: Register](lessons/19-account-system-register.md) | Register accounts with rules and hidden password typing | 2 hrs |
| 20 | [Account System: Log In 🏆](lessons/20-account-system-login.md) | Log in, change the password, and log out (final project) | 2 hrs |

### Appendix

| | |
|---|---|
| [🛟 Troubleshooting](appendix/troubleshooting.md) | Every common error, and how to fix it |
| [📖 Glossary](appendix/glossary.md) | Every new word in the course, in plain language |
| [📋 Cheat Sheet](appendix/cheat-sheet.md) | Commands, C# syntax, and SQL on one page |
| [👩‍🏫 Instructor Guide](INSTRUCTOR-GUIDE.md) | Schedule, teaching tips, and grading ideas |

---

## 📚 How Every Lesson Works

Every lesson has the **same sections**, in the **same order**, so you always know where you are:

| Section | What you will find |
|---|---|
| 🎯 **Goal** | What you will be able to do by the end |
| 🧠 **The Big Idea** | The new idea, explained with everyday examples |
| 🛠️ **Let's Build It** | Numbered steps: exactly what to type |
| ✅ **You should see** | Exactly what should appear on your screen |
| 🔎 **Line by Line** | What every important line of code means |
| ⚠️ **Common Mistakes** | The errors beginners usually see, with the exact message and the fix |
| 🏋️ **Practice** | Exercises, with answers hidden under **"Show answer"**. Try first, then check! |
| 📌 **Recap** | The key points, in a short list |

### How to read the code boxes

| Box | What it means |
|---|---|
| A box after **"Type:"** or **"In the Command Prompt..."** | A **command**. Type it in the Command Prompt, then press **Enter** |
| A box with colored C# code | **Code**. Type it in VS Code |
| A box after **"✅ You should see"** | **Output**. What the screen should show. Words that **you** type are shown after the question, like `What is your name? Juan` |

---

## 🧰 What You Need

- A **Windows** computer (Windows 10 or 11)
- An **internet connection** (to install the tools and the SQLite package)
- About **27 hours** in total, spread over several weeks

Everything used in this course is **free**.

---

## 📁 What Is in This Repository

```text
README.md              ← You are here (the course home page)
INSTRUCTOR-GUIDE.md    ← For teachers
lessons/               ← The 21 lessons (00 to 20)
appendix/              ← Troubleshooting, glossary, cheat sheet
code/                  ← The finished, working program for every lesson
  Lesson01/
  Lesson02/
  ...
  Lesson20/            ← The final project
```

**Stuck?** Compare your code with the finished program in the [`code/`](code/) folder. Every program there has been built and tested with .NET 10.

To run a finished program, open the Command Prompt in its folder and run it:

```cmd
cd code\Lesson15
dotnet run
```

---

## 🌟 Rules for Success

1. **Type the code yourself** in Lessons 01–11. From Lesson 12, you may copy and paste, but **read every line**.
2. **Run your program after every small change.**
3. **Errors are normal.** Read the message: it tells you the **line number**. Fix the **first** error first.
4. **Capital letters matter.** `Console` is not `console`.
5. **Do the practice exercises** before you look at the answers.

**Ready? Start here: [Lesson 00: Before You Start](lessons/00-before-you-start.md)** 🚀
