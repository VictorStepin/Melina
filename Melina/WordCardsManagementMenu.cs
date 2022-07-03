namespace Melina
{
    internal class WordCardsManagementMenu
    {
        const string ARROW_POINTER = "->";
        const string EMPTY_SPACE = "  ";

        private int wcAreaLinesCount;
        private int indexOffset;
        private int pointerPosition;

        private bool isWordCardsMenuActive;
        
        private WordCardsList wordCards;

        public WordCardsManagementMenu(WordCardsList wordCardsList)
        {
            wordCards = wordCardsList;

            wcAreaLinesCount = 20;
            indexOffset = wordCards.Count - wcAreaLinesCount;
            pointerPosition = wcAreaLinesCount - 1;

            isWordCardsMenuActive = true;
        }

        public void Run()
        {
            while (isWordCardsMenuActive)
            {
                Update(pointerPosition);

                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (pointerPosition < wcAreaLinesCount - 1)
                        {
                            pointerPosition++;
                        }
                        else if (pointerPosition == wcAreaLinesCount - 1 && indexOffset < wordCards.Count - wcAreaLinesCount) indexOffset++;

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
                        }

                        indexOffset++;
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

                        indexOffset--;
                        break;
                    case ConsoleKey.Backspace:
                        isWordCardsMenuActive = false;
                        break;
                }
            }
        }

        private void Update(int pointerPosition)
        {
            Console.Clear();

            Console.WriteLine("WORD CARDS");
            Console.WriteLine();

            string border = "+--------------------------------------------------------------------------------+";

            // Рисуем верхнюю границу
            Console.WriteLine(border);

            // Рисуем строки области карточек
            var firstLine = 0;
            var lastLine = wcAreaLinesCount - 1;
            for (int lineNum = firstLine; lineNum <= lastLine; lineNum++)
            {
                var content = "|";

                // Рисование указателя
                if (lineNum == pointerPosition) content += ARROW_POINTER;
                else content += EMPTY_SPACE;

                // Заполнение содержательной частью
                WordCard thisLineWordCard = wordCards.List[lineNum + indexOffset];
                content += $"{thisLineWordCard.Word} {thisLineWordCard.Translation}";

                // заполняем оставшуюся часть строки пробелами
                for (int j = 0; j < border.Length - content.Length + j - 1; j++)
                {
                    content += " ";
                }

                content += "|";

                Console.WriteLine(content);
            }

            // Рисуем нижнюю границу и выводим количество карточек
            Console.WriteLine($"{border} Word Cards Count: {wordCards.Count}");

            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Edit");
            Console.WriteLine("3 - Delete");
            Console.WriteLine("Backspace - Back");
        }
    }
}
