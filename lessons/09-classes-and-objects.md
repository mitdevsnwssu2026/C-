# Lesson 09: Classes and Objects (Making Your Own Types)

| | |
|---|---|
| **Part** | 2: Intermediate |
| **Time** | About 90 minutes |
| **Before this** | [Lesson 08](08-methods.md) |

## 🎯 Goal

By the end of this lesson, you will:

- Understand the difference between a **class** and an **object**
- Create a `Student` class with **properties**
- Put the class in its **own file**
- Keep many students in a `List<Student>`

---

## 🧠 The Big Idea

So far, each variable holds one simple value: a name, an age, a grade. But a **student** has many pieces of information that belong together:

- Student number
- Full name
- Course
- Year level

Instead of four separate variables for every student, you can create your own **type** called `Student` that holds all four together.

### Class vs. Object

Think of a **blank enrollment form**:

```text
CLASS (the blank form)              OBJECTS (filled-in forms)
┌──────────────────────┐            ┌──────────────────────────┐
│ Student Number: ____ │            │ Student Number: 2026-0001│
│ Full Name:      ____ │   ───►     │ Full Name: Juan Dela Cruz│
│ Course:         ____ │            │ Course: BSIT             │
│ Year Level:     ____ │            │ Year Level: 1            │
└──────────────────────┘            └──────────────────────────┘
                                    ┌──────────────────────────┐
                                    │ Student Number: 2026-0002│
                                    │ Full Name: Maria Santos  │
                                    │ Course: BSCS             │
                                    │ Year Level: 2            │
                                    └──────────────────────────┘
```

| Word | Meaning | Example |
|---|---|---|
| **Class** | The **blueprint** or blank form. It describes what information every student has | `Student` |
| **Object** | One **real thing** made from the class. A filled-in form | Juan's record, Maria's record |
| **Property** | One piece of information in the class. A blank on the form | `FullName`, `Course` |

You write the class **once**. Then you can create as many objects from it as you need.

---

## 🛠️ Let's Build It

### Step 1: Create the project

```cmd
cd C:\CSharpCourse
dotnet new console -n Lesson09
cd Lesson09
code .
```

### Step 2: Create a new file called `Student.cs`

Each class goes in its own file, named after the class.

1. In VS Code, look at the **Explorer** panel on the left (it lists your files).
2. Move your mouse over the panel. Click the **New File** icon (a page with a `+`) that appears next to the folder name `LESSON09`.
3. Type the name **`Student.cs`** and press **Enter**.

> [!IMPORTANT]
> The name must be exactly `Student.cs`, with a capital **S** and ending in **`.cs`**. Make sure it is in the same folder as `Program.cs`, not inside `obj`.

### Step 3: Write the class in `Student.cs`

```csharp
// Student.cs - the blueprint for a student

public class Student
{
    public string StudentNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Course { get; set; } = "";
    public int YearLevel { get; set; }

    public void Display()
    {
        Console.WriteLine($"[{StudentNumber}] {FullName} - {Course} {YearLevel}");
    }
}
```

### Step 4: Write the main program in `Program.cs`

Replace everything in `Program.cs` with:

```csharp
// Lesson 9: Keeping a list of Student objects

List<Student> students = new List<Student>();

// Two sample students so the list is not empty
students.Add(new Student
{
    StudentNumber = "2026-0001",
    FullName = "Juan Dela Cruz",
    Course = "BSIT",
    YearLevel = 1
});

students.Add(new Student
{
    StudentNumber = "2026-0002",
    FullName = "Maria Santos",
    Course = "BSCS",
    YearLevel = 2
});

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("===== STUDENT LIST =====");
    Console.WriteLine("1 - Add a student");
    Console.WriteLine("2 - Show all students");
    Console.WriteLine("3 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Student newStudent = new Student();

            Console.Write("Student number: ");
            newStudent.StudentNumber = Console.ReadLine() ?? "";

            Console.Write("Full name: ");
            newStudent.FullName = Console.ReadLine() ?? "";

            Console.Write("Course: ");
            newStudent.Course = Console.ReadLine() ?? "";

            Console.Write("Year level: ");
            newStudent.YearLevel = int.Parse(Console.ReadLine() ?? "");

            students.Add(newStudent);
            Console.WriteLine("Student added!");
            break;

        case "2":
            Console.WriteLine($"There are {students.Count} student(s):");
            foreach (Student student in students)
            {
                student.Display();
            }
            break;

        case "3":
            running = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please type 1, 2, or 3.");
            break;
    }
}
```

### Step 5: Run it

```cmd
dotnet run
```

✅ **You should see** (choosing `2`, then `1` to add Ana Reyes, then `2` again, then `3`):

```text

===== STUDENT LIST =====
1 - Add a student
2 - Show all students
3 - Exit
Choose an option: 2
There are 2 student(s):
[2026-0001] Juan Dela Cruz - BSIT 1
[2026-0002] Maria Santos - BSCS 2

===== STUDENT LIST =====
1 - Add a student
2 - Show all students
3 - Exit
Choose an option: 1
Student number: 2026-0003
Full name: Ana Reyes
Course: BSIT
Year level: 1
Student added!

===== STUDENT LIST =====
1 - Add a student
2 - Show all students
3 - Exit
Choose an option: 2
There are 3 student(s):
[2026-0001] Juan Dela Cruz - BSIT 1
[2026-0002] Maria Santos - BSCS 2
[2026-0003] Ana Reyes - BSIT 1

===== STUDENT LIST =====
1 - Add a student
2 - Show all students
3 - Exit
Choose an option: 3
Goodbye!
```

---

## 🔎 Line by Line

### The class (`Student.cs`)

```cs
public class Student
{
    ...
}
```

`public class Student` creates a new type named `Student`. `public` means *"other files (like Program.cs) are allowed to use it."* Class names use **PascalCase**.

```cs
public string FullName { get; set; } = "";
```

This is a **property**: one piece of information that every `Student` object has.

| Piece | Meaning |
|---|---|
| `public` | Other files can read and change it |
| `string` | Its type |
| `FullName` | Its name (PascalCase for properties) |
| `{ get; set; }` | It can be **read** (get) and **changed** (set) |
| `= "";` | Its starting value is empty text. This prevents C# warnings about "null" text |

```cs
public int YearLevel { get; set; }
```

Number properties start at `0` automatically, so they do not need `= ...` at the end.

```cs
public void Display()
{
    Console.WriteLine($"[{StudentNumber}] {FullName} - {Course} {YearLevel}");
}
```

A class can also have **methods**. `Display` shows this student's information. Inside the class, you can use the properties directly by name.

### Creating objects (`Program.cs`)

There are two ways to create an object and fill in its properties.

**Way 1: create it first, then set each property** (used in option 1):

```cs
Student newStudent = new Student();
newStudent.FullName = "Ana Reyes";
newStudent.Course = "BSIT";
```

**Way 2: create it and fill it in one step** (used for the sample students):

```cs
new Student
{
    StudentNumber = "2026-0001",
    FullName = "Juan Dela Cruz",
    Course = "BSIT",
    YearLevel = 1
}
```

Both ways give you the same result. Way 2 is shorter when you already know all the values.

- `new Student()` means *"make a new blank Student form."*
- `newStudent.FullName` means *"the FullName of this student."* The dot `.` means "belongs to", just like `Console.WriteLine`.

### A list of objects

```cs
List<Student> students = new List<Student>();
```

Just like `List<string>` holds text, `List<Student>` holds `Student` objects.

```cs
foreach (Student student in students)
{
    student.Display();
}
```

For each student in the list, call **that student's** `Display` method.

---

## ⚠️ Common Mistakes

### Mistake 1: The class file name or class name does not match

If `Program.cs` says `new Student()` but your class is named `student` (small s), you get:

```text
error CS0246: The type or namespace name 'Student' could not be found (are you missing a using directive or an assembly reference?)
```

**Fix:** Check that the class is `public class Student` with a capital **S**, and the file `Student.cs` is in the **same folder** as `Program.cs`.

### Mistake 2: Forgetting `new`

```cs
Student s;
s.FullName = "Ana";
```

```text
error CS0165: Use of unassigned local variable 's'
```

**Fix:** Create the object first: `Student s = new Student();`

### Mistake 3: Using a property that does not exist

```cs
newStudent.Name = "Ana";
```

```text
error CS1061: 'Student' does not contain a definition for 'Name' ...
```

**Fix:** Use the exact property name from the class: `FullName`.

---

## 🏋️ Practice

**Exercise 1.** Add an `Email` property to the `Student` class. Ask for it in option 1, and show it in `Display()`.

<details>
<summary>Show answer</summary>

In `Student.cs`, add this property:

```cs
public string Email { get; set; } = "";
```

And change `Display()`:

```cs
Console.WriteLine($"[{StudentNumber}] {FullName} - {Course} {YearLevel} - {Email}");
```

In `Program.cs`, inside `case "1":`, before `students.Add(newStudent);`, add:

```cs
Console.Write("Email: ");
newStudent.Email = Console.ReadLine() ?? "";
```

</details>

**Exercise 2.** Add a method `bool IsGraduating()` to the `Student` class. It returns `true` if `YearLevel` is `4`. In option 2, show `(graduating)` next to graduating students.

<details>
<summary>Show answer</summary>

In `Student.cs`, add this method inside the class:

```cs
public bool IsGraduating()
{
    return YearLevel == 4;
}
```

In `Program.cs`, change the `foreach` in `case "2":`:

```cs
foreach (Student student in students)
{
    student.Display();

    if (student.IsGraduating())
    {
        Console.WriteLine("   (graduating)");
    }
}
```

</details>

**Exercise 3.** In a new project (`Lesson09Practice`), create a `Book` class with `Title`, `Author`, and `Year`. Create three books in a `List<Book>` and print them all.

<details>
<summary>Show answer</summary>

`Book.cs`:

```csharp
public class Book
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
}
```

`Program.cs`:

```cs
List<Book> books = new List<Book>();

books.Add(new Book { Title = "Noli Me Tangere", Author = "Jose Rizal", Year = 1887 });
books.Add(new Book { Title = "El Filibusterismo", Author = "Jose Rizal", Year = 1891 });
books.Add(new Book { Title = "Banaag at Sikat", Author = "Lope K. Santos", Year = 1906 });

foreach (Book book in books)
{
    Console.WriteLine($"{book.Title} by {book.Author} ({book.Year})");
}
```

</details>

---

## 📌 Recap

- A **class** is a blueprint (`Student`). An **object** is one real thing made from it (Juan's record).
- **Properties** hold the information: `public string FullName { get; set; } = "";`
- Create objects with `new Student()` or `new Student { FullName = "...", ... }`.
- Use the dot to reach a property or method: `student.FullName`, `student.Display()`.
- Put each class in its own file, named after the class (`Student.cs`).
- `List<Student>` keeps many objects together.

---

[⬅️ Lesson 08](08-methods.md) | [🏠 Course Home](../README.md) | [Next: Lesson 10 (Handling Errors) ➡️](10-handling-errors.md)
