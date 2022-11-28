namespace Melina
{
    internal class WordCardsManagementMenu
    {
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

                var input = Console.ReadKey(true);

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
                        var confirmAddingInput = Console.ReadKey(true);

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
                        var confirmEditingInput = Console.ReadKey(true);

                        if (confirmEditingInput.Key == ConsoleKey.Y)
                        {
                            wordCards.EditWordCard(wordCardToEdit, newWord, newTranslation);
                        }

                        break;
                    case ConsoleKey.D3:
                        var wordCardToDelete = wordCards.List[indexOffset + pointerPosition];

                        Console.Clear();

                        Console.Write($"Confirm deleting \"{wordCardToDelete.Word}\" (Y/N): ");
                        var confirmDeletingInput = Console.ReadKey(true);

                        if (confirmDeletingInput.Key == ConsoleKey.Y)
                        {
                            wordCards.DeleteWordCard(wordCardToDelete);

                            if (wordCards.Count > MAX_CONTENT_LINES_COUNT) indexOffset--;
                            else
                            {
                                contentLinesCount--;
                                pointerPosition--;
                            }
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
            Console.WriteLine($"Total Word Cards Count: {wordCards.Count}");
            Console.WriteLine();

            //Формируем границу в зависимости от ширины окна
            string wcAreaBorder = "+";
            for (int i = 0; i < GlobalSettings.WINDOW_WIDTH - 3; i++)
            {
                wcAreaBorder += '-';
            }
            wcAreaBorder += "+";

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

                    // Выделение строки
                    var isLineSelected = false;
                    if (lineNum == pointerPosition)
                    {
                        InverseConsoleColors();
                        isLineSelected = true;
                    }

                    // Заполнение содержательной частью
                    WordCard thisLineWordCard = wordCards.List[lineNum + indexOffset];
                    var content = thisLineWordCard.Word;

                    // Заполняем оставшуюся часть строки пробелами
                    for (int i = 0; i < wcAreaBorder.Length / 2 - content.Length + i - 2; i++)
                    {
                        content += " ";
                    }

                    Console.Write(content);

                    Console.Write(sideBorder);
                    Console.Write(sideBorder);

                    content = thisLineWordCard.Translation;
                    for (int i = 0; i < wcAreaBorder.Length / 2 - content.Length + i - 1; i++)
                    {
                        content += " ";
                    }

                    Console.Write(content);

                    if (isLineSelected) InverseConsoleColors();

                    Console.WriteLine(sideBorder);
                }
            }

            // Рисуем нижнюю границу
            Console.WriteLine(wcAreaBorder);

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
