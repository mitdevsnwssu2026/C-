// Lesson 21: Student Records Manager (Create, Read, Update, Delete)

StudentDatabase.CreateTable();

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("========= STUDENT RECORDS =========");
    Console.WriteLine("1 - Add a student");
    Console.WriteLine("2 - Show all students");
    Console.WriteLine("3 - Search by name");
    Console.WriteLine("4 - Update a student");
    Console.WriteLine("5 - Delete a student");
    Console.WriteLine("6 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddStudentScreen();
            break;
        case "2":
            ShowStudents(StudentDatabase.GetAllStudents());
            break;
        case "3":
            string keyword = ReadRequired("Type part of the name: ");
            ShowStudents(StudentDatabase.SearchByName(keyword));
            break;
        case "4":
            UpdateStudentScreen();
            break;
        case "5":
            DeleteStudentScreen();
            break;
        case "6":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Please type a number from 1 to 6.");
            break;
    }
}


// ----- Screens -----

void AddStudentScreen()
{
    Console.WriteLine("--- ADD A STUDENT ---");
    string studentNumber = ReadRequired("Student number: ");

    if (StudentDatabase.StudentNumberExists(studentNumber))
    {
        Console.WriteLine("That student number is already saved.");
        return;
    }

    Student student = new Student
    {
        StudentNumber = studentNumber,
        FullName = ReadRequired("Full name: "),
        Course = ReadRequired("Course: "),
        YearLevel = ReadYearLevel()
    };

    StudentDatabase.AddStudent(student);
    Console.WriteLine($"Saved! {student.FullName} was added.");
}

void ShowStudents(List<Student> students)
{
    if (students.Count == 0)
    {
        Console.WriteLine("No students found.");
        return;
    }

    Console.WriteLine($"{"NUMBER",-12}{"FULL NAME",-25}{"COURSE",-8}{"YEAR"}");
    Console.WriteLine(new string('-', 49));

    foreach (Student s in students)
    {
        Console.WriteLine($"{s.StudentNumber,-12}{s.FullName,-25}{s.Course,-8}{s.YearLevel}");
    }

    Console.WriteLine($"({students.Count} student(s))");
}

void UpdateStudentScreen()
{
    Console.WriteLine("--- UPDATE A STUDENT ---");
    string studentNumber = ReadRequired("Student number to update: ");
    Student? student = StudentDatabase.FindByStudentNumber(studentNumber);

    if (student == null)
    {
        Console.WriteLine("No student has that student number.");
        return;
    }

    Console.WriteLine($"Current: {student.FullName}, {student.Course}, Year {student.YearLevel}");
    Console.WriteLine("Type the new values:");

    student.FullName = ReadRequired("Full name: ");
    student.Course = ReadRequired("Course: ");
    student.YearLevel = ReadYearLevel();

    if (StudentDatabase.UpdateStudent(student))
    {
        Console.WriteLine("Student updated!");
    }
}

void DeleteStudentScreen()
{
    Console.WriteLine("--- DELETE A STUDENT ---");
    string studentNumber = ReadRequired("Student number to delete: ");
    Student? student = StudentDatabase.FindByStudentNumber(studentNumber);

    if (student == null)
    {
        Console.WriteLine("No student has that student number.");
        return;
    }

    Console.Write($"Delete {student.FullName}? Type YES to confirm: ");
    string answer = Console.ReadLine() ?? "";

    if (answer == "YES" && StudentDatabase.DeleteStudent(student.Id))
    {
        Console.WriteLine("Student deleted.");
    }
    else
    {
        Console.WriteLine("Nothing was deleted.");
    }
}


// ----- Input methods -----

string ReadRequired(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = (Console.ReadLine() ?? "").Trim();

        if (input != "")
        {
            return input;
        }

        Console.WriteLine("This field is required. Please type something.");
    }
}

int ReadYearLevel()
{
    while (true)
    {
        Console.Write("Year level (1-5): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int yearLevel) && yearLevel >= 1 && yearLevel <= 5)
        {
            return yearLevel;
        }

        Console.WriteLine("Please type a whole number from 1 to 5.");
    }
}
