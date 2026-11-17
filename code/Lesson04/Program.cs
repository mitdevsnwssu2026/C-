// Lesson 4: Working with text (string manipulation)

Console.Write("Type your full name: ");
string fullName = Console.ReadLine() ?? "";

// 1. Remove extra spaces at the start and at the end
string cleanName = fullName.Trim();

// 2. Count the characters
int length = cleanName.Length;

// 3. Change the letters
string upperName = cleanName.ToUpper();
string lowerName = cleanName.ToLower();

// 4. Ask questions about the text (the answer is true or false)
bool hasSpace = cleanName.Contains(" ");
bool startsWithJ = cleanName.StartsWith("J");

// 5. Take a piece of the text
string firstThree = cleanName.Substring(0, 3);

// 6. Replace part of the text
string dashedName = cleanName.Replace(" ", "-");

Console.WriteLine();
Console.WriteLine($"Clean name:      {cleanName}");
Console.WriteLine($"Characters:      {length}");
Console.WriteLine($"CAPITAL letters: {upperName}");
Console.WriteLine($"small letters:   {lowerName}");
Console.WriteLine($"Has a space?     {hasSpace}");
Console.WriteLine($"Starts with J?   {startsWithJ}");
Console.WriteLine($"First 3 letters: {firstThree}");
Console.WriteLine($"With dashes:     {dashedName}");
