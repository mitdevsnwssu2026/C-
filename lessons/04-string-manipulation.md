# Lesson 04: String Manipulation (Working With Text)

**Part 1: Beginner** · About 60 minutes

## What You Will Learn

- How to join pieces of text together
- How to count the characters in a text
- How to change text: capital letters, small letters, removing spaces, replacing words
- How to take out a piece of a text
- How to ask questions about a text (does it contain a word?)

## The Idea

In C#, text is called a **string**. Changing or checking text is called **string manipulation**. It is one of the most common jobs in programming.

Think of all the times a program works with text:

- A sign-up form removes the extra spaces you typed by accident.
- A school system shows your name in CAPITAL LETTERS on your ID.
- A website checks that your email has an `@` in it.

A string has built-in **tools** for these jobs. To use a tool, you type the variable's name, a **dot**, and the tool's name:

```cs
string name = "juan";
string bigName = name.ToUpper();   // bigName is now "JUAN"
```

Read the dot as *"use this tool on this text."*

> [!IMPORTANT]
> **A tool never changes the original text.** It gives you a **new** text. To keep the result, you must **put it in a variable**:
>
> ```cs
> name.ToUpper();           // ❌ the result is thrown away. name is still "juan"
> name = name.ToUpper();    // ✅ name is now "JUAN"
> ```

---

## Step by Step

### Step 1: Create a new project named `Lesson04`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson04`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Joining Text

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
string firstName = "Juan";
string lastName = "Dela Cruz";

// Way 1: join with +
string fullName = firstName + " " + lastName;
Console.WriteLine(fullName);

// Way 2: join with $"..."
string greeting = $"Hello, {firstName} {lastName}!";
Console.WriteLine(greeting);
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Juan Dela Cruz
Hello, Juan Dela Cruz!
```

**What it means:**

- With text, `+` **joins** the pieces. `firstName + " " + lastName` joins three pieces: `Juan`, a space, and `Dela Cruz`.
- Do not forget the space `" "`. Without it, you would get `JuanDela Cruz`.
- `$"..."` with `{ }` (from Lesson 02) does the same job, and is usually easier to read.

---

## Part 2: Counting and Changing Text

**1.** Clear the editor and type this code:

```csharp
string word = "  Mabuhay  ";

Console.WriteLine($"[{word}]");
Console.WriteLine($"[{word.Trim()}]");
Console.WriteLine(word.Trim().Length);
Console.WriteLine(word.Trim().ToUpper());
Console.WriteLine(word.Trim().ToLower());
Console.WriteLine(word.Trim().Replace("a", "o"));
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
[  Mabuhay  ]
[Mabuhay]
7
MABUHAY
mabuhay
Mobuhoy
```

**What it means:**

- `.Trim()` removes the spaces at the **start** and the **end**. The `[ ]` in the first two lines let you see the spaces disappear.
- `.Length` counts the characters. `Mabuhay` has 7. (Notice: `.Length` has **no** `( )` after it.)
- `.ToUpper()` changes every letter to a CAPITAL letter.
- `.ToLower()` changes every letter to a small letter.
- `.Replace("a", "o")` replaces **every** `a` with `o`.
- You can use tools **one after another**: `word.Trim().ToUpper()` first trims, then makes capital letters.

---

## Part 3: Checking and Cutting Text

**1.** Clear the editor and type this code:

```csharp
string email = "juan.delacruz@school.edu.ph";

// Asking questions: the answer is True or False
Console.WriteLine(email.Contains("@"));
Console.WriteLine(email.StartsWith("juan"));
Console.WriteLine(email.EndsWith(".com"));

// Finding a position
int atPosition = email.IndexOf("@");
Console.WriteLine(atPosition);

// Cutting out a piece
string userName = email.Substring(0, atPosition);
string school = email.Substring(atPosition + 1);
Console.WriteLine(userName);
Console.WriteLine(school);
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
True
True
False
13
juan.delacruz
school.edu.ph
```

**What it means:**

- `.Contains("@")` asks: *is there an `@` somewhere in this text?* The answer is `True` or `False`.
- `.StartsWith(...)` and `.EndsWith(...)` check the beginning and the end.
- `.IndexOf("@")` finds the **position** of `@`.

> [!IMPORTANT]
> **Positions start at 0, not 1.** The first character is at position 0, the second is at position 1, and so on.
>
> ```text
> j  u  a  n  .  d  e  l  a  c  r  u  z  @  s  c  h ...
> 0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16
> ```
>
> That is why `@` is at position **13**, even though it is the 14th character.

- `.Substring(0, atPosition)` cuts out a piece: *start at position 0, and take 13 characters.* That gives `juan.delacruz`.
- `.Substring(atPosition + 1)` with only **one** number means: *start at position 14, and take everything to the end.* That gives `school.edu.ph`.

---

## Part 4: Put It All Together

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 4: Working with text (string manipulation)

Console.Write("Type your full name: ");
string fullName = Console.ReadLine() ?? "";

// 1. Remove extra spaces at the start and at the end
string cleanName = fullName.Trim();

// 2. Count the characters
int length = cleanName.Length;

// 3. Change the letters
string upperName = cleanName.ToUpper();
string lowerName = cleanName.ToLower();

// 4. Ask questions about the text (the answer is true or false)
bool hasSpace = cleanName.Contains(" ");
bool startsWithJ = cleanName.StartsWith("J");

// 5. Take a piece of the text
string firstThree = cleanName.Substring(0, 3);

// 6. Replace part of the text
string dashedName = cleanName.Replace(" ", "-");

Console.WriteLine();
Console.WriteLine($"Clean name:      {cleanName}");
Console.WriteLine($"Characters:      {length}");
Console.WriteLine($"CAPITAL letters: {upperName}");
Console.WriteLine($"small letters:   {lowerName}");
Console.WriteLine($"Has a space?     {hasSpace}");
Console.WriteLine($"Starts with J?   {startsWithJ}");
Console.WriteLine($"First 3 letters: {firstThree}");
Console.WriteLine($"With dashes:     {dashedName}");
```

**2.** Press **Ctrl + F5**. When it asks for your name, press the **Space bar** a few times first, then type `Juan Dela Cruz`, then press **Space** a few more times, and press **Enter**.

## The Output

✅ **You will see:**

```text
Type your full name:    Juan Dela Cruz

Clean name:      Juan Dela Cruz
Characters:      14
CAPITAL letters: JUAN DELA CRUZ
small letters:   juan dela cruz
Has a space?     True
Starts with J?   True
First 3 letters: Jua
With dashes:     Juan-Dela-Cruz
```

Run it again with **your own name**, and see what changes.

---

## What the Code Means

```cs
string cleanName = fullName.Trim();
```

The user typed extra spaces. `.Trim()` gives back the name without them, and we keep the result in a new box, `cleanName`. Every line after this uses the clean name.

```cs
int length = cleanName.Length;
```

`.Length` gives a **number**, so it goes into an `int` box. Spaces **inside** the name are counted too: `Juan Dela Cruz` is 12 letters plus 2 spaces = 14.

```cs
bool hasSpace = cleanName.Contains(" ");
bool startsWithJ = cleanName.StartsWith("J");
```

These tools answer a question, so the result goes into a `bool` box (`true` or `false`). `StartsWith("J")` cares about capital letters: a name that starts with a small `j` gives `False`.

```cs
string firstThree = cleanName.Substring(0, 3);
```

Start at position 0, and take 3 characters.

```cs
string dashedName = cleanName.Replace(" ", "-");
```

Replace every space with a dash.

### The string tools

| Tool | What it does | Example | Result |
|---|---|---|---|
| `.Length` | Counts the characters | `"Juan".Length` | `4` |
| `.ToUpper()` | CAPITAL letters | `"Juan".ToUpper()` | `"JUAN"` |
| `.ToLower()` | small letters | `"Juan".ToLower()` | `"juan"` |
| `.Trim()` | Removes spaces at the start and end | `"  Juan  ".Trim()` | `"Juan"` |
| `.Replace(a, b)` | Replaces every `a` with `b` | `"Juan".Replace("u", "o")` | `"Joan"` |
| `.Contains(x)` | Is `x` inside the text? | `"Juan".Contains("ua")` | `True` |
| `.StartsWith(x)` | Does the text start with `x`? | `"Juan".StartsWith("Ju")` | `True` |
| `.EndsWith(x)` | Does the text end with `x`? | `"Juan".EndsWith("n")` | `True` |
| `.IndexOf(x)` | The position of `x` (starting from 0) | `"Juan".IndexOf("a")` | `2` |
| `.Substring(start, count)` | Cuts out `count` characters | `"Juan".Substring(0, 2)` | `"Ju"` |
| `.Substring(start)` | Cuts from `start` to the end | `"Juan".Substring(2)` | `"an"` |

> You do not need to memorize this table. In Visual Studio, type a string variable's name and then a **dot**. A list of all the tools pops up, and you can pick one.

---

## If Something Goes Wrong

**I used `.ToUpper()`, but my text did not change.**
A tool gives back a **new** text. It does not change the original. Keep the result: `name = name.ToUpper();`

**The black window shows a crash:**

```text
Unhandled exception. System.ArgumentOutOfRangeException: Index and length must refer to a location within the string. (Parameter 'length')
```

`Substring` tried to take more characters than the text has. For example, `"Jo".Substring(0, 3)` asks for 3 characters, but `Jo` has only 2. In the lesson's program, this happens if you type a name shorter than 3 characters.

**Error List: `CS1955  Non-invocable member 'string.Length' cannot be used like a method.`**
You wrote `.Length()`. Remove the `( )`: it is just `.Length`.

**Error List: `CS1061  'string' does not contain a definition for 'toUpper'`**
Capital letters matter. It is `ToUpper`, with a capital **T** and a capital **U**.

**`IndexOf` gave me `-1`.**
That means "not found". The text you searched for is not in the string.

---

## Exercise

Do these in your `Lesson04` project. Clear the editor each time.

**Exercise 1.** Ask the user for any word. Show how many characters it has, and show it in CAPITAL letters.

```text
Type a word: Philippines
Characters: 11
In capital letters: PHILIPPINES
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Type a word: ");
string word = (Console.ReadLine() ?? "").Trim();

Console.WriteLine($"Characters: {word.Length}");
Console.WriteLine($"In capital letters: {word.ToUpper()}");
```

</details>

**Exercise 2.** **Username maker.** Ask for a first name and a last name. Make a username: the first name, an underscore `_`, and the last name. Everything must be in small letters, with **no spaces**.

```text
First name: Juan
Last name: Dela Cruz
Your username: juan_delacruz
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("First name: ");
string firstName = (Console.ReadLine() ?? "").Trim();

Console.Write("Last name: ");
string lastName = (Console.ReadLine() ?? "").Trim();

string userName = firstName + "_" + lastName;
userName = userName.ToLower().Replace(" ", "");

Console.WriteLine($"Your username: {userName}");
```

`.Replace(" ", "")` replaces every space with **nothing**. That removes the spaces.

</details>

**Exercise 3.** Ask for an email address. Show `True` or `False`: does it contain an `@`?

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Email address: ");
string email = (Console.ReadLine() ?? "").Trim();

bool hasAt = email.Contains("@");
Console.WriteLine($"Has an @ sign: {hasAt}");
```

</details>

## ⭐ Bonus Challenge

**Initials.** Ask for a first name and a last name. Show the person's initials in capital letters, with dots.

```text
First name: maria
Last name: santos
Initials: M.S.
```

Hint: the first letter of a text is `.Substring(0, 1)`.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("First name: ");
string firstName = (Console.ReadLine() ?? "").Trim();

Console.Write("Last name: ");
string lastName = (Console.ReadLine() ?? "").Trim();

string firstInitial = firstName.Substring(0, 1).ToUpper();
string lastInitial = lastName.Substring(0, 1).ToUpper();

Console.WriteLine($"Initials: {firstInitial}.{lastInitial}.");
```

`firstName.Substring(0, 1)` takes 1 character, starting at position 0. Then `.ToUpper()` makes it a capital letter.

</details>

---

## Remember

- Text is called a **string**. Changing or checking it is **string manipulation**.
- Join text with `+`, or with `$"...{name}..."`.
- A tool gives back a **new** text. Keep the result: `name = name.Trim();`
- `.Length` counts. `.ToUpper()`, `.ToLower()`, `.Trim()`, and `.Replace()` change.
- `.Contains()`, `.StartsWith()`, and `.EndsWith()` answer `True` or `False`.
- `.IndexOf()` finds a position. `.Substring()` cuts out a piece.
- Positions start at **0**.

---

[⬅️ Lesson 03](03-user-input.md) | [🏠 Course Home](../README.md) | [Next: Lesson 05 (Arithmetic Operators) ➡️](05-arithmetic-operators.md)
