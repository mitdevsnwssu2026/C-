# Lesson 09: Classes and Objects (Making Your Own Types)

**Part 2: Intermediate** · About 90 minutes

## What You Will Learn

- The difference between a **class** and an **object**
- How to make a `Student` class
- How to add a **second file** to your project
- How to keep many students in a list

## The Idea

So far, each variable holds one simple value: a name, an age, a grade. But a **student** has several pieces of information that belong together:

- Student number
- Full name
- Course
- Year level

You can make your **own type**, called `Student`, that holds all four together.

Think of a **blank enrollment form**:

```text
 CLASS (the blank form)             OBJECTS (filled-in forms)
┌──────────────────────┐           ┌───────────────────────────┐
│ Student Number: ____ │           │ Student Number: 2026-0001 │
│ Full Name:      ____ │   ───►    │ Full Name: Juan Dela Cruz │
│ Course:         ____ │           │ Course: BSIT              │
│ Year Level:     ____ │           │ Year Level: 1             │
└──────────────────────┘           └───────────────────────────┘
                                   ┌───────────────────────────┐
                                   │ Student Number: 2026-0002 │
                                   │ Full Name: Maria Santos   │
                                   │ Course: BSCS              │
                                   │ Year Level: 2             │
                                   └───────────────────────────┘
```

- A **class** is the **blank form**. It says what information every student has.
- An **object** is one **filled-in form**: Juan's record, Maria's record.
- A **property** is one blank on the form: `FullName`, `Course`.

You write the class **once**. Then you can make as many objects from it as you need.

---

## Step by Step

### Step 1: Create a new project named `Lesson09`

1. Open **Visual Studio**. (If it is already open, close it first and open it again.)
2. Click **Create a new project**.
3. Click **Console App** (the one with **C#**, not ".NET Framework"), then click **Next**.
4. In **Project name**, type `Lesson09`. Check that **Location** says `C:\CSharpCourse`. Click **Next**.
5. Click **Create**.

### Step 2: Add a new file named `Student.cs`

Each class gets its own file. This is new, so go slowly.

**1.** Look at the **Solution Explorer** on the right side. You will see:

```text
Solution 'Lesson09' (1 of 1 project)
   Lesson09                ← this line is your PROJECT
      Dependencies
      Program.cs
```

**2.** **Right-click** the project line **Lesson09** (the second line, not the first line that starts with "Solution").

**3.** In the menu that appears, move your mouse to **Add**.

**4.** In the next menu, click **Class...**

✅ **You will see:** a small window with a **Name** box.

**5.** In the **Name** box, delete what is there and type: `Student.cs`

**6.** Click **Add**.

✅ **You will see:** a new tab named **Student.cs** opens in the editor. Visual Studio has put some starter code in it. `Student.cs` also appears in the Solution Explorer.

> Can't see the Solution Explorer? Click **View** on the menu bar, then **Solution Explorer**.

### Step 3: Type the class

1. Click inside the editor of the **Student.cs** tab.
2. Press **Ctrl + A**, then **Delete**, to remove the starter code.
3. Type this code:

## The Code

**File: `Student.cs`**

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

### Step 4: Type the main program

1. In the **Solution Explorer**, double-click **Program.cs**. Its tab opens in the editor.
2. Press **Ctrl + A**, then **Delete**.
3. Type this code:

**File: `Program.cs`**

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

> [!TIP]
> You now have **two tabs** at the top of the editor: **Student.cs** and **Program.cs**. Click a tab to switch between the files. Always check which tab you are typing in!

### Step 5: Run it

Press **Ctrl + F5**. Type `2`, then `1` (and add a student), then `2` again, then `3`.

## The Output

✅ **You will see:**

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

## What the Code Means

### The class (`Student.cs`)

```cs
public class Student
{
    ...
}
```

This makes a new type named `Student`. The word `public` means *"other files, like Program.cs, are allowed to use it."*

```cs
public string FullName { get; set; } = "";
```

This is a **property**: one blank on the form. Read it piece by piece:

- `public` → other files can use it.
- `string` → it holds text.
- `FullName` → its name. Property names start with a capital letter.
- `{ get; set; }` → its value can be **read** (get) and **changed** (set).
- `= "";` → it starts as empty text. (This keeps Visual Studio from showing a warning.)

```cs
public int YearLevel { get; set; }
```

Number properties start at `0` by themselves, so they do not need `= ...` at the end.

```cs
public void Display()
{
    Console.WriteLine($"[{StudentNumber}] {FullName} - {Course} {YearLevel}");
}
```

A class can have **methods** too. `Display` shows this student's information.

### Making objects (`Program.cs`)

There are two ways to make an object and fill it in.

**Way 1: make it, then fill in each property.**

```cs
Student newStudent = new Student();
newStudent.FullName = "Ana Reyes";
newStudent.Course = "BSIT";
```

- `new Student()` means *"make a new blank Student form."*
- `newStudent.FullName` means *"the FullName of this student."* The dot means "belongs to".

**Way 2: make it and fill it in, all at once.**

```cs
new Student
{
    StudentNumber = "2026-0001",
    FullName = "Juan Dela Cruz",
    Course = "BSIT",
    YearLevel = 1
}
```

Both ways give the same result. Way 2 is shorter when you already know all the values.

### A list of objects

```cs
List<Student> students = new List<Student>();
```

`List<string>` holds text. In the same way, `List<Student>` holds `Student` objects.

```cs
foreach (Student student in students)
{
    student.Display();
}
```

For each student in the list, call **that student's** `Display` method.

---

## If Something Goes Wrong

**Error List: `CS0246  The type or namespace name 'Student' could not be found`**
Visual Studio cannot find your class. Check three things:

1. In the Solution Explorer, is there a file named `Student.cs`?
2. In that file, does the first line of code say `public class Student` (capital **S**)?
3. Did you delete **all** of the starter code before typing? Nothing should be above `// Student.cs` or `public class Student`.

**Error List: `CS1061  'Student' does not contain a definition for 'Name'`**
You used a property that is not in the class. Use the exact name: `FullName`, not `Name`.

**Error List: `CS0165  Use of unassigned local variable`**
You forgot `new`. Write `Student s = new Student();`

**I typed the class into Program.cs by mistake.**
Look at the tabs at the top of the editor. Click the right tab, and move the code to the correct file.

---

## Exercise

**Exercise 1.** Add an `Email` property to the `Student` class. Ask for it in option 1, and show it in `Display()`.

<details>
<summary>Show answer</summary>

In `Student.cs`, add this property:

```cs
public string Email { get; set; } = "";
```

And change the line inside `Display()`:

```cs
Console.WriteLine($"[{StudentNumber}] {FullName} - {Course} {YearLevel} - {Email}");
```

In `Program.cs`, inside `case "1":`, just before `students.Add(newStudent);`, add:

```cs
Console.Write("Email: ");
newStudent.Email = Console.ReadLine() ?? "";
```

</details>

**Exercise 2.** Add a method `bool IsGraduating()` to the `Student` class. It gives back `true` when `YearLevel` is `4`. In option 2, show `(graduating)` under each graduating student.

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

## ⭐ Bonus Challenge

Start a **new project** named `Lesson09Bonus`. Make a `Book` class (in its own file, `Book.cs`) with three properties: `Title`, `Author`, and `Year`. In `Program.cs`, make a list of three books and show them all.

```text
Noli Me Tangere by Jose Rizal (1887)
El Filibusterismo by Jose Rizal (1891)
Banaag at Sikat by Lope K. Santos (1906)
```

<details>
<summary>Show answer</summary>

**File: `Book.cs`**

```csharp
public class Book
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
}
```

**File: `Program.cs`**

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

## Remember

- A **class** is a blank form (`Student`). An **object** is one filled-in form (Juan's record).
- A **property** holds one piece of information: `public string FullName { get; set; } = "";`
- Make objects with `new Student()` or `new Student { FullName = "...", ... }`
- Use the dot to reach inside an object: `student.FullName`, `student.Display()`
- To add a file: right-click the project → **Add** → **Class...** → type the name → **Add**.
- Each class goes in its own file, named after the class.

---

[⬅️ Lesson 08](08-methods.md) | [🏠 Course Home](../README.md) | [Next: Lesson 10 (Handling Errors) ➡️](10-handling-errors.md)
