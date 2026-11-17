# Lesson 08: Nested If, Switch, and the Conditional Challenge

**Part 1: Beginner** · About 90 minutes

## What You Will Learn

- **Nested `if`**: an `if` inside another `if`
- **`switch`**: a clean way to pick one option from a list
- How to combine everything in the **Conditional Challenge**

## The Idea

Some decisions depend on an **earlier** decision.

> **If** you are 18 or older, the guard asks for your ID.
> &nbsp;&nbsp;&nbsp;&nbsp;**If** you have an ID, you may enter.
> &nbsp;&nbsp;&nbsp;&nbsp;**Otherwise**, you must come back with it.
> **Otherwise**, you are too young, and the guard does not ask for an ID at all.

The second question (about the ID) is only asked **after** the first answer is "yes". An `if` inside another `if` is called a **nested `if`**. "Nested" means *one inside the other*, like a small box inside a bigger box.

---

## Step by Step

### Step 1: Create a new project named `Lesson08`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson08`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Nested `if`

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code. Watch the spacing at the start of the lines: Visual Studio moves the inner `if` further to the right for you.

```csharp
Console.Write("How old are you? ");
int age = int.Parse(Console.ReadLine() ?? "");

if (age >= 18)
{
    Console.Write("Do you have a valid ID? (yes/no) ");
    string answer = (Console.ReadLine() ?? "").ToLower();

    if (answer == "yes")
    {
        Console.WriteLine("You may enter.");
    }
    else
    {
        Console.WriteLine("Please come back with your ID.");
    }
}
else
{
    Console.WriteLine("Sorry, you are too young to enter.");
}
```

**2.** Run it three times (**Ctrl + F5**):

- Type `20`, then `yes`
- Type `20`, then `no`
- Type `15`

✅ **The output:**

```text
How old are you? 20
Do you have a valid ID? (yes/no) yes
You may enter.
```

```text
How old are you? 20
Do you have a valid ID? (yes/no) no
Please come back with your ID.
```

```text
How old are you? 15
Sorry, you are too young to enter.
```

**What it means:**

Look at the third run. The program **never asked about the ID**. That question is inside the first `if`, so it only happens for adults.

```text
if (age >= 18)                       ← the OUTER if
{
    ...ask about the ID...

    if (answer == "yes")             ← the INNER if (nested)
    {
        ...
    }
    else                             ← belongs to the INNER if
    {
        ...
    }
}
else                                 ← belongs to the OUTER if
{
    ...
}
```

Each `else` belongs to the `if` that is **lined up** with it. The indentation shows you which is which.

### Nested `if`, or `&&`?

You could also write `if (age >= 18 && answer == "yes")`. So when do you nest?

- Use **`&&`** when you only need to know if **both** things are true.
- Use a **nested `if`** when you need to **do something in between** (like asking a second question), or when each case needs its **own message**.

---

## Part 2: `switch`

When you compare **one** variable with **several exact values**, a long chain of `else if` gets hard to read. `switch` is cleaner.

**1.** Clear the editor and type this code:

```csharp
Console.WriteLine("1 - Say hello");
Console.WriteLine("2 - Say goodbye");
Console.WriteLine("3 - Say thank you");
Console.Write("Choose an option: ");
string choice = Console.ReadLine() ?? "";

switch (choice)
{
    case "1":
        Console.WriteLine("Hello there!");
        break;

    case "2":
        Console.WriteLine("Goodbye!");
        break;

    case "3":
        Console.WriteLine("Thank you very much!");
        break;

    default:
        Console.WriteLine("Invalid option.");
        break;
}
```

**2.** Press **Ctrl + F5** and type `1`. Run it again and type `9`.

✅ **The output:**

```text
1 - Say hello
2 - Say goodbye
3 - Say thank you
Choose an option: 1
Hello there!
```

```text
1 - Say hello
2 - Say goodbye
3 - Say thank you
Choose an option: 9
Invalid option.
```

**What it means:**

- `switch (choice)` → look at what is in `choice`.
- `case "1":` → if it is `"1"`, run the lines below.
- `break;` → stop here, and leave the `switch`. **Every `case` must end with `break;`**
- `default:` → runs when no `case` matched. It works like `else`.

### Two cases, one action

Sometimes several values should do the same thing. Stack the `case` lines on top of each other:

```csharp
Console.Write("Type a day number (1-7): ");
string day = Console.ReadLine() ?? "";

switch (day)
{
    case "1":
    case "2":
    case "3":
    case "4":
    case "5":
        Console.WriteLine("It is a school day.");
        break;

    case "6":
    case "7":
        Console.WriteLine("It is the weekend!");
        break;

    default:
        Console.WriteLine("Please type a number from 1 to 7.");
        break;
}
```

Typing `3` shows `It is a school day.` Typing `7` shows `It is the weekend!`

### `if` or `switch`?

| Use... | When... | Example |
|---|---|---|
| `if` / `else if` | you check **ranges** or **combined** conditions | `grade >= 75`, `age >= 18 && hasId` |
| `switch` | you pick one of several **exact values** | menu choices `"1"`, `"2"`, `"3"` |

---

## Part 3: The Conditional Challenge 🎯

Time to combine **everything** from Lessons 06, 07, and 08: comparison and logical operators, `if`, `else if`, a nested `if`, and a `switch`.

**Try to write this program yourself before you look at the answer.**

### The task: a movie ticket booth

Write a program that works out how much a customer must pay.

**The rules:**

1. Ask for the customer's age.
   - If it is not a whole number, or it is below 0, show `That is not a valid age.` and stop.
   - Children **6 and below** watch for **free**. Show a message and stop.
2. For everyone else, find the ticket price:
   - **60 and above** (senior citizens): **120 pesos**
   - Everyone else: ask `Are you a student? (yes/no)`
     - Students pay **150 pesos**
     - Others pay **200 pesos**
3. Then ask for the seat, and add its price:
   - `1` - Regular seat: no extra charge
   - `2` - Premium seat: **+50 pesos**
   - `3` - VIP seat: **+100 pesos**
   - Anything else: show `Unknown seat. You get a Regular seat.`
4. Show the total to pay.

### What it should look like

```text
===== MOVIE TICKET BOOTH =====
How old are you? 25
Are you a student? (yes/no) no
Regular price: 200 pesos

1 - Regular seat (no extra charge)
2 - Premium seat (+50 pesos)
3 - VIP seat (+100 pesos)
Choose your seat: 2

Please pay: 250 pesos. Enjoy the movie!
```

```text
===== MOVIE TICKET BOOTH =====
How old are you? 65
Senior citizen price: 120 pesos

1 - Regular seat (no extra charge)
2 - Premium seat (+50 pesos)
3 - VIP seat (+100 pesos)
Choose your seat: 3

Please pay: 220 pesos. Enjoy the movie!
```

```text
===== MOVIE TICKET BOOTH =====
How old are you? 5
Children 6 and below watch for FREE. Enjoy the movie!
```

```text
===== MOVIE TICKET BOOTH =====
How old are you? abc
That is not a valid age.
```

### Hints

- Use `int.TryParse` for the age (Lesson 07).
- Start with an `if` / `else if` / `else` for the three big cases: **not valid**, **free**, and **everyone else**.
- Inside the last `else`, make a box for the price first: `int price;`. Then use a **nested `if`** to put the right number in it.
- Use a **`switch`** for the seat. To add to the price, use `price += 50;`

## The Code

<details>
<summary>Show the answer</summary>

```csharp
// Lesson 8: Conditional Challenge - the movie ticket booth

Console.WriteLine("===== MOVIE TICKET BOOTH =====");
Console.Write("How old are you? ");
bool isNumber = int.TryParse(Console.ReadLine() ?? "", out int age);

if (!isNumber || age < 0)
{
    Console.WriteLine("That is not a valid age.");
}
else if (age <= 6)
{
    Console.WriteLine("Children 6 and below watch for FREE. Enjoy the movie!");
}
else
{
    // 1. Find the ticket price (a nested if)
    int price;

    if (age >= 60)
    {
        price = 120;
        Console.WriteLine("Senior citizen price: 120 pesos");
    }
    else
    {
        Console.Write("Are you a student? (yes/no) ");
        string answer = (Console.ReadLine() ?? "").Trim().ToLower();

        if (answer == "yes")
        {
            price = 150;
            Console.WriteLine("Student price: 150 pesos");
        }
        else
        {
            price = 200;
            Console.WriteLine("Regular price: 200 pesos");
        }
    }

    // 2. Add the price of the seat (a switch)
    Console.WriteLine();
    Console.WriteLine("1 - Regular seat (no extra charge)");
    Console.WriteLine("2 - Premium seat (+50 pesos)");
    Console.WriteLine("3 - VIP seat (+100 pesos)");
    Console.Write("Choose your seat: ");
    string seat = Console.ReadLine() ?? "";

    switch (seat)
    {
        case "1":
            break;

        case "2":
            price += 50;
            break;

        case "3":
            price += 100;
            break;

        default:
            Console.WriteLine("Unknown seat. You get a Regular seat.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine($"Please pay: {price} pesos. Enjoy the movie!");
}
```

</details>

## The Output

Test your program with these customers. Each one should give the total shown.

| Age | Student? | Seat | Total to pay |
|---|---|---|---|
| 25 | no | 2 | 250 pesos |
| 65 | (not asked) | 3 | 220 pesos |
| 19 | yes | 1 | 150 pesos |
| 30 | no | 9 | 200 pesos (unknown seat → Regular) |
| 5 | (not asked) | (not asked) | Free |
| abc | | | "That is not a valid age." |

---

## What the Code Means

```cs
if (!isNumber || age < 0)
```

A **logical operator** joins two problems into one check: *"if it is not a number, **or** it is below 0."*

```cs
int price;
```

This makes a box for the price, with nothing in it yet. The nested `if` below fills it in. Every path puts a number in the box, so C# knows the box is never empty when it is used.

```cs
if (age >= 60)
{
    price = 120;
    ...
}
else
{
    ...ask about being a student...

    if (answer == "yes")
    {
        price = 150;
    }
    else
    {
        price = 200;
    }
}
```

This is the **nested `if`**. The question *"Are you a student?"* is asked only when the customer is **not** a senior.

```cs
case "1":
    break;
```

A `case` can be empty. Here, a Regular seat adds nothing to the price. The `break;` is still required.

```cs
case "2":
    price += 50;
    break;
```

`price += 50;` adds 50 to the price (Lesson 05).

---

## If Something Goes Wrong

**Error List: `CS0163  Control cannot fall through from one case label`**
A `case` is missing its `break;`. Add `break;` at the end of every `case`.

**Error List: `CS0165  Use of unassigned local variable 'price'`**
One of the paths does not put a number into `price`. Check that **every** branch of your `if` has a `price = ...;` line.

**Error List: `CS1513  } expected`**
With nested `if`s, it is easy to forget a `}`. Click just after any `{` or `}`: Visual Studio highlights its partner, so you can see which one is missing.

**The wrong `else` runs.**
Check the indentation. An `else` belongs to the `if` that is lined up with it. To let Visual Studio fix the spacing of the whole file for you, click **Edit** → **Advanced** → **Format Document**.

**Nothing happens when I type `1` in the switch.**
You may be comparing text with a number. `choice` is a `string`, so the cases must have quotes: `case "1":`, not `case 1:`.

---

## Exercise

**Exercise 1.** (Nested `if`) Ask for a username. If it is `admin`, **then** ask for the password. If the password is `1234`, show `Welcome!`. If not, show `Wrong password.` If the username is not `admin`, show `Unknown user.` and do **not** ask for a password.

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Username: ");
string username = Console.ReadLine() ?? "";

if (username == "admin")
{
    Console.Write("Password: ");
    string password = Console.ReadLine() ?? "";

    if (password == "1234")
    {
        Console.WriteLine("Welcome!");
    }
    else
    {
        Console.WriteLine("Wrong password.");
    }
}
else
{
    Console.WriteLine("Unknown user.");
}
```

</details>

**Exercise 2.** (`switch`) Ask the user to type a year level (`1`, `2`, `3`, or `4`). Show `Freshman`, `Sophomore`, `Junior`, or `Senior`. For anything else, show `Unknown year level.`

<details>
<summary>Show answer</summary>

```csharp
Console.Write("Year level (1-4): ");
string yearLevel = Console.ReadLine() ?? "";

switch (yearLevel)
{
    case "1":
        Console.WriteLine("Freshman");
        break;

    case "2":
        Console.WriteLine("Sophomore");
        break;

    case "3":
        Console.WriteLine("Junior");
        break;

    case "4":
        Console.WriteLine("Senior");
        break;

    default:
        Console.WriteLine("Unknown year level.");
        break;
}
```

</details>

## ⭐ Bonus Challenge

**A simple calculator.** Ask for a first number, an operator (`+`, `-`, `*`, or `/`), and a second number. Use a `switch` on the operator to do the math, and show the result.

One more rule: dividing by zero is not allowed. Inside the `/` case, use a **nested `if`** to show `You cannot divide by zero.` instead.

```text
First number: 12
Operator (+ - * /): *
Second number: 3
12 * 3 = 36
```

<details>
<summary>Show answer</summary>

```csharp
Console.Write("First number: ");
double first = double.Parse(Console.ReadLine() ?? "");

Console.Write("Operator (+ - * /): ");
string op = Console.ReadLine() ?? "";

Console.Write("Second number: ");
double second = double.Parse(Console.ReadLine() ?? "");

switch (op)
{
    case "+":
        Console.WriteLine($"{first} + {second} = {first + second}");
        break;

    case "-":
        Console.WriteLine($"{first} - {second} = {first - second}");
        break;

    case "*":
        Console.WriteLine($"{first} * {second} = {first * second}");
        break;

    case "/":
        if (second == 0)
        {
            Console.WriteLine("You cannot divide by zero.");
        }
        else
        {
            Console.WriteLine($"{first} / {second} = {first / second}");
        }
        break;

    default:
        Console.WriteLine("Unknown operator.");
        break;
}
```

An `if` can go inside a `case`. The `break;` comes after the whole `if` / `else`.

</details>

---

## Remember

- A **nested `if`** is an `if` inside another `if`. Use it when a second question only makes sense after the first answer.
- Each `else` belongs to the `if` that is lined up with it.
- `switch` picks from **exact values**. Every `case` ends with `break;`. `default` is like `else`.
- Stack `case` lines to give several values the same action.
- `if` for ranges and combined conditions. `switch` for exact choices.

---

[⬅️ Lesson 07](07-if-and-else.md) | [🏠 Course Home](../README.md) | [Next: Lesson 09 (Loops) ➡️](09-loops.md)
