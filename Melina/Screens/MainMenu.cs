namespace Melina
{
    internal class MainMenu
    {
        private WordCardsList wordCards;
        private bool isMenuActive;

        public MainMenu(WordCardsList wordCardList)
        {
            wordCards = wordCardList;
            isMenuActive = true;
        }

        public void Run()
        {
            while (isMenuActive)
            {
                Update();

                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        new WordCardsManagementMenu(wordCards).Run();
                        break;
                    case ConsoleKey.D2:
                        new TestingMenu(wordCards).Run();
                        break;
                    case ConsoleKey.X:
                        Console.Clear();
                        isMenuActive = false;
                        wordCards.SaveWordCardsToFile("words.txt");
                        break;
                    default:
                        Update();
                        break;
                }
            }
        }

        private void Update()
        {
            Console.Clear();

            Console.WriteLine("MELINA");
            Console.WriteLine();

            Console.WriteLine("1 - Word Cards Management");
            Console.WriteLine("2 - Testing");
            Console.WriteLine("X - Exit");
        }
    }
}
