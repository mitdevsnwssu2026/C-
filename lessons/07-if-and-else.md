# Lesson 07: Making Decisions (if, else, else if)

**Part 1: Beginner** · About 75 minutes

## What You Will Learn

- **Simple `if`**: do something only when a condition is true
- **`if` / `else`**: choose between two paths
- **`else if`**: choose between many paths
- How to check that the user typed a real number, so the program does not crash

## The Idea

You make decisions every day:

> **If** it is raining, bring an umbrella. **Otherwise**, wear a cap.

In Lesson 06, your programs **asked** yes-or-no questions. Now they will **act** on the answers.

```cs
if (isRaining)
{
    Console.WriteLine("Bring an umbrella.");
}
else
{
    Console.WriteLine("Wear a cap.");
}
```

- The part inside `( )` is the **condition**. It is a question from Lesson 06, so it is `true` or `false`.
- The curly braces `{ }` group lines together. Everything between `{` and `}` belongs to that `if` or `else`.

---

## Step by Step

### Step 1: Create a new project named `Lesson07`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson07`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

> [!TIP]
> When you type `{` and press **Enter**, Visual Studio adds the closing `}` for you, and moves your next line a little to the right. That spacing is called **indentation**. It shows which lines belong together.

---

## Part 1: Simple `if`

A simple `if` runs some lines **only when** the condition is true. When it is false, those lines are **skipped**.

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
Console.Write("How old are you? ");
int age = int.Parse(Console.ReadLine() ?? "");

if (age >= 18)
{
    Console.WriteLine("You are an adult.");
}

Console.WriteLine("Thank you!");
```

**2.** Press **Ctrl + F5** and type `20`. Then run it again and type `15`.

✅ **The output** (when you type `20`):

```text
How old are you? 20
You are an adult.
Thank you!
```

✅ **The output** (when you type `15`):

```text
How old are you? 15
Thank you!
```

**What it means:**

- With `20`, the condition `age >= 18` is `true`. The line inside `{ }` runs.
- With `15`, the condition is `false`. The line inside `{ }` is **skipped**.
- `Thank you!` is **outside** the braces, so it shows **every** time.

---

## Part 2: `if` / `else`

`if` / `else` chooses between **two** paths. Exactly **one** of them runs. Never both, and never none.

**1.** Clear the editor and type this code:

```csharp
Console.Write("Enter your grade: ");
double grade = double.Parse(Console.ReadLine() ?? "");

if (grade >= 75)
{
    Console.WriteLine("You passed!");
}
else
{
    Console.WriteLine("You failed. Try again next time.");
}
```

**2.** Press **Ctrl + F5** and type `80`. Then run it again and type `60`.

✅ **The output:**

```text
Enter your grade: 80
You passed!
```

```text
Enter your grade: 60
You failed. Try again next time.
```

**What it means:**

- If the condition is `true`, the **first** `{ }` runs.
- If it is `false`, the `else { }` runs.
- `else` has **no** condition of its own. It means "in every other case".

---

## Part 3: `else if` (Many Paths)

What if there are more than two paths? Add `else if` between `if` and `else`.

This program also fixes a problem from Lesson 03: typing letters instead of a number will **not** crash it.

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 7: Grade remarks using if / else if / else

Console.Write("Enter your final grade (0-100): ");
string input = Console.ReadLine() ?? "";

bool isNumber = double.TryParse(input, out double grade);

if (!isNumber)
{
    Console.WriteLine("That is not a number. Please run the program again.");
}
else if (grade < 0 || grade > 100)
{
    Console.WriteLine("The grade must be from 0 to 100.");
}
else if (grade >= 90)
{
    Console.WriteLine("Remarks: Excellent! You PASSED.");
}
else if (grade >= 75)
{
    Console.WriteLine("Remarks: Good job! You PASSED.");
}
else
{
    Console.WriteLine("Remarks: Sorry, you FAILED. Keep trying!");
}
```

**2.** Run it five times (**Ctrl + F5**), with these five answers: `92`, `80`, `70`, `abc`, `150`.

## The Output

✅ **You will see:**

```text
Enter your final grade (0-100): 92
Remarks: Excellent! You PASSED.
```

```text
Enter your final grade (0-100): 80
Remarks: Good job! You PASSED.
```

```text
Enter your final grade (0-100): 70
Remarks: Sorry, you FAILED. Keep trying!
```

```text
Enter your final grade (0-100): abc
That is not a number. Please run the program again.
```

```text
Enter your final grade (0-100): 150
The grade must be from 0 to 100.
```

The program did **not** crash when you typed `abc`. 🎉

---

## What the Code Means

**Checking if the input is a number:**

```cs
bool isNumber = double.TryParse(input, out double grade);
```

In Lesson 03, typing letters made the program crash. `TryParse` fixes that. It **tries** to turn the text into a number:

- If it works, `isNumber` becomes `true`, and the number goes into a new box named `grade`.
- If it does not work, `isNumber` becomes `false`. No crash.

(`int.TryParse` does the same for whole numbers.)

**Checking the conditions, from top to bottom:**

```cs
if (!isNumber)
```

*"If it is **not** a number..."* This is checked first, because the other checks only make sense for real numbers.

```cs
else if (grade < 0 || grade > 100)
```

*"...otherwise, if the grade is below 0 **or** above 100..."*

```cs
else if (grade >= 90)
...
else if (grade >= 75)
...
else
```

C# checks each condition **from top to bottom**, and runs the **first one that is true**. Then it skips all the rest. If none is true, the `else` part runs.

The **order matters**. A grade of `92` is also `>= 75`. But `>= 90` comes first, so the student sees "Excellent!".

### The three shapes

| Shape | Use it when... |
|---|---|
| `if { }` | Something should happen **only sometimes** |
| `if { } else { }` | There are **two** paths |
| `if { } else if { } else { }` | There are **three or more** paths |

---

## If Something Goes Wrong

**Error List: `CS0029  Cannot implicitly convert type 'double' to 'bool'`**
You wrote `if (grade = 75)` with one `=`. To compare, use two: `if (grade == 75)`

**"You passed!" always shows, even for a low grade.**
You may have a `;` right after the condition: `if (grade >= 75);`. Remove that `;`. Never put `;` directly after `if (...)`.

**The "Excellent" message never shows.**
Your conditions are in the wrong order. Check the **highest** number first (`>= 90` before `>= 75`).

**Error List: `CS1513  } expected`**
A `{` was opened but never closed. Every `{` needs a matching `}`.

**The Error List shows several errors at once on the line near `else`, like `CS1003  Syntax error, '(' expected`.**
The `else` has lost its `if`. The most common cause is a `;` right after the condition: `if (grade >= 75);`. Remove that `;`. Also check that the `}` of the `if` part is directly before the `else`.

---

## Exercise

**Exercise 1.** (Simple `if`) Ask for the temperature in degrees Celsius. If it is above 35, show `It is very hot. Drink water!`. Always show `Have a nice day!` at the end.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Temperature in Celsius: ");
double temperature = double.Parse(Console.ReadLine() ?? "");

if (temperature > 35)
{
    Console.WriteLine("It is very hot. Drink water!");
}

Console.WriteLine("Have a nice day!");
```

</details>

**Exercise 2.** (`if` / `else`) Ask for a whole number. Say if it is **even** or **odd**. (Hint: a number is even when `number % 2 == 0`.)

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Enter a whole number: ");
int number = int.Parse(Console.ReadLine() ?? "");

if (number % 2 == 0)
{
    Console.WriteLine($"{number} is even.");
}
else
{
    Console.WriteLine($"{number} is odd.");
}
```

</details>

**Exercise 3.** (`if` / `else`) **A simple login.** Ask for a username and a password. If the username is `admin` **and** the password is `1234`, show `Welcome, admin!`. Otherwise, show `Access denied.`

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Username: ");
string username = Console.ReadLine() ?? "";

Console.Write("Password: ");
string password = Console.ReadLine() ?? "";

if (username == "admin" && password == "1234")
{
    Console.WriteLine("Welcome, admin!");
}
else
{
    Console.WriteLine("Access denied.");
}
```

⚠️ This is only for practice. Writing a password inside the code is **not safe**. In Lessons 23 to 26, you will learn how real systems keep passwords safe.

</details>

## ⭐ Bonus Challenge

(`else if`) **Can you vote?** Ask for the user's age. Then ask if they have a valid ID (`yes` or `no`). They can vote only if they are **18 or older AND** have an ID.

Make it safe: use `int.TryParse`, so the program does not crash when the user types letters for the age. Also accept `YES`, `Yes`, and `yes`.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("How old are you? ");
bool isNumber = int.TryParse(Console.ReadLine() ?? "", out int age);

Console.Write("Do you have a valid ID? (yes/no) ");
string idAnswer = Console.ReadLine() ?? "";
bool hasId = idAnswer.ToLower() == "yes";

if (!isNumber)
{
    Console.WriteLine("Please type your age as a number.");
}
else if (age >= 18 && hasId)
{
    Console.WriteLine("You can vote!");
}
else
{
    Console.WriteLine("Sorry, you cannot vote yet.");
}
```

Look at `bool hasId = idAnswer.ToLower() == "yes";`. The comparison gives `true` or `false`, and that answer goes into the `bool` box.

</details>

---

## Remember

- **Simple `if`**: the lines in `{ }` run only when the condition is true.
- **`if` / `else`**: exactly one of the two parts runs.
- **`else if`**: conditions are checked from top to bottom. The first true one wins.
- Never put `;` right after `if (...)`.
- `double.TryParse(text, out double number)` checks if text is a number, without crashing.

---

[⬅️ Lesson 06](06-comparison-and-logical-operators.md) | [🏠 Course Home](../README.md) | [Next: Lesson 08 (Nested If and Switch) ➡️](08-nested-if-and-switch.md)
