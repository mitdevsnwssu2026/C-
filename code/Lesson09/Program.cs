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
