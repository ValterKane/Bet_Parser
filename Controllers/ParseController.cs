using Microsoft.Extensions.DependencyInjection;

namespace _1XBetParser.Controllers
{
    public class ParseController
    {
        private readonly IServiceCollection Services;
        private readonly ServiceProvider _provider;
        public ParseController(IServiceCollection Services)
        {
            this.Services = Services;
            _provider = this.Services.BuildServiceProvider();
        }

        private void ColoraizeCW(bool status, string log_Info)
        {
            ConsoleColor _base_Color = Console.ForegroundColor;
            Console.Write(log_Info);
            if (status)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(status.ToString() + "\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(status.ToString() + "\n");
            }
            Console.ForegroundColor = _base_Color;

        }

        public async void ParseControll(object flag)
        {

            if ((bool)flag == true)
            {
                Remover? Rem = _provider.GetService<Remover>();
                Rem.Remove();
            }

            try
            {
                Console.WriteLine("Cycle Start:");
                TypeBetController? TypeBetController = _provider.GetService<TypeBetController>();
                bool status = TypeBetController.LoadData().Result;

                ColoraizeCW(status, "Stage #1{Type of bets parse):");

                SportController? Controller = _provider.GetService<SportController>();
                status = await Controller?.LoadData();

                ColoraizeCW(status, "Stage #2(Sports parse):");


                ChampController? ChampParseController = _provider.GetService<ChampController>();
                status = await ChampParseController.LoadData();

                ColoraizeCW(status, "Stage #3(Championships parse):");

                MatchController? MatchParserController = _provider.GetService<MatchController>();
                status = await MatchParserController.LoadData();

                ColoraizeCW(status, "Stage #4(Matches parse):");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void LoadBets(int MatchID)
        {
            BetController? BetController = _provider.GetService<BetController>();
            bool status = await BetController?.LoadData(MatchID);
            ColoraizeCW(status, $"Bets of match({MatchID}) load:");
        }
    }
}
