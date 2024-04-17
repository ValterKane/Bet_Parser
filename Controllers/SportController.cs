using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using Microsoft.EntityFrameworkCore;

namespace _1XBetParser.Controllers
{
    public class SportController: IController<SportValue>
    {
        public IParser<SportValue> _parser { get; set; }
        public ParserbdContext DBContext { get; set; }
        public IRootObject<SportValue>? RootObject { get; set; }

        public SportController(IParser<SportValue> _parser, ParserbdContext DBContext)
        {
            this._parser = _parser;
            this.DBContext = DBContext;
        }
        public async Task<bool> LoadData()
        {
            RootObject = await _parser.Parse();
            DBContext.SportTables.Load();
            if (DBContext != null)
            {
                for (int i = 0; i < RootObject.Value.Count(); i++)
                {

                    SportTable _temp = new()
                    {
                        SportId = RootObject.Value[i].I,
                        SportName = RootObject.Value[i].N.ToString(),
                    };


                    if (!IsContains(_temp.SportId))
                    {
                        DBContext.SportTables.Add(_temp);
                    }

                }
                DBContext.SaveChanges();
                return true;
            }

            return false;
        }

        private bool IsContains(int id)
        {
            if (DBContext.SportTables.Local.Count > 0)
            {
                foreach (SportTable item in DBContext.SportTables.Local)
                {
                    if (item.SportId == id)
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

        bool IController<SportValue>.IsContains(int id)
        {
            throw new NotImplementedException();
        }
    }


}
