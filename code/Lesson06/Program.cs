// Lesson 6: Comparison, logical, and mixed operators

Console.Write("Prelim grade: ");
double prelim = double.Parse(Console.ReadLine() ?? "");

Console.Write("Midterm grade: ");
double midterm = double.Parse(Console.ReadLine() ?? "");

Console.Write("Finals grade: ");
double finals = double.Parse(Console.ReadLine() ?? "");

Console.Write("Number of absences: ");
int absences = int.Parse(Console.ReadLine() ?? "");

// Arithmetic operators: do the math
double average = (prelim + midterm + finals) / 3;

// Comparison operators: each answer is true or false
bool passed = average >= 75;
bool hasGoodAttendance = absences <= 3;

// Logical operators: combine the answers
bool canJoinHonors = average >= 90 && hasGoodAttendance;
bool needsHelp = average < 75 || absences > 10;

// Mixed operators: math, comparing, and combining in ONE line
bool getsScholarship = (prelim + midterm + finals) / 3 >= 85 && absences <= 3;

Console.WriteLine();
Console.WriteLine($"Average:                 {average:F2}");
Console.WriteLine($"Passed?                  {passed}");
Console.WriteLine($"Good attendance?         {hasGoodAttendance}");
Console.WriteLine($"Can join the honor roll? {canJoinHonors}");
Console.WriteLine($"Needs extra help?        {needsHelp}");
Console.WriteLine($"Gets a scholarship?      {getsScholarship}");
