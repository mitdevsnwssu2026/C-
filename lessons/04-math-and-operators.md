# Lesson 04: Math and Operators

**Part 1: Beginner** · About 60 minutes

## What You Will Learn

- How to add, subtract, multiply, and divide in C#
- How to avoid the most common math mistake (losing the decimals)
- How to show a number with exactly 2 decimal places
- How to build a grade average calculator

## The Idea

A computer is a very good calculator. The math symbols are called **operators**.

| Symbol | Meaning | Example | Result |
|---|---|---|---|
| `+` | Add | `10 + 3` | `13` |
| `-` | Subtract | `10 - 3` | `7` |
| `*` | Multiply | `10 * 3` | `30` |
| `/` | Divide | `10.0 / 4` | `2.5` |
| `%` | **Remainder** after dividing | `10 % 3` | `1` |

`%` gives what is **left over**. 10 divided by 3 is 3, with **1** left over. So `10 % 3` is `1`.

### ⚠️ Watch out: dividing whole numbers

When you divide **two whole numbers** (`int`), C# **cuts off** the decimals.

- `7 / 2` gives `3` (the `.5` is cut off!)
- `7.0 / 2` gives `3.5` (because `7.0` is a `double`)

**The rule:** if you want decimals in the answer, use `double`.

---

## Step by Step

### Step 1: Create a new project named `Lesson04`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson04`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Try every operator

1. Click inside the editor. Press **Ctrl + A**, then **Delete**.
2. Type this code:

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

3. Press **Ctrl + F5**.

✅ **You will see:**

```text
17 + 5 = 22
17 - 5 = 12
17 * 5 = 85
17 / 5 = 3
17 % 5 = 2
17 / 5 = 3.4
Score: 16
```

Look at the 4th and 6th lines. Both say `17 / 5`. The first one used `int`, so the answer is `3`. The second one used `double`, so the answer is `3.4`.

The last part shows two shortcuts:

- `score += 5;` means "add 5 to `score`". (10 becomes 15.)
- `score++;` means "add 1 to `score`". (15 becomes 16.)

Press any key to close the black window.

### Step 3: Build the grade average calculator

1. Click inside the editor. Press **Ctrl + A**, then **Delete**.
2. Type the code below.

## The Code

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

### Step 4: Run it

Press **Ctrl + F5**. Type three grades. Press **Enter** after each one.

## The Output

✅ **You will see** (here the user typed `85`, `90`, and `88`):

```text
=== GRADE AVERAGE CALCULATOR ===
Enter your Prelim grade: 85
Enter your Midterm grade: 90
Enter your Finals grade: 88

Total:   263
Average: 87.67
```

---

## What the Code Means

```cs
double prelim = double.Parse(Console.ReadLine() ?? "");
```

Read the answer and turn it into a decimal number. We use `double` because a grade can have decimals, like `89.5`.

```cs
double total = prelim + midterm + finals;
double average = total / 3;
```

Add the three grades, then divide by 3. `total` is a `double`, so the decimals are kept.

```cs
Console.WriteLine($"Average: {average:F2}");
```

The real average is `87.6666666...`. Adding **`:F2`** after the name means *"show 2 decimal places."* So the screen shows `87.67`. (`:F1` shows 1 decimal place, `:F3` shows 3.)

### The order of math

C# follows the same order as in math class: `*` and `/` first, then `+` and `-`. Use **parentheses** `( )` to choose what happens first.

- `2 + 3 * 4` gives `14` (3 × 4 first)
- `(2 + 3) * 4` gives `20` (2 + 3 first)

---

## If Something Goes Wrong

**My average has no decimals (87 instead of 87.67).**
You used `int` somewhere. Use `double` for `total` and `average`.

**My average is far too big.**
You may have written `prelim + midterm + finals / 3`. That divides only `finals` by 3. Use parentheses: `(prelim + midterm + finals) / 3`.

**The program crashed when I typed `89,5`.**
Type decimals with a **dot**: `89.5`, not a comma.

---

## Exercise

**Exercise 1.** Ask for the **length** and **width** of a rectangle. Show its **area** (length × width) and its **perimeter** (2 × length + 2 × width), with 2 decimal places.

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

**Exercise 2.** **Bill splitter.** Ask for the total restaurant bill and the number of friends. Show how much each friend pays.

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

</details>

## ⭐ Bonus Challenge

Ask for a number of **minutes**. Show it as hours and minutes. You will need both `/` and `%`.

```text
Minutes: 135
135 minutes = 2 hour(s) and 15 minute(s)
```

Hint: this time, cutting off the decimals is exactly what you want.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Minutes: ");
int totalMinutes = int.Parse(Console.ReadLine() ?? "");

int hours = totalMinutes / 60;     // 135 / 60 = 2  (decimals cut off)
int minutes = totalMinutes % 60;   // 135 % 60 = 15 (what is left over)

Console.WriteLine($"{totalMinutes} minutes = {hours} hour(s) and {minutes} minute(s)");
```

</details>

---

## Remember

- `+ - * /` work like in math. `%` gives the **remainder**.
- Dividing two `int` numbers cuts off the decimals. Use `double` to keep them.
- Use parentheses to choose the order: `(a + b) / 2`
- `x += 5;` adds 5 to `x`. `x++;` adds 1.
- `{value:F2}` shows 2 decimal places.

---

[⬅️ Lesson 03](03-user-input.md) | [🏠 Course Home](../README.md) | [Next: Lesson 05 (Decisions) ➡️](05-decisions.md)
