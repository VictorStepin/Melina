namespace Melina
{
    internal class Testing
    {
        private string name;
        private List<WordCard> wordCards;

        public Testing(List<WordCard> wordCardsToTest, TestType testType)
        {
            switch(testType)
            {
                case TestType.AllCards:
                    name = "ALL CARDS TEST";
                    wordCards = Shuffle(wordCardsToTest);
                    break;
                case TestType.RandomTen:
                    name = "RANDOM TEN TEST";
                    var shuffledOriginal = Shuffle(wordCardsToTest);

                    wordCards = new List<WordCard>();
                    for (int i = 0; i < 10; i++)
                    {
                        wordCards.Add(shuffledOriginal[i]);
                    }
                    break;
            }
        }

        public void Run()
        {
            var incorrectAnsweredCards = new WordCardsList();
            var correctAnswersCount = 0;
            var incorrectAnswersCount = 0;

            for (int i = 0; i < wordCards.Count; i++)
            {
                Console.Clear();

                Console.WriteLine(name);
                Console.WriteLine();

                Console.WriteLine($"{wordCards.Count - i} words left");
                Console.WriteLine();

                Console.WriteLine($"Word: {wordCards[i].Word}");

                Console.WriteLine("Press Enter to show translation");
                Console.ReadKey();

                Console.WriteLine($"Translation: {wordCards[i].Translation}");

                Console.Write("Is your answer right? (Y/N): ");
                var answerInput = Console.ReadKey();

                switch (answerInput.Key)
                {
                    case ConsoleKey.Y:
                        correctAnswersCount++;
                        break;
                    default:
                        incorrectAnsweredCards.AddNewWordCard(wordCards[i].Word, wordCards[i].Translation);
                        incorrectAnswersCount++;
                        break;
                }
            }

            Console.Clear();

            Console.WriteLine(name);
            Console.WriteLine();

            Console.WriteLine("RESULT");
            Console.WriteLine();

            Console.WriteLine($"Cards tested: {wordCards.Count}");
            Console.WriteLine($"Correct: {correctAnswersCount}");
            Console.WriteLine($"Incorrect: {incorrectAnswersCount}");
            var percentage = Math.Round(((double)correctAnswersCount / wordCards.Count) * 100, 2);
            Console.WriteLine($"Percentage: {percentage} %");
            Console.WriteLine();

            var iwcFilePath = "incorrect_cards.txt"; //iwc - incorrect word cards
            incorrectAnsweredCards.SaveWordCardsToFile(iwcFilePath);
            Console.WriteLine($"Incorrect word cards saved to {iwcFilePath}");
            Console.WriteLine();

            Console.WriteLine("Press any key to return testing menu...");
            Console.ReadKey();
        }

        private List<WordCard> Shuffle(List<WordCard> wordCards)
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
    }
}
