# Lesson 13: Functions (Methods)

**Part 2: Intermediate** · About 90 minutes

## What You Will Learn

- **Basic functions**: how to make a named block of code you can use again and again
- **Functions with parameters**: how to **give** information to a function
- How to get an answer **back** from a function with `return`
- How to write a `ReadNumber` function that **never crashes**, whatever the user types

## The Idea

Think of a recipe card called **"How to Cook Rice"**. You write it once. Every time you need rice, you just say *"follow the rice card."* You do not write the steps again.

A **function** is a recipe card for code:

- You **write** the steps once, and give them a **name**.
- You **use** the function by its name, whenever you need it. Programmers say you **call** the function.

> [!NOTE]
> **Function or method?** In C#, a function is usually called a **method**. In this course, the two words mean the same thing. You will see "method" in Visual Studio and in most C# books.

You have already used functions that other people wrote: `Console.WriteLine(...)`, `int.Parse(...)`, `names.Add(...)`. Now you will write your own.

Why use functions?

1. **No repeating.** Write once, use many times.
2. **Easier to read.** `ComputeAverage(a, b, c)` explains itself.
3. **Easier to fix.** A mistake is fixed in **one** place.

---

## Step by Step

### Step 1: Create a new project named `Lesson13`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson13`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Basic Functions

A basic function is a named group of lines. It needs nothing, and gives nothing back. It just **does** something.

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
// ----- Main program -----
SayHello();
SayHello();
Console.WriteLine("...some other work...");
SayHello();


// ----- Functions -----

void SayHello()
{
    Console.WriteLine("Hello!");
    Console.WriteLine("Welcome to C#.");
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Hello!
Welcome to C#.
Hello!
Welcome to C#.
...some other work...
Hello!
Welcome to C#.
```

**What it means:**

**Making the function** (the recipe card):

```text
void SayHello()
{
    ...the steps...
}
```

- `void` → this function gives **nothing** back. ("Void" means "empty".)
- `SayHello` → the function's **name**. Start every word with a capital letter.
- `( )` → empty, because this function needs no information.
- `{ ... }` → the steps.

**Using the function** (following the recipe):

```cs
SayHello();
```

Write the name, then `( )`, then `;`. Each time, the two lines inside the function run. The function was written **once**, and used **three times**.

> [!IMPORTANT]
> **Where do functions go?** Put them at the **bottom** of `Program.cs`, after the main program. C# finds them there. Writing a function does **not** run it. It runs only when it is **called**.

---

## Part 2: Functions With Parameters

Most functions need some information to do their job. The information you give to a function is called a **parameter**.

**1.** Clear the editor and type this code:

```csharp
// ----- Main program -----
Greet("Juan");
Greet("Maria");
PrintSum(5, 3);
PrintSum(100, 250);


// ----- Functions -----

void Greet(string name)
{
    Console.WriteLine($"Hello, {name}!");
}

void PrintSum(int a, int b)
{
    int sum = a + b;
    Console.WriteLine($"{a} + {b} = {sum}");
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Hello, Juan!
Hello, Maria!
5 + 3 = 8
100 + 250 = 350
```

**What it means:**

```cs
void Greet(string name)
```

`(string name)` is a **parameter**: a box that gets filled each time the function is called. Like a variable, it has a type and a name.

```cs
Greet("Juan");
```

This calls the function, and puts `"Juan"` into the box `name`. The second call puts `"Maria"` in. **Same function, different information, different result.**

```cs
void PrintSum(int a, int b)
```

A function can have **several** parameters, with commas between them. `PrintSum(5, 3)` puts 5 into `a` and 3 into `b`. **The order matters.**

---

## Part 3: Functions That Give Back an Answer

Many functions **compute** something and **give the answer back**. You have used this already: `int.Parse("18")` gives back the number 18.

**1.** Clear the editor and type this code:

```csharp
// ----- Main program -----
int result = Add(5, 3);
Console.WriteLine(result);

Console.WriteLine(Add(10, 20));

double area = RectangleArea(4.5, 2);
Console.WriteLine($"The area is {area}.");


// ----- Functions -----

int Add(int a, int b)
{
    return a + b;
}

double RectangleArea(double length, double width)
{
    double result = length * width;
    return result;
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
8
30
The area is 9.
```

**What it means:**

```cs
int Add(int a, int b)
{
    return a + b;
}
```

- The first word is now `int`, not `void`. It says what kind of answer the function **gives back**: a whole number.
- `return a + b;` means: *"give back this value, and leave the function."*

```cs
int result = Add(5, 3);
```

The function gives back 8, and the 8 goes into the box `result`. You can use the answer of a function anywhere you could use a value.

| First word | Meaning |
|---|---|
| `void` | The function gives **nothing** back. It just does something |
| `int`, `double`, `string`, `bool`... | The function gives back a value of that type. It **must** have a `return` |

---

## Part 4: Put It All Together

Now a real program, organized with functions. It also fixes the problem from earlier lessons: typing letters will **not** crash it.

**1.** Clear the editor and type the code below. The **main program** is at the top. The **functions** are at the bottom.

## The Code

```csharp
// Lesson 13: Grade calculator organized with methods

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

**2.** Press **Ctrl + F5**. For one of the grades, type letters on purpose (like `abc`).

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

Read the main program first:

```cs
PrintHeader("GRADE CALCULATOR");

double prelim = ReadNumber("Prelim grade: ");
double midterm = ReadNumber("Midterm grade: ");
double finals = ReadNumber("Finals grade: ");

double average = ComputeAverage(prelim, midterm, finals);
string remarks = GetRemarks(average);
```

It reads almost like a story. That is what good function names do. (In the code, the comment says `Methods`. Remember: *method* and *function* mean the same thing.)

- `PrintHeader` → a function **with a parameter**, that gives nothing back (`void`).
- `ComputeAverage` → a function with **three** parameters, that gives back a `double`.

### `ReadNumber`: the most useful function in this course

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
3. If the answer is a number, `return number;` gives it back **and leaves the function**. That also ends the loop.
4. If not, it shows a message, and the loop asks again.

Here, `TryParse` is written directly inside `if ( ... )`. It gives `true` or `false`, so it can be used as a condition.

### A function with two `return` lines

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

If the grade is 75 or more, the function gives back `"PASSED"` and **stops right there**. It reaches the last line only when the grade is below 75.

---

## If Something Goes Wrong

**Nothing happens. My function does not run.**
You wrote the function, but you never **called** it. Add a line like `SayHello();` in the main program.

**Error List: `CS0161  'ComputeAverage(double, double, double)': not all code paths return a value`**
A function that is not `void` must always `return` something. You forgot the `return` line.

**Error List: `CS7036  There is no argument given that corresponds to the required parameter 'c'...`**
The function needs 3 values, and you gave it fewer, like `ComputeAverage(85, 90)`. Give exactly as many values as the function asks for, in the same order.

**Error List: `CS1503  Argument 1: cannot convert from 'string' to 'int'`**
You gave the wrong type, like `PrintSum("5", 3)`. The function wants a number, not text.

**Error List: `CS0103  The name 'total' does not exist in the current context`**
A variable made **inside** a function lives only inside that function. If the main program needs the value, the function must `return` it.

---

## Exercise

Add each function at the **bottom** of the file, and call it from the top to test it.

**Exercise 1.** (Basic function) Write a function `void PrintLine()` that shows a line of 20 dashes. Call it before and after showing your name.

```text
--------------------
Juan Dela Cruz
--------------------
```

<details>
<summary>Show answer</summary>

```csharp
PrintLine();
Console.WriteLine("Juan Dela Cruz");
PrintLine();

void PrintLine()
{
    Console.WriteLine("--------------------");
}
```

</details>

**Exercise 2.** (Function with parameters) Write a function `bool IsEven(int number)`. It gives back `true` if the number is even.

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

`number % 2 == 0` is already `true` or `false`, so the function can give it back directly.

</details>

**Exercise 3.** (Function with parameters) Write a function `int ReadWholeNumber(string prompt)`. It works like `ReadNumber`, but for whole numbers (`int`). Use it to ask for the user's age.

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

Write a function `string GetLetterGrade(double grade)` that gives back a letter:

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

No `else` is needed. Each `return` leaves the function, so the next `if` runs only when the one before it was false.

</details>

---

## Remember

- A **function** (also called a **method**) is a named block of code: `answerType Name(parameters) { steps }`
- **Basic function:** `void SayHello() { ... }`, called with `SayHello();`
- **Parameters** are the information you give it: `void Greet(string name)`
- **`return`** gives an answer back. `void` means the function gives nothing back.
- Function names: `ComputeAverage` (every word capitalized). Variable names: `totalScore`.
- Put functions at the **bottom** of `Program.cs`.
- `ReadNumber` = `while (true)` + `TryParse` + `return`. Input that never crashes.

---

[⬅️ Lesson 12](12-lists.md) | [🏠 Course Home](../README.md) | [Next: Lesson 14 (Classes and Objects) ➡️](14-classes-and-objects.md)
