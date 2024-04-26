using _1XBetParser.Controllers;
using _1XBetParser.CurlCreators;
using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace _1XBetParser
{
    public class App
    {
        private static readonly IServiceCollection HostServices = new ServiceCollection();
        private static bool _cycle_parse_flag = false;

        public static void Main(string[] args)
        {
            
            HostServices.AddTransient<IParser<SportValue>, SportParser>()
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

            ParseController pC = new ParseController(HostServices);

            if (args[0] == "-single_parse" && _cycle_parse_flag == false)
            {
                pC.LoadBets(Convert.ToInt32(args[1]));
                Console.ReadKey();
            }
            if (args[0] == "-cycle_parse" && _cycle_parse_flag == false)
            {
                _cycle_parse_flag = true;
                TimerCallback tm = new TimerCallback(pC.ParseControll);
                Timer t = new Timer(tm, true, 0, Convert.ToInt32(args[1]));
                Console.ReadLine();
            }
        }

    }
}
