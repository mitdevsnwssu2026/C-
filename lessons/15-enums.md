# Lesson 15: Enums (A List of Named Choices)

**Part 2: Intermediate** · About 60 minutes

## What You Will Learn

- What an **enum** is, and when to use one
- How to make an enum and use it in a variable
- How to use enums with `if` and `switch`
- The numbers hidden behind an enum

## The Idea

Some things have only a **few fixed choices**:

- A traffic light is **Red**, **Yellow**, or **Green**. Nothing else.
- A cup size is **Small**, **Medium**, or **Large**.
- An order is **Pending**, **Preparing**, **Ready**, or **Delivered**.

How would you store a cup size in a program?

**With text?** `string size = "Large";` works, until someone types `"large"`, `"LARGE"`, or `"Larg"` by mistake. The program does not complain. It just quietly does the wrong thing.

**With numbers?** `int size = 3;` works too. But what does 3 mean? Nobody remembers.

An **enum** solves both problems. An enum is **your own type with a fixed list of named choices**:

```cs
enum CupSize
{
    Small,
    Medium,
    Large
}
```

Now a variable of type `CupSize` can **only** hold `CupSize.Small`, `CupSize.Medium`, or `CupSize.Large`. A typing mistake becomes an error that Visual Studio catches **before** the program runs.

> "Enum" is short for **enumeration**, which means *a list of things, one by one*. Say it like "ee-num".

---

## Step by Step

### Step 1: Create a new project named `Lesson15`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson15`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Making and Using an Enum

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
OrderStatus status = OrderStatus.Preparing;

Console.WriteLine(status);
Console.WriteLine($"Your order is: {status}");

status = OrderStatus.Ready;
Console.WriteLine($"Your order is now: {status}");


// ----- Enums (always at the very bottom of the file) -----

enum OrderStatus
{
    Pending,
    Preparing,
    Ready,
    Delivered
}
```

> [!TIP]
> Watch what happens when you type `OrderStatus.` (with the dot). Visual Studio shows a list of the four choices. You can pick one with the arrow keys and press **Enter**. You cannot pick a choice that does not exist!

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Preparing
Your order is: Preparing
Your order is now: Ready
```

**What it means:**

**Making the enum:**

```text
enum OrderStatus          ← the word enum, then a name (start each word with a capital letter)
{
    Pending,              ← the choices, with commas between them
    Preparing,
    Ready,
    Delivered
}
```

**Using the enum:**

```cs
OrderStatus status = OrderStatus.Preparing;
```

- The **type** of the variable is `OrderStatus`: your own type.
- The **value** is written as the enum's name, a dot, and one of the choices: `OrderStatus.Preparing`.
- When you show it, C# shows the choice's name: `Preparing`.

> [!IMPORTANT]
> **Where does an enum go?** In `Program.cs`, put enums at the **very bottom** of the file, below all your other code (and below your functions). C# does not allow normal code after an enum.

---

## Part 2: Enums With `if` and `switch`

Enums and `switch` are best friends: a `switch` picks from exact values, and an enum **is** a list of exact values.

**1.** Clear the editor and type this code:

```csharp
TrafficLight light = TrafficLight.Red;

switch (light)
{
    case TrafficLight.Red:
        Console.WriteLine("STOP!");
        break;

    case TrafficLight.Yellow:
        Console.WriteLine("Get ready...");
        break;

    case TrafficLight.Green:
        Console.WriteLine("GO!");
        break;
}

if (light == TrafficLight.Red)
{
    Console.WriteLine("Do not cross the street.");
}


// ----- Enums (always at the very bottom of the file) -----

enum TrafficLight
{
    Red,
    Yellow,
    Green
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
STOP!
Do not cross the street.
```

**3.** Change the first line to `TrafficLight light = TrafficLight.Green;` and run again.

```text
GO!
```

**What it means:**

- `case TrafficLight.Red:` → each case is one of the enum's choices. No quotes, because it is not text.
- `light == TrafficLight.Red` → compare enums with `==`, like numbers.

---

## Part 3: The Numbers Behind an Enum

Secretly, each choice in an enum is a **number**. C# numbers them for you, **starting at 0**:

```text
enum OrderStatus { Pending, Preparing, Ready, Delivered }
                      0         1        2        3
```

You can also choose the numbers yourself.

**1.** Clear the editor and type this code:

```csharp
YearLevel level = YearLevel.Junior;

Console.WriteLine(level);
Console.WriteLine((int)level);

YearLevel fromNumber = (YearLevel)1;
Console.WriteLine(fromNumber);


// ----- Enums (always at the very bottom of the file) -----

enum YearLevel
{
    Freshman = 1,
    Sophomore = 2,
    Junior = 3,
    Senior = 4
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
Junior
3
Freshman
```

**What it means:**

- `Freshman = 1` → you choose the number yourself. Here, year levels start at 1, which makes more sense than 0.
- `(int)level` → *"show me the **number** behind this choice."* `Junior` is 3.
- `(YearLevel)1` → the other direction: *"turn the number 1 into a `YearLevel`."* Number 1 is `Freshman`.

> This is useful later with databases: a database can store the small number `3`, and your program shows the friendly name `Junior`.

---

## Part 4: Put It All Together

A milk tea shop! The customer picks a size from a menu. The program turns that choice into an enum, and uses the enum to find the price.

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 15: Enums - a milk tea order

Console.WriteLine("===== MILK TEA SHOP =====");
Console.WriteLine("1 - Small");
Console.WriteLine("2 - Medium");
Console.WriteLine("3 - Large");
Console.Write("Choose a size: ");
string choice = Console.ReadLine() ?? "";

CupSize size;

switch (choice)
{
    case "1":
        size = CupSize.Small;
        break;

    case "2":
        size = CupSize.Medium;
        break;

    case "3":
        size = CupSize.Large;
        break;

    default:
        Console.WriteLine("Invalid choice. We will give you a Medium.");
        size = CupSize.Medium;
        break;
}

int price = GetPrice(size);

Console.WriteLine();
Console.WriteLine($"Size:  {size}");
Console.WriteLine($"Price: {price} pesos");

if (size == CupSize.Large)
{
    Console.WriteLine("Good choice! Large comes with free pearls today.");
}


// ----- Methods -----

int GetPrice(CupSize cupSize)
{
    if (cupSize == CupSize.Small)
    {
        return 39;
    }

    if (cupSize == CupSize.Medium)
    {
        return 49;
    }

    return 59;
}


// ----- Enums (always at the very bottom of the file) -----

enum CupSize
{
    Small,
    Medium,
    Large
}
```

**2.** Press **Ctrl + F5** and type `3`.

## The Output

✅ **You will see:**

```text
===== MILK TEA SHOP =====
1 - Small
2 - Medium
3 - Large
Choose a size: 3

Size:  Large
Price: 59 pesos
Good choice! Large comes with free pearls today.
```

**3.** Run it again and type `1`. Then run it once more, and type something wrong, like `x`.

```text
Choose a size: 1

Size:  Small
Price: 39 pesos
```

```text
Choose a size: x
Invalid choice. We will give you a Medium.

Size:  Medium
Price: 49 pesos
```

---

## What the Code Means

```cs
CupSize size;
```

Make a box of type `CupSize`, with nothing in it yet.

```cs
switch (choice)
{
    case "1":
        size = CupSize.Small;
        break;
    ...
    default:
        Console.WriteLine("Invalid choice. We will give you a Medium.");
        size = CupSize.Medium;
        break;
}
```

The user types **text** (`"1"`, `"2"`, `"3"`). This `switch` turns that text into an **enum** value. Every path, even `default`, puts a value in the box.

From here on, the program never looks at the text again. It only uses `size`, which can only be one of three safe values.

```cs
int price = GetPrice(size);
```

```cs
int GetPrice(CupSize cupSize)
{
    if (cupSize == CupSize.Small)
    {
        return 39;
    }

    if (cupSize == CupSize.Medium)
    {
        return 49;
    }

    return 59;
}
```

An enum can be a **parameter** of a function, like any other type. The function can only ever receive a real cup size. It never has to worry about `"larg"` or `7`.

**Look at the order of the file:**

```text
1. The main program
2. The functions
3. The enums        ← always last
```

### Why use an enum?

| Without an enum | With an enum |
|---|---|
| `string size = "Larg";` — a typing mistake that nobody notices | `CupSize.Larg` — Visual Studio shows an error right away |
| `int size = 3;` — what does 3 mean? | `CupSize.Large` — the code explains itself |
| Any text or number can sneak in | Only the choices on the list are possible |

---

## If Something Goes Wrong

**Error List: `CS8803  Top-level statements must precede namespace and type declarations.`**
There is normal code **below** an enum. Move the enum to the **very bottom** of the file.

**Error List: `CS0117  'CupSize' does not contain a definition for 'Larg'`**
That choice is not in the enum. Check the spelling: `CupSize.Large`. (This is the enum protecting you from a typing mistake!)

**Error List: `CS0266  Cannot implicitly convert type 'int' to 'CupSize'. An explicit conversion exists (are you missing a cast?)`**
You put a plain number into an enum box, like `CupSize size = 1;`. Use a choice: `CupSize size = CupSize.Medium;`

**Error List: `CS0029  Cannot implicitly convert type 'string' to 'CupSize'`**
You put text into an enum box, like `CupSize size = "Large";`. No quotes: `CupSize size = CupSize.Large;`

**Error List: `CS0165  Use of unassigned local variable 'size'`**
One path of your `switch` does not give `size` a value. Check that **every** `case`, and `default`, has a `size = ...;` line.

---

## Exercise

**Exercise 1.** Make an enum named `Day` with the seven days of the week. Number them so that `Monday` is 1. Make a variable for today, and show the day's name and its number.

```text
Today is Wednesday.
It is day number 3 of the week.
```

Hint: if you write `Monday = 1`, C# keeps counting for the rest: Tuesday is 2, Wednesday is 3...

<details>
<summary>Show answer</summary>

```csharp
Day today = Day.Wednesday;

Console.WriteLine($"Today is {today}.");
Console.WriteLine($"It is day number {(int)today} of the week.");


enum Day
{
    Monday = 1,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
```

</details>

**Exercise 2.** Make an enum `Difficulty` with `Easy`, `Normal`, and `Hard`. Show a menu (1, 2, 3) and ask the user to choose. Then use a `switch` **on the enum** to set the number of lives: Easy = 5, Normal = 3, Hard = 1. If the user types something else, use `Normal`.

```text
1 - Easy
2 - Normal
3 - Hard
Choose a difficulty: 3
Difficulty: Hard
Lives: 1
```

<details>
<summary>Show answer</summary>

```csharp
Console.WriteLine("1 - Easy");
Console.WriteLine("2 - Normal");
Console.WriteLine("3 - Hard");
Console.Write("Choose a difficulty: ");
string choice = Console.ReadLine() ?? "";

Difficulty difficulty = Difficulty.Normal;

if (choice == "1")
{
    difficulty = Difficulty.Easy;
}
else if (choice == "3")
{
    difficulty = Difficulty.Hard;
}

int lives = 0;

switch (difficulty)
{
    case Difficulty.Easy:
        lives = 5;
        break;

    case Difficulty.Normal:
        lives = 3;
        break;

    case Difficulty.Hard:
        lives = 1;
        break;
}

Console.WriteLine($"Difficulty: {difficulty}");
Console.WriteLine($"Lives: {lives}");


enum Difficulty
{
    Easy,
    Normal,
    Hard
}
```

The variable starts as `Difficulty.Normal`. It changes only when the user types `1` or `3`.

</details>

## ⭐ Bonus Challenge

**Enums and classes together.** Start a **new project** named `Lesson15Bonus`. It will have **three files**:

1. `StudentStatus.cs` → an enum with `Enrolled`, `OnLeave`, and `Graduated`
2. `Student.cs` → a class with a `FullName` property and a `Status` property (of type `StudentStatus`)
3. `Program.cs` → makes a list of 4 students, and shows only the ones who are **Enrolled**

```text
Enrolled students:
- Juan Dela Cruz
- Ana Reyes
```

Hints:

- An enum can have its **own file**, just like a class. Add it the same way: right-click the project → **Add** → **Class...** → name it `StudentStatus.cs`. Then replace the starter code with your enum.
- In its own file, write `public enum` (so that other files can use it).

<details>
<summary>Show answer</summary>

**File: `StudentStatus.cs`**

```csharp
public enum StudentStatus
{
    Enrolled,
    OnLeave,
    Graduated
}
```

**File: `Student.cs`**

```cs
public class Student
{
    public string FullName { get; set; } = "";
    public StudentStatus Status { get; set; }
}
```

**File: `Program.cs`**

```cs
List<Student> students = new List<Student>();

students.Add(new Student { FullName = "Juan Dela Cruz", Status = StudentStatus.Enrolled });
students.Add(new Student { FullName = "Maria Santos", Status = StudentStatus.Graduated });
students.Add(new Student { FullName = "Ana Reyes", Status = StudentStatus.Enrolled });
students.Add(new Student { FullName = "Mark Villanueva", Status = StudentStatus.OnLeave });

Console.WriteLine("Enrolled students:");

foreach (Student student in students)
{
    if (student.Status == StudentStatus.Enrolled)
    {
        Console.WriteLine($"- {student.FullName}");
    }
}
```

A property can have an enum as its type. Now a student's status can only ever be one of the three choices.

</details>

---

## Remember

- An **enum** is your own type, with a **fixed list of named choices**.
- Make one: `enum CupSize { Small, Medium, Large }`
- Use one: `CupSize size = CupSize.Large;`
- Enums work with `==` and with `switch`. No quotes.
- Behind each choice is a number, starting at 0. `(int)size` shows it. You can set your own: `Freshman = 1`.
- In `Program.cs`, enums go at the **very bottom**. In bigger projects, an enum gets its own file.
- Use an enum whenever something has only a few fixed choices.

---

[⬅️ Lesson 14](14-classes-and-objects.md) | [🏠 Course Home](../README.md) | [Next: Lesson 16 (Handling Errors) ➡️](16-handling-errors.md)
