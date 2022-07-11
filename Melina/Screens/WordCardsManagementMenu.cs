namespace Melina
{
    internal class WordCardsManagementMenu
    {
        const string ARROW_POINTER = "->";
        const string EMPTY_SPACE = "  ";
        const int MAX_CONTENT_LINES_COUNT = 20;
        
        private WordCardsList wordCards;

        private int contentLinesCount;
        private int pointerPosition;
        private int indexOffset;

        private bool isMenuActive;

        public WordCardsManagementMenu(WordCardsList wordCardsList)
        {
            wordCards = wordCardsList;

            if (wordCards.Count >= MAX_CONTENT_LINES_COUNT) contentLinesCount = MAX_CONTENT_LINES_COUNT;
            else contentLinesCount = wordCards.Count;
            pointerPosition = contentLinesCount - 1;
            indexOffset = wordCards.Count - contentLinesCount;

            isMenuActive = true;
        }

        public void Run()
        {
            while (isMenuActive)
            {
                Update(pointerPosition);

                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (pointerPosition < contentLinesCount - 1)
                        {
                            pointerPosition++;
                        }
                        else if (pointerPosition == contentLinesCount - 1 && indexOffset < wordCards.Count - contentLinesCount) indexOffset++;

                        break;
                    case ConsoleKey.UpArrow:
                        if (pointerPosition > 0)
                        {
                            pointerPosition--;
                        }
                        else if (pointerPosition == 0 && indexOffset > 0) indexOffset--;
                        break;
                    case ConsoleKey.D1:
                        Console.Clear();

                        Console.Write("Enter a word: ");
                        var word = Console.ReadLine();

                        Console.Write("Enter a translation: ");
                        var translation = Console.ReadLine();

                        Console.Write("Confirm adding (Y/N): ");
                        var confirmAddingInput = Console.ReadKey();

                        if (confirmAddingInput.Key == ConsoleKey.Y)
                        {
                            wordCards.AddNewWordCard(word, translation);
                            if (wordCards.Count > MAX_CONTENT_LINES_COUNT) indexOffset++;
                            else
                            {
                                contentLinesCount++;
                            }
                            pointerPosition = contentLinesCount - 1;
                        }
                        
                        break;
                    case ConsoleKey.D2:
                        var wordCardToEdit = wordCards.List[indexOffset + pointerPosition];

                        Console.Clear();

                        Console.WriteLine($"Editing {wordCardToEdit.Word} {wordCardToEdit.Translation}");

                        Console.Write("Enter a new word: ");
                        var newWord = Console.ReadLine();

                        Console.Write("Enter a new Translation: ");
                        var newTranslation = Console.ReadLine();

                        Console.Write("Confirm editing (Y/N): ");
                        var confirmEditingInput = Console.ReadKey();

                        if (confirmEditingInput.Key == ConsoleKey.Y)
                        {
                            wordCards.EditWordCard(wordCardToEdit, newWord, newTranslation);
                        }

                        break;
                    case ConsoleKey.D3:
                        var wordCardToDelete = wordCards.List[indexOffset + pointerPosition];

                        Console.Clear();

                        Console.Write($"Confirm deleting \"{wordCardToDelete.Word}\" (Y/N): ");
                        var confirmDeletingInput = Console.ReadKey();

                        if (confirmDeletingInput.Key == ConsoleKey.Y)
                        {
                            wordCards.DeleteWordCard(wordCardToDelete);
                        }

                        if (wordCards.Count > MAX_CONTENT_LINES_COUNT) indexOffset--;
                        else
                        {
                            contentLinesCount--;
                            pointerPosition--;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        isMenuActive = false;
                        break;
                }
            }
        }

        private void Update(int pointerPosition)
        {
            Console.Clear();

            Console.WriteLine("WORD CARDS");
            Console.WriteLine();

            string wcAreaBorder = "+--------------------------------------------------------------------------------+";

            // Рисуем верхнюю границу
            Console.WriteLine(wcAreaBorder);

            if (wordCards.Count == 0)
            {
                Console.WriteLine("THERE ARE NO CARDS HERE. ADD ONE!");
            }
            else
            {
                // Рисуем строки области карточек
                for (int lineNum = 0; lineNum < contentLinesCount; lineNum++)
                {
                    var sideBorder = "|";
                    Console.Write(sideBorder);

                    var content = "";

                    // Выделение строки
                    var isLineSelected = false;
                    if (lineNum == pointerPosition)
                    {
                        InverseConsoleColors();
                        isLineSelected = true;
                    }

                    // Заполнение содержательной частью
                    WordCard thisLineWordCard = wordCards.List[lineNum + indexOffset];
                    content += $"{thisLineWordCard.Word} {thisLineWordCard.Translation}";

                    // заполняем оставшуюся часть строки пробелами
                    for (int j = 0; j < wcAreaBorder.Length - content.Length + j - 2; j++)
                    {
                        content += " ";
                    }

                    Console.Write(content);

                    if (isLineSelected) InverseConsoleColors();

                    Console.WriteLine(sideBorder);
                }
            }

            // Рисуем нижнюю границу и выводим количество карточек
            Console.WriteLine($"{wcAreaBorder} Word Cards Count: {wordCards.Count}");

            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Edit");
            Console.WriteLine("3 - Delete");
            Console.WriteLine("Backspace - Back");
        }

        private void InverseConsoleColors()
        {
            if (Console.BackgroundColor == ConsoleColor.Black) Console.BackgroundColor = ConsoleColor.White;
            else if (Console.BackgroundColor == ConsoleColor.White) Console.BackgroundColor = ConsoleColor.Black;

            if (Console.ForegroundColor == ConsoleColor.Gray) Console.ForegroundColor = ConsoleColor.Black;
            else if (Console.ForegroundColor == ConsoleColor.Black) Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
