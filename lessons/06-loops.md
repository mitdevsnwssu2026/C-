# Lesson 06: Loops (Repeating Things)

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 75 minutes |
| **Before this** | [Lesson 05](05-decisions.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Repeat code a set number of times with a `for` loop
- Repeat code **while** something is true with a `while` loop
- Stop a loop early with `break`, and skip ahead with `continue`
- Build a **menu that keeps coming back** until the user chooses Exit

---

## 🧠 The Big Idea

Imagine your teacher asks you to write "I will practice coding" 100 times. You could write the same line 100 times... or you could say:

> **Repeat 100 times:** write "I will practice coding".

That is a **loop**. A loop repeats a block of code without you having to copy and paste it.

C# has two main loops:

| Loop | Use it when... | Real-life example |
|---|---|---|
| `for` | You know **how many times** to repeat | "Do 10 push-ups" |
| `while` | You repeat **until something changes** | "Keep stirring **while** the sauce is not thick yet" |

---

## 🧠 The `for` Loop: Counting

```cs
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"Push-up number {i}");
}
```

✅ Shows:

```text
Push-up number 1
Push-up number 2
Push-up number 3
Push-up number 4
Push-up number 5
```

The first line has **three parts**, separated by `;`:

| Part | Meaning |
|---|---|
| `int i = 1` | **Start:** make a counter box `i` and set it to 1 |
| `i <= 5` | **Keep going while** this is true |
| `i++` | **After each round:** add 1 to `i` |

So the loop runs with `i = 1`, then `2`, `3`, `4`, `5`. When `i` becomes `6`, the condition `i <= 5` is `false`, and the loop stops.

> [!NOTE]
> `i` is short for "index". Programmers often use `i` for loop counters. You can use any name.

You can also count **down** by starting high and using `i--`:

```cs
for (int count = 5; count >= 1; count--)
{
    Console.WriteLine(count);
}
```

---

## 🧠 The `while` Loop: Repeat Until Something Changes

```cs
while (condition)
{
    // repeat this
}
```

Before each round, C# checks the condition. If it is `true`, it runs the block again. If it is `false`, it stops.

### Example: 3 tries to enter the correct PIN

Create a new project and try it:

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson06
cd Lesson06
code .
```

Replace `Program.cs` with:

```csharp
string correctPin = "1234";
int attempts = 0;
bool unlocked = false;

while (attempts < 3 && !unlocked)
{
    Console.Write("Enter your PIN: ");
    string pin = Console.ReadLine() ?? "";
    attempts++;

    if (pin == correctPin)
    {
        unlocked = true;
    }
    else
    {
        Console.WriteLine($"Wrong PIN. Tries left: {3 - attempts}");
    }
}

if (unlocked)
{
    Console.WriteLine("Access granted!");
}
else
{
    Console.WriteLine("Too many wrong tries. Your card is locked.");
}
```

✅ **You should see** (if you type a wrong PIN twice, then the correct one):

```text
Enter your PIN: 1111
Wrong PIN. Tries left: 2
Enter your PIN: 0000
Wrong PIN. Tries left: 1
Enter your PIN: 1234
Access granted!
```

The loop keeps going **while** the user still has tries left **and** has not unlocked yet. Remember this idea. You will use it again in the final project's login screen!

---

## 🧠 `break` and `continue`

| Keyword | What it does |
|---|---|
| `break;` | **Leave the loop right now.** Jump to the code after the loop |
| `continue;` | **Skip the rest of this round** and go to the next round |

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i == 3)
    {
        continue;   // skip 3
    }

    if (i == 6)
    {
        break;      // stop completely at 6
    }

    Console.WriteLine(i);
}
```

✅ Shows `1`, `2`, `4`, `5` (one per line). Number 3 was skipped, and the loop stopped at 6.

> [!WARNING]
> **Infinite loops.** If a loop's condition never becomes `false`, the loop runs **forever**, and your program will not stop. If this happens, press **Ctrl + C** in the Command Prompt to stop it.

---

## 🛠️ Let's Build It: A Menu That Repeats

Almost every program you use has a **menu** that comes back after each action. Here is the pattern. You will use it in **every** big project in this course.

Replace everything in `Program.cs` with:

```csharp
// Lesson 6: A menu that repeats until the user chooses Exit

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("===== MAIN MENU =====");
    Console.WriteLine("1 - Multiplication table");
    Console.WriteLine("2 - Countdown");
    Console.WriteLine("3 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Which number? ");
            int number = int.Parse(Console.ReadLine() ?? "");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{number} x {i} = {number * i}");
            }
            break;

        case "2":
            for (int count = 5; count >= 1; count--)
            {
                Console.WriteLine(count);
            }
            Console.WriteLine("Liftoff!");
            break;

        case "3":
            running = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please type 1, 2, or 3.");
            break;
    }
}
```

Run it with `dotnet run`.

✅ **You should see** (typing `1`, then `7`, then `3`):

```text

===== MAIN MENU =====
1 - Multiplication table
2 - Countdown
3 - Exit
Choose an option: 1
Which number? 7
7 x 1 = 7
7 x 2 = 14
7 x 3 = 21
7 x 4 = 28
7 x 5 = 35
7 x 6 = 42
7 x 7 = 49
7 x 8 = 56
7 x 9 = 63
7 x 10 = 70

===== MAIN MENU =====
1 - Multiplication table
2 - Countdown
3 - Exit
Choose an option: 3
Goodbye!
```

Try option `2` and an invalid option like `9`, too.

---

## 🔎 Line by Line

```cs
bool running = true;

while (running)
{
    ...
}
```

The menu repeats **while** `running` is `true`. The only way to stop is to make `running` become `false`.

```cs
case "3":
    running = false;
    Console.WriteLine("Goodbye!");
    break;
```

When the user picks `3`, we set `running` to `false`. The `break;` here only leaves the **`switch`**. Then the `while` loop checks `running`, sees `false`, and stops.

```cs
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{number} x {i} = {number * i}");
}
```

A `for` loop **inside** a `while` loop. You can put loops inside loops, and `if`s inside loops. That is how real programs are built.

> [!NOTE]
> Option 1 uses `int.Parse`, so typing letters for "Which number?" still crashes. You will learn to fix that cleanly in Lesson 08.

---

## ⚠️ Common Mistakes

### Mistake 1: A loop that never ends

```cs
int i = 1;
while (i <= 5)
{
    Console.WriteLine(i);
    // forgot i++;
}
```

`i` stays `1` forever, so `i <= 5` is always `true`. **Fix:** Make sure something inside the loop changes the condition. Press **Ctrl + C** to stop a stuck program.

### Mistake 2: Off by one

```cs
for (int i = 1; i < 10; i++)   // runs 9 times, not 10!
```

`i < 10` stops **before** 10. **Fix:** Use `i <= 10` to include 10.

### Mistake 3: Semicolon after the loop line

```cs
for (int i = 1; i <= 5; i++);
```

The `;` ends the loop immediately, so the block below runs only once. **Fix:** Never put `;` right after `for (...)` or `while (...)`.

---

## 🏋️ Practice

**Exercise 1.** Print the numbers from 1 to 100.

<details>
<summary>Show answer</summary>

```csharp
for (int i = 1; i <= 100; i++)
{
    Console.WriteLine(i);
}
```

</details>

**Exercise 2.** Add up all the numbers from 1 to 100 and print the total. (The answer is `5050`.)

<details>
<summary>Show answer</summary>

```csharp
int total = 0;

for (int i = 1; i <= 100; i++)
{
    total += i;
}

Console.WriteLine($"Total: {total}");
```

The box `total` starts at 0. Each round adds the next number to it.

</details>

**Exercise 3.** Keep asking for the secret word until the user types `letmein`. Then print `Welcome!`.

<details>
<summary>Show answer</summary>

```csharp
while (true)
{
    Console.Write("Secret word: ");
    string word = Console.ReadLine() ?? "";

    if (word == "letmein")
    {
        break;
    }

    Console.WriteLine("Wrong. Try again.");
}

Console.WriteLine("Welcome!");
```

`while (true)` repeats forever. The **only** way out is the `break;`, which happens when the word is correct.

</details>

**Exercise 4.** In the menu program, add a new option **3 - Even numbers from 1 to 20**, and move **Exit** to option **4**.

<details>
<summary>Show answer</summary>

Change the menu text:

```cs
Console.WriteLine("1 - Multiplication table");
Console.WriteLine("2 - Countdown");
Console.WriteLine("3 - Even numbers from 1 to 20");
Console.WriteLine("4 - Exit");
```

Add a new `case`, and change the Exit case to `"4"`:

```cs
case "3":
    for (int i = 1; i <= 20; i++)
    {
        if (i % 2 == 0)
        {
            Console.WriteLine(i);
        }
    }
    break;

case "4":
    running = false;
    Console.WriteLine("Goodbye!");
    break;
```

Also update the `default` message to say `Please type 1, 2, 3, or 4.`

</details>

---

## 📌 Recap

- `for (int i = 1; i <= 10; i++) { ... }` repeats a set number of times.
- `while (condition) { ... }` repeats as long as the condition is `true`.
- `break;` leaves a loop. `continue;` skips to the next round.
- The **menu loop** pattern: `bool running = true; while (running) { show menu; switch (choice) { ... } }`
- Press **Ctrl + C** to stop a program stuck in an infinite loop.

---

[⬅️ Lesson 05](05-decisions.md) | [🏠 Course Home](../README.md) | [Next: Lesson 07 (Lists) ➡️](07-lists.md)
