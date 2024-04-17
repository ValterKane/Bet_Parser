using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Controllers
{
    public class BetController : IController<BetValue>
    {
        public IParser<BetValue> _parser { get; set; }
        public ParserbdContext DBContext { get; set; }
        public IRootObject<BetValue>? RootObject { get; set; }

        public BetController(IParser<BetValue> parser, ParserbdContext dBContext)
        {
            _parser = parser;
            DBContext = dBContext;
        }


        public bool IsContains(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoadData()
        {

            if (DBContext != null)
            {
                DBContext.ChampTables.Load();
                DBContext.MatchTables.Load();

                foreach (MatchTable item in DBContext.MatchTables.Local)
                {
                    _parser.InnerParams["sports"] = item.Champ.SportId.ToString();
                    _parser.InnerParams["champs"] = item.ChampId.ToString();
                    _parser.InnerParams["subGames"] = item.MatchId.ToString();
                    RootObject = await _parser.Parse();
                    
                    var data = RootObject.Value.Where(t=>t.SG != null).ToList();
                    if(data.Count > 0)
                    {
                        data = data.Where(t => t.SG.Count() != 0).ToList();
                        foreach (var game in data)
                        {
                            foreach (SG sg in game.SG)
                            {
                                List<BetsTable> list = new List<BetsTable>();

                                if (sg.E.Count() > 10)
                                {
                                    for(int i = 0; i<10; i++)
                                    {
                                        list.Add(new BetsTable()
                                        {
                                            BetId = sg.CI,
                                            BetName = sg.PN,
                                            BetTypeId = sg.E[i].T,
                                            MatchId = game.I,
                                            BetValue = (decimal)sg.E[i].C,
                                        });
                                    }
                                }
                                else 
                                {
                                    for (int i = 0; i < sg.E.Count(); i++)
                                    {
                                        list.Add(new BetsTable()
                                        {
                                            BetId = sg.CI,
                                            BetName = sg.PN,
                                            BetTypeId = sg.E[i].T,
                                            MatchId = game.I,
                                            BetValue = (decimal)sg.E[i].C,
                                        });
                                    }
                                }

                                if(list.Count > 0)
                                    DBContext.BetsTables.AddRange(list);
                            }

                        }
                    }

                    
                }
                DBContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
