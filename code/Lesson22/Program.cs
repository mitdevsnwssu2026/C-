// Lesson 22: Uploading many students from a CSV file into the database

StudentDatabase.CreateTable();

Console.WriteLine("===== IMPORT STUDENTS FROM A CSV FILE =====");
Console.Write("File name (just press Enter to use students.csv): ");
string fileName = (Console.ReadLine() ?? "").Trim();

if (fileName == "")
{
    fileName = "students.csv";
}

if (!File.Exists(fileName))
{
    Console.WriteLine($"File not found: {fileName}");
    Console.WriteLine("Make sure the file is inside your project folder.");
    return;
}

// ----- Step 1: Read and check every line -----
string[] lines = File.ReadAllLines(fileName);
List<Student> validStudents = new List<Student>();
List<string> problems = new List<string>();
int rowsChecked = 0;

// i starts at 1 to skip line 0, which is the header row
for (int i = 1; i < lines.Length; i++)
{
    int lineNumber = i + 1; // people count lines starting from 1
    string line = lines[i].Trim();

    if (line == "")
    {
        continue; // skip empty lines
    }

    rowsChecked++;
    string[] parts = line.Split(',');

    if (parts.Length != 4)
    {
        problems.Add($"Line {lineNumber}: needs 4 values but has {parts.Length}.");
        continue;
    }

    string studentNumber = parts[0].Trim();
    string fullName = parts[1].Trim();
    string course = parts[2].Trim();
    string yearText = parts[3].Trim();

    if (studentNumber == "" || fullName == "" || course == "")
    {
        problems.Add($"Line {lineNumber}: student number, full name, and course are required.");
        continue;
    }

    bool isNumber = int.TryParse(yearText, out int yearLevel);

    if (!isNumber || yearLevel < 1 || yearLevel > 5)
    {
        problems.Add($"Line {lineNumber}: year level must be a number from 1 to 5.");
        continue;
    }

    validStudents.Add(new Student
    {
        StudentNumber = studentNumber,
        FullName = fullName,
        Course = course,
        YearLevel = yearLevel
    });
}

// ----- Step 2: Show a report before saving -----
Console.WriteLine();
Console.WriteLine($"Rows checked:       {rowsChecked}");
Console.WriteLine($"Valid rows:         {validStudents.Count}");
Console.WriteLine($"Rows with problems: {problems.Count}");

foreach (string problem in problems)
{
    Console.WriteLine($"  - {problem}");
}

if (validStudents.Count == 0)
{
    Console.WriteLine("There is nothing to import.");
    return;
}

// ----- Step 3: Ask before saving -----
Console.WriteLine();
Console.Write($"Upload the {validStudents.Count} valid row(s) to the database? (Y/N): ");
string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

if (answer != "Y")
{
    Console.WriteLine("Upload cancelled. Nothing was saved.");
    return;
}

// ----- Step 4: Save everything in one transaction -----
int addedCount = StudentDatabase.ImportStudents(validStudents);
int skippedCount = validStudents.Count - addedCount;

Console.WriteLine($"Done! {addedCount} student(s) added.");

if (skippedCount > 0)
{
    Console.WriteLine($"{skippedCount} student(s) skipped because the student number was already saved.");
}

// ----- Step 5: Show what is now in the database -----
Console.WriteLine();
Console.WriteLine("===== STUDENTS IN THE DATABASE =====");

foreach (Student s in StudentDatabase.GetAllStudents())
{
    Console.WriteLine($"{s.StudentNumber,-12}{s.FullName,-25}{s.Course,-8}{s.YearLevel}");
}
