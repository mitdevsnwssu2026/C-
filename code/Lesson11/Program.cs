// Lesson 11: Arrays - a quiz score report

int[] scores = { 85, 92, 78, 95, 88 };

// Array access: get items by their index
Console.WriteLine($"First score: {scores[0]}");
Console.WriteLine($"Last score:  {scores[scores.Length - 1]}");

// Array iteration: visit every item
Console.WriteLine();
Console.WriteLine("All scores:");

for (int i = 0; i < scores.Length; i++)
{
    Console.WriteLine($"  Quiz {i + 1}: {scores[i]}");
}

// Array operations: total, average, highest, lowest
double total = 0;
int highest = scores[0];
int lowest = scores[0];

foreach (int score in scores)
{
    total += score;

    if (score > highest)
    {
        highest = score;
    }

    if (score < lowest)
    {
        lowest = score;
    }
}

double average = total / scores.Length;

Console.WriteLine();
Console.WriteLine($"Total:   {total}");
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Highest: {highest}");
Console.WriteLine($"Lowest:  {lowest}");

// Array operations: sort from lowest to highest
Array.Sort(scores);

Console.WriteLine();
Console.Write("Sorted:  ");

foreach (int score in scores)
{
    Console.Write($"{score} ");
}

Console.WriteLine();
