using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.EntityFrameworkCore;

namespace _1XBetParser.Controllers
{
    public class MatchController : IController<MatchValue>
    {
        public IParser<MatchValue> _parser { get; set; }
        public ParserbdContext DBContext { get; set; }
        public IRootObject<MatchValue>? RootObject { get; set; }

        public MatchController(IParser<MatchValue> parser, ParserbdContext dBContext)
        {
            _parser = parser;
            DBContext = dBContext;
        }

        public bool IsContains(int id)
        {
            if (DBContext.MatchTables.Local.Count > 0)
            {
                foreach (MatchTable item in DBContext.MatchTables.Local)
                {
                    if (item.MatchId == id)
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

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public async Task<bool> LoadData()
        {

            if (DBContext != null)
            {
                DBContext.ChampTables.Load();
                DBContext.MatchTables.Load();

                List<MatchTable> firstHalf = new();
                List<MatchTable> secondHalf = new();
                var count = DBContext.ChampTables.Local.Count;
                List<ChampTable> firstHalfData = DBContext.ChampTables.Local.ToList()[0..(count/2)];
                List<ChampTable> secondHalfData = DBContext.ChampTables.Local.ToList()[((count/2)+1)..count];

                using SemaphoreSlim semaphore = new(Environment.ProcessorCount * 2);

                Task t1 = Task.Run(async () =>
                {
                    await Task.Yield();
                    await semaphore.WaitAsync();
                    try
                    {
                        foreach (ChampTable? item in firstHalfData)
                        {      
                            _parser.InnerParams["sports"] = item.SportId.ToString();
                            _parser.InnerParams["champs"] = item.ChampId.ToString();
                            RootObject = await _parser.Parse();
                            for (int i = 0; i < RootObject.Value.Count(); i++)
                            {
                                if (RootObject.Value[i].MIO != null && RootObject.Value[i].MIS != null)
                                {
                                    firstHalf.Add(new MatchTable()
                                    {
                                        ChampId = RootObject.Value[i].LI,
                                        MatchId = RootObject.Value[i].I,
                                        Opponent1 = RootObject.Value[i].O1,
                                        Opponent2 = RootObject.Value[i].O2,
                                        MatchTime = UnixTimeStampToDateTime(RootObject.Value[i].S).ToString(),
                                        MatchType = RootObject.Value[i].MIO.TSt,
                                        MatchSubtype = RootObject.Value[i].MIO.SSc,
                                        MatchPlace = RootObject.Value[i].MIO.Loc,
                                        Temperture = RootObject.Value[i].MIS.Where(t=>t.K==9).FirstOrDefault()?.V,
                                        AirPressureOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 25).FirstOrDefault()?.V,
                                        WetherType = RootObject.Value[i].MIS.Where(t => t.K == 21).FirstOrDefault()?.V,
                                        WindOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 24).FirstOrDefault()?.V,
                                        DownfallOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 35).FirstOrDefault()?.V,
                                        HumidityOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 27).FirstOrDefault()?.V,
                                    });
                                }
                                else
                                {
                                    firstHalf.Add(new MatchTable()
                                    {
                                        ChampId = RootObject.Value[i].LI,
                                        MatchId = RootObject.Value[i].I,
                                        Opponent1 = RootObject.Value[i].O1,
                                        Opponent2 = RootObject.Value[i].O2,
                                        MatchTime = UnixTimeStampToDateTime(RootObject.Value[i].S).ToString(),

                                    });
                                }
                                
                            }
                        }
                    }
                    finally { semaphore.Release(); }
                });

                Task t2 = Task.Run(async () =>
                {
                    await Task.Yield();
                    await semaphore.WaitAsync();
                    try
                    {
                        foreach (ChampTable? item in secondHalfData)
                        {
                            _parser.InnerParams["sports"] = item.SportId.ToString();
                            _parser.InnerParams["champs"] = item.ChampId.ToString();
                            RootObject = await _parser.Parse();
                            for (int i = 0; i < RootObject.Value.Count(); i++)
                            {
                                if (RootObject.Value[i].MIO != null && RootObject.Value[i].MIS != null)
                                {
                                    secondHalf.Add(new MatchTable()
                                    {
                                        ChampId = RootObject.Value[i].LI,
                                        MatchId = RootObject.Value[i].I,
                                        Opponent1 = RootObject.Value[i].O1,
                                        Opponent2 = RootObject.Value[i].O2,
                                        MatchTime = UnixTimeStampToDateTime(RootObject.Value[i].S).ToString(),
                                        MatchType = RootObject.Value[i].MIO.TSt,
                                        MatchSubtype = RootObject.Value[i].MIO.SSc,
                                        MatchPlace = RootObject.Value[i].MIO.Loc,
                                        Temperture = RootObject.Value[i].MIS.Where(t => t.K == 9).FirstOrDefault()?.V,
                                        AirPressureOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 25).FirstOrDefault()?.V + "мм.рт.ст.",
                                        WetherType = RootObject.Value[i].MIS.Where(t => t.K == 21).FirstOrDefault()?.V,
                                        WindOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 24).FirstOrDefault()?.V,
                                        DownfallOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 35).FirstOrDefault()?.V + "%",
                                        HumidityOnMatch = RootObject.Value[i].MIS.Where(t => t.K == 27).FirstOrDefault()?.V + "%",
                                    });
                                }
                                else
                                {
                                    secondHalf.Add(new MatchTable()
                                    {
                                        ChampId = RootObject.Value[i].LI,
                                        MatchId = RootObject.Value[i].I,
                                        Opponent1 = RootObject.Value[i].O1,
                                        Opponent2 = RootObject.Value[i].O2,
                                        MatchTime = UnixTimeStampToDateTime(RootObject.Value[i].S).ToString(),

                                    });
                                }
                            }
                        }
                    }
                    finally { semaphore.Release(); }
                    
                });
                
                Task.WaitAll(t1, t2);
                firstHalf.AddRange(secondHalf);
                foreach (MatchTable item in firstHalf)
                {
                    if (!IsContains(item.MatchId))
                    {
                        DBContext.MatchTables.Add(item);
                    }
                }
                DBContext.SaveChanges();
                return true;

                /* foreach (var item in this.DBContext.ChampTables.Local)
                 {
                     _parser.InnerParams["sports"] = item.SportId.ToString();
                     _parser.InnerParams["champs"] = item.ChampId.ToString();
                     RootObject = await _parser.Parse();

                     for (int i = 0; i < RootObject.Value.Count(); i++)
                     {
                         MatchTable _temp = new MatchTable()
                         {
                             ChampId = RootObject.Value[i].LI,
                             MatchId = RootObject.Value[i].I,
                             Opponent1 = RootObject.Value[i].O1,
                             Opponent2 = RootObject.Value[i].O2,
                         };

                         if (!IsContains(_temp.MatchId))
                         {
                             DBContext.MatchTables.Add(_temp);
                         }

                     }
                 }*/

            }
            return false;
        }
    }
}
