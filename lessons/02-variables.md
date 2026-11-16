# Lesson 02: Variables (Storing Information)

**Part 1: Beginner** · About 60 minutes

## What You Will Learn

- How to store information in **variables**
- The four basic kinds of information: text, whole numbers, decimal numbers, and true/false
- How to put a variable inside a sentence
- How to change what a variable holds

## The Idea

A **variable** is a **box with a label** on it. The box holds one piece of information.

```text
    fullName               age          isEnrolled
┌──────────────────┐    ┌──────┐       ┌──────┐
│ "Juan Dela Cruz" │    │  18  │       │ true │
└──────────────────┘    └──────┘       └──────┘
```

- The **label** is the variable's **name** (like `age`).
- The thing inside is its **value** (like `18`).
- Each box holds only one **kind** of thing. A box for numbers cannot hold text.

You make a variable like this:

```cs
int age = 18;
```

Read it as: *"Make a box for whole numbers. Name it `age`. Put `18` inside."*

There are four kinds of boxes you need to know. The kind is called the **type**.

| Type | What it holds | Examples |
|---|---|---|
| `string` | Text. Always inside `" "` | `"Juan"`, `"BSIT"` |
| `int` | Whole numbers | `18`, `0`, `2026` |
| `double` | Numbers with a decimal point | `1.65`, `89.5` |
| `bool` | Only `true` or `false` | `true`, `false` |

> [!IMPORTANT]
> In C#, the `=` sign does **not** mean "is equal to". It means **"put the value on the right into the box on the left."**

---

## Step by Step

### Step 1: Create a new project named `Lesson02`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson02`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

> Need more help with these clicks? Look at [Lesson 01](01-hello-world.md), Steps 1 to 5.

### Step 2: Type the code

1. Click inside the editor. Press **Ctrl + A**, then **Delete**, to clear it.
2. Type the code below.

## The Code

```csharp
// Lesson 2: A student profile card using variables

string fullName = "Juan Dela Cruz";
int age = 18;
double height = 1.65;
bool isEnrolled = true;

Console.WriteLine("===== STUDENT PROFILE =====");
Console.WriteLine($"Name:     {fullName}");
Console.WriteLine($"Age:      {age}");
Console.WriteLine($"Height:   {height} meters");
Console.WriteLine($"Enrolled: {isEnrolled}");
Console.WriteLine("===========================");

// A variable's value can change
age = 19;
Console.WriteLine($"Next year, {fullName} will be {age} years old.");
```

### Step 3: Run it

Press **Ctrl + F5** (or click **Debug** → **Start Without Debugging**).

## The Output

✅ **You will see:**

```text
===== STUDENT PROFILE =====
Name:     Juan Dela Cruz
Age:      18
Height:   1.65 meters
Enrolled: True
===========================
Next year, Juan Dela Cruz will be 19 years old.
```

Press any key to close the black window.

---

## What the Code Means

**Making four boxes:**

```cs
string fullName = "Juan Dela Cruz";
int age = 18;
double height = 1.65;
bool isEnrolled = true;
```

Each line makes one box: the type first, then the name, then `=`, then the value, then `;`.

**Putting a variable inside a sentence:**

```cs
Console.WriteLine($"Name:     {fullName}");
```

This has two special parts:

1. A **`$`** right before the opening quote.
2. The variable's name inside **curly braces** `{ }`.

C# replaces `{fullName}` with what is in the box. So the screen shows `Name:     Juan Dela Cruz`.

You can use many `{ }` in one line:

```cs
Console.WriteLine($"{fullName} is {age} years old.");
```

This shows: `Juan Dela Cruz is 18 years old.`

**Changing what is in a box:**

```cs
age = 19;
```

The box `age` already exists, so you do **not** write `int` again. The old value (`18`) is thrown away, and `19` goes in.

### Rules for naming variables

- Start with a letter. ✅ `name1` ❌ `1name`
- No spaces. ✅ `fullName` ❌ `full name`
- Start with a small letter. If the name has more words, start each **next** word with a capital letter: `yearLevel`, `isEnrolled`. This style is called **camelCase**.
- Choose names that explain the value. ✅ `studentAge` ❌ `x`
- Capital letters matter: `age` and `Age` are two different boxes.

---

## If Something Goes Wrong

**The screen shows `{fullName}` instead of the name.**
You forgot the `$`. Write `$"Name: {fullName}"`, with the `$` right before the first quote.

**Error List: `CS0029  Cannot implicitly convert type 'string' to 'int'`**
You put text into a number box, like `int age = "eighteen";`. An `int` box only holds whole numbers: `int age = 18;`

**Error List: `CS0128  A local variable or function named 'age' is already defined in this scope`**
You made the same box twice (`int age = 18;` and later `int age = 19;`). The second time, leave out the type: `age = 19;`

**Error List: `CS0103  The name 'fulName' does not exist in the current context`**
A typing mistake in the variable's name. Spell it exactly the same way every time, with the same capital letters.

**My number has quotes around it: `string age = "18";`**
This runs, but `age` is now **text**, not a number. You cannot do math with it. Use `int age = 18;` for numbers.

---

## Exercise

Do these in your `Lesson02` project. Clear the editor each time (**Ctrl + A**, **Delete**).

**Exercise 1.** Make four variables: your favorite food (`string`), how many siblings you have (`int`), your general average last year (`double`), and whether you are a working student (`bool`). Show all four using `$"..."`.

<details>
<summary>Show answer</summary>

```csharp
string favoriteFood = "Adobo";
int siblings = 3;
double generalAverage = 89.5;
bool isWorkingStudent = false;

Console.WriteLine($"Favorite food: {favoriteFood}");
Console.WriteLine($"Siblings: {siblings}");
Console.WriteLine($"General average: {generalAverage}");
Console.WriteLine($"Working student: {isWorkingStudent}");
```

</details>

**Exercise 2.** Do **not** run this yet. First, guess what it will show. Then type it, run it, and check.

```csharp
string city = "Calbayog";
Console.WriteLine($"I live in {city}.");
city = "Catbalogan";
Console.WriteLine($"Now I live in {city}.");
```

<details>
<summary>Show answer</summary>

```text
I live in Calbayog.
Now I live in Catbalogan.
```

The box `city` first holds `"Calbayog"`. Then `city = "Catbalogan";` replaces it.

</details>

**Exercise 3.** This code has **three** mistakes. Find them and fix them.

```cs
string course = BSIT;
int year = 1
Console.WriteLine("Course: {course}, Year: {year}");
```

<details>
<summary>Show answer</summary>

1. `BSIT` is text, so it needs quotes: `"BSIT"`
2. The second line has no `;` at the end
3. The text has no `$` before the quote

```csharp
string course = "BSIT";
int year = 1;
Console.WriteLine($"Course: {course}, Year: {year}");
```

</details>

## ⭐ Bonus Challenge

**The cup swap.** The left cup has coffee, and the right cup has milk. Write code that **swaps** them, so the left cup has milk and the right cup has coffee. Start with this:

```cs
string leftCup = "coffee";
string rightCup = "milk";
```

Rule: you may **not** just type `leftCup = "milk";`. Your code must still work if someone changes the drinks in the first two lines.

Hint: how would you swap two real cups? You would need a **third, empty cup**.

<details>
<summary>Show answer</summary>

```csharp
string leftCup = "coffee";
string rightCup = "milk";

string spareCup = leftCup;   // 1. pour the coffee into the spare cup
leftCup = rightCup;          // 2. pour the milk into the left cup
rightCup = spareCup;         // 3. pour the coffee into the right cup

Console.WriteLine($"Left cup: {leftCup}");
Console.WriteLine($"Right cup: {rightCup}");
```

It shows:

```text
Left cup: milk
Right cup: coffee
```

Without the spare cup, step 2 would throw away the coffee forever. Programmers use this "third box" trick all the time.

</details>

---

## Remember

- A **variable** is a box with a label: `type name = value;`
- `string` = text, `int` = whole number, `double` = decimal number, `bool` = true or false
- `=` means "put this value into the box"
- `$"Hello {name}"` puts a variable inside a sentence
- To change a variable: `name = newValue;` (no type the second time)

---

[⬅️ Lesson 01](01-hello-world.md) | [🏠 Course Home](../README.md) | [Next: Lesson 03 (User Input) ➡️](03-user-input.md)
