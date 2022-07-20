using Melina;

Console.SetWindowSize(GlobalSettings.WINDOW_WIDTH, GlobalSettings.WINDOW_HEIGHT);

WordCardsList wordCards = new WordCardsList("words.txt", "words.txt");
new MainMenu(wordCards).Run();
