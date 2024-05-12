using LottonRandomNumberGeneratorV2.Driver;
using LottonRandomNumberGeneratorV2.Exceptions;
using LottonRandomNumberGeneratorV2.Models;

public class ConsoleRunner
{
    readonly ConsoleDecorator _consoleDecorator;

    readonly ConsoleBuilder _consoleBuilder;

    readonly LottoEngine _lottoEngine;

    readonly TheNationalLotteryWebsiteDriver _website;

    public ConsoleRunner(ConsoleDecorator consoleDecorator, ConsoleBuilder consoleBuilder, LottoEngine lottoEngine, TheNationalLotteryWebsiteDriver nationalLotteryWebsiteDriver)
    {
        this._consoleDecorator = consoleDecorator;
        this._consoleBuilder = consoleBuilder;
        this._lottoEngine = lottoEngine;
        this._website = nationalLotteryWebsiteDriver;
    }

    public void Run()
    {
        this._consoleBuilder.SetUI(() =>
        {
            this._consoleDecorator.WriteLine("Lotto winning numbers generator");

            this._consoleDecorator.WriteLine("Games:", WriteLineSeparator.Both);
            this._consoleDecorator.WriteLineMultiple(this._lottoEngine.GetGames(), x => x.Type.ToString());

            IGameConfig gameConfig = this.RetryGet<IGameConfig>("Select game: ", (a) => this._lottoEngine.GetGameById(a));

            this._consoleDecorator.WriteLine("Algorithms:", WriteLineSeparator.Both);
            this._consoleDecorator.WriteLineMultiple(this._lottoEngine.GetAlgorithms(), x => x.Type.ToString());

            IAlgorithm algorithm = this.RetryGet<IAlgorithm>("Select algorithm: ", (a) => this._lottoEngine.GetAlgorithmById(a));

            var numberOfGamesConfig = this.RetryGet<NumetricConfig>("Select number of games: ", (a) => this.GetNumetricConfig(a));

            if (gameConfig != null && algorithm != null && numberOfGamesConfig.Number > 0)
            {
                var numbers = this.GenerateAndPrintNumbers(gameConfig, algorithm, numberOfGamesConfig.Number).ToList();

                bool isRetry = true;
                while (isRetry)
                {
                    this._consoleDecorator.WriteLine("Next:", WriteLineSeparator.Both);
                    this._consoleDecorator.WriteLineMultiple(new List<string> { "Regenerate", "Modify", "Buy" }, x => x.ToString());

                    var nextConfig = this.RetryGet<NumetricConfig>("Select action: ", (a) => new NumetricConfig { Number = a });
                    if (nextConfig.Number == 1)
                    {
                        numbers = this.GenerateAndPrintNumbers(gameConfig, algorithm, numberOfGamesConfig.Number).ToList();
                        continue;
                    }

                    if (nextConfig.Number == 2)
                    {
                        var indexes = this.RetryGetArray("Write comma separated ids for modification: ");
                        foreach ( var index in indexes)
                        {
                            numbers[index - 1] = this._lottoEngine.GenerateNumbers(gameConfig, algorithm, 1).FirstOrDefault();
                        }

                        this.WriteLine(gameConfig, algorithm, numbers);

                        continue;
                    }

                    if (nextConfig.Number == 3)
                    {
                        this._website.SetEnabledValue(true);

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

                                try
                                {
                                    this._website.ChooseNumbersButton(i);
                                    this._website.ChooseMainNumbers(mainArray);
                                    this._website.ChooseLuckyStartNumbers(startsArray);
                                    this._website.ConfirmButton();

                                    this._consoleDecorator.WriteLine($"{i + 1}. Picked numbers: {item}");

                                    i++;
                                }
                                catch (Exception ex)
                                {
                                    this._consoleDecorator.WriteLine($"Error: {ex.Message}", WriteLineSeparator.Both);
                                    this._consoleDecorator.WriteLineMultiple(new List<string> { "Ignore", "End play" }, x => x.ToString());

                                    var onErrorConfig = this.RetryGet<IBoolConfig>("Select action: ", (a) => this.IsTrueIfEqualsTo(a, 1));
                                    if(!onErrorConfig.IsValue)
                                    {
                                        isRetryBrowser = false;
                                        this._website.SetEnabledValue(false);
                                        this._consoleDecorator.WriteLine("End play", WriteLineSeparator.Before);
                                    }

                                    i = 0;
                                }
                            }

                            this._consoleDecorator.WriteLine("Try again:", WriteLineSeparator.Both);
                            this._consoleDecorator.WriteLineMultiple(new List<string> { "Yes", "No" }, x => x.ToString());
                            var toBuyConfigAgain = this.RetryGet<IBoolConfig>("Select action: ", (a) => this.IsTrueIfEqualsTo(a, 1));

                            if (!toBuyConfigAgain.IsValue)
                            {
                                isRetryBrowser = false;
                                this._website.SetEnabledValue(false);
                                this._consoleDecorator.WriteLine("End play", WriteLineSeparator.Before);
                            }
                        }
                    }

                    isRetry = false;
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

    IEnumerable<string> GenerateAndPrintNumbers(IGameConfig gameConfig, IAlgorithm algorithm, int numberOfGames)
    {
        var numbers = this._lottoEngine.GenerateNumbers(gameConfig, algorithm, numberOfGames);

        this.WriteLine(gameConfig, algorithm, numbers);

        return numbers;
    }

    void WriteLine(IGameConfig gameConfig, IAlgorithm algorithm, IEnumerable<string> numbers)
    {
        this._consoleDecorator.WriteLine($"Generating for {gameConfig.Type} using {algorithm.Type} algorithm for {numbers.Count()} games...", WriteLineSeparator.Both);
        this._consoleDecorator.WriteLineMultiple(numbers, x => x);
    }

    T RetryGet<T>(string writeLine, Func<int, T> value) where T : class
    {
        T returnValue = null;

        while (returnValue == null)
        {
            try
            {
                this._consoleDecorator.Write(writeLine);

                int selectedOption = this._consoleDecorator.ReadLine().ToInt32(); //Add validation numetric

                returnValue = value.Invoke(selectedOption);
            }
            catch
            {
            }
        }

        return returnValue;
    }

    IEnumerable<int> RetryGetArray(string writeLine)
    {
        bool isRetry = true;
        while (isRetry)
        {
            try
            {
                this._consoleDecorator.Write(writeLine);

                string selectedOption = this._consoleDecorator.ReadLine();
                if(selectedOption == null)
                {
                    continue;
                }

                return selectedOption.Split(",").Select(x =>
                {
                    int.TryParse(x, out int output);
                    return output;
                }).Where(x => x > 0);
            }
            catch
            {
            }
        }

        return Enumerable.Empty<int>();
    }

    NumetricConfig GetNumetricConfig(int value)
    {
        if (value <= 0)
        {
            throw new InvalidNumerticValueSelectedException();
        }

        return new NumetricConfig { Number = value };
    }

    IBoolConfig IsTrueIfEqualsTo(int value, int equalsToValue)
    {
        return new BoolConfig { IsValue = value == equalsToValue };
    }
}