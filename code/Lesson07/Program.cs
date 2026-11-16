// Lesson 7: Collecting many names in a List

List<string> names = new List<string>();

Console.WriteLine("Type the names of your classmates.");
Console.WriteLine("When you are done, just press Enter on an empty line.");

while (true)
{
    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";

    if (name == "")
    {
        break;
    }

    names.Add(name);
}

Console.WriteLine();
Console.WriteLine($"You entered {names.Count} name(s):");

for (int i = 0; i < names.Count; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}
