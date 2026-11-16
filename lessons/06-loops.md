# Lesson 06: Loops (Repeating Things)

**Part 1: Beginner** · About 75 minutes

## What You Will Learn

- How to repeat code a set number of times with `for`
- How to repeat code **while** something is true with `while`
- How to stop a loop early (`break`) or skip one round (`continue`)
- How to build a **menu that keeps coming back** until the user chooses Exit

## The Idea

Imagine your teacher tells you to write "I will practice coding" 100 times. You could write the line 100 times. Or you could say:

> **Repeat 100 times:** write "I will practice coding".

That is a **loop**. A loop repeats lines of code, so you do not have to copy them.

C# has two main loops:

- **`for`**: use it when you know **how many times**. ("Do 10 push-ups.")
- **`while`**: use it when you repeat **until something changes**. ("Keep stirring **while** the sauce is still thin.")

---

## Step by Step

### Step 1: Create a new project named `Lesson06`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson06`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Try a `for` loop

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this:

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"Push-up number {i}");
}
```

**2.** Press **Ctrl + F5**.

✅ **You will see:**

```text
Push-up number 1
Push-up number 2
Push-up number 3
Push-up number 4
Push-up number 5
```

One `WriteLine`, but five lines on the screen! The first line of the loop has **three parts**, with `;` between them:

- `int i = 1` → **Start:** make a counter box named `i`, and put 1 in it.
- `i <= 5` → **Keep going while** this is true.
- `i++` → **After each round**, add 1 to `i`.

So the loop runs with `i` as 1, then 2, 3, 4, and 5. When `i` becomes 6, `i <= 5` is false, and the loop stops.

> Programmers often name the counter `i`. You can use any name.

### Step 3: Try a `while` loop

This program gives the user 3 tries to type the correct PIN.

**1.** Clear the editor and type this:

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

**2.** Press **Ctrl + F5**. Type a wrong PIN twice, then type `1234`.

✅ **You will see:**

```text
Enter your PIN: 1111
Wrong PIN. Tries left: 2
Enter your PIN: 0000
Wrong PIN. Tries left: 1
Enter your PIN: 1234
Access granted!
```

Before each round, C# checks the condition in `while ( ... )`. Here it says: *"keep going while the user has used fewer than 3 tries **and** has not unlocked yet."*

Remember this program. You will use the same idea for the login screen of your final project!

### Step 4: Build a menu that repeats

Almost every program has a **menu** that comes back after each action. You will use this pattern in every big project in this course.

**1.** Clear the editor and type the code below.

## The Code

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

Run it again and try option `2`, and a wrong option like `9`.

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

When the user picks `3`, `running` becomes `false`. The loop checks it, sees `false`, and stops.

```cs
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{number} x {i} = {number * i}");
}
```

This is a `for` loop **inside** a `while` loop. You can put loops inside loops, and `if`s inside loops. That is how real programs are built.

```cs
for (int count = 5; count >= 1; count--)
```

A loop can also count **down**: start at 5, keep going while `count >= 1`, and subtract 1 each round (`count--`).

### `break` and `continue`

- `break;` → **leave the loop now.**
- `continue;` → **skip the rest of this round**, and start the next round.

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

This shows `1`, `2`, `4`, `5`. The 3 was skipped, and the loop stopped at 6.

---

## If Something Goes Wrong

**The black window keeps printing the same thing forever.**
This is called an **infinite loop**. The loop's condition never becomes false. For example:

```cs
int i = 1;
while (i <= 5)
{
    Console.WriteLine(i);
    // forgot i++;
}
```

**To stop it:** click the **X** at the top-right corner of the black window. Then fix your code so that something inside the loop changes the condition.

**My loop runs 9 times, not 10.**
You wrote `i < 10`. That stops **before** 10. Use `i <= 10` to include 10.

**The lines inside my loop run only once.**
You may have a `;` right after the loop line: `for (int i = 1; i <= 5; i++);`. Remove that last `;`.

**The program crashed when I typed letters for "Which number?"**
Option 1 uses `int.Parse`, which crashes on letters. You will learn a clean fix for this in Lesson 08.

---

## Exercise

**Exercise 1.** Add up all the numbers from 1 to 100 and show the total. (The answer is `5050`.)

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

**Exercise 2.** Keep asking for the secret word until the user types `letmein`. Then show `Welcome!`.

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

`while (true)` repeats forever. The only way out is `break;`, which happens when the word is correct.

</details>

**Exercise 3.** In the menu program, add a new option **3 - Even numbers from 1 to 20**, and move **Exit** to option **4**.

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

Make it safe: if the user types letters, say `Please type a whole number.` and do not count it as a try.

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

- `for (int i = 1; i <= 10; i++) { ... }` repeats a set number of times.
- `while (condition) { ... }` repeats as long as the condition is true.
- `break;` leaves a loop. `continue;` skips to the next round.
- The **menu loop**: `bool running = true; while (running) { show the menu; switch (choice) { ... } }`
- Stuck in an infinite loop? Close the black window with the **X**.

---

[⬅️ Lesson 05](05-decisions.md) | [🏠 Course Home](../README.md) | [Next: Lesson 07 (Lists) ➡️](07-lists.md)
