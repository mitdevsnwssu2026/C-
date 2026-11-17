// Lesson 16: Catching errors so the program does not crash

Console.Write("Enter a whole number to divide 100 by: ");
string input = Console.ReadLine() ?? "";

try
{
    int number = int.Parse(input);
    int result = 100 / number;
    Console.WriteLine($"100 / {number} = {result}");
}
catch (FormatException)
{
    Console.WriteLine("Oops! That was not a whole number.");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Oops! You cannot divide by zero.");
}
catch (Exception ex)
{
    Console.WriteLine($"Oops! Something else went wrong: {ex.Message}");
}

Console.WriteLine("The program is still running. Goodbye!");
