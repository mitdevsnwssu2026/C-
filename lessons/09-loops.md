# Lesson 09: Loops (for, while, do-while)

**Part 1: Beginner** · About 75 minutes

## What You Will Learn

- **`for` loop**: repeat code a set number of times
- **`while` loop**: repeat code **while** something is true
- **`do-while` loop**: do it once first, then repeat while something is true
- How to choose the right loop

## The Idea

Imagine your teacher tells you to write "I will practice coding" 100 times. You could write the line 100 times. Or you could say:

> **Repeat 100 times:** write "I will practice coding".

That is a **loop**. A loop repeats lines of code, so you do not have to copy them.

C# has three main loops:

| Loop | Use it when... | Real-life example |
|---|---|---|
| `for` | you know **how many times** | "Do 10 push-ups." |
| `while` | you repeat **until something changes**, and you check **before** each round | "**While** the sauce is still thin, keep stirring." |
| `do-while` | you must do it **at least once**, and you check **after** each round | "Taste the soup. Not salty enough? Add salt and taste again." |

---

## Step by Step

### Step 1: Create a new project named `Lesson09`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson09`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: The `for` Loop

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"Push-up number {i}");
}

Console.WriteLine("Done!");
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Push-up number 1
Push-up number 2
Push-up number 3
Push-up number 4
Push-up number 5
Done!
```

One `WriteLine` inside the loop, but five lines on the screen!

**What it means:** the first line of the loop has **three parts**, with `;` between them:

```text
for (int i = 1;   i <= 5;   i++)
     └───┬───┘    └──┬──┘   └┬┘
       START      KEEP      AFTER
                  GOING     EACH ROUND
                  WHILE
```

- `int i = 1` → **Start:** make a counter box named `i`, and put 1 in it.
- `i <= 5` → **Keep going while** this is true.
- `i++` → **After each round**, add 1 to `i`.

So the loop runs with `i` as 1, then 2, 3, 4, and 5. When `i` becomes 6, the condition `i <= 5` is false, and the loop stops. Then the program continues with `Done!`.

> Programmers often name the counter `i`. You can use any name.

### Counting in other ways

A `for` loop can count down, or jump by more than 1. Try each of these:

```csharp
// Counting DOWN from 5 to 1
for (int count = 5; count >= 1; count--)
{
    Console.WriteLine(count);
}

Console.WriteLine("Liftoff!");

// Counting by TWOS: 2, 4, 6, 8, 10
for (int number = 2; number <= 10; number += 2)
{
    Console.WriteLine(number);
}
```

✅ **The output:**

```text
5
4
3
2
1
Liftoff!
2
4
6
8
10
```

- `count--` subtracts 1 after each round.
- `number += 2` adds 2 after each round.

---

## Part 2: The `while` Loop

A `while` loop does not count by itself. It just keeps going **while** its condition is true. It checks the condition **before** every round.

This program gives the user 3 tries to type the correct PIN.

**1.** Clear the editor and type this code:

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

✅ **The output:**

```text
Enter your PIN: 1111
Wrong PIN. Tries left: 2
Enter your PIN: 0000
Wrong PIN. Tries left: 1
Enter your PIN: 1234
Access granted!
```

**3.** Run it again, and type a wrong PIN three times.

```text
Enter your PIN: 1
Wrong PIN. Tries left: 2
Enter your PIN: 2
Wrong PIN. Tries left: 1
Enter your PIN: 3
Wrong PIN. Tries left: 0
Too many wrong tries. Your card is locked.
```

**What it means:**

```cs
while (attempts < 3 && !unlocked)
```

*"Keep going while the user has used fewer than 3 tries, **and** has not unlocked yet."* The loop stops as soon as **one** of those becomes false: either the tries run out, or the PIN is correct.

Something **inside** the loop must change the condition. Here, `attempts++` and `unlocked = true` do that. If nothing changed, the loop would never stop.

> Remember this program. You will use the same idea for the login screen of your final project!

---

## Part 3: The `do-while` Loop

A `do-while` loop runs its lines **first**, and checks the condition **after**. So the lines always run **at least once**.

That is perfect for asking a question again and again until the answer is acceptable.

**1.** Clear the editor and type this code:

```csharp
int number;

do
{
    Console.Write("Type a number from 1 to 10: ");
    number = int.Parse(Console.ReadLine() ?? "");
}
while (number < 1 || number > 10);

Console.WriteLine($"Thank you! You typed {number}.");
```

**2.** Press **Ctrl + F5**. Type `15`, then `0`, then `7`.

✅ **The output:**

```text
Type a number from 1 to 10: 15
Type a number from 1 to 10: 0
Type a number from 1 to 10: 7
Thank you! You typed 7.
```

**What it means:**

```text
do
{
    ...these lines run FIRST...
}
while (condition);     ← checked AFTER each round. Notice the ; at the end!
```

- `int number;` makes the box **before** the loop, with nothing in it yet. That way the box can be used in the condition, and after the loop.
- The question is asked **once** no matter what.
- `while (number < 1 || number > 10);` means: *"if the number is too small **or** too big, go around again."*

> [!IMPORTANT]
> A `do-while` loop ends with a **`;`** after the condition. This is the only loop that does. (For `for` and `while`, a `;` in that place is a mistake.)

### `while` or `do-while`?

| | `while` | `do-while` |
|---|---|---|
| Checks the condition... | **before** each round | **after** each round |
| Can it run **zero** times? | Yes | No. It always runs at least once |

---

## Part 4: The Three Loops Working Together

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 9: The three loops working together (do-while, for, while)

// 1. do-while: keep asking until the answer is valid
int number;

do
{
    Console.Write("Type a number from 1 to 10: ");
    number = int.Parse(Console.ReadLine() ?? "");
}
while (number < 1 || number > 10);

// 2. for: repeat exactly 10 times
Console.WriteLine();
Console.WriteLine($"The multiplication table of {number}:");

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{number} x {i} = {number * i}");
}

// 3. while: repeat until something changes
Console.WriteLine();
Console.WriteLine($"Counting down from {number}:");

int count = number;

while (count >= 1)
{
    Console.WriteLine(count);
    count--;
}

Console.WriteLine("Liftoff!");
```

**2.** Press **Ctrl + F5**. Type `15` first (it is refused), then `7`.

## The Output

✅ **You will see:**

```text
Type a number from 1 to 10: 15
Type a number from 1 to 10: 7

The multiplication table of 7:
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

Counting down from 7:
7
6
5
4
3
2
1
Liftoff!
```

---

## What the Code Means

Each loop was chosen for a reason:

1. **`do-while`** asks for the number. The question must be asked **at least once**, and repeated only when the answer is bad.
2. **`for`** shows the multiplication table. We know **exactly** how many times: 10.
3. **`while`** counts down. It keeps going **while** `count >= 1`, and `count--` makes `count` smaller each round, so the loop is sure to end.

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

**To stop it:** click the **X** at the top-right corner of the black window. Then fix your code, so that something inside the loop changes the condition.

**My loop runs 9 times, not 10.**
You wrote `i < 10`. That stops **before** 10. Use `i <= 10` to include 10.

**The lines inside my `for` or `while` loop run only once.**
You may have a `;` right after the loop line: `for (int i = 1; i <= 5; i++);`. Remove that last `;`.

**Error List: `CS1002  ; expected`** on a `do-while` loop.
A `do-while` needs a `;` after its condition: `while (number < 1);`

**Error List: `CS0103  The name 'number' does not exist in the current context`** on a `do-while` loop.
You made the box **inside** the `do { }` part, so the condition after it cannot see it. Make the box **before** the loop (`int number;`), and give it a value **inside** the loop.

**The program crashed when I typed letters.**
These programs use `int.Parse`, which crashes on letters. You will learn a clean fix in Lesson 13.

---

## Exercise

**Exercise 1.** (`for`) Add up all the numbers from 1 to 100 and show the total. (The answer is `5050`.)

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

**Exercise 2.** (`while`) Start with the number 1. Keep **doubling** it (1, 2, 4, 8, ...) and show each number, while the number is less than 1000.

<details>
<summary>Show answer</summary>

```csharp
int number = 1;

while (number < 1000)
{
    Console.WriteLine(number);
    number = number * 2;
}
```

It shows 1, 2, 4, 8, 16, 32, 64, 128, 256, and 512. The next number would be 1024, which is not less than 1000, so the loop stops.

</details>

**Exercise 3.** (`do-while`) Keep asking for the secret word until the user types `letmein`. Then show `Welcome!`.

<details>
<summary>Show answer</summary>

```csharp
string word;

do
{
    Console.Write("Secret word: ");
    word = Console.ReadLine() ?? "";
}
while (word != "letmein");

Console.WriteLine("Welcome!");
```

</details>

## ⭐ Bonus Challenge

**The savings plan.** You want to save **5,000 pesos**. Ask the user how much they can save each week. Then use a loop to find out **how many weeks** it will take.

```text
How much can you save each week? 350
You will reach 5000 pesos in 15 weeks.
You will have 5250 pesos.
```

Hint: keep adding the weekly amount to your savings **while** the savings are less than 5000. Count the rounds.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("How much can you save each week? ");
double weeklyAmount = double.Parse(Console.ReadLine() ?? "");

double savings = 0;
int weeks = 0;

while (savings < 5000)
{
    savings += weeklyAmount;
    weeks++;
}

Console.WriteLine($"You will reach 5000 pesos in {weeks} weeks.");
Console.WriteLine($"You will have {savings} pesos.");
```

A `while` loop is the right choice here, because you do **not** know in advance how many rounds are needed.

⚠️ What happens if the user types `0`? The savings never grow, so the loop never ends: an infinite loop! (Close the black window to stop it.) A real program would check that the amount is more than 0 first.

</details>

---

## Remember

- `for (int i = 1; i <= 10; i++) { ... }` → you know how many times.
- `while (condition) { ... }` → checks **before** each round. It may run zero times.
- `do { ... } while (condition);` → checks **after** each round. It runs at least once. It ends with `;`
- Something inside a loop must **change the condition**, or the loop never ends.
- Stuck in an infinite loop? Close the black window with the **X**.

---

[⬅️ Lesson 08](08-nested-if-and-switch.md) | [🏠 Course Home](../README.md) | [Next: Lesson 10 (Nested Loops and Loop Control) ➡️](10-nested-loops-and-loop-control.md)
