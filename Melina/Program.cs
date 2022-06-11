var isProgramRunning = true;
var wordCards = new List<WordCard>();
LoadWordCardsFromFile("words.txt");

Console.WriteLine("1 - Print all word cards");
Console.WriteLine("2 - Add new word card");
Console.WriteLine("3 - Add word cards from file");
Console.WriteLine("4 - Delete word card");
Console.WriteLine("x - Exit");

while (isProgramRunning)
{
    var input = Console.ReadKey();

    Console.Clear();

    switch (input.Key)
    {
        case ConsoleKey.D1:
            PrintWordCards();
            break;
        case ConsoleKey.D2:
            Console.WriteLine("Enter a word/phrase: ");
            var word = Console.ReadLine();

            Console.WriteLine("Enter a translation: ");
            var translation = Console.ReadLine();

            AddNewWordCard(word, translation);
            SaveWordCardsToFile("words.txt");
            
            Console.WriteLine("The word card has been successfully added.");
            break;
        case ConsoleKey.D3:
            Console.Write("Enter a path: ");
            var path = Console.ReadLine();
            
            LoadWordCardsFromFile(path);
            SaveWordCardsToFile("words.txt");
            
            Console.Write("The new word cards have been successfully added.");
            break;
        case ConsoleKey.D4:
            Console.Write("Enter a word to delete: ");
            var wordToDelete = Console.ReadLine();
            
            DeleteWordCard(wordToDelete);
            SaveWordCardsToFile("words.txt");
            
            Console.WriteLine("The word card has been successfully deleted.");
            break;
        case ConsoleKey.X:
            isProgramRunning = false;
            break;
    }
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

void PrintWordCards()
{
    var rowNumber = 1;
    foreach (var wordCard in wordCards)
    {
        Console.WriteLine($"{rowNumber} {wordCard.Word} {wordCard.Translation} {wordCard.CreationDate}");
        rowNumber++;
    }
}

void AddNewWordCard(string word, string translation)
{
    wordCards.Add(new WordCard(word, translation));
}

void DeleteWordCard(string word)
{
    foreach (var wordCard in wordCards)
    {
        if (wordCard.Word == word)
        {
            wordCards.Remove(wordCard);
            return;
        }
    }

    Console.WriteLine("This word card cannot be found.");
}
