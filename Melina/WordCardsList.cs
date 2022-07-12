namespace Melina
{
    internal class WordCardsList
    {
        public int Count { get; private set; }

        public List<WordCard> List { get; private set; }

        private string savePath;

        /// <summary>
        /// Creates a new word cards list from data file with given save path.
        /// </summary>
        /// <param name="filePath">Data file.</param>
        /// <param name="savePath">Path to save current list.</param>
        public WordCardsList(string filePath, string savePath)
        {
            List = new List<WordCard>();
            LoadWordCardsFromFile(filePath);
            Count = List.Count;
            this.savePath = savePath;
        }

        /// <summary>
        /// Creates a new epmty word cards list with given save path.
        /// </summary>
        /// <param name="savePath">Path to save current list.</param>
        public WordCardsList(string savePath)
        {
            List = new List<WordCard>();
            Count = List.Count;
            this.savePath = savePath;
        }

        private void LoadWordCardsFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var splittedLine = line.Split("\t", StringSplitOptions.None);
                var word = splittedLine[0];
                var translation = splittedLine[1];
                var creationDate = DateTime.Parse(splittedLine[2]);
                List.Add(new WordCard(word, translation, creationDate));
            }
        }

        private void SaveWordCardsToFile()
        {
            var textToFile = "";
            for (int i = 0; i < List.Count; i++)
            {
                textToFile += List[i].Word + "\t" + List[i].Translation + "\t" + List[i].CreationDate;
                if (i != List.Count - 1) textToFile += "\r\n";
            }

            File.WriteAllText(savePath, textToFile);
        }

        public void AddNewWordCard(string word, string translation)
        {
            var wordCardToAdd = new WordCard(word, translation);
            List.Add(wordCardToAdd);
            Count++;
            SaveWordCardsToFile();
        }

        public void EditWordCard(WordCard wordCard, string newWord, string newTranslation)
        {
            List[List.IndexOf(wordCard)] = new WordCard(newWord, newTranslation);
            SaveWordCardsToFile();
        }

        public void DeleteWordCard(WordCard wordCardToDelete)
        {
            List.Remove(wordCardToDelete);
            Count--;
            SaveWordCardsToFile();
        }
    }
}
