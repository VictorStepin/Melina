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
                    SaveWordCardsToFile("words.txt", wordCards);
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
                    SaveWordCardsToFile("words.txt", wordCards);
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
                    SaveWordCardsToFile("words.txt", wordCards);
                }

                pointerPosition = wordCards.Count - 1;
                break;
            case ConsoleKey.Backspace:
                isWordCardsMenuActive = false;
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
            case ConsoleKey.D1:
                RunAllCardsTest();
                break;
            case ConsoleKey.D2:
                RunRandomTenTest();
                break;
            case ConsoleKey.Backspace:
                isTestingMenuActive = false;
                break;
        }
    }
}

void UpdateTestingMenu()
{
    Console.Clear();

    Console.WriteLine("TESTING");
    Console.WriteLine();

    Console.WriteLine("1 - All Cards Test");
    Console.WriteLine("2 - Random Ten Test");
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

void SaveWordCardsToFile(string path, List<WordCard> wordCardsToSave)
{
    var textToFile = "";
    for (int i = 0; i < wordCardsToSave.Count; i++)
    {
        textToFile += wordCardsToSave[i].Word + "\t" + wordCardsToSave[i].Translation + "\t" + wordCardsToSave[i].CreationDate;
        if (i != wordCardsToSave.Count - 1) textToFile += "\r\n";
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

void RunAllCardsTest()
{
    var wordCardsToTest = Shuffle(wordCards);

    var incorrectAnsweredCards = new List<WordCard>();
    var correctAnswersCount = 0;
    var incorrectAnswersCount = 0;


    for (int i = 0; i < wordCardsToTest.Count; i++)
    {
        Console.Clear();

        Console.WriteLine("ALL CARDS TEST");
        Console.WriteLine();

        Console.WriteLine($"{wordCardsToTest.Count - i} words left");
        Console.WriteLine();

        Console.WriteLine($"Word: {wordCardsToTest[i].Word}");
        
        Console.Write("Press Enter to show translation");
        Console.ReadKey();

        Console.WriteLine($"Translation: {wordCardsToTest[i].Translation}");

        Console.Write("Is your answer right? (Y/N): ");
        var answerInput = Console.ReadKey();

        switch (answerInput.Key)
        {
            case ConsoleKey.Y:
                correctAnswersCount++;
                break;
            default:
                incorrectAnsweredCards.Add(wordCardsToTest[i]);
                incorrectAnswersCount++;
                break;
        }
    }

    Console.Clear();

    Console.WriteLine("ALL CARDS TEST");
    Console.WriteLine();

    Console.WriteLine("RESULT");
    Console.WriteLine();

    Console.WriteLine($"Cards tested: {wordCardsToTest.Count}");
    Console.WriteLine($"Correct: {correctAnswersCount}");
    Console.WriteLine($"Incorrect: {incorrectAnswersCount}");
    var percentage = (double)correctAnswersCount / wordCardsToTest.Count;
    Console.WriteLine($"Percentage: {percentage} %");
    Console.WriteLine();

    var iwcFilePath = "incorrect_cards.txt"; //iwc - incorrect word cards
    SaveWordCardsToFile(iwcFilePath, incorrectAnsweredCards);
    Console.WriteLine($"Incorrect word cards saved to {iwcFilePath}");
    Console.WriteLine();

    Console.WriteLine("Press any key to return testing menu...");
    Console.ReadKey();
}

void RunRandomTenTest()
{
    var shuffledOriginal = Shuffle(wordCards);
    
    var wordCardsToTest = new List<WordCard>();
    for (int i = 0; i < 10; i++)
    {
        wordCardsToTest.Add(shuffledOriginal[i]);
    }

    var incorrectAnsweredCards = new List<WordCard>();
    var correctAnswersCount = 0;
    var incorrectAnswersCount = 0;


    for (int i = 0; i < wordCardsToTest.Count; i++)
    {
        Console.Clear();

        Console.WriteLine("RANDOM TEN TEST");
        Console.WriteLine();

        Console.WriteLine($"{wordCardsToTest.Count - i} words left");
        Console.WriteLine();

        Console.WriteLine($"Word: {wordCardsToTest[i].Word}");

        Console.WriteLine("Press Enter to show translation");
        Console.ReadKey();

        Console.WriteLine($"Translation: {wordCardsToTest[i].Translation}");

        Console.Write("Is your answer right? (Y/N): ");
        var answerInput = Console.ReadKey();

        switch (answerInput.Key)
        {
            case ConsoleKey.Y:
                correctAnswersCount++;
                break;
            default:
                incorrectAnsweredCards.Add(wordCardsToTest[i]);
                incorrectAnswersCount++;
                break;
        }
    }

    Console.Clear();

    Console.WriteLine("RANDOM TEN TEST");
    Console.WriteLine();

    Console.WriteLine("RESULT");
    Console.WriteLine();

    Console.WriteLine($"Cards tested: {wordCardsToTest.Count}");
    Console.WriteLine($"Correct: {correctAnswersCount}");
    Console.WriteLine($"Incorrect: {incorrectAnswersCount}");
    var percentage = (double)correctAnswersCount / wordCardsToTest.Count;
    Console.WriteLine($"Percentage: {percentage} %");
    Console.WriteLine();

    var iwcFilePath = "incorrect_cards.txt"; //iwc - incorrect word cards
    SaveWordCardsToFile(iwcFilePath, incorrectAnsweredCards);
    Console.WriteLine($"Incorrect word cards saved to {iwcFilePath}");
    Console.WriteLine();

    Console.WriteLine("Press any key to return testing menu...");
    Console.ReadKey();
}

List<WordCard> Shuffle(List<WordCard> wordCards)
{
    var shuffledList = new List<WordCard>();
    for (int i = 0; i < wordCards.Count; i++)
    {
        shuffledList.Add(wordCards[i]);
    }

    for (int i = 0; i < shuffledList.Count; i++)
    {
        var randIndex = new Random().Next(0, shuffledList.Count);
        
        WordCard temp = shuffledList[randIndex];
        shuffledList[randIndex] = shuffledList[i];
        shuffledList[i] = temp;
    }

    return shuffledList;
}