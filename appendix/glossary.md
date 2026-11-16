# 📖 Glossary

Every new word in the course, in plain language, from A to Z. The lesson number shows where the word is first explained.

| Word | Meaning | Lesson |
|---|---|---|
| **`&&` (AND)** | Both conditions must be true | 05 |
| **`\|\|` (OR)** | At least one condition must be true | 05 |
| **`!` (NOT)** | Flips true to false, and false to true | 05 |
| **`??`** | "If the left side is nothing (`null`), use the right side instead." Used as `Console.ReadLine() ?? ""` | 03 |
| **Argon2** | A modern, slow password hashing method, like PBKDF2 | 17 |
| **Array** | A fixed-size group of values in one variable, like `string[] days` | 07 |
| **Base64** | A way to write bytes as normal text (letters, numbers, `+`, `/`, `=`) | 18 |
| **bcrypt** | A popular, slow password hashing method, like PBKDF2 | 17 |
| **Block** | Lines of code grouped inside `{ }` | 05 |
| **`bool`** | A type that holds only `true` or `false` | 02 |
| **`break`** | Leave a loop (or a `switch` case) right now | 05, 06 |
| **Build** | Turning your C# code into a program the computer can run. `dotnet run` does this for you | 01 |
| **Build error** | A mistake in how the code is written, found **before** the program runs | 10 |
| **Byte** | The smallest unit computers store: a number from 0 to 255 | 17 |
| **Call** | To use a method, like `PrintHeader("Hi");` | 08 |
| **camelCase** | Naming style for variables: first word lowercase, next words capitalized: `fullName` | 02 |
| **`catch`** | The block that runs if the code in `try` crashes | 10 |
| **`char`** | A single character, like `'a'` or `','`. Uses single quotes | 11, 19 |
| **Class** | A blueprint for making objects, like `Student` | 09 |
| **Column** | One kind of information in a database table, like `FullName` | 12 |
| **Command** | (Command Prompt) An instruction you type, like `dotnet run`. (Database) A SQL instruction sent from C# | 00, 13 |
| **Command Prompt** | A Windows window where you type commands | 00 |
| **Comment** | A note for humans, starting with `//`. The computer ignores it | 01 |
| **`Commit()`** | Saves all the changes in a transaction for real | 16 |
| **Condition** | Something that is either true or false, like `grade >= 75` | 05 |
| **Connection** | An open line between your program and a database | 13 |
| **Connection string** | Text that tells C# where the database is, like `"Data Source=school.db"` | 13 |
| **Console** | The text window where your program runs | 01 |
| **Console app** | A program that runs in the Command Prompt window | 01 |
| **`const`** | A value that never changes | 14 |
| **`continue`** | Skip the rest of this loop round and go to the next round | 06 |
| **CRUD** | Create, Read, Update, Delete: the four basic things systems do with data | 15 |
| **CSV** | "Comma-Separated Values". A text file where each line is a record and values are separated by commas | 11 |
| **Database** | A program that stores data in organized tables and finds it quickly | 12 |
| **`double`** | A type that holds numbers with a decimal point, like `89.5` | 02 |
| **Encryption** | Scrambling data so it can be unscrambled later with a key. **Not** for passwords | 17 |
| **Exception** | A crash: something unexpected happened while the program was running | 10 |
| **`ExecuteNonQuery()`** | Runs SQL that changes things (CREATE, INSERT, UPDATE, DELETE). Gives back how many rows changed | 13 |
| **`ExecuteReader()`** | Runs a SELECT and lets you read many rows, one at a time | 15 |
| **`ExecuteScalar()`** | Runs SQL that gives back one single value, like a count | 13 |
| **Extension** | An add-on for VS Code, like the C# extension | 00 |
| **`foreach`** | A loop that runs once for each item in a list or array | 07 |
| **`for`** | A loop that counts: `for (int i = 1; i <= 10; i++)` | 06 |
| **Hash** | A fixed-length "fingerprint" of some text. Cannot be turned back into the text | 17 |
| **Header** | The first line of a CSV file, with the column names | 16 |
| **Hexadecimal** | A way to write bytes with `0-9` and `A-F` | 17 |
| **`if` / `else`** | Run one block of code or another, depending on a condition | 05 |
| **Index** | The position of an item in a list or array. Starts at **0** | 07 |
| **`int`** | A type that holds whole numbers, like `18` | 02 |
| **Iterations** | How many times a slow password hash repeats. We use 600,000 | 18 |
| **`LIKE`** | SQL for "contains a pattern". `%` means any text | 15 |
| **List** | A group of values that can grow, like `List<string>` | 07 |
| **`long`** | Like `int`, but for much bigger whole numbers | 14 |
| **Loop** | Code that repeats | 06 |
| **Method** | A named block of code you can use again and again | 08 |
| **`new`** | Creates a new object: `new Student()` | 07, 09 |
| **`NOT NULL`** | A database rule: this column is required | 13 |
| **NuGet** | The online "app store" for C# packages | 12 |
| **`null`** | "Nothing". No value, no object | 03, 15 |
| **Object** | One real thing made from a class, like Juan's student record | 09 |
| **Operator** | A symbol that does something with values: `+`, `==`, `&&` | 04 |
| **OWASP** | A respected worldwide organization that publishes web security advice | 18 |
| **Package** | An extra toolbox you add to a project, like `Microsoft.Data.Sqlite` | 12 |
| **Parameter** | (Method) Information you give to a method. (SQL) A placeholder like `$name` that is filled in safely | 08, 14 |
| **Parse** | Turn text into a number: `int.Parse("18")` | 03 |
| **PascalCase** | Naming style for methods, classes, and properties: every word capitalized: `ComputeAverage` | 08 |
| **PBKDF2** | A slow, salted password hashing method built into .NET | 18 |
| **Primary key** | A column that is different for every row and identifies it, like `Id` | 12 |
| **`private`** | Only this class can use it | 15 |
| **Program** | A list of instructions the computer follows | 00 |
| **Project** | A folder that holds everything for one program | 01 |
| **Prompt** | (Command Prompt) The text before `>` showing your current folder. (Your program) The question you show before reading input | 00, 08 |
| **Property** | One piece of information in a class, like `FullName` | 09 |
| **`public`** | Any file can use it | 09 |
| **`return`** | Give back an answer from a method, and leave the method | 08 |
| **Row** | One record in a database table | 12 |
| **Salt** | A random value added to a password before hashing. Different for every user | 17 |
| **SDK** | Software Development Kit: the complete toolbox for building programs | 00 |
| **SHA-256** | A fast hash function. Good for fingerprints of files, **not** for passwords by itself | 17 |
| **SQL** | Structured Query Language: the language for talking to databases | 12 |
| **SQL injection** | An attack where a hacker types SQL code into an input box | 14 |
| **SQLite** | A database that lives in a single file. Nothing to install | 12 |
| **Statement** | One instruction, ending with `;` | 01 |
| **`static`** | Use it directly by its class name, without `new` | 15 |
| **`string`** | A type that holds text, like `"Juan"` | 02 |
| **String interpolation** | Putting variables inside text: `$"Hello {name}"` | 02 |
| **`switch`** | Pick one option from a list of exact values | 05 |
| **Table** | A collection of rows and columns in a database | 12 |
| **Transaction** | A group of database changes that are saved all together, or not at all | 16 |
| **`Trim()`** | Removes spaces from the start and end of text | 10 |
| **`try`** | The block of code that might crash | 10 |
| **`TryParse`** | Safely tries to turn text into a number, and says if it worked | 05 |
| **Type** | What kind of value a variable holds: `string`, `int`, `double`, `bool`, or a class | 02 |
| **`UNIQUE`** | A database rule: no two rows can have the same value in this column | 13 |
| **`using`** | (At the top of a file) Use tools from a package or library. (`using var`) Close this automatically when done | 13 |
| **Validate** | Check that input follows the rules before using it | 16, 19 |
| **Variable** | A labeled box that holds a value | 02 |
| **VS Code** | Visual Studio Code: a free program for writing code | 00 |
| **`void`** | A method that gives nothing back | 08 |
| **Warning** | A yellow message about a possible problem. The program still runs | 03 |
| **`WHERE`** | SQL for choosing which rows. **Always** use it with UPDATE and DELETE | 12 |
| **`while`** | A loop that repeats as long as a condition is true | 06 |

[🏠 Course Home](../README.md)
