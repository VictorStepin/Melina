namespace Melina
{
    internal class WordCardsList
    {
        public int Count { get; private set; }

        public List<WordCard> List { get; private set; }

        /// <summary>
        /// Creates a new word cards list from data file.
        /// </summary>
        /// <param name="filePath">Data file.</param>
        public WordCardsList(string filePath)
        {
            List = new List<WordCard>();
            LoadWordCardsFromFile(filePath);
            Count = List.Count;
        }

        /// <summary>
        /// Creates a new word cards list from existing List object.
        /// </summary>
        /// <param name="list">List with data.</param>
        public WordCardsList(List<WordCard> list)
        {
            List = list;
            Count = List.Count;
        }

        /// <summary>
        /// Creates a new epmty word cards list.
        /// </summary>
        public WordCardsList()
        {
            List = new List<WordCard>();
            Count = List.Count;
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

        public void SaveWordCardsToFile(string path)
        {
            var textToFile = "";
            for (int i = 0; i < List.Count; i++)
            {
                textToFile += List[i].Word + "\t" + List[i].Translation + "\t" + List[i].CreationDate;
                if (i != List.Count - 1) textToFile += "\r\n";
            }

            File.WriteAllText(path, textToFile);
        }

        public void AddNewWordCard(string word, string translation)
        {
            var wordCardToAdd = new WordCard(word, translation);
            List.Add(wordCardToAdd);
            Count++;
        }

        public void EditWordCard(WordCard wordCard, string newWord, string newTranslation)
        {
            List[List.IndexOf(wordCard)] = new WordCard(newWord, newTranslation);
        }

        public void DeleteWordCard(WordCard wordCardToDelete)
        {
            List.Remove(wordCardToDelete);
            Count--;
        }
    }
}
