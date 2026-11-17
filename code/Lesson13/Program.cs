// Lesson 13: Grade calculator organized with methods

// ----- Main program -----
PrintHeader("GRADE CALCULATOR");

double prelim = ReadNumber("Prelim grade: ");
double midterm = ReadNumber("Midterm grade: ");
double finals = ReadNumber("Finals grade: ");

double average = ComputeAverage(prelim, midterm, finals);
string remarks = GetRemarks(average);

Console.WriteLine();
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Remarks: {remarks}");


// ----- Methods -----

void PrintHeader(string title)
{
    Console.WriteLine("==============================");
    Console.WriteLine($"  {title}");
    Console.WriteLine("==============================");
}

double ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";

        if (double.TryParse(input, out double number))
        {
            return number;
        }

        Console.WriteLine("Please type a valid number.");
    }
}

double ComputeAverage(double a, double b, double c)
{
    double total = a + b + c;
    return total / 3;
}

string GetRemarks(double grade)
{
    if (grade >= 75)
    {
        return "PASSED";
    }

    return "FAILED";
}
