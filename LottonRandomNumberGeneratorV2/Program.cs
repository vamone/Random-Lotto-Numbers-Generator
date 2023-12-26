var games = new List<Game>();

games.Add(new Game(GameType.Euromilions, 50, 5, 12, 2));
games.Add(new Game(GameType.Setforlive, 47, 5, 10, 1));
games.Add(new Game(GameType.Lotto, 59, 6, 0, 0));

var gameBox = new GameBox(games);

var consoleDecorator = new ConsoleDecorator();
var generator = new Generator(consoleDecorator, new CombinationAlgorithm(), new RandomAlgorithm());

var builder = new ConsoleBuilder();

builder.SetUI(() =>
{
    consoleDecorator.WriteLine("Lotto winning numbers generator", WriteLineSeparator.After);
    consoleDecorator.WriteLine("Games:");

    int i = 0;
    foreach (var item in games)
    {
        i++;
        consoleDecorator.WriteLine($"{i}. {item.Name}");
    }

    consoleDecorator.WriteLine("4. Custom");
    consoleDecorator.WriteLine("5. Help");

    consoleDecorator.Write("Select option: ");
     
    int selectedOption = consoleDecorator.ReadLine().ToInt32(); //Add validation numetric

    if (gameBox.IsGameOption(selectedOption))
    {
        var game = gameBox.ResolveGame(selectedOption);
        if (game != null)
        {
            generator.PrintNumbers(game);
        }
    }

    if (selectedOption == (int)ActionType.Custom)
    {
        consoleDecorator.Write("Enter command: ");

        string commandValue = consoleDecorator.ReadLine(); //Add validation regex

        var customGame = gameBox.ResolveGame(commandValue);
        if (customGame != null)
        {
            generator.PrintNumbers(customGame);
        }
    }

    if (selectedOption == (int)ActionType.Help)
    {
        consoleDecorator.WriteLine("-g 1 = game and game number", WriteLineSeparator.Before);
        consoleDecorator.WriteLine("-r 2 = print repetitions and it's number");
        consoleDecorator.WriteLine("-a 1-2 = algorithm and random 1, combination 2");
    }
});

while (true)
{
    builder.BuildUI();

    consoleDecorator.WriteLine("Press enter to play again...", WriteLineSeparator.Before);
    consoleDecorator.ReadLine();

    //var answer = Console.ReadKey();
    //if (answer.Key == ConsoleKey.Enter)
    //{
    //    PrintNumbers(numberOfNumbers, pickedNumbers, luckyNumberOfNumbers, pickedLuckyNumbers);
    //}
}