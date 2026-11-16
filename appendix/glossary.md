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
| **Tab** | The label at the top of the editor with a file's name. Click a tab to switch files | 01, 09 |
| **Solution Explorer** | The panel on the right that lists the files in your project | 01 |
| **Error List** | The panel at the bottom that lists the mistakes in your code | 01 |
| **Properties panel** | The panel under the Solution Explorer that shows the settings of the file you clicked | 16 |
| **Start Without Debugging** | The command that runs your program. Shortcut: **Ctrl + F5** | 01 |
| **Build** | Turning your C# code into a program the computer can run. Visual Studio does this every time you run | 01 |
| **NuGet** | The online "app store" for C# packages | 12 |
| **Package** | An extra toolbox you add to a project, like `Microsoft.Data.Sqlite` | 12 |
| **bin → Debug → net10.0** | The folder inside your project where your program runs, and where it saves its files | 11 |
| **Copy to Output Directory** | A file setting. **Copy if newer** puts a copy of the file next to your running program | 16 |

## Programming Words

| Word | Meaning | Lesson |
|---|---|---|
| **`&&` (AND)** | Both conditions must be true | 05 |
| **`\|\|` (OR)** | At least one condition must be true | 05 |
| **`!` (NOT)** | Flips true to false, and false to true | 05 |
| **`??`** | "If the left side is nothing, use the right side instead." Used as `Console.ReadLine() ?? ""` | 03 |
| **Array** | A fixed-size group of values in one variable, like `string[] days` | 07 |
| **`bool`** | A type that holds only `true` or `false` | 02 |
| **`break`** | Leave a loop (or a `switch` case) right now | 05, 06 |
| **Byte** | The smallest unit a computer stores: a number from 0 to 255 | 17 |
| **Call** | To use a method, like `PrintHeader("Hi");` | 08 |
| **camelCase** | The naming style for variables: `fullName`, `yearLevel` | 02 |
| **`catch`** | The part that runs if the code in `try` crashes | 10 |
| **`char`** | A single character, like `'a'` or `','`. It uses single quotes | 11, 19 |
| **Class** | A blueprint for making objects, like `Student`. A blank form | 09 |
| **Comment** | A note for people, starting with `//`. The computer skips it | 01 |
| **Condition** | Something that is either true or false, like `grade >= 75` | 05 |
| **`const`** | A value that never changes | 14 |
| **`continue`** | Skip the rest of this loop round, and start the next round | 06 |
| **Crash** | When a running program stops because something unexpected happened | 03, 10 |
| **CSV** | "Comma-Separated Values". A text file with one record per line, and commas between the values | 11 |
| **`double`** | A type that holds numbers with a decimal point, like `89.5` | 02 |
| **Exception** | The C# word for a crash | 10 |
| **`for`** | A loop that counts: `for (int i = 1; i <= 10; i++)` | 06 |
| **`foreach`** | A loop that runs once for each item in a list or array | 07 |
| **Header** | The first line of a CSV file, with the names of the columns | 16 |
| **`if` / `else`** | Run one part of the code or another, depending on a condition | 05 |
| **Indentation** | The spaces at the start of a line that show which lines belong together. Visual Studio adds them for you | 05 |
| **Index** | The position number of an item in a list or array. It starts at **0** | 07 |
| **Infinite loop** | A loop that never stops, because its condition never becomes false | 06 |
| **`int`** | A type that holds whole numbers, like `18` | 02 |
| **List** | A group of values that can grow, like `List<string>` | 07 |
| **`long`** | Like `int`, but for much bigger whole numbers | 14 |
| **Loop** | Code that repeats | 06 |
| **Method** | A named block of code you can use again and again | 08 |
| **`new`** | Makes a new object: `new Student()` | 07, 09 |
| **`null`** | "Nothing". No value, no object | 03, 15 |
| **Object** | One real thing made from a class, like Juan's student record. A filled-in form | 09 |
| **Operator** | A symbol that does something with values: `+`, `==`, `&&` | 04 |
| **Parameter** | (Method) Information you give to a method. (SQL) A placeholder like `$name` that is filled in safely | 08, 14 |
| **Parse** | Turn text into a number: `int.Parse("18")` | 03 |
| **`private`** | Only this class can use it | 15 |
| **Program** | A list of instructions that a computer follows | 00 |
| **Property** | One piece of information in a class, like `FullName`. One blank on the form | 09 |
| **`public`** | Any file can use it | 09 |
| **`return`** | Give back an answer from a method, and leave the method | 08 |
| **`static`** | Use it directly by its class name, without `new` | 15 |
| **`string`** | A type that holds text, like `"Juan"` | 02 |
| **`switch`** | Pick one option from a list of exact values | 05 |
| **`Trim()`** | Removes spaces from the start and end of text | 10 |
| **`try`** | The part of the code that might crash | 10 |
| **`TryParse`** | Safely tries to turn text into a number, and tells you if it worked | 05 |
| **Type** | The kind of value a variable holds: `string`, `int`, `double`, `bool`, or a class | 02 |
| **`using`** | (At the top of a file) Use the tools from a package. (`using var`) Close this automatically when done | 13 |
| **Validate** | Check that something follows the rules before using it | 16, 19 |
| **Variable** | A box with a label that holds a value | 02 |
| **`void`** | A method that gives nothing back | 08 |
| **Warning** | A green wavy line: a possible problem. The program still runs | 03 |
| **`while`** | A loop that repeats as long as a condition is true | 06 |

## Database Words

| Word | Meaning | Lesson |
|---|---|---|
| **Column** | One kind of information in a table, like `FullName` | 12 |
| **`Commit()`** | Saves all the changes of a transaction for real | 16 |
| **Connection** | An open line between your program and a database | 13 |
| **Connection string** | Text that says where the database is, like `"Data Source=school.db"` | 13 |
| **CRUD** | Create, Read, Update, Delete: the four things systems do with data | 15 |
| **Database** | A program that stores data in organized tables and finds it quickly | 12 |
| **`ExecuteNonQuery()`** | Runs SQL that changes things. Gives back how many rows changed | 13 |
| **`ExecuteReader()`** | Runs a `SELECT` and lets you read many rows, one at a time | 15 |
| **`ExecuteScalar()`** | Runs SQL that gives back one single value, like a count | 13 |
| **`INSERT OR IGNORE`** | Add a row, but skip it quietly if it breaks a rule | 13, 16 |
| **`LIKE`** | SQL for "contains this pattern". `%` means any text | 15 |
| **`NOT NULL`** | A rule: this column is required | 12 |
| **Primary key** | A column that is different for every row and identifies it, like `Id` | 12 |
| **Row** | One record in a table | 12 |
| **SQL** | The language for talking to databases | 12 |
| **SQL injection** | An attack where a hacker types SQL code into an input box | 14 |
| **SQLite** | A database that lives in one single file | 12 |
| **Table** | A collection of rows and columns in a database | 12 |
| **Transaction** | A group of changes that are saved all together, or not at all | 16 |
| **`UNIQUE`** | A rule: no two rows can have the same value in this column | 13 |
| **`WHERE`** | SQL for choosing which rows. **Always** use it with `UPDATE` and `DELETE` | 12 |

## Security Words

| Word | Meaning | Lesson |
|---|---|---|
| **Argon2** | A modern, slow password hashing method, like PBKDF2 | 17 |
| **Base64** | A way to write bytes as normal text (letters, numbers, `+`, `/`, `=`) | 18 |
| **bcrypt** | A popular, slow password hashing method, like PBKDF2 | 17 |
| **Encryption** | Scrambling data so it can be unscrambled later with a key. **Not** for passwords | 17 |
| **Hash** | A fixed-length "fingerprint" of some text. It cannot be turned back into the text | 17 |
| **Iterations** | How many times a slow password hash is repeated (the "rounds"). We use 600,000 | 18 |
| **OWASP** | A respected worldwide group that publishes web security advice | 18 |
| **PBKDF2** | A slow password hashing method with salt. It is built into C# | 18 |
| **Salt** | A random value added to a password before hashing. Different for every user | 17 |
| **SHA-256** | A fast hash function. Good for fingerprints of files. **Not** enough for passwords by itself | 17 |

[🏠 Course Home](../README.md)
