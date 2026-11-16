# Lesson 07: Arrays and Lists (Many Values in One Place)

| | |
|---|---|
| **Part** | 1: Beginner |
| **Time** | About 60 minutes |
| **Before this** | [Lesson 06](06-loops.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Store many values in one variable using an **array** or a **List**
- Get a value by its position (its **index**)
- Go through every item with `foreach`
- Build a program that collects names until the user is done

---

## 🧠 The Big Idea

A variable holds **one** value. But what if you need to keep 40 student names? Making 40 variables (`name1`, `name2`, ... `name40`) would be terrible.

Instead, use **one** variable that holds a **list** of values, like a class attendance sheet:

```text
 index:    0        1        2
        ┌───────┬───────┬─────────┐
names:  │ "Ana" │ "Ben" │ "Carla" │
        └───────┴───────┴─────────┘
```

Each item has a **position number** called an **index**.

> [!IMPORTANT]
> **Counting starts at 0, not 1.** The first item is at index `0`, the second at index `1`, and so on. This feels strange at first, but every programmer gets used to it.

C# has two common ways to hold many values:

| | Array | List |
|---|---|---|
| Size | **Fixed.** Decided when you create it | **Grows** when you add items |
| Use when... | The items never change (days of the week) | You add or remove items (students who sign up) |

---

## 🧠 Arrays

```cs
string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri" };

Console.WriteLine(days[0]);           // Mon
Console.WriteLine(days[4]);           // Fri
Console.WriteLine(days.Length);       // 5  (how many items)
```

- `string[]` means "an array of strings". The `[]` means "many".
- `days[0]` means "the item at index 0".
- `.Length` tells you how many items are in the array.

---

## 🧠 Lists

```cs
List<string> names = new List<string>();   // make an empty list

names.Add("Ana");        // add to the end
names.Add("Ben");
names.Add("Carla");

Console.WriteLine(names[0]);      // Ana
Console.WriteLine(names.Count);   // 3  (how many items)
```

| You want to... | Write |
|---|---|
| Make an empty list of text | `List<string> names = new List<string>();` |
| Make an empty list of whole numbers | `List<int> scores = new List<int>();` |
| Add an item to the end | `names.Add("Ana");` |
| Get the item at an index | `names[0]` |
| Count the items | `names.Count` |
| Check if an item is in the list | `names.Contains("Ben")` (gives `true` or `false`) |
| Remove an item | `names.Remove("Ben");` |

`List<string>` is read as "a List **of** strings". The type inside `< >` says what kind of items the list holds.

> [!NOTE]
> Arrays use `.Length`. Lists use `.Count`. Both mean "how many items".

---

## 🧠 Going Through Every Item: `foreach`

`foreach` runs a block of code **once for each item** in a list or array.

```cs
foreach (string name in names)
{
    Console.WriteLine($"Hello, {name}!");
}
```

Read it as: *"For each `name` in `names`, say hello."*

Each round, the box `name` holds the next item: first `"Ana"`, then `"Ben"`, then `"Carla"`.

**Need the position number too?** Use a `for` loop with an index instead:

```cs
for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}
```

We write `i + 1` because people count from 1, but the index starts from 0.

---

## 🛠️ Let's Build It: Classmate Name Collector

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson07
cd Lesson07
code .
```

### Step 2: Write the code

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

### Step 3: Run it

✅ **You should see** (type three names, then press **Enter** on an empty line):

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

## 🔎 Line by Line

```cs
List<string> names = new List<string>();
```

Create an empty list. Right now, `names.Count` is `0`.

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

Keep asking forever. When the user presses Enter without typing anything, `name` is empty text `""`, so we `break` out of the loop. Otherwise, we add the name to the list.

```cs
for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}
```

Go through the list by index, from `0` up to (but not including) `names.Count`, and print each name with a number.

---

## ⚠️ Common Mistakes

### Mistake 1: Using an index that does not exist

```cs
List<string> names = new List<string>();
names.Add("Ana");
Console.WriteLine(names[1]);   // there is only index 0!
```

```text
Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
```

For an array, the message is `Index was outside the bounds of the array.`

**Fix:** The last index is always `Count - 1` (or `Length - 1`). A list with 1 item only has index `0`.

### Mistake 2: Forgetting `new List<string>()`

```cs
List<string> names;
names.Add("Ana");
```

```text
error CS0165: Use of unassigned local variable 'names'
```

**Fix:** Create the list before using it: `List<string> names = new List<string>();`

### Mistake 3: Mixing up `.Length` and `.Count`

Arrays use `.Length`. Lists use `.Count`. If you mix them up, C# shows an error like `'List<string>' does not contain a definition for 'Length'`.

---

## 🏋️ Practice

**Exercise 1.** Ask the user for **5 grades** (use a `for` loop), store them in a `List<double>`, then use `foreach` to compute and show the average.

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

**Exercise 2.** Make a list of 5 fruits. Ask the user to type a fruit. Show whether it is in the list using `.Contains`.

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

We use `.ToLower()` so that `Mango` and `mango` both match.

</details>

**Exercise 3.** Make a **shopping list** program with a menu: `1 - Add item`, `2 - Show items`, `3 - Exit`. (Hint: combine this lesson with the menu loop from Lesson 06.)

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

## 📌 Recap

- An **array** has a fixed size: `string[] days = { "Mon", "Tue" };`
- A **List** grows: `List<string> names = new List<string>();` then `names.Add("Ana");`
- Indexes start at **0**. The last index is `Count - 1`.
- Arrays use `.Length`. Lists use `.Count`.
- `foreach (string name in names) { ... }` visits every item.

---

[⬅️ Lesson 06](06-loops.md) | [🏠 Course Home](../README.md) | [Next: Lesson 08 (Methods) ➡️](08-methods.md)
