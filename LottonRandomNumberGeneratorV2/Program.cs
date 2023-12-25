// See https://aka.ms/new-console-template for more information

using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Extensions;

Console.WriteLine("Lotto winning numbers generator");
Console.WriteLine("---");

var builder = new ConsoleBuilder();

builder.Add(x =>
{
    x.Action = () => Console.WriteLine("Games:");

    //x.Id = "GAME_TYPE";
    x.WriteLineBeforeConfig = new WriteLineConfig("Games: ");
    x.SetGames(y =>
    {
        y.Add(z =>
        {
            z.Id = 1;
            z.Name = "Euromilions";
            z.Action = () => z.SetGameInput(50, 5, 12, 2);
        });

        y.Add(z =>
        {
            z.Id = 2;
            z.Name = "Setforlive";
            z.Action = () => z.SetGameInput(47, 5, 10, 1);
        });

        y.Add(z =>
        {
            z.Id = 3;
            z.Name = "Lotto";
            z.Action = () => z.SetGameInput(59, 6, 0, 0);
        });

        y.Add(z =>
        {
            z.Id = 4;
            z.Name = "Custom (-g1 -r1 -a2)";
            z.AddReadLine(o =>
            {
                o.WriteLineBeforeConfig = new WriteLineConfig("Enter command: ", lineSpacers: WriteLineSpacers.Both);
                o.FuncValidateReadlineValue = (a) => a.IsRegexMatches(@"(-([A-z])\s?([0-9]))");
                o.ValidationErrorMessage = "Entered value is not valid, try again please.";
                o.IsRepeatQuestionIfValidationFailed = true;
            });
        });

        y.Add(z =>
        {
            z.Id = 5;
            z.Name = "Help";
            z.Action = () => builder.PrintHelp();
        });
    });
    x.WriteLineAfterConfig = new WriteLineConfig("Select game: ", lineSpacers: WriteLineSpacers.Both);
    x.FuncValidateReadlineValue = (a) => a.IsDigitsOnly();
    x.ValidationErrorMessage = "Entered value is not valid, try again please.";
    x.IsRepeatQuestionIfValidationFailed = true;
});

builder.Add(x =>
{

});

while (true)
{
    var keyValuePairs = builder.GetGameInput();
    if (keyValuePairs != null)
    {
        var generator = new Generator();
        generator.PrintNumbers(keyValuePairs);
    }

    Console.WriteLine("---");
    Console.WriteLine("Press enter to play again...");
    Console.ReadLine();
}