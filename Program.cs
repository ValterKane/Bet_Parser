
//https://1xstavka.ru/LineFeed/Get1x2_VZip?count=10&mode=4&country=1&top=true&partner=51&gr=44

using _1XBetParser;
using _1XBetParser.Controllers;
using _1XBetParser.CurlCreators;
using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


IServiceCollection HostServices = new ServiceCollection()
    .AddTransient<IParser<SportValue>, SportParser>()
    .AddTransient<IParser<ChampValue>, ChampParser>()
    .AddTransient<IParser<MatchValue>, MatchesParser>()
    .AddDbContext<ParserbdContext>(ServiceLifetime.Singleton)
    .AddTransient<ICurlCreator, LeageCurl>()
    .AddTransient<SportController>()
    .AddTransient<ChampController>()
    .AddTransient<MatchController>();

IServiceProvider serviceProvider = HostServices.BuildServiceProvider();

var Controller = serviceProvider.GetService<SportController>();

var status = await Controller?.LoadData();

Console.WriteLine(status);

var ChampParseController = serviceProvider.GetService<ChampController>();

status = await ChampParseController.LoadData();

Console.WriteLine(status);

var MatchParserController = serviceProvider.GetService<MatchController>();

status = await MatchParserController.LoadData();

Console.WriteLine(status);



