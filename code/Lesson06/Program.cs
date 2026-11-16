// Lesson 6: A menu that repeats until the user chooses Exit

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("===== MAIN MENU =====");
    Console.WriteLine("1 - Multiplication table");
    Console.WriteLine("2 - Countdown");
    Console.WriteLine("3 - Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Which number? ");
            int number = int.Parse(Console.ReadLine() ?? "");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{number} x {i} = {number * i}");
            }
            break;

        case "2":
            for (int count = 5; count >= 1; count--)
            {
                Console.WriteLine(count);
            }
            Console.WriteLine("Liftoff!");
            break;

        case "3":
            running = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please type 1, 2, or 3.");
            break;
    }
}
