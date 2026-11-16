# Lesson 07: Arrays and Lists (Many Values in One Place)

**Part 1: Beginner** · About 60 minutes

## What You Will Learn

- How to keep **many values** in one variable
- How to get one value by its position number (its **index**)
- How to go through every item with `foreach`
- How to build a program that collects names until the user is done

## The Idea

A variable holds **one** value. But what if you need to keep 40 student names? Making 40 variables would be terrible.

Instead, use **one** variable that holds a **list**, like a class attendance sheet:

```text
 position:   0        1        2
          ┌───────┬───────┬─────────┐
 names:   │ "Ana" │ "Ben" │ "Carla" │
          └───────┴───────┴─────────┘
```

Each item has a position number, called its **index**.

> [!IMPORTANT]
> **Counting starts at 0, not 1.** The first item is at index `0`. The second is at index `1`. This feels strange at first. Every programmer gets used to it.

C# has two ways to hold many values:

- An **array** has a **fixed size**. Good for things that never change, like the days of the week.
- A **List** can **grow**. Good for things you add to, like students who sign up.

---

## Step by Step

### Step 1: Create a new project named `Lesson07`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson07`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Try an array and a List

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this:

```csharp
// An array: fixed size
string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };

Console.WriteLine(days[0]);
Console.WriteLine(days[4]);
Console.WriteLine(days.Length);

// A List: it can grow
List<string> names = new List<string>();

names.Add("Ana");
names.Add("Ben");
names.Add("Carla");

Console.WriteLine(names[0]);
Console.WriteLine(names.Count);

foreach (string name in names)
{
    Console.WriteLine($"Hello, {name}!");
}
```

**2.** Press **Ctrl + F5**.

✅ **You will see:**

```text
Mon
Fri
5
Ana
3
Hello, Ana!
Hello, Ben!
Hello, Carla!
```

Here is what each part did.

**The array:**

- `string[] days = { ... };` makes an array of text. The `[]` means "many".
- `days[0]` is the item at index 0: `Mon`. `days[4]` is the fifth item: `Fri`.
- `days.Length` is how many items it has: `5`.

**The List:**

- `List<string> names = new List<string>();` makes an **empty** list of text. Read `List<string>` as "a list **of** strings".
- `names.Add("Ana");` adds an item to the end.
- `names[0]` is the first item.
- `names.Count` is how many items it has.

**The `foreach` loop:**

```cs
foreach (string name in names)
{
    Console.WriteLine($"Hello, {name}!");
}
```

Read it as: *"For each `name` in `names`, say hello."* The loop runs once for every item. Each round, `name` holds the next item.

> Arrays use `.Length`. Lists use `.Count`. Both mean "how many items".

### Step 3: Build the name collector

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 7: Collecting many names in a List

List<string> names = new List<string>();

Console.WriteLine("Type the names of your classmates.");
Console.WriteLine("When you are done, just press Enter on an empty line.");

while (true)
{
    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";

    if (name == "")
    {
        break;
    }

    names.Add(name);
}

Console.WriteLine();
Console.WriteLine($"You entered {names.Count} name(s):");

for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}
```

**2.** Press **Ctrl + F5**. Type three names, pressing **Enter** after each one. Then press **Enter** on an empty line to finish.

## The Output

✅ **You will see:**

```text
Type the names of your classmates.
When you are done, just press Enter on an empty line.
Name: Ana
Name: Ben
Name: Carla
Name:

You entered 3 name(s):
1. Ana
2. Ben
3. Carla
```

---

## What the Code Means

```cs
List<string> names = new List<string>();
```

Make an empty list. Right now, `names.Count` is `0`.

```cs
while (true)
{
    ...
    if (name == "")
    {
        break;
    }

    names.Add(name);
}
```

Keep asking forever. When the user presses **Enter** without typing anything, `name` is empty text (`""`), so `break` leaves the loop. Otherwise, the name is added to the list.

```cs
for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}
```

Go through the list by index: 0, 1, 2... We show `i + 1` because people count from 1, but indexes start from 0.

**`foreach` or `for`?** Use `foreach` when you only need each item. Use `for` when you also need the position number.

### More things a List can do

- `names.Contains("Ben")` → is `"Ben"` in the list? Gives `true` or `false`.
- `names.Remove("Ben");` → removes `"Ben"` from the list.
- `List<int> scores = new List<int>();` → a list of whole numbers.

---

## If Something Goes Wrong

**The black window shows a crash:**

```text
Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
```

You used an index that does not exist. A list with 3 items has indexes 0, 1, and 2 only. The last index is always `Count - 1`.

(For an array, the message is `Index was outside the bounds of the array.`)

**Error List: `CS0165  Use of unassigned local variable 'names'`**
You forgot to create the list. Write `List<string> names = new List<string>();`

**Error List: `CS1061  'List<string>' does not contain a definition for 'Length'`**
Lists use `.Count`, not `.Length`.

---

## Exercise

**Exercise 1.** Ask the user for **5 grades** (use a `for` loop). Keep them in a `List<double>`. Then use `foreach` to compute the average, and show it.

<details>
<summary>Show answer</summary>

```csharp
List<double> grades = new List<double>();

for (int i = 1; i <= 5; i++)
{
    Console.Write($"Grade {i}: ");
    double grade = double.Parse(Console.ReadLine() ?? "");
    grades.Add(grade);
}

double total = 0;

foreach (double grade in grades)
{
    total += grade;
}

double average = total / grades.Count;
Console.WriteLine($"Average: {average:F2}");
```

</details>

**Exercise 2.** Make a list of 5 fruits. Ask the user to type a fruit. Say if it is in the list, using `.Contains`.

<details>
<summary>Show answer</summary>

```csharp
List<string> fruits = new List<string>();
fruits.Add("mango");
fruits.Add("banana");
fruits.Add("papaya");
fruits.Add("pineapple");
fruits.Add("lanzones");

Console.Write("Type a fruit: ");
string fruit = (Console.ReadLine() ?? "").ToLower();

if (fruits.Contains(fruit))
{
    Console.WriteLine($"Yes! {fruit} is in the list.");
}
else
{
    Console.WriteLine($"Sorry, {fruit} is not in the list.");
}
```

`.ToLower()` makes `Mango` and `mango` both match.

</details>

## ⭐ Bonus Challenge

Build a **shopping list** program with a menu:

```text
1 - Add item
2 - Show items
3 - Exit
```

Hint: combine the **List** from this lesson with the **menu loop** from Lesson 06.

<details>
<summary>Show answer</summary>

```csharp
List<string> items = new List<string>();
bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("1 - Add item");
    Console.WriteLine("2 - Show items");
    Console.WriteLine("3 - Exit");
    Console.Write("Choose: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Item: ");
            items.Add(Console.ReadLine() ?? "");
            Console.WriteLine("Added!");
            break;

        case "2":
            Console.WriteLine($"You have {items.Count} item(s):");
            foreach (string item in items)
            {
                Console.WriteLine($"- {item}");
            }
            break;

        case "3":
            running = false;
            break;

        default:
            Console.WriteLine("Please type 1, 2, or 3.");
            break;
    }
}
```

</details>

---

## Remember

- An **array** has a fixed size: `string[] days = { "Mon", "Tue" };`
- A **List** grows: `List<string> names = new List<string>();` then `names.Add("Ana");`
- Indexes start at **0**. The last index is `Count - 1`.
- Arrays use `.Length`. Lists use `.Count`.
- `foreach (string name in names) { ... }` visits every item.

---

[⬅️ Lesson 06](06-loops.md) | [🏠 Course Home](../README.md) | [Next: Lesson 08 (Methods) ➡️](08-methods.md)
