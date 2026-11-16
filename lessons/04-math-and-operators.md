# Lesson 04: Math and Operators

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 03](03-user-input.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Add, subtract, multiply, and divide in C#
- Avoid the most common math trap (whole-number division)
- Show a number with exactly 2 decimal places
- Build a grade average calculator

---

## 🧠 The Big Idea

Computers are excellent calculators. The symbols you use for math are called **operators**.

| Operator | Meaning | Example | Result |
|---|---|---|---|
| `+` | Add | `10 + 3` | `13` |
| `-` | Subtract | `10 - 3` | `7` |
| `*` | Multiply | `10 * 3` | `30` |
| `/` | Divide | `10 / 4` | `2` (see the trap below!) |
| `%` | **Remainder** after dividing | `10 % 3` | `1` (10 ÷ 3 = 3, remainder **1**) |

### ⚠️ The division trap

When you divide **two whole numbers** (`int`), C# gives back a **whole number**. It simply **cuts off** the decimal part.

| Code | Result | Why |
|---|---|---|
| `7 / 2` | `3` | Both are `int`, so the `.5` is cut off |
| `7.0 / 2` | `3.5` | `7.0` is a `double`, so the answer keeps its decimals |
| `7.0 / 2.0` | `3.5` | Both are `double` |

**Rule:** If you need decimals in your answer, make sure at least one number is a `double`.

### Order of operations

C# follows the same order you learned in math class: `*` and `/` first, then `+` and `-`. Use **parentheses `( )`** to control the order.

| Code | Result |
|---|---|
| `2 + 3 * 4` | `14` (3 × 4 first, then + 2) |
| `(2 + 3) * 4` | `20` (2 + 3 first) |

### Shortcuts

| Shortcut | Same as | Meaning |
|---|---|---|
| `score += 5;` | `score = score + 5;` | Add 5 to `score` |
| `score -= 5;` | `score = score - 5;` | Subtract 5 from `score` |
| `count++;` | `count = count + 1;` | Add 1 |
| `count--;` | `count = count - 1;` | Subtract 1 |

### Showing exactly 2 decimal places

```cs
double average = 87.666666;
Console.WriteLine($"{average:F2}");   // Shows: 87.67
```

Adding **`:F2`** after the variable name inside `{ }` means *"show 2 decimal places, rounded."* `:F1` shows 1 decimal place, `:F3` shows 3, and so on.

---

## 🛠️ Try It: An Operator Playground

Create the project and try all the operators:

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson04
cd Lesson04
code .
```

Replace `Program.cs` with:

```csharp
int a = 17;
int b = 5;

Console.WriteLine($"{a} + {b} = {a + b}");
Console.WriteLine($"{a} - {b} = {a - b}");
Console.WriteLine($"{a} * {b} = {a * b}");
Console.WriteLine($"{a} / {b} = {a / b}");
Console.WriteLine($"{a} % {b} = {a % b}");

double c = 17.0;
Console.WriteLine($"{c} / {b} = {c / b}");

int score = 10;
score += 5;
score++;
Console.WriteLine($"Score: {score}");
```

Run it with `dotnet run`.

✅ **You should see:**

```text
17 + 5 = 22
17 - 5 = 12
17 * 5 = 85
17 / 5 = 3
17 % 5 = 2
17 / 5 = 3.4
Score: 16
```

Look at lines 4 and 6. Both say "17 / 5", but the first one uses `int` (answer `3`) and the second one uses `double` (answer `3.4`).

---

## 🛠️ Let's Build It: Grade Average Calculator

Replace everything in `Program.cs` with:

```csharp
// Lesson 4: Grade average calculator

Console.WriteLine("=== GRADE AVERAGE CALCULATOR ===");

Console.Write("Enter your Prelim grade: ");
double prelim = double.Parse(Console.ReadLine() ?? "");

Console.Write("Enter your Midterm grade: ");
double midterm = double.Parse(Console.ReadLine() ?? "");

Console.Write("Enter your Finals grade: ");
double finals = double.Parse(Console.ReadLine() ?? "");

double total = prelim + midterm + finals;
double average = total / 3;

Console.WriteLine();
Console.WriteLine($"Total:   {total}");
Console.WriteLine($"Average: {average:F2}");
```

Run it:

```cmd
dotnet run
```

✅ **You should see** (the numbers after each `:` are what you type):

```text
=== GRADE AVERAGE CALCULATOR ===
Enter your Prelim grade: 85
Enter your Midterm grade: 90
Enter your Finals grade: 88

Total:   263
Average: 87.67
```

---

## 🔎 Line by Line

```cs
double prelim = double.Parse(Console.ReadLine() ?? "");
```

We use `double` (not `int`) because grades can have decimals, like `89.5`.

```cs
double total = prelim + midterm + finals;
double average = total / 3;
```

Because `total` is a `double`, `total / 3` keeps the decimals. No trap here!

```cs
Console.WriteLine($"Average: {average:F2}");
```

The real average is `87.6666666...`. `:F2` shows it as `87.67`.

---

## ⚠️ Common Mistakes

### Mistake 1: Losing decimals because of `int`

```cs
int total = 263;
int average = total / 3;     // average is 87, not 87.67!
```

**Fix:** Use `double` for values that can have decimals.

### Mistake 2: Forgetting parentheses

```cs
double average = prelim + midterm + finals / 3;
```

This divides **only** `finals` by 3, then adds the others. **Fix:** `(prelim + midterm + finals) / 3`

### Mistake 3: Using a comma for decimals

Type `89.5`, not `89,5`. C# code always uses a **dot** for decimals.

---

## 🏋️ Practice

**Exercise 1.** Ask for the **length** and **width** of a rectangle (decimals allowed). Show its **area** (length × width) and **perimeter** (2 × length + 2 × width).

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Length: ");
double length = double.Parse(Console.ReadLine() ?? "");

Console.Write("Width: ");
double width = double.Parse(Console.ReadLine() ?? "");

double area = length * width;
double perimeter = (2 * length) + (2 * width);

Console.WriteLine($"Area: {area:F2}");
Console.WriteLine($"Perimeter: {perimeter:F2}");
```

</details>

**Exercise 2.** **Bill splitter.** Ask for the total restaurant bill and the number of friends. Show how much each friend pays, with 2 decimal places.

```text
Total bill: 1250
Number of friends: 3
Each friend pays: 416.67
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Total bill: ");
double bill = double.Parse(Console.ReadLine() ?? "");

Console.Write("Number of friends: ");
int friends = int.Parse(Console.ReadLine() ?? "");

double eachPays = bill / friends;
Console.WriteLine($"Each friend pays: {eachPays:F2}");
```

`bill` is a `double`, so `bill / friends` keeps its decimals.

</details>

**Exercise 3.** Ask for a number of **minutes** and show it in hours and minutes. Use `/` and `%`.

```text
Minutes: 135
135 minutes = 2 hour(s) and 15 minute(s)
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Minutes: ");
int totalMinutes = int.Parse(Console.ReadLine() ?? "");

int hours = totalMinutes / 60;     // whole-number division: 135 / 60 = 2
int minutes = totalMinutes % 60;   // remainder: 135 % 60 = 15

Console.WriteLine($"{totalMinutes} minutes = {hours} hour(s) and {minutes} minute(s)");
```

Here, whole-number division is exactly what we want!

</details>

---

## 📌 Recap

- `+ - * /` work like in math. `%` gives the **remainder**.
- `int / int` cuts off decimals. Use `double` when you need decimals.
- Use parentheses to control the order: `(a + b) / 2`.
- `x += 5;` adds 5 to `x`. `x++;` adds 1.
- `{value:F2}` shows a number with 2 decimal places.

---

[⬅️ Lesson 03](03-user-input.md) | [🏠 Course Home](../README.md) | [Next: Lesson 05 (Decisions) ➡️](05-decisions.md)
