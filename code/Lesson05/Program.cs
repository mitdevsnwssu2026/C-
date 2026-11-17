// Lesson 5: Grade average calculator

Console.WriteLine("=== GRADE AVERAGE CALCULATOR ===");

Console.Write("Enter your Prelim grade: ");
double prelim = double.Parse(Console.ReadLine() ?? "");

Console.Write("Enter your Midterm grade: ");
double midterm = double.Parse(Console.ReadLine() ?? "");

Console.Write("Enter your Finals grade: ");
double finals = double.Parse(Console.ReadLine() ?? "");

double total = prelim + midterm + finals;
double average = total / 3;

Console.WriteLine();
Console.WriteLine($"Total:   {total}");
Console.WriteLine($"Average: {average:F2}");
