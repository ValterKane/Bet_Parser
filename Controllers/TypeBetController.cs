using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Controllers
{
    public class TypeBetController : IController<BetValue>
    {
        public IParser<BetValue> _parser { get; set; }
        public ParserbdContext DBContext { get; set; }
        public IRootObject<BetValue>? RootObject { get; set; }


        public TypeBetController(IParser<BetValue> parser, ParserbdContext dBContext)
        {
            _parser = parser;
            DBContext = dBContext;
        }

        public bool IsContains(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadData()
        {
            try
            {
                List<TypeBetTable> _temp = new List<TypeBetTable>();
                _temp.Add(
               new TypeBetTable
               {
                   BetTypeId = 1,
                   BetTypeName = "Победа Оп№1"
               });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 2,
                        BetTypeName = "Ничья"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 3,
                        BetTypeName = "Победа Оп№2"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 4,
                        BetTypeName = "Победа Оп№1 или ничья"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 5,
                        BetTypeName = "Победа Оп№1 или Победа Оп№2"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 6,
                        BetTypeName = "Победа Оп№2 или ничья"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 7,
                        BetTypeName = "Фора Оп№1"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 8,
                        BetTypeName = "Фора Оп№2"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 9,
                        BetTypeName = "Тотал Больше"
                    });

                _temp.Add(
                    new TypeBetTable
                    {
                        BetTypeId = 10,
                        BetTypeName = "Тотал Меньше"
                    });

                DBContext.TypeBetTables.AddRange(_temp);

                DBContext.SaveChanges();
                return Task.FromResult(true);

            }
    
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

           

            
        }
    }
}
