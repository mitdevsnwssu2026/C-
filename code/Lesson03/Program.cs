// Lesson 3: Talking with the user

Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";

Console.Write("How old are you? ");
string ageText = Console.ReadLine() ?? "";
int age = int.Parse(ageText);

int ageInFiveYears = age + 5;

Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"In 5 years, you will be {ageInFiveYears} years old.");
