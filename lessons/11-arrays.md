# Lesson 11: Arrays

**Part 1: Beginner** · About 90 minutes

## What You Will Learn

- **Array basics**: how to keep many values in one variable
- **Array access**: how to get or change one value by its position
- **Array iteration**: how to visit every value with a loop
- **Array operations**: total, average, highest, lowest, searching, and sorting
- How to combine everything in the **Array Challenge**

## The Idea

A variable holds **one** value. But what if you need to keep the quiz scores of 5 quizzes? Or the names of 40 students? Making 40 separate variables would be terrible.

An **array** is **one** variable that holds a **row of values** of the same type. Think of an egg tray: one tray, many slots, and each slot holds one egg.

```text
 index:     0      1      2      3      4
          ┌──────┬──────┬──────┬──────┬──────┐
 scores:  │  85  │  92  │  78  │  95  │  88  │
          └──────┴──────┴──────┴──────┴──────┘
```

Each slot has a position number, called its **index**.

> [!IMPORTANT]
> **Indexes start at 0, not 1.** The first item is at index `0`. The second is at index `1`. In an array of 5 items, the last index is `4`. (You saw the same idea with text positions in Lesson 04.)

---

## Step by Step

### Step 1: Create a new project named `Lesson11`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson11`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Array Basics

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
// An array of text
string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };

// An array of whole numbers
int[] scores = { 85, 92, 78, 95, 88 };

// An empty array with room for 3 decimal numbers
double[] prices = new double[3];

Console.WriteLine($"days has {days.Length} items.");
Console.WriteLine($"scores has {scores.Length} items.");
Console.WriteLine($"prices has {prices.Length} items.");
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
days has 5 items.
scores has 5 items.
prices has 3 items.
```

**What it means:**

- `string[]` means *"an array of strings"*. The `[]` after the type means **"many"**. In the same way, `int[]` is an array of whole numbers, and `double[]` is an array of decimal numbers.
- `{ "Mon", "Tue", ... }` lists the values, with commas between them.
- `new double[3]` makes an array with **3 empty slots**. For numbers, every slot starts at `0`.
- `.Length` tells you how many items the array has.

> [!NOTE]
> **An array's size is fixed.** Once it is made, it cannot grow or shrink. (In the next lesson you will meet the **List**, which can grow.)

---

## Part 2: Array Access

To use one item, write the array's name and the item's **index** in square brackets `[ ]`.

**1.** Clear the editor and type this code:

```csharp
int[] scores = { 85, 92, 78, 95, 88 };

// Reading items
Console.WriteLine($"First score:  {scores[0]}");
Console.WriteLine($"Second score: {scores[1]}");
Console.WriteLine($"Last score:   {scores[scores.Length - 1]}");

// Changing an item
scores[2] = 80;
Console.WriteLine($"The third score is now: {scores[2]}");

// Using items in math
int firstTwo = scores[0] + scores[1];
Console.WriteLine($"First two scores added: {firstTwo}");
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
First score:  85
Second score: 92
Last score:   88
The third score is now: 80
First two scores added: 177
```

**What it means:**

- `scores[0]` is the item at index 0: the **first** item.
- `scores[scores.Length - 1]` is always the **last** item. The array has 5 items, so the last index is 5 − 1 = **4**.
- `scores[2] = 80;` puts a new value into the slot at index 2. The old value (78) is replaced.
- An item like `scores[0]` works just like a normal variable. You can show it, change it, and do math with it.

---

## Part 3: Array Iteration

**Iteration** means *visiting every item, one by one*. You do it with a loop.

**1.** Clear the editor and type this code:

```csharp
string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };

// Way 1: a for loop (gives you the index)
for (int i = 0; i < days.Length; i++)
{
    Console.WriteLine($"Day {i + 1} is {days[i]}");
}

Console.WriteLine();

// Way 2: a foreach loop (simpler)
foreach (string day in days)
{
    Console.WriteLine(day);
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Day 1 is Mon
Day 2 is Tue
Day 3 is Wed
Day 4 is Thu
Day 5 is Fri

Mon
Tue
Wed
Thu
Fri
```

**What it means:**

**Way 1: `for`.** The counter `i` goes 0, 1, 2, 3, 4, and `days[i]` gives the item at that index.

- The loop starts at `i = 0`, because indexes start at 0.
- The condition is `i < days.Length` (**less than**, not "less than or equal"). The last index is 4, and 4 is less than 5.
- We show `i + 1`, because people count from 1.

**Way 2: `foreach`.** Read it as: *"for each `day` in `days`..."* The loop runs once for every item. Each round, the box `day` holds the next item. There is no index and no counter to worry about.

**Which one should I use?**

- Use **`foreach`** when you only need each item.
- Use **`for`** when you also need the **index**, or when you want to **change** the items.

### Filling an array with a loop

A `for` loop can also **put values into** an array:

```csharp
double[] grades = new double[3];

for (int i = 0; i < grades.Length; i++)
{
    Console.Write($"Grade {i + 1}: ");
    grades[i] = double.Parse(Console.ReadLine() ?? "");
}

Console.WriteLine("You typed:");

foreach (double grade in grades)
{
    Console.WriteLine(grade);
}
```

Typing `85`, `90`, and `88` shows:

```text
Grade 1: 85
Grade 2: 90
Grade 3: 88
You typed:
85
90
88
```

---

## Part 4: Array Operations

Now the useful part: **doing things** with all the values. This program finds the total, the average, the highest score, and the lowest score, and then sorts the scores.

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 11: Arrays - a quiz score report

int[] scores = { 85, 92, 78, 95, 88 };

// Array access: get items by their index
Console.WriteLine($"First score: {scores[0]}");
Console.WriteLine($"Last score:  {scores[scores.Length - 1]}");

// Array iteration: visit every item
Console.WriteLine();
Console.WriteLine("All scores:");

for (int i = 0; i < scores.Length; i++)
{
    Console.WriteLine($"  Quiz {i + 1}: {scores[i]}");
}

// Array operations: total, average, highest, lowest
double total = 0;
int highest = scores[0];
int lowest = scores[0];

foreach (int score in scores)
{
    total += score;

    if (score > highest)
    {
        highest = score;
    }

    if (score < lowest)
    {
        lowest = score;
    }
}

double average = total / scores.Length;

Console.WriteLine();
Console.WriteLine($"Total:   {total}");
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Highest: {highest}");
Console.WriteLine($"Lowest:  {lowest}");

// Array operations: sort from lowest to highest
Array.Sort(scores);

Console.WriteLine();
Console.Write("Sorted:  ");

foreach (int score in scores)
{
    Console.Write($"{score} ");
}

Console.WriteLine();
```

**2.** Press **Ctrl + F5**.

## The Output

✅ **You will see:**

```text
First score: 85
Last score:  88

All scores:
  Quiz 1: 85
  Quiz 2: 92
  Quiz 3: 78
  Quiz 4: 95
  Quiz 5: 88

Total:   438
Average: 87.60
Highest: 95
Lowest:  78

Sorted:  78 85 88 92 95
```

---

## What the Code Means

### Finding the total

```cs
double total = 0;

foreach (int score in scores)
{
    total += score;
    ...
}
```

Start with a total of 0. Visit every score, and add it to the total. (`total` is a `double`, so that the average keeps its decimals.)

### Finding the highest and the lowest

```cs
int highest = scores[0];
int lowest = scores[0];
```

Start by **guessing** that the first score is both the highest and the lowest.

```cs
if (score > highest)
{
    highest = score;
}
```

Then look at every score. If one is **bigger** than the highest so far, it becomes the new highest. The same idea works for the lowest, with `<`.

It is like finding the tallest student in a line. You remember the tallest one you have seen so far, and you change your mind only when you see someone taller.

### Finding the average

```cs
double average = total / scores.Length;
```

The total, divided by how many scores there are.

### Sorting

```cs
Array.Sort(scores);
```

`Array.Sort` puts the items in order, from lowest to highest. It changes the array itself. (For text, it sorts from A to Z.)

### Shortcuts for common operations

C# has ready-made tools for the most common jobs. Try this:

```csharp
int[] scores = { 85, 92, 78, 95, 88 };

Console.WriteLine(scores.Sum());
Console.WriteLine(scores.Average());
Console.WriteLine(scores.Max());
Console.WriteLine(scores.Min());
Console.WriteLine(scores.Contains(95));
Console.WriteLine(Array.IndexOf(scores, 95));

Array.Sort(scores);
Console.WriteLine(string.Join(", ", scores));

Array.Reverse(scores);
Console.WriteLine(string.Join(", ", scores));
```

✅ **The output:**

```text
438
87.6
95
78
True
3
78, 85, 88, 92, 95
95, 92, 88, 85, 78
```

| Tool | What it does |
|---|---|
| `scores.Sum()` | Adds all the items |
| `scores.Average()` | The average of the items |
| `scores.Max()` / `scores.Min()` | The highest / the lowest item |
| `scores.Contains(95)` | Is `95` in the array? `True` or `False` |
| `Array.IndexOf(scores, 95)` | The index of `95` (or `-1` if it is not there) |
| `Array.Sort(scores)` | Sorts from lowest to highest |
| `Array.Reverse(scores)` | Turns the order around |
| `string.Join(", ", scores)` | Makes one text from all the items, with `, ` between them |

> **Why learn the long way, then?** Because the long way (a loop with `if`) teaches you how to **think**. In real programs, you will often need an operation that has no ready-made tool, like "count how many students passed". Then you write the loop yourself.

---

## Part 5: The Array Challenge 🎯

Time to combine **everything**: an array, access, iteration, and operations.

**Try to write this program yourself before you look at the answer.**

### The task: a class report

1. Ask how many students are in the class.
2. Make an array with that many slots.
3. Ask for each student's grade, and store it in the array.
4. Show a report with:
   - the number of students
   - the average (2 decimal places)
   - the highest and the lowest grade
   - how many students **passed** (75 or higher) and how many **failed**
   - a list of the students whose grade is **above the average**

### What it should look like

```text
How many students? 5
Grade of student 1: 88
Grade of student 2: 70
Grade of student 3: 95
Grade of student 4: 60
Grade of student 5: 82

===== CLASS REPORT =====
Students:      5
Average:       79.00
Highest grade: 95
Lowest grade:  60
Passed:        3
Failed:        2

Grades above the average:
  Student 1: 88
  Student 3: 95
  Student 5: 82
```

### Hints

- Make the array **after** you know the number: `double[] grades = new double[count];`
- Fill it with a `for` loop (see Part 3).
- Use **one** `foreach` loop for the total, the highest, the lowest, and the count of passed students.
- You need the average **before** you can list the grades above it. So use a **second** loop for that list. Use `for`, because you need the student's number.

<details>
<summary>Show the answer</summary>

```csharp
Console.Write("How many students? ");
int count = int.Parse(Console.ReadLine() ?? "");

double[] grades = new double[count];

// Fill the array
for (int i = 0; i < grades.Length; i++)
{
    Console.Write($"Grade of student {i + 1}: ");
    grades[i] = double.Parse(Console.ReadLine() ?? "");
}

// Array operations
double total = 0;
double highest = grades[0];
double lowest = grades[0];
int passedCount = 0;

foreach (double grade in grades)
{
    total += grade;

    if (grade > highest)
    {
        highest = grade;
    }

    if (grade < lowest)
    {
        lowest = grade;
    }

    if (grade >= 75)
    {
        passedCount++;
    }
}

double average = total / grades.Length;

// The report
Console.WriteLine();
Console.WriteLine("===== CLASS REPORT =====");
Console.WriteLine($"Students:      {grades.Length}");
Console.WriteLine($"Average:       {average:F2}");
Console.WriteLine($"Highest grade: {highest}");
Console.WriteLine($"Lowest grade:  {lowest}");
Console.WriteLine($"Passed:        {passedCount}");
Console.WriteLine($"Failed:        {grades.Length - passedCount}");

Console.WriteLine();
Console.WriteLine("Grades above the average:");

for (int i = 0; i < grades.Length; i++)
{
    if (grades[i] > average)
    {
        Console.WriteLine($"  Student {i + 1}: {grades[i]}");
    }
}
```

</details>

---

## If Something Goes Wrong

**The black window shows a crash:**

```text
Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
```

You used an index that does not exist. An array with 5 items has indexes 0, 1, 2, 3, and 4 only. The most common causes:

- `scores[5]` on an array of 5 items. The last one is `scores[4]`.
- A loop with `i <= scores.Length`. It must be `i < scores.Length`.
- In the Array Challenge: typing `0` students. An empty array has no `grades[0]`.

**My loop skips the first item.**
The loop starts at `i = 1`. Indexes start at **0**: `for (int i = 0; ...`

**Error List: `CS0029  Cannot implicitly convert type 'string' to 'int'`**
You put text into an array of numbers, like `scores[0] = "85";`. Every item in an array must have the array's type.

**Error List: `CS1061  'int[]' does not contain a definition for 'Add'`**
An array cannot grow, so it has no `Add`. Use a **List** for that (next lesson).

---

## Exercise

**Exercise 1.** (Basics and access) Make an array with your 5 favorite foods. Show the first one, the last one, and how many there are. Then **change** the third food to something else, and show all five with `foreach`.

<details>
<summary>Show answer</summary>

```csharp
string[] foods = { "Adobo", "Sinigang", "Lechon", "Pancit", "Halo-halo" };

Console.WriteLine($"First: {foods[0]}");
Console.WriteLine($"Last: {foods[foods.Length - 1]}");
Console.WriteLine($"Count: {foods.Length}");

foods[2] = "Kare-kare";

foreach (string food in foods)
{
    Console.WriteLine(food);
}
```

</details>

**Exercise 2.** (Iteration and searching) Make an array of 5 names. Ask the user for a name. Use a loop to check if that name is in the array, and show `Found!` or `Not found.` Ignore capital letters, so `ana` finds `Ana`.

<details>
<summary>Show answer</summary>

```csharp
string[] names = { "Ana", "Ben", "Carla", "Dino", "Ella" };

Console.Write("Who are you looking for? ");
string search = (Console.ReadLine() ?? "").Trim();

bool found = false;

foreach (string name in names)
{
    if (name.ToLower() == search.ToLower())
    {
        found = true;
    }
}

if (found)
{
    Console.WriteLine("Found!");
}
else
{
    Console.WriteLine("Not found.");
}
```

The box `found` starts as `false`. It becomes `true` only if the loop sees a matching name.

</details>

## ⭐ Bonus Challenge

Show an array **backwards**, from the last item to the first. Do **not** use `Array.Reverse`. Use a `for` loop that counts **down**.

Start with: `string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };`

```text
Fri
Thu
Wed
Tue
Mon
```

Hint: what is the index of the last item? What is the index of the first?

<details>
<summary>Show answer</summary>

```csharp
string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };

for (int i = days.Length - 1; i >= 0; i--)
{
    Console.WriteLine(days[i]);
}
```

The loop starts at the **last** index (`days.Length - 1`, which is 4), keeps going while `i >= 0`, and subtracts 1 each round.

</details>

---

## Remember

- An **array** holds many values of the same type: `int[] scores = { 85, 92, 78 };`
- `new double[3]` makes an array with 3 empty slots. The size is **fixed**.
- **Access:** `scores[0]` is the first item. `scores[scores.Length - 1]` is the last.
- **Indexes start at 0.**
- **Iteration:** `foreach (int score in scores)` or `for (int i = 0; i < scores.Length; i++)`
- **Operations:** use a loop for the total, highest, and lowest. Or use `.Sum()`, `.Max()`, `.Min()`, `Array.Sort()`.

---

[⬅️ Lesson 10](10-nested-loops-and-loop-control.md) | [🏠 Course Home](../README.md) | [Next: Lesson 12 (Lists) ➡️](12-lists.md)
