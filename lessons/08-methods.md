# Lesson 08: Methods (Reusable Blocks of Code)

**Part 2: Intermediate** · About 75 minutes

## What You Will Learn

- How to make your own **methods**: named blocks of code you can use again and again
- How to **give** information to a method
- How to get an answer **back** from a method
- How to write a `ReadNumber` method that **never crashes**, whatever the user types

## The Idea

Think of a recipe card called **"How to Cook Rice"**. You write it once. Every time you need rice, you just say *"follow the rice card."* You do not write the steps again.

A **method** is a recipe card for code:

- You **write** the steps once and give them a **name**.
- You **use** the method by its name whenever you need it. Programmers say you **call** the method.

You have already used methods that other people wrote: `Console.WriteLine(...)`, `int.Parse(...)`, `names.Add(...)`. Now you will write your own.

Why use methods?

1. **No repeating.** Write once, use many times.
2. **Easier to read.** `ComputeAverage(a, b, c)` explains itself.
3. **Easier to fix.** A mistake is fixed in **one** place.

---

## Step by Step

### Step 1: Create a new project named `Lesson08`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson08`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Type the code

1. Clear the editor (**Ctrl + A**, **Delete**).
2. Type the code below. The **main program** is at the top. The **methods** are at the bottom.

## The Code

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

Press **Ctrl + F5**. For one of the grades, type letters on purpose (like `abc`).

## The Output

✅ **You will see:**

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

## What the Code Means

### The parts of a method

```cs
double ComputeAverage(double a, double b, double c)
{
    double total = a + b + c;
    return total / 3;
}
```

- `double` (the first word) → the kind of answer the method **gives back**.
- `ComputeAverage` → the method's **name**. Method names start each word with a capital letter.
- `(double a, double b, double c)` → the information you **give** the method. These are called **parameters**.
- `{ ... }` → the steps.
- `return total / 3;` → **give back** the answer and leave the method.

To use it:

```cs
double average = ComputeAverage(prelim, midterm, finals);
```

The three grades go into `a`, `b`, and `c`. The method gives back the average, and it goes into the box `average`.

### A method that gives nothing back: `void`

```cs
void PrintHeader(string title)
{
    Console.WriteLine("==============================");
    Console.WriteLine($"  {title}");
    Console.WriteLine("==============================");
}
```

Some methods just **do** something, like showing text. They do not give back an answer. Their first word is **`void`**, which means "nothing".

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

1. It repeats forever (`while (true)`).
2. Each round, it shows the question and reads the answer.
3. If the answer is a number, `return number;` gives it back **and leaves the method**. That also ends the loop.
4. If not, it shows a message, and the loop asks again.

Here, `TryParse` is written directly inside `if ( ... )`. It gives `true` or `false`, so it can be used as a condition.

### A method with two `return` lines

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

If the grade is 75 or more, the method gives back `"PASSED"` and **stops right there**. It reaches the last line only when the grade is below 75.

### Where do methods go?

Put your methods at the **bottom** of `Program.cs`, after the main program. C# finds them there.

Now read the main program again:

```cs
PrintHeader("GRADE CALCULATOR");

double prelim = ReadNumber("Prelim grade: ");
double midterm = ReadNumber("Midterm grade: ");
double finals = ReadNumber("Finals grade: ");

double average = ComputeAverage(prelim, midterm, finals);
string remarks = GetRemarks(average);
```

It reads almost like a story. That is what good method names do.

---

## If Something Goes Wrong

**Error List: `CS0161  'ComputeAverage(double, double, double)': not all code paths return a value`**
A method that is not `void` must always `return` something. You forgot the `return` line.

**Error List: `CS7036  There is no argument given that corresponds to the required parameter 'c'...`**
The method needs 3 values, and you gave it fewer, like `ComputeAverage(85, 90)`. Give exactly as many values as the method asks for, in the same order.

**Error List: `CS0103  The name 'total' does not exist in the current context`**
A variable made **inside** a method lives only inside that method. If the main program needs the value, the method must `return` it.

---

## Exercise

Add each method at the **bottom** of the file, and call it from the top to test it.

**Exercise 1.** Write a method `bool IsEven(int number)`. It gives back `true` if the number is even.

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

`number % 2 == 0` is already `true` or `false`, so the method can give it back directly.

</details>

**Exercise 2.** Write a method `void PrintLine(int length)`. It shows a line of dashes. `PrintLine(10)` shows `----------`.

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

**Exercise 3.** Write a method `int ReadWholeNumber(string prompt)`. It works like `ReadNumber`, but for whole numbers (`int`). Use it to ask for the user's age.

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

## ⭐ Bonus Challenge

Write a method `string GetLetterGrade(double grade)` that gives back a letter:

- 90 or more → `"A"`
- 80 or more → `"B"`
- 75 or more → `"C"`
- below 75 → `"F"`

Then use `ReadNumber` (from this lesson) to ask for a grade, and show its letter.

```text
Enter a grade: 82
Letter grade: B
```

<details>
<summary>Show answer</summary>

```csharp
double grade = ReadNumber("Enter a grade: ");
Console.WriteLine($"Letter grade: {GetLetterGrade(grade)}");

string GetLetterGrade(double grade)
{
    if (grade >= 90)
    {
        return "A";
    }

    if (grade >= 80)
    {
        return "B";
    }

    if (grade >= 75)
    {
        return "C";
    }

    return "F";
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
```

No `else` is needed. Each `return` leaves the method, so the next `if` runs only when the one before it was false.

</details>

---

## Remember

- A **method** is a named block of code: `answerType Name(parameters) { steps }`
- **Parameters** are what you give it. **`return`** gives the answer back.
- `void` means the method gives nothing back.
- Method names: `ComputeAverage` (every word capitalized). Variable names: `totalScore`.
- Put methods at the **bottom** of `Program.cs`.
- `ReadNumber` = `while (true)` + `TryParse` + `return`. Input that never crashes.

---

[⬅️ Lesson 07](07-lists.md) | [🏠 Course Home](../README.md) | [Next: Lesson 09 (Classes and Objects) ➡️](09-classes-and-objects.md)
