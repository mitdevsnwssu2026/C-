# Lesson 00: Setup (Install Visual Studio)

**Getting Started** · About 1 hour (most of it is waiting for the download)

## What You Will Do

In this lesson you will **not** write any code yet. You will:

- Learn 4 simple words: *program*, *C#*, *Visual Studio*, *console*
- Download and install **Visual Studio Community** (it is free)
- Open Visual Studio for the first time

When you finish this lesson, your computer is ready for the whole course.

---

## 4 Words to Know First

**Program.** A program is a list of instructions for a computer. The computer follows the instructions one by one, from top to bottom, exactly as written. It is like a recipe:

```text
1. Boil water.
2. Add the noodles.
3. Wait 3 minutes.
4. Serve.
```

**C#** (say "C sharp"). C# is a **language** for writing those instructions. Microsoft made it. People use it to build school systems, payroll systems, websites, and games.

**Visual Studio.** Visual Studio is the **program you will use to write C#**. Think of it as Microsoft Word, but for code. It gives you a place to type, a button to run your program, and it points out your mistakes.

**Console.** The console is a **black window that shows text**. It looks like the Windows Command Prompt. All the programs in this course show their results in that black window.

---

## What You Need

- A computer with **Windows 10** or **Windows 11**
- An **internet connection**
- About **10 GB of free space** on your computer

> [!WARNING]
> There are two different programs with similar names. You need **Visual Studio** (purple icon). Do **not** install **Visual Studio Code** (blue icon). It is a different program, and the steps in this course will not match it.

---

## Part A: Download Visual Studio

**1.** Open your web browser (Chrome or Edge).

**2.** Click the address bar at the top, type this address, and press **Enter**:

```text
https://visualstudio.microsoft.com/downloads/
```

**3.** Find the box named **Community**. This is the free version.

**4.** In the **Community** box, click the **Free download** button.

✅ **You will see:** a small file starts downloading. Its name is `VisualStudioSetup.exe` (or a similar name that starts with `VisualStudio`). This takes only a few seconds, because this small file is just the *installer*.

---

## Part B: Install Visual Studio

**1.** Open the file you just downloaded.

- In your browser, the download appears at the **top-right corner**. Click the file name.
- If you cannot find it there, open your **Downloads** folder and double-click the file.

**2.** Windows asks: *"Do you want to allow this app to make changes to your device?"* Click **Yes**.

**3.** A small window named **Visual Studio Installer** appears. Click **Continue**.

Wait about a minute while it gets ready.

**4.** A big window appears with many boxes. These boxes are called **workloads**. Each one adds a set of tools.

Find the box named **.NET desktop development** and **click it**.

✅ **You will see:** a check mark appears in the corner of that box.

> [!IMPORTANT]
> Click **only** this one box: **.NET desktop development**. You do not need any of the others for this course. More boxes means a much bigger and slower download.

**5.** Click the **Install** button at the **bottom-right corner** of the window.

**6.** Wait. The download is big, so this takes from **30 minutes to more than 1 hour**, depending on your internet speed. You can leave the computer and come back.

✅ **You will see:** progress bars moving. When they finish, Visual Studio opens by itself. (If it does not open by itself, click the **Launch** button.)

---

## Part C: Open Visual Studio for the First Time

The first time it opens, Visual Studio asks two quick questions.

**1. It asks you to sign in.** You do **not** need an account for this course. Click the link that lets you **skip** signing in. (Look for the word **Skip** near the bottom of the window.)

**2. It asks you to choose a color theme.** Pick the one you like. **Dark** is easy on the eyes. **Light** looks like paper. You can change it later. Then click **Start Visual Studio**.

✅ **You will see:** the **start window**. It has a list called **Open recent** on the left (empty for now), and four big buttons under **Get started** on the right. The last button says **Create a new project**.

**That's it. Visual Studio is installed!** 🎉

You can close Visual Studio now: click the **X** at the top-right corner of the window.

---

## Part D: Make Visual Studio Easy to Find

You will open Visual Studio at the start of every lesson. Let's put it on your taskbar (the bar at the bottom of your screen).

**1.** Click the **Start** button (the Windows logo at the bottom of your screen).

**2.** Type: `Visual Studio`

**3.** In the results, find **Visual Studio** with the **purple** icon. **Right-click** it.

> Make sure it is **not** "Visual Studio Installer" and **not** "Visual Studio Code".

**4.** Click **Pin to taskbar**.

✅ **You will see:** the purple Visual Studio icon on your taskbar. From now on, one click on that icon opens Visual Studio.

---

## If Something Goes Wrong

**"I can't find the Community box on the download page."**
Scroll down a little. The page shows three versions side by side: **Community**, **Professional**, and **Enterprise**. Choose **Community**. The other two cost money.

**"The download stopped" or "the installation failed."**
Check your internet connection, then open the downloaded file again (Part B, Step 1). The installer continues where it stopped.

**"I forgot to click .NET desktop development."**
No problem. Click **Start**, type `Visual Studio Installer`, and open it. Click **Modify**, click the **.NET desktop development** box, then click **Modify** at the bottom-right.

**"It says I don't have enough disk space."**
Free up some space (empty the Recycle Bin, delete large files you do not need), then try again. You need about 10 GB.

**"I installed Visual Studio Code by mistake."**
That is OK. You can leave it. Just go back to Part A and install **Visual Studio Community** too.

---

## Exercise

**Exercise 1.** Close Visual Studio. Now open it again **by yourself**, using the taskbar icon or the Start button. Find the **Create a new project** button with your eyes (do not click it yet).

**Exercise 2.** Answer these questions out loud or on paper. Then check your answers.

1. What is a program?
2. What is C#?
3. What is Visual Studio for?
4. What is the console?

<details>
<summary>Show answers</summary>

1. A list of instructions that a computer follows, one by one.
2. A language for writing instructions for a computer.
3. It is the program where you write C# code, run it, and see your mistakes.
4. The black window that shows the text from your program.

</details>

## ⭐ Bonus Challenge

Look at the **start window** of Visual Studio. There are four buttons under **Get started**. One of them is **Create a new project**. What are the names of the other three? (You will not use them in this course, but it is good to know your tools.)

<details>
<summary>Show answer</summary>

- **Clone a repository**
- **Open a project or solution**
- **Open a local folder**
- **Create a new project** ← the one you will use in every lesson

</details>

---

## Remember

- A **program** is a list of instructions. **C#** is the language. **Visual Studio** is where you write it. The **console** is the black window that shows the result.
- You installed **Visual Studio Community** with the **.NET desktop development** workload.
- You do not need to sign in.
- Every lesson starts by opening Visual Studio and clicking **Create a new project**.

---

[🏠 Course Home](../README.md) | [Next: Lesson 01 (Hello World) ➡️](01-hello-world.md)
