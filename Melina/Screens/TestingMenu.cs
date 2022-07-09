namespace Melina
{
    internal class TestingMenu
    {
        private WordCardsList wordCards;

        private bool isMenuActive;

        public TestingMenu(WordCardsList wordCardsList)
        {
            wordCards = wordCardsList;
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
                        new Testing(wordCards.List, TestType.AllCards).Run();
                        break;
                    case ConsoleKey.D2:
                        if (wordCards.List.Count < 10)
                        {
                            Console.Clear();

                            Console.WriteLine("You haven't 10 words in your collection.");
                            Console.WriteLine();

                            Console.WriteLine("Press any key to return testing menu...");
                            Console.ReadKey();
                        }
                        else
                        {
                            new Testing(wordCards.List, TestType.RandomTen).Run();
                        }
                        break;
                    case ConsoleKey.Backspace:
                        isMenuActive = false;
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
    }
}
