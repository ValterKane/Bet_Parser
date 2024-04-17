
using _1XBetParser;
using _1XBetParser.Controllers;
using _1XBetParser.CurlCreators;
using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.Extensions.DependencyInjection;


IServiceCollection HostServices = new ServiceCollection()
    .AddTransient<IParser<SportValue>, SportParser>()
    .AddTransient<IParser<ChampValue>, ChampParser>()
    .AddTransient<IParser<MatchValue>, MatchesParser>()
    .AddTransient<IParser<BetValue>, BetParser>()
    .AddDbContext<ParserbdContext>(ServiceLifetime.Singleton)
    .AddTransient<ICurlCreator, LeageCurl>()
    .AddTransient<SportController>()
    .AddTransient<ChampController>()
    .AddTransient<MatchController>()
    .AddTransient<BetController>()
    .AddTransient<Remover>()
    .AddTransient<TypeBetController>();

IServiceProvider serviceProvider = HostServices.BuildServiceProvider();

var TypeBetController = serviceProvider.GetService<TypeBetController>();


var Rem = serviceProvider.GetService<Remover>();

Rem.Remove();


var status = TypeBetController.LoadData().Result;

Console.WriteLine($"Type of Bet Parse:{status}\n");

SportController? Controller = serviceProvider.GetService<SportController>();

status = await Controller?.LoadData();

Console.WriteLine($"Parse Sports:{status}\n");

ChampController? ChampParseController = serviceProvider.GetService<ChampController>();

status = await ChampParseController.LoadData();

Console.WriteLine($"Parse Champ:{status}\n");

MatchController? MatchParserController = serviceProvider.GetService<MatchController>();

status = await MatchParserController.LoadData();

Console.WriteLine($"Parse Matches:{status}\n");

BetController? BetController = serviceProvider.GetService<BetController>();
status = await BetController?.LoadData();

Console.WriteLine($"Parse Bets:{status}\n");




