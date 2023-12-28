using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottonRandomNumberGeneratorV2.Console
{
    public class ConsoleRunner
    {
        readonly ConsoleDecorator _consoleDecorator;

        readonly ConsoleBuilder _consoleBuilder;

        readonly GameBox _gameBox;

        readonly Generator _generator;

        public ConsoleRunner(ConsoleDecorator consoleDecorator, ConsoleBuilder consoleBuilder, GameBox gameBox, Generator generator)
        {
            this._consoleDecorator = consoleDecorator;
            this._consoleBuilder = consoleBuilder;
            this._gameBox = gameBox;
            this._generator = generator;
        }

        public void Run()
        {
            this._consoleBuilder.SetUI(() =>
            {
                this._consoleDecorator.WriteLine("Lotto winning numbers generator", WriteLineSeparator.After);
                this._consoleDecorator.WriteLine("Games:");

                int i = 0;
                foreach (var item in this._gameBox.GetGames())
                {
                    i++;
                    this._consoleDecorator.WriteLine($"{i}. {item.Name}");
                }

                this._consoleDecorator.WriteLine("4. Custom (-g1 -r1 -a1 -c1)");
                this._consoleDecorator.WriteLine("5. Help");

                this._consoleDecorator.Write("Select option: ");

                int selectedOption = this._consoleDecorator.ReadLine().ToInt32(); //Add validation numetric

                if (this._gameBox.IsGameOption(selectedOption))
                {
                    var game = this._gameBox.ResolveGame(selectedOption);
                    if (game != null)
                    {
                        this._generator.PrintNumbers(game);
                    }
                }

                if (selectedOption == (int)ActionType.Custom)
                {
                    this._consoleDecorator.Write("Enter command: ");

                    string commandValue = this._consoleDecorator.ReadLine(); //Add validation regex

                    var customGame = this._gameBox.ResolveGame(commandValue);
                    if (customGame != null)
                    {
                        this._generator.PrintNumbers(customGame);
                    }
                }

                if (selectedOption == (int)ActionType.Help)
                {
                    this._consoleDecorator.WriteLine("-g 1-3 = game and game number", WriteLineSeparator.Before);
                    this._consoleDecorator.WriteLine("-r 1> = print repetitions and it's number");
                    this._consoleDecorator.WriteLine("-a 1-2 = algorithm and random 1, combination 2, index 3");
                    this._consoleDecorator.WriteLine("-c 1> = chunk all combinations into blocks and pick one item");
                }
            });

            bool isRepeat = true;
            while (isRepeat)
            {
                this._consoleBuilder.BuildUI();

                this._consoleDecorator.WriteLine("Press enter to play again or exit by any other keys...", WriteLineSeparator.Before);

                var answer = this._consoleDecorator.ReadKey();
                if (answer.Key != ConsoleKey.Enter)
                {
                    isRepeat = false;
                }
            }
        }
    }
}