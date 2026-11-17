# Lesson 06: Comparison, Logical, and Mixed Operators

**Part 1: Beginner** · About 75 minutes

## What You Will Learn

- **Comparison operators**: how to compare two values (is it bigger? is it equal?)
- **Logical operators**: how to combine answers with **AND**, **OR**, and **NOT**
- **Mixed operators**: how to use math, comparing, and combining in one line
- Why every comparison gives `true` or `false`

## The Idea

In Lesson 05, operators did **math**: `10 + 3` gives `13`.

In this lesson, operators ask **yes-or-no questions**:

- Is your grade 75 or higher?
- Is your age the same as 18?
- Do you have an ID **and** are you old enough?

The answer to a yes-or-no question is always `true` or `false`. Remember the `bool` type from Lesson 02? This is what it is for:

```cs
int grade = 80;
bool passed = grade >= 75;   // "Is grade 75 or higher?" The answer is true
```

You are not making decisions yet. You are learning to **ask the questions**. In the next lesson, your program will **act** on the answers.

---

## Step by Step

### Step 1: Create a new project named `Lesson06`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson06`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Comparison Operators

A **comparison operator** compares two values.

| Symbol | Meaning | Example | Answer |
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
> Two `==` **compare** two values: `age == 18`

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
int age = 18;
int votingAge = 18;

Console.WriteLine($"age == votingAge : {age == votingAge}");
Console.WriteLine($"age != votingAge : {age != votingAge}");
Console.WriteLine($"age >  votingAge : {age > votingAge}");
Console.WriteLine($"age <  votingAge : {age < votingAge}");
Console.WriteLine($"age >= votingAge : {age >= votingAge}");
Console.WriteLine($"age <= votingAge : {age <= votingAge}");
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
age == votingAge : True
age != votingAge : False
age >  votingAge : False
age <  votingAge : False
age >= votingAge : True
age <= votingAge : True
```

**What it means:** both values are 18. So "equal to" is `True`, "not equal to" is `False`, "greater than" and "less than" are both `False`, and the two "or equal to" questions are `True`.

**3.** Change the first line to `int age = 20;` and run again. Before you run, guess each answer!

<details>
<summary>Show the answers for age = 20</summary>

```text
age == votingAge : False
age != votingAge : True
age >  votingAge : True
age <  votingAge : False
age >= votingAge : True
age <= votingAge : False
```

</details>

### Comparing text

`==` and `!=` also work with text. But capital letters matter:

```csharp
string answer = "YES";

Console.WriteLine(answer == "YES");
Console.WriteLine(answer == "yes");
Console.WriteLine(answer.ToLower() == "yes");
```

✅ **The output:**

```text
True
False
True
```

`"YES"` and `"yes"` are **not** equal. To ignore capital letters, change the text to small letters first with `.ToLower()` (from Lesson 04).

---

## Part 2: Logical Operators

A **logical operator** combines `true` and `false` answers.

| Symbol | Say it as | The result is `true` when... |
|---|---|---|
| `&&` | AND | **both** sides are true |
| `\|\|` | OR | **at least one** side is true |
| `!` | NOT | the value is **false** (it flips the answer) |

> **How to type them:** `&` is **Shift + 7**. The `|` key is above the **Enter** key on most keyboards: hold **Shift** and press **\\**. Type each one twice for `&&` and `||`.

Think of real life:

- **AND:** To vote, you must be 18 or older **AND** have an ID. Both must be true.
- **OR:** You can rest if it is Saturday **OR** it is Sunday. One is enough.
- **NOT:** If you are **NOT** enrolled, you cannot take the exam.

**1.** Clear the editor and type this code:

```csharp
int age = 20;
bool hasId = true;
bool isSaturday = false;
bool isSunday = true;

bool canVote = age >= 18 && hasId;         // AND
bool isWeekend = isSaturday || isSunday;   // OR
bool isWeekday = !isWeekend;               // NOT

Console.WriteLine($"Can vote:   {canVote}");
Console.WriteLine($"Is weekend: {isWeekend}");
Console.WriteLine($"Is weekday: {isWeekday}");
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Can vote:   True
Is weekend: True
Is weekday: False
```

**What it means:**

- `age >= 18 && hasId` → `true && true` → **`true`**. Both sides are true.
- `isSaturday || isSunday` → `false || true` → **`true`**. One side is enough.
- `!isWeekend` → NOT `true` → **`false`**. `!` flips the answer.

### All the possible results

| A | B | `A && B` (AND) | `A \|\| B` (OR) |
|---|---|---|---|
| `true` | `true` | `true` | `true` |
| `true` | `false` | `false` | `true` |
| `false` | `true` | `false` | `true` |
| `false` | `false` | `false` | `false` |

- **AND** is strict: it is `true` only in the **first** row.
- **OR** is easygoing: it is `false` only in the **last** row.

---

## Part 3: Mixed Operators

You can use **math**, **comparing**, and **combining** together in one line. These are called **mixed operators**.

```cs
bool result = 5 + 3 > 7 && 10 % 2 == 0;
```

Which part does C# do first? It follows an **order**:

| Order | Operators | Kind |
|---|---|---|
| 1st | `( )` | Parentheses |
| 2nd | `!` | NOT |
| 3rd | `*` `/` `%` | Multiply, divide, remainder |
| 4th | `+` `-` | Add, subtract |
| 5th | `<` `>` `<=` `>=` | Comparing sizes |
| 6th | `==` `!=` | Comparing equality |
| 7th | `&&` | AND |
| 8th | `\|\|` | OR |

**An easy way to remember: math first, then comparing, then AND, then OR.**

Let's work out the example, step by step:

```text
5 + 3 > 7 && 10 % 2 == 0
  8   > 7 &&    0   == 0        ← 1. the math
    true  &&      true          ← 2. the comparing
         true                   ← 3. the AND
```

**1.** Clear the editor and type this code:

```csharp
bool first = 5 + 3 > 7 && 10 % 2 == 0;
bool second = 2 + 3 * 4 == 20;
bool third = (2 + 3) * 4 == 20;
bool fourth = true || false && false;
bool fifth = (true || false) && false;

Console.WriteLine($"first:  {first}");
Console.WriteLine($"second: {second}");
Console.WriteLine($"third:  {third}");
Console.WriteLine($"fourth: {fourth}");
Console.WriteLine($"fifth:  {fifth}");
```

**2.** Before you run it, try to work out each answer on paper. Then press **Ctrl + F5**.

✅ **The output:**

```text
first:  True
second: False
third:  True
fourth: True
fifth:  False
```

**What it means:**

- **second:** `3 * 4` first (12), then `2 + 12` is 14. Is 14 equal to 20? `False`.
- **third:** the parentheses go first: `2 + 3` is 5, then `5 * 4` is 20. Is 20 equal to 20? `True`.
- **fourth:** AND goes before OR. `false && false` is `false`. Then `true || false` is `True`.
- **fifth:** the parentheses go first. `true || false` is `true`. Then `true && false` is `False`.

> [!TIP]
> **When you are not sure about the order, add parentheses.** `(average >= 85) && (absences <= 3)` means the same as without them, and it is easier to read.

---

## Part 4: Put It All Together

This program uses **all three** kinds of operators to check a student's standing.

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 6: Comparison, logical, and mixed operators

Console.Write("Prelim grade: ");
double prelim = double.Parse(Console.ReadLine() ?? "");

Console.Write("Midterm grade: ");
double midterm = double.Parse(Console.ReadLine() ?? "");

Console.Write("Finals grade: ");
double finals = double.Parse(Console.ReadLine() ?? "");

Console.Write("Number of absences: ");
int absences = int.Parse(Console.ReadLine() ?? "");

// Arithmetic operators: do the math
double average = (prelim + midterm + finals) / 3;

// Comparison operators: each answer is true or false
bool passed = average >= 75;
bool hasGoodAttendance = absences <= 3;

// Logical operators: combine the answers
bool canJoinHonors = average >= 90 && hasGoodAttendance;
bool needsHelp = average < 75 || absences > 10;

// Mixed operators: math, comparing, and combining in ONE line
bool getsScholarship = (prelim + midterm + finals) / 3 >= 85 && absences <= 3;

Console.WriteLine();
Console.WriteLine($"Average:                 {average:F2}");
Console.WriteLine($"Passed?                  {passed}");
Console.WriteLine($"Good attendance?         {hasGoodAttendance}");
Console.WriteLine($"Can join the honor roll? {canJoinHonors}");
Console.WriteLine($"Needs extra help?        {needsHelp}");
Console.WriteLine($"Gets a scholarship?      {getsScholarship}");
```

**2.** Press **Ctrl + F5**. Type `88`, `92`, `90`, and `2`.

## The Output

✅ **You will see:**

```text
Prelim grade: 88
Midterm grade: 92
Finals grade: 90
Number of absences: 2

Average:                 90.00
Passed?                  True
Good attendance?         True
Can join the honor roll? True
Needs extra help?        False
Gets a scholarship?      True
```

**3.** Run it again with `70`, `72`, `74`, and `12` absences.

```text
Average:                 72.00
Passed?                  False
Good attendance?         False
Can join the honor roll? False
Needs extra help?        True
Gets a scholarship?      False
```

---

## What the Code Means

```cs
double average = (prelim + midterm + finals) / 3;
```

**Arithmetic operators.** Plain math, like in Lesson 05.

```cs
bool passed = average >= 75;
bool hasGoodAttendance = absences <= 3;
```

**Comparison operators.** Each line asks one question and keeps the answer in a `bool` box.

```cs
bool canJoinHonors = average >= 90 && hasGoodAttendance;
```

**Logical operator AND.** The student needs an average of 90 or more **and** good attendance. Both.

```cs
bool needsHelp = average < 75 || absences > 10;
```

**Logical operator OR.** The student needs help if the average is below 75, **or** if there are more than 10 absences. One is enough.

```cs
bool getsScholarship = (prelim + midterm + finals) / 3 >= 85 && absences <= 3;
```

**Mixed operators.** One line with math, comparing, and combining. C# works it out in order:

1. The parentheses: `prelim + midterm + finals`
2. The division: `/ 3`
3. The comparisons: `>= 85` and `<= 3`
4. The AND: `&&`

---

## If Something Goes Wrong

**Error List: `CS0029  Cannot implicitly convert type 'int' to 'bool'`**
You used one `=` where you need two. `bool same = age = 18;` should be `bool same = age == 18;`

**Error List: `CS0019  Operator '&&' cannot be applied to operands of type 'bool' and 'int'`**
`&&` and `||` only work with `true` / `false` values. Each side must be a **question**. Write `age >= 13 && age <= 19`, not `age >= 13 && 19`.

**Error List: `CS0019  Operator '<=' cannot be applied to operands of type 'bool' and 'int'`**
You wrote something like `13 <= age <= 19`. C# cannot read that. Ask two questions, and join them with AND: `age >= 13 && age <= 19`

**My answer is wrong, but there is no error.**
Check the **order** of your operators. Add parentheses around the part that should happen first.

---

## Exercise

**Exercise 1.** Ask the user for two whole numbers. Show three answers: are they equal? Is the first one bigger? Is the first one smaller?

```text
First number: 8
Second number: 5
Equal?            False
First is bigger?  True
First is smaller? False
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("First number: ");
int first = int.Parse(Console.ReadLine() ?? "");

Console.Write("Second number: ");
int second = int.Parse(Console.ReadLine() ?? "");

Console.WriteLine($"Equal?            {first == second}");
Console.WriteLine($"First is bigger?  {first > second}");
Console.WriteLine($"First is smaller? {first < second}");
```

</details>

**Exercise 2.** Ask for the user's age. Show `True` or `False`: is the user a **teenager** (13 to 19 years old)?

<details>
<summary>Show answer</summary>

```csharp
Console.Write("How old are you? ");
int age = int.Parse(Console.ReadLine() ?? "");

bool isTeenager = age >= 13 && age <= 19;
Console.WriteLine($"Teenager? {isTeenager}");
```

A teenager is at least 13 **AND** at most 19. Both must be true.

</details>

**Exercise 3.** Work these out **on paper** first. Then type them into a program to check your answers.

```cs
bool a = 10 - 4 > 5;
bool b = 9 % 3 == 0 && 7 > 9;
bool c = !(4 == 4) || 2 * 3 == 6;
```

<details>
<summary>Show answer</summary>

```csharp
bool a = 10 - 4 > 5;
bool b = 9 % 3 == 0 && 7 > 9;
bool c = !(4 == 4) || 2 * 3 == 6;

Console.WriteLine($"a: {a}");
Console.WriteLine($"b: {b}");
Console.WriteLine($"c: {c}");
```

- **a** is `True`: 10 − 4 is 6, and 6 is greater than 5.
- **b** is `False`: `9 % 3 == 0` is true, but `7 > 9` is false. AND needs both.
- **c** is `True`: `!(4 == 4)` is false, but `2 * 3 == 6` is true. OR needs only one.

</details>

## ⭐ Bonus Challenge

**Is it a leap year?** A year is a leap year when:

- it can be divided by 4, **and** it can **not** be divided by 100
- **or** it can be divided by 400

Ask for a year, and show `True` or `False`. Test it with `2024` (True), `2026` (False), `1900` (False), and `2000` (True).

Hint: "can be divided by 4" means the remainder is zero: `year % 4 == 0`.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Type a year: ");
int year = int.Parse(Console.ReadLine() ?? "");

bool isLeapYear = (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;

Console.WriteLine($"Leap year? {isLeapYear}");
```

This one line uses **all three** kinds of operators: arithmetic (`%`), comparison (`==`, `!=`), and logical (`&&`, `||`). The parentheses make the two rules easy to see.

</details>

---

## Remember

- A **comparison** asks a yes-or-no question. The answer is `true` or `false`.
- Comparison operators: `==` `!=` `>` `<` `>=` `<=`
- One `=` stores a value. Two `==` compare.
- Logical operators: `&&` (AND, both), `||` (OR, at least one), `!` (NOT, flips)
- **Mixed operators** follow an order: math first, then comparing, then AND, then OR.
- Not sure about the order? Add **parentheses**.

---

[⬅️ Lesson 05](05-arithmetic-operators.md) | [🏠 Course Home](../README.md) | [Next: Lesson 07 (If and Else) ➡️](07-if-and-else.md)
