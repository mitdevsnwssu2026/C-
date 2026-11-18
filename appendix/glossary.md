# 📖 Glossary

Every new word in the course, in plain language, from A to Z. The number shows the lesson where the word is first explained.

## Visual Studio Words

| Word | Meaning | Lesson |
|---|---|---|
| **Visual Studio** | The program where you write C# code, run it, and see your mistakes | 00 |
| **Workload** | A set of tools you choose when installing Visual Studio. This course needs **.NET desktop development** | 00 |
| **Start window** | The first window of Visual Studio, with **Open recent** and **Create a new project** | 00 |
| **Project** | A folder that holds everything for one program | 01 |
| **Console App** | A program that shows text in the black console window | 01 |
| **Console** | The black window where your program shows text and reads what you type | 00 |
| **Menu bar** | The row of words at the top: **File**, **Edit**, **View**, **Debug**... | 01 |
| **Editor** | The big area in the middle, where you type code | 01 |
| **Tab** | The label at the top of the editor with a file's name. Click a tab to switch files | 01, 14 |
| **Solution Explorer** | The panel on the right that lists the files in your project | 01 |
| **Error List** | The panel at the bottom that lists the mistakes in your code | 01 |
| **Properties panel** | The panel under the Solution Explorer that shows the settings of the file you clicked | 23 |
| **Start Without Debugging** | The command that runs your program. Shortcut: **Ctrl + F5** | 01 |
| **Build** | Turning your C# code into a program the computer can run. Visual Studio does this every time you run | 01 |
| **NuGet** | The online "app store" for C# packages | 19 |
| **Package** | An extra toolbox you add to a project, like `Microsoft.Data.Sqlite` | 19 |
| **bin → Debug → net10.0** | The folder inside your project where your program runs, and where it saves its files | 17 |
| **Copy to Output Directory** | A file setting. **Copy if newer** puts a copy of the file next to your running program | 23 |
| **Package Manager Console** | A panel in Visual Studio where you type commands for packages, like `Scaffold-DbContext` | 25 |

## Programming Words

| Word | Meaning | Lesson |
|---|---|---|
| **`&&` (AND)** | Both conditions must be true | 06 |
| **`\|\|` (OR)** | At least one condition must be true | 06 |
| **`!` (NOT)** | Flips true to false, and false to true | 06 |
| **`??`** | "If the left side is nothing, use the right side instead." Used as `Console.ReadLine() ?? ""` | 03 |
| **Arithmetic operator** | A symbol that does math: `+` `-` `*` `/` `%` | 05 |
| **Array** | A fixed-size row of values in one variable, like `int[] scores` | 11 |
| **`bool`** | A type that holds only `true` or `false` | 02 |
| **`break`** | Leave a loop (or a `switch` case) right now | 08, 10 |
| **Byte** | The smallest unit a computer stores: a number from 0 to 255 | 26 |
| **Call** | To use a function, like `PrintHeader("Hi");` | 13 |
| **camelCase** | The naming style for variables: `fullName`, `yearLevel` | 02 |
| **`catch`** | The part that runs if the code in `try` crashes | 16 |
| **`char`** | A single character, like `'a'` or `','`. It uses single quotes | 17, 28 |
| **Class** | A blueprint for making objects, like `Student`. A blank form | 14 |
| **Class method** | A function that belongs to a class, like `student.Display()` | 14 |
| **Comment** | A note for people, starting with `//`. The computer skips it | 01 |
| **Comparison operator** | A symbol that compares two values: `==` `!=` `>` `<` `>=` `<=`. The answer is `true` or `false` | 06 |
| **Condition** | Something that is either true or false, like `grade >= 75` | 07 |
| **`const`** | A value that never changes | 21 |
| **`continue`** | Skip the rest of this loop round, and start the next round | 10 |
| **Crash** | When a running program stops because something unexpected happened | 03, 16 |
| **Data entry** | Typing records into a computer system, one field at a time | 18 |
| **CSV** | "Comma-Separated Values". A text file with one record per line, and commas between the values | 17 |
| **`do-while`** | A loop that runs once first, then repeats while a condition is true | 09 |
| **`double`** | A type that holds numbers with a decimal point, like `89.5` | 02 |
| **Enum** | Your own type with a fixed list of named choices, like `CupSize.Large` | 15 |
| **Exception** | The C# word for a crash | 16 |
| **`for`** | A loop that counts: `for (int i = 1; i <= 10; i++)` | 09 |
| **`foreach`** | A loop that runs once for each item in an array or list | 11 |
| **Function** | A named block of code you can use again and again. In C#, usually called a **method** | 13 |
| **Header** | The first line of a CSV file, with the names of the columns | 23 |
| **`if` / `else`** | Run one part of the code or another, depending on a condition | 07 |
| **Indentation** | The spaces at the start of a line that show which lines belong together. Visual Studio adds them for you | 07 |
| **Index** | The position number of an item in an array, a list, or a text. It starts at **0** | 04, 11 |
| **Infinite loop** | A loop that never stops, because its condition never becomes false | 09 |
| **`int`** | A type that holds whole numbers, like `18` | 02 |
| **Iteration** | Visiting every item of an array or list, one by one, with a loop | 11 |
| **List** | A group of values that can grow, like `List<string>` | 12 |
| **Logical operator** | A symbol that combines true/false answers: `&&` `\|\|` `!` | 06 |
| **`long`** | Like `int`, but for much bigger whole numbers | 21 |
| **Loop** | Code that repeats | 09 |
| **Method** | The C# word for a function | 13 |
| **Namespace** | A "folder name" for classes, like `Lesson25.Models`. `using Lesson25.Models;` lets a file use those classes | 25 |
| **Mixed operators** | Arithmetic, comparison, and logical operators used together in one line | 06 |
| **Nested `if`** | An `if` inside another `if` | 08 |
| **Nested loop** | A loop inside another loop | 10 |
| **`new`** | Makes a new object, array, or list: `new Student()` | 11, 14 |
| **`null`** | "Nothing". No value, no object | 03, 22 |
| **Object** | One real thing made from a class, like Juan's student record. A filled-in form | 14 |
| **Operator** | A symbol that does something with values: `+`, `==`, `&&` | 05 |
| **Parameter** | (Function) Information you give to a function. (SQL) A placeholder like `$name` that is filled in safely | 13, 21 |
| **Parse** | Turn text into a number: `int.Parse("18")` | 03 |
| **`private`** | Only this class can use it | 22 |
| **Program** | A list of instructions that a computer follows | 00 |
| **Property** | One piece of information in a class, like `FullName`. One blank on the form | 14 |
| **`public`** | Any file can use it | 14 |
| **`return`** | Give back an answer from a function, and leave the function | 13 |
| **`static`** | Use it directly by its class name, without `new` | 22 |
| **`string`** | A type that holds text, like `"Juan"` | 02 |
| **String manipulation** | Changing or checking text: joining, counting, cutting, replacing | 04 |
| **`switch`** | Pick one option from a list of exact values | 08 |
| **`Trim()`** | Removes spaces from the start and end of text | 04 |
| **`try`** | The part of the code that might crash | 16 |
| **`TryParse`** | Safely tries to turn text into a number, and tells you if it worked | 07 |
| **Type** | The kind of value a variable holds: `string`, `int`, `double`, `bool`, a class, or an enum | 02 |
| **`using`** | (At the top of a file) Use the tools from a package. (`using var`) Close this automatically when done | 20 |
| **Validate** | Check that something follows the rules before using it | 23, 28 |
| **Variable** | A box with a label that holds a value | 02 |
| **`void`** | A function that gives nothing back | 13 |
| **Warning** | A green wavy line: a possible problem. The program still runs | 03 |
| **`while`** | A loop that repeats as long as a condition is true | 09 |

## Database Words

| Word | Meaning | Lesson |
|---|---|---|
| **ADO.NET** | The basic, built-in C# tools for talking to a database: connection, command, reader, parameter | 20 |
| **Column** | One kind of information in a table, like `FullName` | 19 |
| **`Commit()`** | Saves all the changes of a transaction for real | 23 |
| **Connection** | An open line between your program and a database | 20 |
| **Connection string** | Text that says where the database is, like `"Data Source=school.db"` | 20 |
| **CRUD** | Create, Read, Update, Delete: the four things systems do with data | 22 |
| **Dapper** | A small package on top of ADO.NET. You write the SQL, and Dapper turns the rows into objects | 24 |
| **Database First** | Starting with a database that already exists, and letting EF Core write the classes from it | 25 |
| **Database** | A program that stores data in organized tables and finds it quickly | 19 |
| **`DbContext`** | In EF Core, the class that is your "door" to the database, like `SchoolContext` | 25 |
| **`DbSet`** | In EF Core, a property that stands for one whole table, like `db.Students` | 25 |
| **EF Core** | Entity Framework Core. A package that writes the SQL for you. You work with objects and lists | 25 |
| **`ExecuteNonQuery()`** | Runs SQL that changes things. Gives back how many rows changed | 20 |
| **`ExecuteReader()`** | Runs a `SELECT` and lets you read many rows, one at a time | 22 |
| **`ExecuteScalar()`** | Runs SQL that gives back one single value, like a count | 20 |
| **`INSERT OR IGNORE`** | Add a row, but skip it quietly if it breaks a rule | 20, 23 |
| **`LIKE`** | SQL for "contains this pattern". `%` means any text | 22 |
| **`NOT NULL`** | A rule: this column is required | 19 |
| **Primary key** | A column that is different for every row and identifies it, like `Id` | 19 |
| **Row** | One record in a table | 19 |
| **`SaveChanges()`** | In EF Core, sends your added, changed, and removed objects to the database | 25 |
| **Scaffolding** | Letting EF Core read a database and write the C# classes for it | 25 |
| **SQL** | The language for talking to databases | 19 |
| **SQL injection** | An attack where a hacker types SQL code into an input box | 21 |
| **SQLite** | A database that lives in one single file | 19 |
| **Table** | A collection of rows and columns in a database | 19 |
| **Transaction** | A group of changes that are saved all together, or not at all | 23 |
| **`UNIQUE`** | A rule: no two rows can have the same value in this column | 20 |
| **`WHERE`** | SQL for choosing which rows. **Always** use it with `UPDATE` and `DELETE` | 19 |

## Security Words

| Word | Meaning | Lesson |
|---|---|---|
| **Argon2** | A modern, slow password hashing method, like PBKDF2 | 26 |
| **Base64** | A way to write bytes as normal text (letters, numbers, `+`, `/`, `=`) | 27 |
| **bcrypt** | A popular, slow password hashing method, like PBKDF2 | 26 |
| **Encryption** | Scrambling data so it can be unscrambled later with a key. **Not** for passwords | 26 |
| **Hash** | A fixed-length "fingerprint" of some text. It cannot be turned back into the text | 26 |
| **Iterations** | How many times a slow password hash is repeated (the "rounds"). We use 600,000 | 27 |
| **OWASP** | A respected worldwide group that publishes web security advice | 27 |
| **PBKDF2** | A slow password hashing method with salt. It is built into C# | 27 |
| **Salt** | A random value added to a password before hashing. Different for every user | 26 |
| **SHA-256** | A fast hash function. Good for fingerprints of files. **Not** enough for passwords by itself | 26 |

[🏠 Course Home](../README.md)
