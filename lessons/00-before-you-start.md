# Lesson 00: Before You Start (Setting Up Your Computer)

| | |
|---|---|
| **Part** | Getting Started |
| **Time** | About 45 minutes |
| **You need** | A Windows computer with an internet connection |

## 🎯 Goal

By the end of this lesson, your computer will be ready to write and run C# programs.

You will **not** write any code yet. You will install two free tools and learn how to use the Command Prompt.

---

## 🧠 The Big Idea

Read this part slowly. These words will appear in every lesson.

### What is a program?

A **program** is a list of instructions that a computer follows, one step at a time, from top to bottom.

It is like a recipe:

```text
1. Boil water.
2. Add the noodles.
3. Wait 3 minutes.
4. Serve.
```

The computer is a very fast cook, but it is also very **literal**. It does exactly what you write: nothing more, nothing less. If you misspell an instruction, it will not guess what you meant. It will stop and tell you there is a mistake.

### What is C#?

**C#** (pronounced "C sharp") is a **programming language**. It is a language for writing instructions that a computer can understand. Microsoft created it. People use C# to build:

- Business systems (enrollment, payroll, inventory)
- Websites and web apps
- Windows desktop apps
- Games (the Unity game engine uses C#)

### What is .NET?

**.NET** (pronounced "dot net") is a free set of tools that turns your C# instructions into a program the computer can run.

You will install the **.NET SDK**. SDK means **Software Development Kit**: the complete toolbox for building programs.

### What is the Command Prompt?

The **Command Prompt** is a window where you **type commands** instead of clicking buttons. It looks old-fashioned, but programmers use it every day. In this course, you will use it to **create** and **run** your programs.

### What is Visual Studio Code?

**Visual Studio Code** (or **VS Code**) is a free program for writing code. It is like Microsoft Word, but made for code. It colors your code so it is easier to read, and it underlines mistakes in red.

### Your setup: two windows

In every lesson, you will use two windows side by side:

| Window | What it is for | Think of it as... |
|---|---|---|
| **VS Code** | Writing your code | Your notebook |
| **Command Prompt** | Running your code | Your "play" button |

---

## 🛠️ Step 1: Install the .NET SDK

1. Open your web browser and go to: **https://dotnet.microsoft.com/download**
2. Find the version marked **LTS** (Long Term Support). When this course was written, that was **.NET 10**.
3. Click the download button for the **SDK** for **Windows x64**.
   > If you are not sure what "x64" means, choose x64. It is correct for almost all Windows computers.
4. When the download finishes, open the file. Its name starts with `dotnet-sdk-` and ends with `-win-x64.exe`.
5. Click **Install**.
6. If Windows asks *"Do you want to allow this app to make changes to your device?"*, click **Yes**.
7. Wait until you see **"Installation was successful"**, then click **Close**.

## 🛠️ Step 2: Install Visual Studio Code

1. Go to: **https://code.visualstudio.com**
2. Click **Download for Windows**.
3. Open the downloaded file.
4. Select **"I accept the agreement"**, then click **Next** on each page.
5. On the page called **"Select Additional Tasks"**, make sure **"Add to PATH"** has a check mark. (It is checked already. Just leave it.)
6. Click **Install**, then **Finish**. VS Code will open.

## 🛠️ Step 3: Add C# support to VS Code

1. In VS Code, look at the bar on the far left. Click the **Extensions** icon (it looks like four small squares). You can also press **Ctrl + Shift + X**.
2. In the search box, type: `C#`
3. Find the one named exactly **C#**, made by **Microsoft**. Click **Install**.

> [!NOTE]
> You may also see an extension called **C# Dev Kit**. You do **not** need it for this course. If VS Code suggests it later, you can close that message.

Now close VS Code. You will open it again in Lesson 01.

## 🛠️ Step 4: Open the Command Prompt

1. Press the **Windows key** on your keyboard.
2. Type: `cmd`
3. Press **Enter**.

A black window opens. It looks like this:

```text
Microsoft Windows [Version 10.0.22631.4460]
(c) Microsoft Corporation. All rights reserved.

C:\Users\Juan>
```

The last line, `C:\Users\Juan>`, is called the **prompt**. It shows **which folder you are in right now**. (Yours will show your own Windows user name instead of `Juan`.) You type your commands after the `>` sign.

## 🛠️ Step 5: Check that .NET is installed

In the Command Prompt, type this command and press **Enter**:

```cmd
dotnet --version
```

✅ **You should see a version number**, like this:

```text
10.0.100
```

Your number may be a little different. **Any number that starts with 10 or higher is correct.**

> [!WARNING]
> **Do you see `'dotnet' is not recognized as an internal or external command`?**
> Close the Command Prompt and open a new one (Step 4). A Command Prompt that was already open before the installation does not know about .NET yet. If you still see the error, go to the [Troubleshooting guide](../appendix/troubleshooting.md).

> [!NOTE]
> The very first time you use `dotnet`, you may see a long **"Welcome to .NET!"** message. That is normal. It only appears once.

## 🛠️ Step 6: Create your course folder

You will keep all your lessons in one folder: `C:\CSharpCourse`.

Type these two commands. Press **Enter** after each one.

```cmd
mkdir C:\CSharpCourse
```

```cmd
cd C:\CSharpCourse
```

✅ **Your prompt should now show:**

```text
C:\CSharpCourse>
```

This means you are now "standing inside" the `C:\CSharpCourse` folder. Every lesson in this course starts from here.

---

## 📋 Command Prompt Cheat Sheet

You only need these commands for the whole course.

| Command | What it does | Example |
|---|---|---|
| `cd FolderName` | Go **into** a folder | `cd Lesson01` |
| `cd C:\CSharpCourse` | Go **straight to** the course folder from anywhere | |
| `cd ..` | Go **back up** one folder | |
| `dir` | **List** the files in the folder you are in | |
| `mkdir FolderName` | **Make** a new folder | `mkdir Practice` |
| `cls` | **Clear** the screen | |

Helpful keys:

| Key | What it does |
|---|---|
| **↑** (up arrow) | Brings back the command you typed before, so you do not need to type it again |
| **Tab** | Finishes a file or folder name for you. Type `cd Less` then press Tab |
| **Ctrl + C** | Stops a program that is running |

---

## 🌟 Rules for Success in This Course

1. **Type the code yourself in Lessons 01–11.** Do not copy and paste. Typing helps your brain and fingers learn. From Lesson 12 the programs get longer, so you may copy and paste, but **read every line**.
2. **Run your program often.** After every small change, run it and check that it works.
3. **Errors are normal.** Every programmer sees errors every day. An error message is not a failure. It is the computer telling you **where** to look.
4. **Capital letters matter.** To C#, `Console` and `console` are two different words.
5. **When you are stuck,** check the **Common Mistakes** section of the lesson, then the [Troubleshooting guide](../appendix/troubleshooting.md).

---

## ✅ Checklist

Before you go to Lesson 01, make sure that:

- [ ] `dotnet --version` shows a number that starts with **10** or higher
- [ ] VS Code is installed, and the **C#** extension is installed
- [ ] The folder `C:\CSharpCourse` exists
- [ ] You know how to open the Command Prompt and use `cd` and `dir`

---

[🏠 Course Home](../README.md) | [Next: Lesson 01 (Hello World) ➡️](01-hello-world.md)
