var wordCards = new List<WordCard>();
LoadWordCards();
PrintWordCards();

void LoadWordCards()
{
    var lines = File.ReadAllLines("words.txt");

    foreach (var line in lines)
    {
        var splittedLine = line.Split("\t", StringSplitOptions.None);
        var word = splittedLine[0];
        var translate = splittedLine[1];
        wordCards.Add(new WordCard(word, translate));
    }
}

void PrintWordCards()
{
    foreach (var wordCard in wordCards)
    {
        Console.WriteLine($"{wordCard.Word} {wordCard.Translation} {wordCard.CreationDate}");
    }
}
