# Lesson 05: Making Decisions (if, else, switch)

**Part 1: Beginner** · About 75 minutes

## What You Will Learn

- How to make your program choose what to do, with `if` and `else`
- How to compare values (`==`, `>`, `<`, and more)
- How to combine conditions with **AND**, **OR**, and **NOT**
- How to check that the user typed a real number, so the program does not crash
- How to pick from a list of choices with `switch`

## The Idea

You make decisions every day:

> **If** it is raining, bring an umbrella. **Otherwise**, wear a cap.

A program decides the same way:

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

- The part inside `( )` is the **condition**. It is either `true` or `false`.
- If it is `true`, the lines inside the **first** `{ }` run.
- If it is `false`, the lines inside the `else { }` run.
- Only **one** of the two runs. Never both.

The curly braces `{ }` group lines together. Everything between `{` and `}` belongs to that `if` or `else`.

### Comparing values

| Symbol | Meaning | Example | Result |
|---|---|---|---|
| `==` | is equal to | `5 == 5` | `true` |
| `!=` | is **not** equal to | `5 != 3` | `true` |
| `>` | is greater than | `90 > 75` | `true` |
| `<` | is less than | `60 < 75` | `true` |
| `>=` | is greater than **or equal to** | `75 >= 75` | `true` |
| `<=` | is less than **or equal to** | `80 <= 75` | `false` |

> [!WARNING]
> `=` and `==` are different!
> One `=` **puts** a value in a box: `age = 18;`
> Two `==` **compare** two values: `if (age == 18)`

### Combining conditions

| Symbol | Say it as | Example | True when... |
|---|---|---|---|
| `&&` | AND | `age >= 18 && hasId` | **both** sides are true |
| `\|\|` | OR | `day == "Sat" \|\| day == "Sun"` | **at least one** side is true |
| `!` | NOT | `!isEnrolled` | the value is **false** |

> The `|` key is above the **Enter** key on most keyboards. Hold **Shift** and press **\\** to type it. Type it twice for `||`.

---

## Step by Step

### Step 1: Create a new project named `Lesson05`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson05`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Type the code

1. Click inside the editor. Press **Ctrl + A**, then **Delete**.
2. Type the code below.

> [!TIP]
> When you type `{` and press **Enter**, Visual Studio adds the closing `}` and moves your next line a little to the right for you. That spacing (called **indentation**) makes the code easier to read.

## The Code

```csharp
// Lesson 5: Grade remarks using if / else if / else

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

### Step 3: Run it five times

Press **Ctrl + F5**, type an answer, and look at the result. Close the black window. Do this five times, with these five answers: `92`, `80`, `70`, `abc`, `150`.

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

*"If it is **not** a number..."* We check this first, because the other checks only make sense for real numbers.

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

C# checks each condition **from top to bottom** and runs the **first one that is true**. Then it skips all the rest. If none is true, the `else` part runs.

The order matters. A grade of `92` is also `>= 75`. But `>= 90` comes first, so the student sees "Excellent!".

### Comparing text

Text comparison cares about capital letters: `"yes" == "YES"` is `false`. To ignore capital letters, change the text to small letters first with `.ToLower()`:

```cs
if (answer.ToLower() == "yes")
```

---

## Another Way to Choose: `switch`

When you compare **one** variable with **several exact values** (like menu choices), `switch` is cleaner.

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this:

```csharp
Console.WriteLine("1 - Say hello");
Console.WriteLine("2 - Say goodbye");
Console.Write("Choose an option: ");
string choice = Console.ReadLine() ?? "";

switch (choice)
{
    case "1":
        Console.WriteLine("Hello there!");
        break;

    case "2":
        Console.WriteLine("Goodbye!");
        break;

    default:
        Console.WriteLine("Invalid option.");
        break;
}
```

**2.** Press **Ctrl + F5** and type `1`.

✅ **You will see:**

```text
1 - Say hello
2 - Say goodbye
Choose an option: 1
Hello there!
```

- `switch (choice)` means: look at what is in `choice`.
- `case "1":` means: if it is `"1"`, run the lines below.
- `break;` means: stop here. Every `case` must end with `break;`
- `default:` runs when no `case` matched. It works like `else`.

**Which one should I use?**

- Use `if` for ranges and questions: `grade >= 75`, `age >= 18 && hasId`
- Use `switch` to pick one of several exact values: `"1"`, `"2"`, `"3"`

---

## If Something Goes Wrong

**Error List: `CS0029  Cannot implicitly convert type 'double' to 'bool'`**
You wrote `if (grade = 75)` with one `=`. To compare, use two: `if (grade == 75)`

**"Passed" always shows, even for a low grade.**
You may have a `;` right after the condition: `if (grade >= 75);`. Remove that `;`. Never put `;` directly after `if (...)`.

**The "Excellent" message never shows.**
Your conditions are in the wrong order. Check the **highest** number first (`>= 90` before `>= 75`).

**Error List: `CS0163  Control cannot fall through from one case label`**
A `case` is missing its `break;`. Add `break;` at the end of every `case`.

**Error List: `CS1513  } expected`**
A `{` was opened but never closed. Every `{` needs a matching `}`.

---

## Exercise

**Exercise 1.** Ask for a whole number. Say if it is **even** or **odd**. (Hint: a number is even when `number % 2 == 0`.)

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

**Exercise 2.** **A simple login.** Ask for a username and a password. If the username is `admin` **and** the password is `1234`, show `Welcome, admin!`. Otherwise, show `Access denied.`

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

⚠️ This is only for practice. Writing a password inside the code is **not safe**. In Lessons 17 to 20, you will learn how real systems keep passwords safe.

</details>

## ⭐ Bonus Challenge

**Can you vote?** Ask for the user's age. Then ask if they have a valid ID (`yes` or `no`). They can vote only if they are **18 or older AND** have an ID.

Make it safe: use `int.TryParse` so the program does not crash if the user types letters for the age. Also accept `YES`, `Yes`, and `yes`.

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

- `if (condition) { ... } else { ... }` runs one part or the other.
- `else if` checks more conditions, from top to bottom. The first true one wins.
- One `=` stores a value. Two `==` compare.
- `&&` is AND, `||` is OR, `!` is NOT.
- `double.TryParse(text, out double number)` checks if text is a number, without crashing.
- `switch` picks from exact values. End every `case` with `break;`

---

[⬅️ Lesson 04](04-math-and-operators.md) | [🏠 Course Home](../README.md) | [Next: Lesson 06 (Loops) ➡️](06-loops.md)
