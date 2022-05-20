internal class WordCard
{
    public string Word { get; }
    public string Translation { get; }

    public WordCard(string word, string translation)
    {
        Word = word;
        Translation = translation;
    }
}
