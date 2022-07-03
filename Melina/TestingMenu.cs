namespace Melina
{
    internal class TestingMenu
    {
        private WordCardsList wordCards;

        private bool isTestingMenuActive;

        public TestingMenu(WordCardsList wordCardsList)
        {
            wordCards = wordCardsList;
            isTestingMenuActive = true;


        }

        public void Run()
        {
            while (isTestingMenuActive)
            {
                Update();

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

        private void Update()
        {
            Console.Clear();

            Console.WriteLine("TESTING");
            Console.WriteLine();

            Console.WriteLine("1 - All Cards Test");
            Console.WriteLine("2 - Random Ten Test");
            Console.WriteLine("Backspace - Back");
        }

        private void RunAllCardsTest()
        {
            var wordCardsToTest = Shuffle(wordCards.List);

            var incorrectAnsweredCards = new WordCardsList();
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
                        incorrectAnsweredCards.AddNewWordCard(wordCardsToTest[i].Word, wordCardsToTest[i].Translation);
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
            incorrectAnsweredCards.SaveWordCardsToFile(iwcFilePath);
            Console.WriteLine($"Incorrect word cards saved to {iwcFilePath}");
            Console.WriteLine();

            Console.WriteLine("Press any key to return testing menu...");
            Console.ReadKey();
        }

        private void RunRandomTenTest()
        {
            var shuffledOriginal = Shuffle(wordCards.List);

            var wordCardsToTest = new List<WordCard>();
            for (int i = 0; i < 10; i++)
            {
                wordCardsToTest.Add(shuffledOriginal[i]);
            }

            var incorrectAnsweredCards = new WordCardsList();
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
                        incorrectAnsweredCards.AddNewWordCard(wordCardsToTest[i].Word, wordCardsToTest[i].Translation);
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
