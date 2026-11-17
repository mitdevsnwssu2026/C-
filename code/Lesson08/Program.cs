// Lesson 8: Conditional Challenge - the movie ticket booth

Console.WriteLine("===== MOVIE TICKET BOOTH =====");
Console.Write("How old are you? ");
bool isNumber = int.TryParse(Console.ReadLine() ?? "", out int age);

if (!isNumber || age < 0)
{
    Console.WriteLine("That is not a valid age.");
}
else if (age <= 6)
{
    Console.WriteLine("Children 6 and below watch for FREE. Enjoy the movie!");
}
else
{
    // 1. Find the ticket price (a nested if)
    int price;

    if (age >= 60)
    {
        price = 120;
        Console.WriteLine("Senior citizen price: 120 pesos");
    }
    else
    {
        Console.Write("Are you a student? (yes/no) ");
        string answer = (Console.ReadLine() ?? "").Trim().ToLower();

        if (answer == "yes")
        {
            price = 150;
            Console.WriteLine("Student price: 150 pesos");
        }
        else
        {
            price = 200;
            Console.WriteLine("Regular price: 200 pesos");
        }
    }

    // 2. Add the price of the seat (a switch)
    Console.WriteLine();
    Console.WriteLine("1 - Regular seat (no extra charge)");
    Console.WriteLine("2 - Premium seat (+50 pesos)");
    Console.WriteLine("3 - VIP seat (+100 pesos)");
    Console.Write("Choose your seat: ");
    string seat = Console.ReadLine() ?? "";

    switch (seat)
    {
        case "1":
            break;

        case "2":
            price += 50;
            break;

        case "3":
            price += 100;
            break;

        default:
            Console.WriteLine("Unknown seat. You get a Regular seat.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine($"Please pay: {price} pesos. Enjoy the movie!");
}
