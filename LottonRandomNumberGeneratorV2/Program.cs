using LottonRandomNumberGeneratorV2;
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

foreach (var algorithm in Constraints.GetAlgorithms())
{
    builder.Services.AddTransient<IAlgorithm>(x => algorithm.Value);
}

foreach (var gameConfig in Constraints.GetGameConfigs())
{
    builder.Services.AddTransient<IGameConfig>(x => gameConfig.Value);
}

builder.Services.AddTransient<GameBox>();
builder.Services.AddTransient<TheNationalLotteryWebsiteDriver>(x => new TheNationalLotteryWebsiteDriver(null, x.GetService<IOptions<LoginConfig>>()));

builder.Services.Configure<LoginConfig>(config.GetSection(LoginConfig.ConfigBinding));

var host = builder.Build();

var console = ActivatorUtilities.CreateInstance<ConsoleRunner>(host.Services);
console.Run();

await host.RunAsync();