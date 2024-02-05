using LottonRandomNumberGeneratorV2.Driver;
using LottonRandomNumberGeneratorV2.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class ConsoleRunner
{
    readonly ConsoleDecorator _consoleDecorator;

    readonly ConsoleBuilder _consoleBuilder;

    readonly GameBox _gameBox;

    readonly TheNationalLotteryWebsiteDriver _website;

    public ConsoleRunner(ConsoleDecorator consoleDecorator, ConsoleBuilder consoleBuilder, GameBox gameBox, TheNationalLotteryWebsiteDriver nationalLotteryWebsiteDriver)
    {
        this._consoleDecorator = consoleDecorator;
        this._consoleBuilder = consoleBuilder;
        this._gameBox = gameBox;
        this._website = nationalLotteryWebsiteDriver;
    }

    public void Run()
    {
        this._consoleBuilder.SetUI(() =>
        {
            this._consoleDecorator.WriteLine("Lotto winning numbers generator");

            this._consoleDecorator.WriteLine("Games:", WriteLineSeparator.Both);
            this._consoleDecorator.WriteLineMultiple(this._gameBox.GetGames(), x => x.Type.ToString());

            IGameConfig gameConfig = this.RetryGet<IGameConfig>("Select game: ", (a) => this._gameBox.GetGameById(a));

            this._consoleDecorator.WriteLine("Algorithms:", WriteLineSeparator.Both);
            this._consoleDecorator.WriteLineMultiple(this._gameBox.GetAlgorithms(), x => x.Type.ToString());

            IAlgorithm algorithm = this.RetryGet<IAlgorithm>("Select algorithm: ", (a) => this._gameBox.GetAlgorithmById(a));

            int numberOfGames = 0;
            while (numberOfGames == 0)
            {
                this._consoleDecorator.Write("Select number of games: ");
                numberOfGames = this._consoleDecorator.ReadLine().ToInt32(); //Add validation numetric
            }

            if (gameConfig != null && algorithm != null && numberOfGames > 0)
            {
                this._consoleDecorator.WriteLine($"Generating for {gameConfig.Type} using {algorithm.Type} algorithm for {numberOfGames} games...", WriteLineSeparator.Both);

                var numbers = this._gameBox.GenerateNumbers(gameConfig, algorithm, numberOfGames);
                this._consoleDecorator.WriteLineMultiple(numbers, x => x);

                this._consoleDecorator.WriteLine("Buy:", WriteLineSeparator.Both);
                this._consoleDecorator.WriteLineMultiple(new List<string> { "Yes", "No" }, x => x.ToString());
                var toBuyConfig = this.RetryGet<IBoolConfig>("Select action: ", (a) => this.IsTrueIfEqualsTo(a, 1));

                if (!toBuyConfig.IsValue)
                {
                    return;
                }

                this._website.SetEnabledValue(toBuyConfig.IsValue);

                this._website.GoToHomePage();
                this._website.AcceptCookies();
                this._website.GoToLoginPage();
                this._website.Login();

                bool isRetryBrowser = true;
                while (isRetryBrowser)
                {
                    this._website.GoToGamePage(gameConfig.Url);

                    int i = 0;
                    foreach (var item in numbers)
                    {
                        var list = item.Split('|');

                        var main = list.Count() > 0 ? list[0] : string.Empty;
                        var starts = list.Count() > 1 ? list[1] : string.Empty;

                        var mainArray = main.Split('-');
                        var startsArray = starts.Split('-');

                        this._website.ChooseNumbersButton(i);
                        this._website.ChooseMainNumbers(mainArray);
                        this._website.ChooseLuckyStartNumbers(startsArray);
                        this._website.ConfirmButton();

                        this._consoleDecorator.WriteLine($"Selected numbers: {item}", WriteLineSeparator.Before);

                        i++;
                    }

                    this._consoleDecorator.WriteLine("Try again:", WriteLineSeparator.Both);
                    this._consoleDecorator.WriteLineMultiple(new List<string> { "Yes", "No" }, x => x.ToString());
                    var toBuyConfigAgain = this.RetryGet<IBoolConfig>("Select action: ", (a) => this.IsTrueIfEqualsTo(a, 1));

                    if(!toBuyConfigAgain.IsValue)
                    {
                        isRetryBrowser = false;
                        this._website.SetEnabledValue(false);
                        this._consoleDecorator.WriteLine("End", WriteLineSeparator.Before);
                    }
                }
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

    T RetryGet<T>(string writeLine, Func<int, T> value) where T : class
    {
        T returnValue = null;

        while (returnValue == null)
        {
            this._consoleDecorator.Write(writeLine);

            int selectedOption = this._consoleDecorator.ReadLine().ToInt32(); //Add validation numetric

            returnValue = value.Invoke(selectedOption);
        }

        return returnValue;
    }

    IBoolConfig IsTrueIfEqualsTo(int value, int equalsToValue)
    {
        return new IBoolConfig { IsValue = value == equalsToValue };
    }
}