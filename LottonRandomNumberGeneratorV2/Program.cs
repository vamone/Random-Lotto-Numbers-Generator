using LottonRandomNumberGeneratorV2.Algorithms;
using LottonRandomNumberGeneratorV2.Console;
using LottonRandomNumberGeneratorV2.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<ConsoleRunner>();
builder.Services.AddTransient<ConsoleDecorator>();
builder.Services.AddTransient<ConsoleBuilder>();
builder.Services.AddTransient<Generator>();
builder.Services.AddTransient<Manager>();
builder.Services.AddTransient<IAlgorithm, CombinationAlgorithm>();
builder.Services.AddTransient<IAlgorithm, RandomAlgorithm>();
builder.Services.AddTransient<IAlgorithm, IndexAlgorithm>();
builder.Services.AddTransient<List<IAlgorithm>>(x =>
{
    return new List<IAlgorithm>
    {
        new RandomAlgorithm(),
        new CombinationAlgorithm(),
        new IndexAlgorithm(new CombinationAlgorithm(), new RandomAlgorithm())
    };
});
builder.Services.AddTransient<List<Game>>(x =>
{
    return new List<Game>
    {
        new Game(GameType.Euromilions, 50, 5, 12, 2, AlgorithmType.Random),
        new Game(GameType.Setforlive, 47, 5, 10, 1, AlgorithmType.Random),
        new Game(GameType.Lotto, 59, 6, 0, 0, AlgorithmType.Combination)
    };
});
builder.Services.AddTransient<GameBox>();

var host = builder.Build();

var console = ActivatorUtilities.CreateInstance<ConsoleRunner>(host.Services);
console.Run();

await host.RunAsync();