using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Controllers
{
    public class ChampController : IController<ChampValue>
    {
        public IParser<ChampValue> _parser { get; set; }
        public ParserbdContext DBContext { get; set; }
        public IRootObject<ChampValue>? RootObject { get; set; }

        public ChampController(IParser<ChampValue> parser, ParserbdContext dBContext)
        {
            _parser = parser;
            DBContext = dBContext;
        }

        public bool IsContains(int id)
        {
            if (DBContext.ChampTables.Local.Count > 0)
            {
                foreach (var item in DBContext.ChampTables.Local)
                {
                    if (item.ChampId == id)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoadData()
        {
            this.DBContext.SportTables.Load();
            this.DBContext.ChampTables.Load();
            if (DBContext != null)
            {
                foreach (var item in this.DBContext.SportTables.Local)
                {
                    _parser.InnerParams["sport"] = item.SportId.ToString();
                    RootObject = await _parser.Parse();

                    for (int i = 0; i < RootObject.Value.Count(); i++)
                    {
                        if (RootObject.Value[i].SC == null)
                        {
                            ChampTable _temp = new ChampTable()
                            {
                                ChampId = RootObject.Value[i].LI,
                                ChampRuName = RootObject.Value[i].L,
                                ChampEuName = RootObject.Value[i].LE,
                                SportId = item.SportId,
                            };

                            if (!IsContains(_temp.ChampId))
                            {
                                DBContext.ChampTables.Add(_temp);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < RootObject.Value[i].SC.Count(); j++)
                            {
                                ChampTable _temp = new ChampTable()
                                {
                                    ChampId = RootObject.Value[i].SC[j].LI,
                                    ChampRuName = RootObject.Value[i].SC[j].L,
                                    ChampEuName = RootObject.Value[i].SC[j].LE,
                                    SportId = item.SportId,
                                };

                                if (!IsContains(_temp.ChampId))
                                {
                                    DBContext.ChampTables.Add(_temp);
                                }
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
