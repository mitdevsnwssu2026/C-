# Lesson 02: Variables (Storing Information)

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 01](01-hello-world.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Store information in **variables**
- Know the four basic **data types**: `string`, `int`, `double`, `bool`
- Put variables inside text using `$"..."`
- Change the value of a variable

---

## 🧠 The Big Idea

A **variable** is a **labeled box** that holds one piece of information.

```text
   fullName              age             isEnrolled
┌──────────────────┐   ┌──────┐   ┌──────┐
│ "Juan Dela Cruz" │   │  18  │   │ true │
└──────────────────┘   └──────┘   └──────┘
```

- The **label** on the box is the variable's **name** (`age`).
- The thing **inside** the box is its **value** (`18`).
- Each box only holds one **type** of thing. A box for numbers cannot hold text.

### How to create a variable

```cs
int age = 18;
```

| Piece | Meaning |
|---|---|
| `int` | The **type**: what kind of value the box holds (`int` = whole number) |
| `age` | The **name** of the box |
| `=` | "Put this value into the box" |
| `18` | The **value** |
| `;` | End of the instruction |

> [!IMPORTANT]
> In programming, `=` does **not** mean "is equal to". It means **"put the value on the right into the box on the left."**

### The four basic types

| Type | What it holds | Examples | Notes |
|---|---|---|---|
| `string` | Text | `"Juan"`, `"BSIT"`, `"09171234567"` | Always inside double quotes `" "` |
| `int` | Whole numbers | `18`, `0`, `-5`, `2026` | No decimal point |
| `double` | Numbers with a decimal point | `1.65`, `89.5`, `3.0` | Use for grades, height, weight, money |
| `bool` | Only `true` or `false` | `true`, `false` | Short for "Boolean". For yes/no facts |

> [!TIP]
> A phone number like `09171234567` should be a `string`, not an `int`. You never do math with phone numbers, and an `int` would remove the `0` at the start.

---

## 🛠️ Let's Build It: A Student Profile Card

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson02
cd Lesson02
code .
```

### Step 2: Write the code

In VS Code, open `Program.cs`, delete everything, and type:

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

```cmd
dotnet run
```

✅ **You should see:**

```text
===== STUDENT PROFILE =====
Name:     Juan Dela Cruz
Age:      18
Height:   1.65 meters
Enrolled: True
===========================
Next year, Juan Dela Cruz will be 19 years old.
```

---

## 🔎 Line by Line

### Creating the variables

```cs
string fullName = "Juan Dela Cruz";
int age = 18;
double height = 1.65;
bool isEnrolled = true;
```

Four boxes are created, each with a type, a name, and a value.

### Putting variables inside text: `$"..."`

```cs
Console.WriteLine($"Name:     {fullName}");
```

This is called **string interpolation**. That is a big name for a simple idea:

1. Put a **`$`** right before the opening quote.
2. Inside the text, write a variable name inside **curly braces `{ }`**.
3. C# replaces `{fullName}` with the value in the box: `Juan Dela Cruz`.

You can put as many `{ }` as you want in one line:

```cs
Console.WriteLine($"{fullName} is {age} years old.");
// Shows: Juan Dela Cruz is 18 years old.
```

### Changing a variable

```cs
age = 19;
```

The box `age` already exists, so you do **not** write `int` again. You just put a new value in. The old value (`18`) is replaced.

---

## 🔎 Naming Rules for Variables

| Rule | ✅ Good | ❌ Bad |
|---|---|---|
| Start with a letter | `name1` | `1name` |
| No spaces | `fullName` | `full name` |
| Use **camelCase**: the first word is lowercase, and each next word starts with a capital letter | `yearLevel`, `isEnrolled` | `yearlevel`, `YEARLEVEL` |
| Use names that explain the value | `studentAge` | `x`, `a1` |

> [!NOTE]
> Names are case-sensitive. `age` and `Age` would be two **different** boxes. Pick one way and use it every time.

---

## ⚠️ Common Mistakes

### Mistake 1: Forgetting the `$`

```cs
Console.WriteLine("Name: {fullName}");
```

This shows `Name: {fullName}`, the curly braces and all. **Fix:** add `$` before the quote: `$"Name: {fullName}"`.

### Mistake 2: Putting text into a number box

```cs
int age = "eighteen";
```

```text
error CS0029: Cannot implicitly convert type 'string' to 'int'
```

**Fix:** An `int` box only holds whole numbers: `int age = 18;`

### Mistake 3: Putting quotes around a number

```cs
string age = "18";
```

This works, but now `age` is **text**, not a number. You cannot do math with it. Use `int age = 18;` when you need a number.

### Mistake 4: Creating the same variable twice

```cs
int age = 18;
int age = 19;
```

```text
error CS0128: A local variable or function named 'age' is already defined in this scope
```

**Fix:** The second time, leave out the type: `age = 19;`

### Mistake 5: A typo in the variable name

```cs
Console.WriteLine(fulName);
```

```text
error CS0103: The name 'fulName' does not exist in the current context
```

**Fix:** Spell the name **exactly** the same way every time, including capital letters.

---

## 🏋️ Practice

**Exercise 1.** Create variables for: your favorite food (`string`), how many siblings you have (`int`), your general average from last year (`double`), and whether you are a working student (`bool`). Print them all using `$"..."`.

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

**Exercise 2.** Without running it, guess what this program shows. Then run it to check.

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

The box `city` first holds `"Calbayog"`. Then the line `city = "Catbalogan";` replaces it.

</details>

**Exercise 3.** This program has **three** mistakes. Find and fix them.

```cs
string course = BSIT;
int year = 1
Console.WriteLine("Course: {course}, Year: {year}");
```

<details>
<summary>Show answer</summary>

1. `BSIT` is text, so it needs quotes: `"BSIT"`
2. The second line is missing `;`
3. The text is missing `$` before the quote

```csharp
string course = "BSIT";
int year = 1;
Console.WriteLine($"Course: {course}, Year: {year}");
```

</details>

---

## 📌 Recap

- A **variable** is a labeled box: `type name = value;`
- `string` = text, `int` = whole number, `double` = decimal number, `bool` = true/false
- `=` means "put this value into the box"
- `$"Hello {name}"` puts a variable's value inside text
- To change a variable, write `name = newValue;` (no type the second time)
- Use clear **camelCase** names like `fullName` and `yearLevel`

---

[⬅️ Lesson 01](01-hello-world.md) | [🏠 Course Home](../README.md) | [Next: Lesson 03 (User Input) ➡️](03-user-input.md)
