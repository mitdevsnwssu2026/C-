// Lesson 5: Grade remarks using if / else if / else

Console.Write("Enter your final grade (0-100): ");
string input = Console.ReadLine() ?? "";

bool isNumber = double.TryParse(input, out double grade);

if (!isNumber)
{
    Console.WriteLine("That is not a number. Please run the program again.");
}
else if (grade < 0 || grade > 100)
{
    Console.WriteLine("The grade must be from 0 to 100.");
}
else if (grade >= 90)
{
    Console.WriteLine("Remarks: Excellent! You PASSED.");
}
else if (grade >= 75)
{
    Console.WriteLine("Remarks: Good job! You PASSED.");
}
else
{
    Console.WriteLine("Remarks: Sorry, you FAILED. Keep trying!");
}
