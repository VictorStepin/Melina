internal class WordCard
{
    public string Word { get; set; }
    public string Translation { get; set; }
    public DateTime CreationDate { get; }

    public WordCard(string word, string translation)
    {
        Word = word;
        Translation = translation;
        CreationDate = DateTime.Now;
    }
}
