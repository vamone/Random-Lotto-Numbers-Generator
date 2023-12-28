using LottonRandomNumberGeneratorV2.Services;

public class Generator
{
    readonly ConsoleDecorator _consoleDecorator;

    readonly Manager _manager;

    public Generator(ConsoleDecorator consoleDecorator, Manager manager)
    {
        this._consoleDecorator = consoleDecorator;
        this._manager = manager;
    }

    public void PrintNumbers(Game gameInput)
    {
        this._consoleDecorator.WriteLine($"Generating for {gameInput.Name} using {gameInput.Algorithm} algorithm...", WriteLineSeparator.Both);

        var numbers = this._manager.ManageNumbers(gameInput);

        int i = 0;
        foreach (var number in numbers)
        {
            this._consoleDecorator.WriteLine(number, writeLineSeparator: numbers.Count() > 1 && numbers.Count() != (i + 1) ? WriteLineSeparator.After : WriteLineSeparator.None);

            i++;
        }
    }
}