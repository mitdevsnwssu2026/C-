# Lesson 10: Nested Loops and Loop Control

**Part 1: Beginner** · About 75 minutes

## What You Will Learn

- **Nested loops**: a loop inside another loop
- **Loop control**: how to leave a loop early (`break`), or skip one round (`continue`)
- How to build a **menu that keeps coming back** until the user chooses Exit

## The Idea

Look at a clock. The **minute hand** goes all the way around (60 minutes) for **every single step** of the **hour hand**.

```text
Hour 1:  minute 1, minute 2, minute 3 ... minute 60
Hour 2:  minute 1, minute 2, minute 3 ... minute 60
Hour 3:  minute 1, minute 2, minute 3 ... minute 60
```

That is a **nested loop**: a loop **inside** another loop.

- The **outer loop** is the hour hand. It moves slowly.
- The **inner loop** is the minute hand. It runs **completely**, from start to finish, for each step of the outer loop.

---

## Step by Step

### Step 1: Create a new project named `Lesson10`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson10`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Nested Loops

### A rectangle of stars

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
for (int row = 1; row <= 3; row++)
{
    for (int column = 1; column <= 5; column++)
    {
        Console.Write("* ");
    }

    Console.WriteLine();
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
* * * * *
* * * * *
* * * * *
```

**What it means:**

- The **outer loop** runs 3 times: once for each **row**.
- For **each** row, the **inner loop** runs 5 times and writes 5 stars on the same line (`Console.Write` stays on the line).
- After the inner loop finishes, `Console.WriteLine();` moves to the next line.
- In total, the star is written 3 × 5 = **15 times**.

Follow it with your finger:

```text
row 1 → column 1, 2, 3, 4, 5 → new line
row 2 → column 1, 2, 3, 4, 5 → new line
row 3 → column 1, 2, 3, 4, 5 → new line
```

### A multiplication grid

The inner loop can **use** the outer loop's counter. Here, each cell is `row * column`.

**1.** Clear the editor and type this code:

```csharp
for (int row = 1; row <= 5; row++)
{
    for (int column = 1; column <= 5; column++)
    {
        Console.Write($"{row * column,4}");
    }

    Console.WriteLine();
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
   1   2   3   4   5
   2   4   6   8  10
   3   6   9  12  15
   4   8  12  16  20
   5  10  15  20  25
```

**What it means:**

- `row * column` multiplies the two counters. In row 3, column 4, the cell shows 12.
- `,4` after the value means: *"give this number 4 spaces."* That makes the columns line up neatly.

---

## Part 2: Loop Control (`break` and `continue`)

Sometimes you want to change how a loop runs **from the inside**.

- `break;` → **leave the loop now.** Skip all the rounds that are left.
- `continue;` → **skip the rest of this round**, and start the next round.

**1.** Clear the editor and type this code:

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

Console.WriteLine("The loop has ended.");
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
1
2
4
5
The loop has ended.
```

**What it means:**

- When `i` is 3, `continue` jumps to the next round. So `3` is never shown.
- When `i` is 6, `break` leaves the loop. So 6, 7, 8, 9, and 10 are never shown.

### `while (true)` with `break`

A very common pattern: loop **forever**, and use `break` as the only way out.

```csharp
while (true)
{
    Console.Write("Type a word (or type stop): ");
    string word = Console.ReadLine() ?? "";

    if (word == "stop")
    {
        break;
    }

    Console.WriteLine($"You typed: {word}");
}

Console.WriteLine("Goodbye!");
```

`while (true)` never ends by itself, because `true` is always true. The loop ends only when the user types `stop`, and `break` runs.

---

## Part 3: A Menu That Repeats

Almost every program has a **menu** that comes back after each action. You will use this pattern in **every** big project in this course.

This program has a `for` loop **inside** a `switch`, **inside** a `while` loop. That is nesting, too.

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 10: A menu that repeats until the user chooses Exit

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

**2.** Press **Ctrl + F5**. Type `1`, then `7`, then `3`.

## The Output

✅ **You will see:**

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

Run it again. Try option `2`, and a wrong option like `9`.

---

## What the Code Means

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

When the user picks `3`, `running` becomes `false`. The `break;` here only leaves the **`switch`**. Then the `while` loop checks `running`, sees `false`, and stops.

```cs
case "1":
    ...
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine($"{number} x {i} = {number * i}");
    }
    break;
```

A `for` loop inside a `case`, inside the `while` loop. You can put loops inside loops, `if`s inside loops, and loops inside `if`s. That is how real programs are built.

### The menu loop pattern

Learn this shape by heart. You will type it many times:

```text
bool running = true;

while (running)
{
    1. Show the menu
    2. Read the user's choice
    3. switch (choice)
       {
           one case for each option
           one case that sets running = false
           default: "Invalid option"
       }
}
```

---

## If Something Goes Wrong

**My stars are all on one long line.**
You forgot `Console.WriteLine();` after the inner loop. It must be **inside** the outer loop, but **outside** the inner loop.

**Every star is on its own line.**
You used `Console.WriteLine("* ")` in the inner loop. Use `Console.Write("* ")`, so the stars stay on the same line.

**Error List: `CS0136  A local or parameter named 'i' cannot be declared in this scope...`**
The inner loop uses the **same counter name** as the outer loop. Give them different names, like `row` and `column`.

**Error List: `CS0139  No enclosing loop out of which to break or continue`**
`break` or `continue` is not inside a loop (or a `switch`). Check your `{ }`.

**The menu never stops.**
Check that the Exit `case` has the line `running = false;`.

**The program crashed when I typed letters for "Which number?"**
Option 1 uses `int.Parse`, which crashes on letters. You will learn a clean fix in Lesson 13.

---

## Exercise

**Exercise 1.** (Nested loops) Show this triangle of stars. Use a nested loop.

```text
*
**
***
****
*****
```

Hint: row 1 has 1 star, row 2 has 2 stars... The inner loop should run `row` times.

<details>
<summary>Show answer</summary>

```csharp
for (int row = 1; row <= 5; row++)
{
    for (int star = 1; star <= row; star++)
    {
        Console.Write("*");
    }

    Console.WriteLine();
}
```

The inner loop's limit is `row`, not a fixed number. So each row is one star longer than the one before.

</details>

**Exercise 2.** (Loop control) Show the numbers from 1 to 20, but **skip** every number that can be divided by 3. (So no 3, 6, 9, 12, 15, 18.)

<details>
<summary>Show answer</summary>

```csharp
for (int i = 1; i <= 20; i++)
{
    if (i % 3 == 0)
    {
        continue;
    }

    Console.WriteLine(i);
}
```

</details>

**Exercise 3.** (Menu) In the menu program, add a new option **3 - Even numbers from 1 to 20**, and move **Exit** to option **4**.

<details>
<summary>Show answer</summary>

Change the menu lines:

```cs
Console.WriteLine("1 - Multiplication table");
Console.WriteLine("2 - Countdown");
Console.WriteLine("3 - Even numbers from 1 to 20");
Console.WriteLine("4 - Exit");
```

Add a new `case "3":`, and change the Exit case to `"4"`:

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

Also change the `default` message to `Please type 1, 2, 3, or 4.`

</details>

## ⭐ Bonus Challenge

**The guessing game.** The computer "thinks" of the number 7. The user keeps guessing. After each guess, the program says `Too low!` or `Too high!`. When the user gets it right, show how many tries it took.

```text
I am thinking of a number from 1 to 10.
Your guess: 3
Too low!
Your guess: 9
Too high!
Your guess: 7
Correct! You got it in 3 tries.
```

Make it safe: if the user types letters, say `Please type a whole number.`, and do **not** count it as a try. (Hint: `continue`)

<details>
<summary>Show answer</summary>

```csharp
int secretNumber = 7;
int tries = 0;
bool guessed = false;

Console.WriteLine("I am thinking of a number from 1 to 10.");

while (!guessed)
{
    Console.Write("Your guess: ");
    bool isNumber = int.TryParse(Console.ReadLine() ?? "", out int guess);

    if (!isNumber)
    {
        Console.WriteLine("Please type a whole number.");
        continue;
    }

    tries++;

    if (guess < secretNumber)
    {
        Console.WriteLine("Too low!");
    }
    else if (guess > secretNumber)
    {
        Console.WriteLine("Too high!");
    }
    else
    {
        guessed = true;
    }
}

Console.WriteLine($"Correct! You got it in {tries} tries.");
```

**Want a real game?** Change the first line to this, and the number will be different every time:

```cs
int secretNumber = Random.Shared.Next(1, 11);
```

`Random.Shared.Next(1, 11)` gives a random whole number from 1 to 10. (The second number, 11, is not included.)

</details>

---

## Remember

- A **nested loop** is a loop inside a loop. The inner loop runs **completely** for each round of the outer loop.
- Give the two loops different counter names, like `row` and `column`.
- `break;` leaves the loop. `continue;` skips to the next round.
- `while (true)` with `break` is a loop whose only exit is the `break`.
- The **menu loop**: `bool running = true; while (running) { show the menu; switch (choice) { ... } }`

---

[⬅️ Lesson 09](09-loops.md) | [🏠 Course Home](../README.md) | [Next: Lesson 11 (Arrays) ➡️](11-arrays.md)
