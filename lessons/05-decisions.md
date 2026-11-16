# Lesson 05: Making Decisions (if, else, switch)

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 75 minutes |
| **Before this** | [Lesson 04](04-math-and-operators.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Make your program choose what to do with `if`, `else if`, and `else`
- Compare values with `==`, `!=`, `>`, `<`, `>=`, `<=`
- Combine conditions with `&&` (AND), `||` (OR), and `!` (NOT)
- Check if the user typed a real number with `TryParse` (no more crashes!)
- Pick from many choices with `switch`

---

## 🧠 The Big Idea

Every day, you make decisions:

> **If** it is raining, bring an umbrella. **Otherwise**, wear a cap.

Programs make decisions the same way:

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

- The part inside `( )` is the **condition**. It must be `true` or `false`.
- If it is `true`, the code inside the **first** `{ }` runs.
- If it is `false`, the code inside the `else { }` runs.
- **Only one** of the two blocks runs, never both.

The curly braces `{ }` group lines together into a **block**. Everything inside the braces belongs to that `if` or `else`.

### Comparison operators

| Operator | Meaning | Example | Result |
|---|---|---|---|
| `==` | is equal to | `5 == 5` | `true` |
| `!=` | is NOT equal to | `5 != 3` | `true` |
| `>` | is greater than | `90 > 75` | `true` |
| `<` | is less than | `60 < 75` | `true` |
| `>=` | is greater than **or equal to** | `75 >= 75` | `true` |
| `<=` | is less than **or equal to** | `80 <= 75` | `false` |

> [!WARNING]
> **`=` and `==` are different!**
> `=` **puts** a value into a box: `age = 18;`
> `==` **compares** two values: `if (age == 18)`

### Many choices: `else if`

```cs
if (grade >= 90)
{
    Console.WriteLine("Excellent!");
}
else if (grade >= 75)
{
    Console.WriteLine("Passed.");
}
else
{
    Console.WriteLine("Failed.");
}
```

C# checks the conditions **from top to bottom** and runs the **first** one that is `true`. Then it skips the rest. If **none** are true, the `else` block runs.

### Combining conditions

| Operator | Meaning | Example | `true` when... |
|---|---|---|---|
| `&&` | AND | `age >= 18 && hasId` | **both** sides are true |
| `\|\|` | OR | `day == "Sat" \|\| day == "Sun"` | **at least one** side is true |
| `!` | NOT | `!isEnrolled` | the value is **false** |

> [!TIP]
> Read `!` out loud as **"not"**. `if (!isEnrolled)` reads as *"if not enrolled."*
> The `||` symbol is two "pipe" keys. On most keyboards, the pipe `|` is above the Enter key (press **Shift + \\**).

### Comparing text

Text comparison is **case-sensitive**: `"yes" == "YES"` is `false`.

To ignore capital letters, change the text to lowercase first with `.ToLower()`:

```cs
string answer = "YES";
if (answer.ToLower() == "yes")
{
    Console.WriteLine("You said yes!");
}
```

---

## 🧠 Checking If Input Is a Number: `TryParse`

In Lesson 03, typing letters instead of a number crashed the program. `TryParse` fixes that. It **tries** to turn text into a number, and it tells you whether it worked.

```cs
bool isNumber = double.TryParse(input, out double grade);
```

| Piece | Meaning |
|---|---|
| `double.TryParse(input, ...)` | **Try** to turn the text `input` into a `double` |
| `out double grade` | If it works, put the number into a new box called `grade` |
| `bool isNumber =` | Keep the answer: `true` if it worked, `false` if not |

Then you decide what to do:

```cs
if (!isNumber)
{
    Console.WriteLine("That is not a number.");
}
```

`int.TryParse` works the same way for whole numbers.

---

## 🛠️ Let's Build It: Grade Remarks

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson05
cd Lesson05
code .
```

### Step 2: Write the code

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

### Step 3: Run it several times

Run `dotnet run` five times. Each time, type a different answer.

✅ **You should see:**

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

No more crashing when the user types letters! 🎉

---

## 🔎 Line by Line

```cs
bool isNumber = double.TryParse(input, out double grade);
```

Try to read the input as a number. `isNumber` becomes `true` or `false`.

```cs
if (!isNumber)
```

"If it is **not** a number..." We check this **first**, because the other checks only make sense for real numbers.

```cs
else if (grade < 0 || grade > 100)
```

"...otherwise, if the grade is below 0 **or** above 100..." This catches impossible grades.

```cs
else if (grade >= 90)
...
else if (grade >= 75)
```

The order matters. A grade of `92` is also `>= 75`, but because `>= 90` comes first, the student sees "Excellent!".

```cs
else
```

If nothing above was true, the grade is from 0 to 74, so the student failed.

---

## 🧠 Another Way to Choose: `switch`

When you compare **one** variable against **many exact values** (like menu choices), `switch` is cleaner than a long `else if` chain.

Try it in your `Lesson05` project. Replace `Program.cs` with:

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

✅ **You should see** (when you type `1`):

```text
1 - Say hello
2 - Say goodbye
Choose an option: 1
Hello there!
```

| Piece | Meaning |
|---|---|
| `switch (choice)` | Look at the value of `choice` |
| `case "1":` | If it is `"1"`, run the lines below this |
| `break;` | **Stop here.** Every `case` must end with `break;` |
| `default:` | If no `case` matched, run this. It works like `else` |

**When to use which?**

- Use **`if` / `else if`** for ranges and complex conditions (`grade >= 75`, `age >= 18 && hasId`).
- Use **`switch`** to pick one option from a list of exact values (menu choices `"1"`, `"2"`, `"3"`).

---

## ⚠️ Common Mistakes

### Mistake 1: Using `=` instead of `==`

```cs
if (grade = 75)
```

```text
error CS0029: Cannot implicitly convert type 'double' to 'bool'
```

**Fix:** To compare, use two equal signs: `if (grade == 75)`

### Mistake 2: Putting a semicolon after the condition

```cs
if (grade >= 75);
{
    Console.WriteLine("Passed.");
}
```

The `;` ends the `if` immediately, so "Passed." **always** shows. C# warns you about this with `warning CS0642: Possible mistaken empty statement`. **Fix:** Never put `;` right after `if (...)`.

### Mistake 3: Wrong order in `else if`

```cs
if (grade >= 75) { ... }
else if (grade >= 90) { ... }   // never runs!
```

Any grade of 90 or more is also `>= 75`, so the first block always wins. **Fix:** Check the **highest** value first.

### Mistake 4: Forgetting `break;` in a `switch`

```text
error CS0163: Control cannot fall through from one case label ('case "1":') to another
```

**Fix:** End every `case` with `break;`

---

## 🏋️ Practice

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

**Exercise 2.** Ask for the user's age **and** whether they have a valid ID (`yes` or `no`). They can vote only if they are **18 or older AND** have an ID. Use `int.TryParse` so the program does not crash on bad input.

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

Notice `bool hasId = idAnswer.ToLower() == "yes";`. The comparison gives `true` or `false`, and we keep that answer in a `bool` box.

</details>

**Exercise 3.** **A simple login.** Ask for a username and password. If the username is `admin` **and** the password is `1234`, show `Welcome, admin!`. Otherwise, show `Access denied.`

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

⚠️ This is **only for practice**. Writing a password directly in code is **unsafe**. In Lessons 17 to 20, you will learn how real systems store passwords safely.

</details>

---

## 📌 Recap

- `if (condition) { ... } else { ... }` runs one block or the other.
- `else if` checks more conditions, from top to bottom. The first `true` one wins.
- Compare with `== != > < >= <=`. Remember: `=` stores, `==` compares.
- Combine with `&&` (AND), `||` (OR), `!` (NOT).
- `double.TryParse(text, out double number)` safely checks if text is a number.
- `switch` picks one option from exact values. End each `case` with `break;`.

---

[⬅️ Lesson 04](04-math-and-operators.md) | [🏠 Course Home](../README.md) | [Next: Lesson 06 (Loops) ➡️](06-loops.md)
