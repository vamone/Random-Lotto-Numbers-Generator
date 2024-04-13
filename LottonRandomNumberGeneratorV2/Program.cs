using LottonRandomNumberGeneratorV2.Algorithms;
using LottonRandomNumberGeneratorV2.Configs;
using LottonRandomNumberGeneratorV2.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

builder.Services.AddTransient<ConsoleRunner>();
builder.Services.AddTransient<ConsoleDecorator>();
builder.Services.AddTransient<ConsoleBuilder>();
builder.Services.AddTransient<GameManager>();

builder.Services.AddTransient<IAlgorithm, CombinationAlgorithm>();
builder.Services.AddTransient<IAlgorithm, RandomAlgorithm>();
builder.Services.AddTransient<IAlgorithm>(x => new IndexAlgorithm(new CombinationAlgorithm(), new RandomAlgorithm()));

builder.Services.AddTransient<IGameConfig>(x => new GameConfig(GameType.Euromilions).ConfigSetOfNumbers(50, 5).ConfigSetOfNumbers(12, 2).SetUrl("https://www.national-lottery.co.uk/games/euromillions"));
builder.Services.AddTransient<IGameConfig>(x => new GameConfig(GameType.Setforlive).ConfigSetOfNumbers(47, 5).ConfigSetOfNumbers(10, 1).SetUrl("https://www.national-lottery.co.uk/games/set-for-life"));
builder.Services.AddTransient<IGameConfig>(x => new GameConfig(GameType.Lotto).ConfigSetOfNumbers(59, 6).SetUrl("https://www.national-lottery.co.uk/games/lotto"));
builder.Services.AddTransient<IGameConfig>(x => new GameConfig(GameType.Thunderball).ConfigSetOfNumbers(39, 5).ConfigSetOfNumbers(14, 1).SetUrl("https://www.national-lottery.co.uk/games/thunderball"));

builder.Services.AddTransient<GameBox>();
builder.Services.AddTransient<TheNationalLotteryWebsiteDriver>(x => new TheNationalLotteryWebsiteDriver(null, x.GetService<IOptions<LoginConfig>>()));

builder.Services.Configure<LoginConfig>(config.GetSection(LoginConfig.ConfigBinding));

var host = builder.Build();

var console = ActivatorUtilities.CreateInstance<ConsoleRunner>(host.Services);
console.Run();

await host.RunAsync();