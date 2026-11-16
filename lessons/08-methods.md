# Lesson 08: Methods (Reusable Blocks of Code)

| | |
|---|---|
| **Part** | 2: Intermediate |
| **Time** | About 75 minutes |
| **Before this** | [Lesson 07](07-lists.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Create your own **methods** (named blocks of code you can use again and again)
- Give information **to** a method using **parameters**
- Get an answer **back** from a method using `return`
- Write a `ReadNumber` method that **never crashes**, no matter what the user types

---

## 🧠 The Big Idea

Imagine a recipe card titled **"How to Cook Rice"**. You write it once. Every time you need rice, you just say *"follow the rice card."* You do not rewrite the steps every time.

A **method** is a recipe card for code:

- You **write** the steps once and give them a **name**.
- You **call** (use) the method by its name whenever you need it.

You have already been using methods made by others:

| Method | What it does |
|---|---|
| `Console.WriteLine("Hi")` | Shows text |
| `int.Parse("18")` | Turns text into a number |
| `names.Add("Ana")` | Adds to a list |

Now you will make your **own**.

### Why use methods?

1. **No repeating.** Write once, use many times.
2. **Easier to read.** `ComputeAverage(a, b, c)` explains itself.
3. **Easier to fix.** If there is a bug, you fix it in **one** place.

---

## 🧠 The Parts of a Method

```cs
double ComputeAverage(double a, double b, double c)
{
    double total = a + b + c;
    return total / 3;
}
```

| Part | In the example | Meaning |
|---|---|---|
| **Return type** | `double` | What kind of value the method **gives back** |
| **Name** | `ComputeAverage` | What you call it. Methods use **PascalCase**: every word starts with a capital letter |
| **Parameters** | `(double a, double b, double c)` | The information you **give** the method. Each one has a type and a name |
| **Body** | `{ ... }` | The steps |
| **`return`** | `return total / 3;` | **Give back** the answer and leave the method |

To **call** it:

```cs
double average = ComputeAverage(85, 90, 88);
```

The values `85`, `90`, `88` go into `a`, `b`, `c`. The method gives back `87.666...`, which goes into the box `average`.

### Methods that give nothing back: `void`

Some methods just **do** something, like showing text, and do not give back an answer. Their return type is **`void`** (meaning "nothing"):

```cs
void PrintHeader(string title)
{
    Console.WriteLine("==============================");
    Console.WriteLine($"  {title}");
    Console.WriteLine("==============================");
}
```

Call it like this:

```cs
PrintHeader("GRADE CALCULATOR");
```

### Where do methods go?

In this course, put your methods at the **bottom** of `Program.cs`, **after** your main code. C# finds them there automatically.

```text
Program.cs
┌──────────────────────────────┐
│ // ----- Main program -----  │  ← runs from top to bottom
│ PrintHeader("...");          │
│ ...                          │
│                              │
│ // ----- Methods -----       │  ← recipe cards, used by the code above
│ void PrintHeader(...) {...}  │
│ double ReadNumber(...) {...} │
└──────────────────────────────┘
```

---

## 🛠️ Let's Build It: A Crash-Proof Grade Calculator

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson08
cd Lesson08
code .
```

### Step 2: Write the code

```csharp
// Lesson 8: Grade calculator organized with methods

// ----- Main program -----
PrintHeader("GRADE CALCULATOR");

double prelim = ReadNumber("Prelim grade: ");
double midterm = ReadNumber("Midterm grade: ");
double finals = ReadNumber("Finals grade: ");

double average = ComputeAverage(prelim, midterm, finals);
string remarks = GetRemarks(average);

Console.WriteLine();
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Remarks: {remarks}");


// ----- Methods -----

void PrintHeader(string title)
{
    Console.WriteLine("==============================");
    Console.WriteLine($"  {title}");
    Console.WriteLine("==============================");
}

double ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";

        if (double.TryParse(input, out double number))
        {
            return number;
        }

        Console.WriteLine("Please type a valid number.");
    }
}

double ComputeAverage(double a, double b, double c)
{
    double total = a + b + c;
    return total / 3;
}

string GetRemarks(double grade)
{
    if (grade >= 75)
    {
        return "PASSED";
    }

    return "FAILED";
}
```

### Step 3: Run it

Try typing letters on purpose for one of the grades.

✅ **You should see:**

```text
==============================
  GRADE CALCULATOR
==============================
Prelim grade: 85
Midterm grade: abc
Please type a valid number.
Midterm grade: 90
Finals grade: 88

Average: 87.67
Remarks: PASSED
```

The program did **not** crash when you typed `abc`. It just asked again. 🎉

---

## 🔎 Line by Line

### The main program reads like a story

```cs
PrintHeader("GRADE CALCULATOR");

double prelim = ReadNumber("Prelim grade: ");
double midterm = ReadNumber("Midterm grade: ");
double finals = ReadNumber("Finals grade: ");

double average = ComputeAverage(prelim, midterm, finals);
string remarks = GetRemarks(average);
```

Even without reading the methods, you can tell what this program does. That is the power of good method names.

### `ReadNumber`: the most useful method in this course

```cs
double ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";

        if (double.TryParse(input, out double number))
        {
            return number;
        }

        Console.WriteLine("Please type a valid number.");
    }
}
```

1. It loops **forever** (`while (true)`).
2. Each round, it shows the question (`prompt`) and reads the answer.
3. If the answer is a number, `return number;` gives it back **and leaves the method**. This also ends the loop.
4. If not, it shows a message and the loop asks again.

Notice that `TryParse` is written **directly inside** the `if (...)`. It gives `true` or `false`, so it can be used as a condition. This is the same as:

```cs
bool isNumber = double.TryParse(input, out double number);
if (isNumber)
```

### `GetRemarks`: more than one `return`

```cs
string GetRemarks(double grade)
{
    if (grade >= 75)
    {
        return "PASSED";
    }

    return "FAILED";
}
```

If the grade is 75 or more, the method returns `"PASSED"` and **stops there**. It only reaches the last line if the grade is below 75.

---

## ⚠️ Common Mistakes

### Mistake 1: Forgetting to `return` a value

```cs
double ComputeAverage(double a, double b, double c)
{
    double total = a + b + c;
}
```

```text
error CS0161: 'ComputeAverage(double, double, double)': not all code paths return a value
```

**Fix:** A method that is not `void` **must** `return` a value.

### Mistake 2: Giving the wrong number of values

```cs
double average = ComputeAverage(85, 90);
```

```text
error CS7036: There is no argument given that corresponds to the required parameter 'c' of 'ComputeAverage(double, double, double)'
```

**Fix:** Give exactly as many values as the method has parameters, in the same order.

### Mistake 3: Using a variable from inside another method

Variables created **inside** a method only exist inside that method. `total` inside `ComputeAverage` cannot be used in the main program. If you need the value outside, `return` it.

---

## 🏋️ Practice

Add these methods to your `Lesson08` project (at the bottom), and call them from the main program to test.

**Exercise 1.** Write a method `bool IsEven(int number)` that returns `true` if the number is even.

<details>
<summary>Show answer</summary>

```csharp
Console.WriteLine(IsEven(4));   // True
Console.WriteLine(IsEven(7));   // False

bool IsEven(int number)
{
    return number % 2 == 0;
}
```

`number % 2 == 0` is already `true` or `false`, so we can return it directly.

</details>

**Exercise 2.** Write a method `void PrintLine(int length)` that prints a line of dashes (`-`) of the given length. `PrintLine(10)` should print `----------`.

<details>
<summary>Show answer</summary>

```csharp
PrintLine(10);
PrintLine(25);

void PrintLine(int length)
{
    for (int i = 1; i <= length; i++)
    {
        Console.Write("-");
    }

    Console.WriteLine();
}
```

</details>

**Exercise 3.** Write a method `int ReadWholeNumber(string prompt)` that works like `ReadNumber`, but for whole numbers (`int`). Use it to ask for the user's age.

<details>
<summary>Show answer</summary>

```csharp
int age = ReadWholeNumber("How old are you? ");
Console.WriteLine($"Next year you will be {age + 1}.");

int ReadWholeNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int number))
        {
            return number;
        }

        Console.WriteLine("Please type a whole number, like 18.");
    }
}
```

</details>

---

## 📌 Recap

- A **method** is a named block of code: `returnType Name(parameters) { body }`
- **Parameters** are the information you give it. **`return`** gives an answer back.
- `void` means the method gives nothing back.
- Method names use **PascalCase** (`ComputeAverage`). Variable names use **camelCase** (`totalScore`).
- Put methods at the **bottom** of `Program.cs`.
- `ReadNumber` with `while (true)` + `TryParse` + `return` = input that never crashes.

---

[⬅️ Lesson 07](07-lists.md) | [🏠 Course Home](../README.md) | [Next: Lesson 09 (Classes and Objects) ➡️](09-classes-and-objects.md)
