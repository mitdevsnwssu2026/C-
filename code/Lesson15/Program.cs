// Lesson 15: Enums - a milk tea order

Console.WriteLine("===== MILK TEA SHOP =====");
Console.WriteLine("1 - Small");
Console.WriteLine("2 - Medium");
Console.WriteLine("3 - Large");
Console.Write("Choose a size: ");
string choice = Console.ReadLine() ?? "";

CupSize size;

switch (choice)
{
    case "1":
        size = CupSize.Small;
        break;

    case "2":
        size = CupSize.Medium;
        break;

    case "3":
        size = CupSize.Large;
        break;

    default:
        Console.WriteLine("Invalid choice. We will give you a Medium.");
        size = CupSize.Medium;
        break;
}

int price = GetPrice(size);

Console.WriteLine();
Console.WriteLine($"Size:  {size}");
Console.WriteLine($"Price: {price} pesos");

if (size == CupSize.Large)
{
    Console.WriteLine("Good choice! Large comes with free pearls today.");
}


// ----- Methods -----

int GetPrice(CupSize cupSize)
{
    if (cupSize == CupSize.Small)
    {
        return 39;
    }

    if (cupSize == CupSize.Medium)
    {
        return 49;
    }

    return 59;
}


// ----- Enums (always at the very bottom of the file) -----

enum CupSize
{
    Small,
    Medium,
    Large
}
