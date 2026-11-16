# Lesson 03: User Input (Talking With the User)

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 02](02-variables.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Ask the user a question and read their answer with `Console.ReadLine()`
- Understand that everything the user types is **text**
- Turn text into a number with `int.Parse` and `double.Parse`

---

## 🧠 The Big Idea

So far, your programs always show the same thing. Real programs **listen** to the user.

`Console.ReadLine()` makes the program **stop and wait** until the user types something and presses **Enter**. Then it gives back what they typed.

```text
Program: "What is your name?"
User types: Juan  (and presses Enter)
Program: "Hello, Juan!"
```

### The pattern you will use every time

```cs
Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";
```

| Piece | Meaning |
|---|---|
| `Console.Write("What is your name? ");` | Shows the question. We use `Write` (not `WriteLine`) so the user types on the **same line** as the question |
| `string name =` | Make a text box called `name`... |
| `Console.ReadLine()` | ...wait for the user to type something and press Enter... |
| `?? ""` | ...and if nothing comes back at all, use empty text `""` instead |

> [!IMPORTANT]
> **Always write `?? ""` after `Console.ReadLine()`** in this course.
> In rare cases, `ReadLine()` gives back **nothing at all** (C# calls this `null`). The `?? ""` part means *"if it is nothing, use empty text instead."* Without it, VS Code shows a yellow **warning**. With it, your code is safe and clean.

---

## 🧠 Everything the User Types Is Text

When the user types `18`, your program receives the **text** `"18"`, not the number `18`.

Why does this matter? Look at the difference:

```csharp
string a = "5";
string b = "5";
Console.WriteLine(a + b);   // Shows: 55   (text is joined together)

int x = 5;
int y = 5;
Console.WriteLine(x + y);   // Shows: 10   (numbers are added)
```

With text, `+` **joins** the two pieces. With numbers, `+` **adds** them.

### Turning text into a number

| To get a... | Use | Example |
|---|---|---|
| whole number (`int`) | `int.Parse(text)` | `int age = int.Parse("18");` |
| decimal number (`double`) | `double.Parse(text)` | `double grade = double.Parse("89.5");` |

"Parse" means **"read this text and turn it into a number."**

---

## 🛠️ Let's Build It

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson03
cd Lesson03
code .
```

### Step 2: Write the code

Replace everything in `Program.cs` with:

```csharp
// Lesson 3: Talking with the user

Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";

Console.Write("How old are you? ");
string ageText = Console.ReadLine() ?? "";
int age = int.Parse(ageText);

int ageInFiveYears = age + 5;

Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"In 5 years, you will be {ageInFiveYears} years old.");
```

### Step 3: Run it

```cmd
dotnet run
```

The program asks a question and **waits**. Type your answer and press **Enter**.

✅ **You should see** (the words after each `?` are what **you** type):

```text
What is your name? Juan
How old are you? 18
Hello, Juan!
In 5 years, you will be 23 years old.
```

---

## 🔎 Line by Line

```cs
Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";
```

Show the question, then wait for the answer and keep it in the box `name`.

```cs
Console.Write("How old are you? ");
string ageText = Console.ReadLine() ?? "";
int age = int.Parse(ageText);
```

The answer comes in as text (`"18"`), so we keep it in a `string` box called `ageText`. Then `int.Parse` turns it into a real number and we keep that in the `int` box `age`.

```cs
int ageInFiveYears = age + 5;
```

Now that `age` is a number, we can do math with it.

---

## ⚠️ Common Mistakes

### Mistake 1: The program "freezes"

You run the program and nothing seems to happen. **It is not frozen.** It is waiting for you to type something and press **Enter**.

### Mistake 2: Typing letters when a number is expected

If you type `eighteen` for your age, the program **crashes**:

```text
Unhandled exception. System.FormatException: The input string 'eighteen' was not in a correct format.
```

`int.Parse` can only read digits like `18`. For now, **type a valid number**.

> [!NOTE]
> Do not worry. In **Lesson 05** you will learn to check if the input is a number before using it, and in **Lesson 10** you will learn to stop crashes completely.

### Mistake 3: Using a text box in math

```cs
string ageText = Console.ReadLine() ?? "";
int ageInFiveYears = ageText + 5;
```

```text
error CS0029: Cannot implicitly convert type 'string' to 'int'
```

**Fix:** Turn the text into a number first with `int.Parse(ageText)`.

### Mistake 4: Forgetting `?? ""`

```cs
string name = Console.ReadLine();
```

This shows a yellow warning: `warning CS8600: Converting null literal or possible null value to non-nullable type.` The program still runs, but **fix it** by adding `?? ""`.

---

## 🏋️ Practice

Use your `Lesson03` project. Replace the code for each exercise.

**Exercise 1.** Ask the user for their favorite color and their favorite food. Then print: `You like red and pancit!` (using their answers).

<details>
<summary>Show answer</summary>

```csharp
Console.Write("What is your favorite color? ");
string color = Console.ReadLine() ?? "";

Console.Write("What is your favorite food? ");
string food = Console.ReadLine() ?? "";

Console.WriteLine($"You like {color} and {food}!");
```

</details>

**Exercise 2.** Ask for two whole numbers and print their sum.

```text
First number: 12
Second number: 30
12 + 30 = 42
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("First number: ");
int first = int.Parse(Console.ReadLine() ?? "");

Console.Write("Second number: ");
int second = int.Parse(Console.ReadLine() ?? "");

int sum = first + second;
Console.WriteLine($"{first} + {second} = {sum}");
```

Notice that `int.Parse(Console.ReadLine() ?? "")` reads **and** converts in one line. Both ways are correct.

</details>

**Exercise 3.** Ask for the year the user was born, then show how old they will be in the year **2030**.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("What year were you born? ");
int birthYear = int.Parse(Console.ReadLine() ?? "");

int ageIn2030 = 2030 - birthYear;
Console.WriteLine($"In 2030, you will be {ageIn2030} years old.");
```

</details>

---

## 📌 Recap

- `Console.ReadLine() ?? ""` waits for the user to type and press Enter, then gives back the text.
- Use `Console.Write` for the question so the answer appears on the same line.
- Everything the user types is **text** (`string`).
- `int.Parse(text)` and `double.Parse(text)` turn text into numbers.
- If the user types something that is not a number, `Parse` crashes. You will fix this in Lessons 05 and 10.

---

[⬅️ Lesson 02](02-variables.md) | [🏠 Course Home](../README.md) | [Next: Lesson 04 (Math) ➡️](04-math-and-operators.md)
