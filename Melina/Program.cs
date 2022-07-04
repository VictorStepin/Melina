using Melina;

WordCardsList wordCards = new WordCardsList("Words.txt");

var isProgramRunning = true;
while (isProgramRunning)
{
    UpdateMainMenu();
    
    var input = Console.ReadKey();

    switch (input.Key)
    {
        case ConsoleKey.D1:
            new WordCardsManagementMenu(wordCards).Run();
            break;
        case ConsoleKey.D2:
            new TestingMenu(wordCards).Run();
            break;
        case ConsoleKey.X:
            Console.Clear();
            isProgramRunning = false;
            break;
        default:
            UpdateMainMenu();
            break;
    }
}

void UpdateMainMenu()
{
    Console.Clear();

    Console.WriteLine("MELINA");
    Console.WriteLine();

    Console.WriteLine("1 - Word Cards Management");
    Console.WriteLine("2 - Testing");
    Console.WriteLine("X - Exit");
}
