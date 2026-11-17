# Lesson 12: Lists (Arrays That Can Grow)

**Part 1: Beginner** · About 60 minutes

## What You Will Learn

- The difference between an **array** and a **List**
- How to add, remove, count, and search items in a List
- How to build a program that collects names until the user is done

## The Idea

In Lesson 11, you learned that an array has a **fixed size**. That is fine for things that never change, like the days of the week.

But what about a sign-up sheet? You do not know how many students will sign up. You need something that can **grow**.

That is a **List**. A List works like an array, but you can **add** items to it and **remove** items from it at any time.

| | Array | List |
|---|---|---|
| Size | **Fixed** | **Grows and shrinks** |
| How many items? | `.Length` | `.Count` |
| Get an item | `scores[0]` | `names[0]` (the same!) |
| Add an item | Not possible | `names.Add("Ana");` |
| Good for... | Things that never change | Things you add to |

Real programs use Lists much more often than arrays. From now on, this course mostly uses Lists.

---

## Step by Step

### Step 1: Create a new project named `Lesson12`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson12`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

---

## Part 1: Using a List

**1.** Clear the editor (**Ctrl + A**, **Delete**) and type this code:

```csharp
// Make an empty list of text
List<string> names = new List<string>();

// Add items
names.Add("Ana");
names.Add("Ben");
names.Add("Carla");

Console.WriteLine($"First name: {names[0]}");
Console.WriteLine($"Number of names: {names.Count}");

// Search
bool hasBen = names.Contains("Ben");
Console.WriteLine($"Is Ben in the list? {hasBen}");

// Remove an item
names.Remove("Ben");
hasBen = names.Contains("Ben");
Console.WriteLine($"Is Ben in the list now? {hasBen}");
Console.WriteLine($"Number of names: {names.Count}");

// Visit every item
foreach (string name in names)
{
    Console.WriteLine($"Hello, {name}!");
}
```

**2.** Press **Ctrl + F5**.

✅ **The output:**

```text
First name: Ana
Number of names: 3
Is Ben in the list? True
Is Ben in the list now? False
Number of names: 2
Hello, Ana!
Hello, Carla!
```

**What it means:**

```cs
List<string> names = new List<string>();
```

This makes an **empty** list of text. Read `List<string>` as *"a list **of** strings"*. The type inside `< >` says what kind of items the list holds. The word `new` means *"make a new one"*.

- `names.Add("Ana");` → adds an item to the **end** of the list.
- `names[0]` → the first item. Indexes start at 0, just like arrays.
- `names.Count` → how many items are in the list right now.
- `names.Contains("Ben")` → is `"Ben"` in the list? `True` or `False`.
- `names.Remove("Ben");` → takes `"Ben"` out of the list. The list gets shorter.
- `foreach` works exactly like it does with arrays.

### Lists of other types

```cs
List<int> scores = new List<int>();         // a list of whole numbers
List<double> grades = new List<double>();   // a list of decimal numbers
```

---

## Part 2: The Name Collector

This program keeps asking for names **until the user is done**. A List is perfect, because nobody knows in advance how many names there will be.

**1.** Clear the editor and type the code below.

## The Code

```csharp
// Lesson 12: Collecting many names in a List

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

This is the `while (true)` with `break` pattern from Lesson 10. The loop keeps asking forever. When the user presses **Enter** without typing anything, `name` is empty text (`""`), so `break` leaves the loop. Otherwise, the name is added to the list.

```cs
for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}
```

Go through the list by index: 0, 1, 2... We show `i + 1`, because people count from 1.

### The List tools

| I want to... | Write |
|---|---|
| Make an empty list of text | `List<string> names = new List<string>();` |
| Add an item to the end | `names.Add("Ana");` |
| Get the item at an index | `names[0]` |
| Change the item at an index | `names[0] = "Anna";` |
| Count the items | `names.Count` |
| Check if an item is in the list | `names.Contains("Ben")` |
| Remove an item | `names.Remove("Ben");` |
| Remove **all** items | `names.Clear();` |
| Sort the items | `names.Sort();` |

---

## If Something Goes Wrong

**The black window shows a crash:**

```text
Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
```

You used an index that does not exist. A list with 3 items has indexes 0, 1, and 2 only. The last index is always `Count - 1`. An **empty** list has no `names[0]` at all.

**Error List: `CS0165  Use of unassigned local variable 'names'`**
You forgot to create the list. Write `List<string> names = new List<string>();`

**Error List: `CS1061  'List<string>' does not contain a definition for 'Length'`**
Lists use `.Count`, not `.Length`. (Arrays use `.Length`.)

**Error List: `CS1503  Argument 1: cannot convert from 'int' to 'string'`**
You added the wrong type to a list, like `names.Add(5);` on a `List<string>`. A list only holds the type written inside its `< >`.

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
3 - Remove item
4 - Exit
```

Hint: combine the **List** from this lesson with the **menu loop** from Lesson 10.

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
    Console.WriteLine("3 - Remove item");
    Console.WriteLine("4 - Exit");
    Console.Write("Choose: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Item to add: ");
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
            Console.Write("Item to remove: ");
            string itemToRemove = Console.ReadLine() ?? "";

            if (items.Contains(itemToRemove))
            {
                items.Remove(itemToRemove);
                Console.WriteLine("Removed!");
            }
            else
            {
                Console.WriteLine("That item is not in the list.");
            }
            break;

        case "4":
            running = false;
            break;

        default:
            Console.WriteLine("Please type 1, 2, 3, or 4.");
            break;
    }
}
```

</details>

---

## Remember

- An **array** has a fixed size. A **List** can grow and shrink.
- `List<string> names = new List<string>();` makes an empty list.
- `.Add()` adds, `.Remove()` removes, `.Contains()` searches, `.Count` counts.
- Indexes start at **0**. The last index is `Count - 1`.
- Arrays use `.Length`. Lists use `.Count`.

---

[⬅️ Lesson 11](11-arrays.md) | [🏠 Course Home](../README.md) | [Next: Lesson 13 (Functions) ➡️](13-methods.md)
