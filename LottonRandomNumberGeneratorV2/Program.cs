using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Enums;
using LottonRandomNumberGeneratorV2.Extensions;
using LottonRandomNumberGeneratorV2.Helpers;
using System.Text.RegularExpressions;

var games = new List<Game>();

games.Add(new Game(GameType.Euromilions, 50, 5, 12, 2));
games.Add(new Game(GameType.Setforlive, 47, 5, 10, 1));
games.Add(new Game(GameType.Lotto, 59, 6, 0, 0));

var gameIds = games.Select(x => (int)x.Type);

var generator = new Generator();
var consoleDecorator = new ConsoleDecorator();
var builder = new ConsoleBuilder();

builder.SetUI(() =>
{
    consoleDecorator.WriteLine("Lotto winning numbers generator", WriteLineSeparator.After);
    consoleDecorator.WriteLine("Games:");
    consoleDecorator.WriteLine("1. Euromilions");
    consoleDecorator.WriteLine("2. Setforlive");
    consoleDecorator.WriteLine("3. Lotto");
    consoleDecorator.WriteLine("4. Custom");
    consoleDecorator.WriteLine("5. Help");
    consoleDecorator.Write("Select option: ");

    int selectedOption = consoleDecorator.ReadLine().ToInt32();
    if (gameIds.Contains(selectedOption))
    {
        var game = games.SingleOrDefault(x => (int)x.Type == selectedOption);
        if (game != null)
        {
            generator.PrintNumbers(game);
        }
    }

    if (selectedOption == (int)ActionType.Custom)
    {
        consoleDecorator.Write("Enter command: ");

        string readValue = consoleDecorator.ReadLine();

        Game customGame = null;

        var matches = Regex.Matches(readValue.Trim(), @"(-([A-z])\s?([0-9]))").ToList();
        foreach (var match in matches)
        {
            string actionValue = match.Groups[2].Value;
            string numberValue = match.Groups[3].Value;

            if (actionValue == "g")
            {
                customGame = games.SingleOrDefault(x => (int)x.Type == numberValue.ToInt32());
            }

            if (customGame != null && actionValue == "r")
            {
                customGame.Take = numberValue.ToInt32();
            }

            if (customGame != null && actionValue == "a")
            {
                customGame.Algorithm = numberValue.ToInt32() == 1 ? AlgorithmType.Random : AlgorithmType.Combination;
            }
        }

        generator.PrintNumbers(customGame);
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
}