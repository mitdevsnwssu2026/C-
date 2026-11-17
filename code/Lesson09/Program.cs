// Lesson 9: The three loops working together (do-while, for, while)

// 1. do-while: keep asking until the answer is valid
int number;

do
{
    Console.Write("Type a number from 1 to 10: ");
    number = int.Parse(Console.ReadLine() ?? "");
}
while (number < 1 || number > 10);

// 2. for: repeat exactly 10 times
Console.WriteLine();
Console.WriteLine($"The multiplication table of {number}:");

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{number} x {i} = {number * i}");
}

// 3. while: repeat until something changes
Console.WriteLine();
Console.WriteLine($"Counting down from {number}:");

int count = number;

while (count >= 1)
{
    Console.WriteLine(count);
    count--;
}

Console.WriteLine("Liftoff!");
