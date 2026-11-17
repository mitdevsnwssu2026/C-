// Lesson 17: Saving students to a CSV file so they are not forgotten

string filePath = "students.csv";

Console.WriteLine("=== SAVE A STUDENT ===");

Console.Write("Student number: ");
string studentNumber = (Console.ReadLine() ?? "").Trim();

Console.Write("Full name: ");
string fullName = (Console.ReadLine() ?? "").Trim();

Console.Write("Course: ");
string course = (Console.ReadLine() ?? "").Trim();

// Join the values with commas to make one line of text
string line = $"{studentNumber},{fullName},{course}";

// Add the line to the end of the file (the file is created if it does not exist)
File.AppendAllText(filePath, line + Environment.NewLine);
Console.WriteLine("Saved to students.csv!");

// Read every line back from the file
Console.WriteLine();
Console.WriteLine("=== EVERYONE IN THE FILE ===");

string[] lines = File.ReadAllLines(filePath);

foreach (string savedLine in lines)
{
    string[] parts = savedLine.Split(',');
    Console.WriteLine($"Number: {parts[0]} | Name: {parts[1]} | Course: {parts[2]}");
}

Console.WriteLine($"Total saved: {lines.Length}");
