public class ConsoleRunner
{
    readonly ConsoleDecorator _consoleDecorator;

    readonly ConsoleBuilder _consoleBuilder;

    readonly GameBox _gameBox;

    public ConsoleRunner(ConsoleDecorator consoleDecorator, ConsoleBuilder consoleBuilder, GameBox gameBox)
    {
        this._consoleDecorator = consoleDecorator;
        this._consoleBuilder = consoleBuilder;
        this._gameBox = gameBox;
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

    private T RetryGet<T>(string writeLine, Func<int, T> value) where T : class
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
}