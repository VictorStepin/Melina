var wordCards = new List<WordCard>();
LoadWordCardsFromFile("words.txt");

var isProgramRunning = true;
while (isProgramRunning)
{
    UpdateMainMenu();
    
    var input = Console.ReadKey();

    switch (input.Key)
    {
        case ConsoleKey.D1:
            RunWordCardsManagementMenu();
            break;
        case ConsoleKey.D2:
            RunTestingMenu();
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

void RunWordCardsManagementMenu()
{
    var isWordCardsMenuActive = true;
    var pointerPosition = wordCards.Count - 1;

    while (isWordCardsMenuActive)
    {
        UpdateWordCardsManagementMenu(pointerPosition);

        var input = Console.ReadKey();

        switch (input.Key)
        {
            case ConsoleKey.DownArrow:
                if (pointerPosition < wordCards.Count - 1)
                {
                    pointerPosition++;
                }
                break;
            case ConsoleKey.UpArrow:
                if (pointerPosition > 0)
                {
                    pointerPosition--;
                }
                break;
            case ConsoleKey.D1:
                Console.Clear();
                
                Console.Write("Enter a word: ");
                var word = Console.ReadLine();

                Console.Write("Enter a translation: ");
                var translation = Console.ReadLine();

                Console.Write("Confirm adding (Y/N): ");
                var confirmAddingInput = Console.ReadKey();

                if (confirmAddingInput.Key == ConsoleKey.Y)
                {
                    AddNewWordCard(word, translation);
                    SaveWordCardsToFile("words.txt");
                }

                pointerPosition = wordCards.Count - 1;
                break;
            case ConsoleKey.D2:
                var wordCardToEdit = wordCards[pointerPosition];


                Console.Clear();

                Console.WriteLine($"Editing {wordCardToEdit.Word} {wordCardToEdit.Translation}");

                Console.Write("Enter a new word: ");
                var newWord = Console.ReadLine();

                Console.Write("Enter a new Translation: ");
                var newTranslation = Console.ReadLine();

                Console.Write("Confirm editing (Y/N): ");
                var confirmEditingInput = Console.ReadKey();

                if (confirmEditingInput.Key == ConsoleKey.Y)
                {
                    EditWordCard(wordCardToEdit, newWord, newTranslation);
                    SaveWordCardsToFile("words.txt");
                }

                pointerPosition = wordCards.Count - 1;
                break;
            case ConsoleKey.D3:
                var wordCardToDelete = wordCards[pointerPosition];

                Console.Clear();

                Console.Write($"Confirm deleting \"{wordCardToDelete.Word}\" (Y/N): ");
                var confirmDeletingInput = Console.ReadKey();

                if (confirmDeletingInput.Key == ConsoleKey.Y)
                {
                    DeleteWordCard(wordCardToDelete);
                    SaveWordCardsToFile("words.txt");
                }

                pointerPosition = wordCards.Count - 1;
                break;
            case ConsoleKey.Backspace:
                isWordCardsMenuActive = false;
                break;
            default:
                UpdateWordCardsManagementMenu(pointerPosition);
                break;
        }
    }
}

void UpdateWordCardsManagementMenu(int pointerPosition)
{
    Console.Clear();

    Console.WriteLine("WORD CARDS");
    Console.WriteLine();

    string border = "+--------------------------------------------------------------------------------+";

    // Рисуем верхнюю границу
    Console.WriteLine(border);

    // Рисуем строки области карточек
    var wcAreaLinesCount = 20;
    for (int i = 0; i < wcAreaLinesCount; i++)
    {
        var content = "|";

        // Переключение указателя по строкам
        if (pointerPosition < wcAreaLinesCount - 1)
        {
            if (i == pointerPosition) content += "->";
            else content += "  ";
        }
        else
        {
            if (i == wcAreaLinesCount - 1) content += "->";
            else content += "  ";
        }

        // Заполнение содержательной частью
        WordCard wordCard;
        if (pointerPosition < wcAreaLinesCount)
        {
            wordCard = wordCards[i];
        }
        else
        {
            wordCard = wordCards[pointerPosition - wcAreaLinesCount + 1 + i];
        }
        content += $"{wordCard.Word} {wordCard.Translation}";

        // заполняем оставшуюся часть строки пробелами
        for (int j = 0; j < border.Length - content.Length + j - 1; j++)
        {
            content += " ";
        }

        content += "|";

        Console.WriteLine(content);
    }

    // Рисуем нижнюю границу и выводим количество карточек
    Console.WriteLine( $"{border} Word Cards Count: {wordCards.Count}");

    Console.WriteLine("1 - Add");
    Console.WriteLine("2 - Edit");
    Console.WriteLine("3 - Delete");
    Console.WriteLine("Backspace - Back");
}

void RunTestingMenu()
{
    var isTestingMenuActive = true;
    while (isTestingMenuActive)
    {
        UpdateTestingMenu();

        var input = Console.ReadKey();

        switch (input.Key)
        {
            case ConsoleKey.Backspace:
                isTestingMenuActive = false;
                break;
            default:
                UpdateTestingMenu();
                break;
        }
    }
}

void UpdateTestingMenu()
{
    Console.Clear();

    Console.WriteLine("IN DEVELOPMENT...");
    Console.WriteLine();

    Console.WriteLine("Backspace - Back");
}

void LoadWordCardsFromFile(string path)
{
    var lines = File.ReadAllLines(path);
    foreach (var line in lines)
    {
        var splittedLine = line.Split("\t", StringSplitOptions.None);
        var word = splittedLine[0];
        var translation = splittedLine[1];
        var creationDate = DateTime.Parse(splittedLine[2]);
        wordCards.Add(new WordCard(word, translation, creationDate));
    }
}

void SaveWordCardsToFile(string path)
{
    var textToFile = "";
    for (int i = 0; i < wordCards.Count; i++)
    {
        textToFile += wordCards[i].Word + "\t" + wordCards[i].Translation + "\t" + wordCards[i].CreationDate;
        if (i != wordCards.Count - 1) textToFile += "\r\n";
    }

    File.WriteAllText(path, textToFile);
}

void AddNewWordCard(string word, string translation)
{
    var wordCardToAdd = new WordCard(word, translation);
    wordCards.Add(wordCardToAdd);
}

void EditWordCard(WordCard wordCard, string newWord, string newTranslation)
{
    wordCards[wordCards.IndexOf(wordCard)] = new WordCard(newWord, newTranslation);
}

void DeleteWordCard(WordCard wordCardToDelete)
{
    wordCards.Remove(wordCardToDelete);
}
