# Lesson 03: User Input (Talking With the User)

**Part 1: Beginner** · About 60 minutes

## What You Will Learn

- How to ask the user a question and read the answer
- Why everything the user types is **text**
- How to turn text into a number

## The Idea

So far, your programs always show the same thing. Real programs **listen**.

`Console.ReadLine()` makes the program **stop and wait**. The user types an answer in the black window and presses **Enter**. Then the program continues, and it knows what the user typed.

```text
Program asks:   What is your name?
User types:     Juan   (then presses Enter)
Program says:   Hello, Juan!
```

You will always use these two lines together:

```cs
Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";
```

- The first line shows the question. It uses `Write` (not `WriteLine`), so the user types the answer on the **same line** as the question.
- The second line waits for the answer and keeps it in the box `name`.

> [!IMPORTANT]
> **Always type `?? ""` after `Console.ReadLine()`.**
> It means: *"If nothing comes back at all, use empty text instead."* Without it, Visual Studio shows a warning (a green wavy line). Just make it a habit: `Console.ReadLine() ?? ""`

---

## Step by Step

### Step 1: Create a new project named `Lesson03`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson03`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Type the code

1. Click inside the editor. Press **Ctrl + A**, then **Delete**, to clear it.
2. Type the code below.

## The Code

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

### Step 3: Run it and answer the questions

1. Press **Ctrl + F5**.
2. The black window shows `What is your name?` and **waits**. Click on the black window, type your name, and press **Enter**.
3. It asks for your age. Type a number and press **Enter**.

## The Output

✅ **You will see** (here the user typed `Juan` and `18`):

```text
What is your name? Juan
How old are you? 18
Hello, Juan!
In 5 years, you will be 23 years old.
```

Press any key to close the black window.

> [!WARNING]
> **Always close the black window before you run the program again.** If an old black window is still waiting for an answer, Visual Studio cannot start the new one and shows an error that says a file "is being used by another process". If that happens, close all the black windows and run again.

---

## What the Code Means

**Everything the user types is text.** When the user types `18`, the program gets the **text** `"18"`, not the number `18`. That matters, because text and numbers behave differently:

```csharp
string a = "5";
string b = "5";
Console.WriteLine(a + b);   // shows 55  (text is joined together)

int x = 5;
int y = 5;
Console.WriteLine(x + y);   // shows 10  (numbers are added)
```

So before doing math, you must **turn the text into a number**:

```cs
string ageText = Console.ReadLine() ?? "";
int age = int.Parse(ageText);
```

- `int.Parse(text)` turns text into a **whole number**.
- `double.Parse(text)` turns text into a **decimal number**.

"Parse" simply means *"read this text and turn it into a number."*

Now `age` is a real number, so the math works:

```cs
int ageInFiveYears = age + 5;
```

---

## If Something Goes Wrong

**Nothing happens after I run the program.**
The program is not stuck. It is **waiting for you**. Click the black window, type an answer, and press **Enter**.

**I typed letters for my age, and the black window shows a long message:**

```text
Unhandled exception. System.FormatException: The input string 'eighteen' was not in a correct format.
```

This is called a **crash**. `int.Parse` can only read digits like `18`. Close the black window, run again, and type a number.

> Do not worry. In Lesson 05 you will learn to check the input first, and in Lesson 10 you will learn to stop crashes completely.

**Error List: `CS0029  Cannot implicitly convert type 'string' to 'int'`**
You tried to do math with text, like `int result = ageText + 5;`. Turn the text into a number first: `int.Parse(ageText)`.

**A green wavy line under `Console.ReadLine()`**
You forgot `?? ""`. Write `Console.ReadLine() ?? ""`.

**An error says a file "is being used by another process".**
An old black window is still open. Close every black window, then run again.

---

## Exercise

Do these in your `Lesson03` project. Clear the editor each time.

**Exercise 1.** Ask the user for their favorite color and favorite food. Then show: `You like red and pancit!` (using their answers).

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

**Exercise 2.** Ask for two whole numbers and show their sum, like this:

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

Here, `int.Parse(Console.ReadLine() ?? "")` reads the answer **and** turns it into a number in one line. Both ways are correct.

</details>

## ⭐ Bonus Challenge

Ask the user what **year** they were born. Then show how old they will be in the year **2030**.

```text
What year were you born? 2008
In 2030, you will be 22 years old.
```

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

## Remember

- `Console.ReadLine() ?? ""` waits for the user to type and press **Enter**.
- Use `Console.Write` for the question, so the answer is typed on the same line.
- Everything the user types is **text**.
- `int.Parse(text)` and `double.Parse(text)` turn text into numbers.
- Close the black window before you run the program again.

---

[⬅️ Lesson 02](02-variables.md) | [🏠 Course Home](../README.md) | [Next: Lesson 04 (Math) ➡️](04-math-and-operators.md)
