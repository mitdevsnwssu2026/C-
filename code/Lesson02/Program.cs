// Lesson 2: A student profile card using variables

string fullName = "Juan Dela Cruz";
int age = 18;
double height = 1.65;
bool isEnrolled = true;

Console.WriteLine("===== STUDENT PROFILE =====");
Console.WriteLine($"Name:     {fullName}");
Console.WriteLine($"Age:      {age}");
Console.WriteLine($"Height:   {height} meters");
Console.WriteLine($"Enrolled: {isEnrolled}");
Console.WriteLine("===========================");

// A variable's value can change
age = 19;
Console.WriteLine($"Next year, {fullName} will be {age} years old.");
