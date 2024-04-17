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

 
        public async Task<bool> LoadData()
        {
            DBContext.ChampTables.Load();
            DBContext.MatchTables.Load();

            if (DBContext != null)
            {
                List<MatchTable> firstHalf = new();
                List<MatchTable> secondHalf = new();
                var count = DBContext.ChampTables.Local.Count;
                List<ChampTable> firstHalfData = DBContext.ChampTables.Local.ToList()[0..(count/2)];
                List<ChampTable> secondHalfData = DBContext.ChampTables.Local.ToList()[((count/2)+1)..count];

                using SemaphoreSlim semaphore = new(Environment.ProcessorCount * 2);

                Task t1 = Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        foreach (ChampTable? item in firstHalfData)
                        {
                            await Task.Yield();
                            _parser.InnerParams["sports"] = item.SportId.ToString();
                            _parser.InnerParams["champs"] = item.ChampId.ToString();
                            RootObject = await _parser.Parse();
                            for (int i = 0; i < RootObject.Value.Count(); i++)
                            {
                                firstHalf.Add(new MatchTable()
                                {
                                    ChampId = RootObject.Value[i].LI,
                                    MatchId = RootObject.Value[i].I,
                                    Opponent1 = RootObject.Value[i].O1,
                                    Opponent2 = RootObject.Value[i].O2,
                                });
                            }
                        }
                    }
                    finally { semaphore.Release(); }
                });

                Task t2 = Task.Run(async () =>
                { 
                    await semaphore.WaitAsync();
                    try
                    {
                        foreach (ChampTable? item in secondHalfData)
                        {
                            await Task.Yield();
                            _parser.InnerParams["sports"] = item.SportId.ToString();
                            _parser.InnerParams["champs"] = item.ChampId.ToString();
                            RootObject = await _parser.Parse();
                            for (int i = 0; i < RootObject.Value.Count(); i++)
                            {
                                secondHalf.Add(new MatchTable()
                                {
                                    ChampId = RootObject.Value[i].LI,
                                    MatchId = RootObject.Value[i].I,
                                    Opponent1 = RootObject.Value[i].O1,
                                    Opponent2 = RootObject.Value[i].O2,
                                });

                            }
                        }
                    }
                    finally { semaphore.Release(); }
                    
                    semaphore.Release();
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
